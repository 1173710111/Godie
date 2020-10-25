using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionTriggerDog : MonoBehaviour
{
    [Tooltip("交互类型,0表示推门，1表示扒石头，2表示跳箱子，3表示拿零件")]
    public int interaction_type;
    public GameObject getOrLose_prefab;

    private bool dog_inBounds=false;
    private GameObject m_dog;
    private bool action_finished = false;
    private GameObject getOrLose;
    private GetOrLostItem getOrLostItem;
    private ZimuUI zimu;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Dog")
        {
            
            if (interaction_type == 0)
            {
                if (collision.contacts[0].normal.x == 1)
                { 
                    gameObject.GetComponent<Animator>().SetTrigger("Open");
                    gameObject.GetComponent<Collider2D>().isTrigger=true;
                    InputController.BanMouse(true);
                    InputController.BanButton(true);
                    Invoke("CanMove", 2f);
                }
            }
            
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Dog"))
        {
            dog_inBounds = true;
            m_dog = collision.gameObject;
            if (getOrLose_prefab != null)
            {
                getOrLose = Instantiate(getOrLose_prefab);
                getOrLostItem = getOrLose.GetComponent<GetOrLostItem>();
            }
            zimu = GameObject.Find("UI").transform.Find("字幕UI").GetComponent<ZimuUI>();
        }   
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Dog"))
        {
            dog_inBounds = false;
            m_dog = null;
        }
    }

    private void CanMove()
    {
        InputController.BanButton(false);
        InputController.BanMouse(false);
    }

    private void Awake()
    {
        if (interaction_type == 2)
        {
            Physics2D.IgnoreCollision(GetComponent<Collider2D>(), GameObject.FindGameObjectWithTag("Player").GetComponent<Collider2D>(), true);
        }
    }

    private void Update()
    {
        Vector3 playerWordDir = Camera.main.WorldToScreenPoint(new Vector3(0, 0, 0f));
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, playerWordDir.z));

        switch (interaction_type)
        {
            case 1:
                if (transform.parent.GetComponent<Collider2D>().bounds.Contains(mousePosition))
                {
                    if (!action_finished)
                    {
                        GameObject.Find("MouseCursor").GetComponent<MouseCursorController>().InterationPrompt();
                    }
                    InputController.GetKey();
                    if (!dog_inBounds)
                    {
                        return;
                    }
                    if (m_dog.GetComponent<PlayerActions>().GetInteraction()&&!action_finished)
                    {
                        action_finished = true;
                        GameObject.Find("MouseCursor").GetComponent<MouseCursorController>().EnterPointPlane();
                        Transform stone = transform.parent;
                        stone.GetChild(1).GetComponent<ShowAndHide>().Hide(3f);
                        stone.GetChild(2).gameObject.SetActive(true);
                        stone.GetChild(2).GetComponent<ShowAndHide>().Show(3f);
                        StartCoroutine(DelayToInvoke.DelayToInvokeDo(() =>
                        {
                            m_dog.GetComponent<DogMoving>().StopMoving();
                            gameObject.SetActive(false);
                            stone.GetComponent<Collider2D>().isTrigger = true;
                        }, 3f));
                    }
                }else
                {
                    GameObject.Find("MouseCursor").GetComponent<MouseCursorController>().EnterPointPlane();
                }
                break;
            case 3:
                if (transform.parent.GetComponent<Collider2D>().bounds.Contains(mousePosition))
                {
                    if (!action_finished)
                    {
                        GameObject.Find("MouseCursor").GetComponent<MouseCursorController>().InterationPrompt();
                    }
                    InputController.GetKey();
                    if (!dog_inBounds)
                    {
                        return;
                    }
                    if (m_dog.GetComponent<PlayerActions>().GetInteraction() && !action_finished)
                    {
                        GetSomething("零件");
                        GameObject.Find("BackpackUI").GetComponent<BackpackUI>().AddItem("零件");
                        GameObject.Find("MouseCursor").GetComponent<MouseCursorController>().EnterPointPlane();
                        zimu.Show("拿到了零件！可以修理一下轮椅了。");
                        GameObject.Find("CameraAndCharacterController").GetComponent<CameraAndCharacterController>().SendMessage("LookAtMan");
                    }
                }else
                {
                    GameObject.Find("MouseCursor").GetComponent<MouseCursorController>().EnterPointPlane();
                }
                break;
            default:
                break;
        }
        
    }
    private void GetSomething(string name)
    {
        getOrLostItem.character = m_dog.transform;
        getOrLostItem.xOffset = 0f;
        getOrLostItem.yOffset = 2.6f;
        transform.parent.GetChild(1).GetComponent<ShowAndHide>().Hide(2f);
        GetComponent<InteractionTriggerDog>().enabled=false;
        InputController.BanButton(true);
        InputController.BanMouse(true);
        getOrLostItem.GetShow(name, 1f, 1f, 1f, delegate () {
            Destroy(getOrLostItem);
            GameObject.Find("箱子").GetComponent<Collider2D>().isTrigger = true;
            InputController.BanButton(false);
            InputController.BanMouse(false);

        }, 1f);
    }
}
