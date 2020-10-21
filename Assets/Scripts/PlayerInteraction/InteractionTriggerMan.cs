using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionTriggerMan : MonoBehaviour
{
    public GameObject remind_prefab;
    public GameObject getOrLose_prefab;
    public Transform remind_position;

    [Tooltip("交互的类型，0表示捡起纸条，1表示捡起罐头，2表示摸狗头，3表示清理轮椅，4表示关闭电源开关，5表示触发警示")]
    public int interaction_type;
    [Tooltip("捡起的物品的图片")]
    public GameObject get_object;
    

    private GameObject remind;
    private bool is_inBounds;
    private GameObject player;
    private GameObject getOrLose;
    private GetOrLostItem getOrLostItem;
    private ItemsData itemsData;

    private void Awake()
    {
        if (getOrLose_prefab != null)
        {
            getOrLose = Instantiate(getOrLose_prefab, transform);
            getOrLostItem = getOrLose.GetComponent<GetOrLostItem>();
        }
        if (GameObject.Find("ItemsData") != null)
        {
            itemsData = GameObject.Find("ItemsData").GetComponent<ItemsData>();
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            player = collision.gameObject;
            is_inBounds = true;
            if (interaction_type == 5)
            {
                remind = Instantiate(remind_prefab);
                remind.transform.parent = player.transform;
                remind.transform.localPosition = new Vector3(0f, 3f, 0f);
                remind.transform.localScale = new Vector3(0.4f,0.4f,0.4f);
                remind.GetComponent<ShowAndHide>().Show(1f);
                return;
            }
            if (gameObject.transform.GetChild(1).gameObject.activeInHierarchy)
            {
                remind = Instantiate(remind_prefab, remind_position);
            }
            
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (interaction_type == 5)
            {
                remind.GetComponent<ShowAndHide>().Hide(1f);
                return;
            }
            Destroy(remind);
            player = null;
            is_inBounds = false;
        }
    }

    private void Update()
    {
        if (!gameObject.transform.GetChild(1).gameObject.activeInHierarchy)
        {
            return;
        }
        if (is_inBounds && player != null)
        {
            
            if (player.GetComponent<PlayerActions>().GetInteraction())
            {
                
                switch (interaction_type)
                {
                    case 0:
                        break;
                    case 1:
                        GetSomething("罐头");
                        //Invoke("LoadSence", 3f);
                        StartCoroutine(DelayToInvoke.DelayToInvokeDo(() =>
                        {
                            GameController.LoadScene(4);
                        }, 8f));
                        break;
                    case 2:
                        break;
                    case 3:
                        GameObject.Find("零件堆").GetComponent<Animator>().SetTrigger("Disappear");
                        itemsData.items.Add(new Item("轮椅","", get_object.GetComponent<SpriteRenderer>().sprite));
                        GetSomething("轮椅");
                        transform.parent.Find("SwitchTrigger").gameObject.SetActive(true);
                        break;
                    case 4:
                        InputController.BanButton(true);
                        InputController.BanMouse(true);
                        GameObject.Find("TrackCameraController").GetComponent<TrackCameraController>().StartMove();
                        transform.GetChild(1).gameObject.SetActive(false);
                        StartCoroutine(DelayToInvoke.DelayToInvokeDo(() =>
                        {
                            GameObject.Find("漏电的电线").GetComponent<Animator>().SetTrigger("IsCut");
                            GameObject.Find("ElectricLine").SetActive(false);
                            Destroy(remind);
                        }, 4f));
                        StartCoroutine(DelayToInvoke.DelayToInvokeDo(() =>
                        {
                            GameObject.Find("TrackCameraController").GetComponent<TrackCameraController>().Finished();
                            Invoke("CanMove", 2.4f);
                        }, 5.5f));
                        StartCoroutine(DelayToInvoke.DelayToInvokeDo(() =>
                        {
                            InputController.BanButton(false);
                            InputController.BanMouse(false);
                        }, 8f));
                        //Invoke("Cut", 4f);
                        //Invoke("Back", 5.5f);
                        break;
                    case 5:
                        break;
                    default:
                        break;
                }
            }
        }
    }

    private void GetSomething(string name)
    {
        getOrLostItem.character = player.transform;
        getOrLostItem.xOffset = 0f;
        getOrLostItem.yOffset = 2.6f;
        gameObject.transform.GetChild(1).GetComponent<ShowAndHide>().Hide(2f);
        getOrLostItem.GetShow(name, 0.5f, 1f, 1f, 1f);
        Destroy(remind);
        //Invoke("Finished", 1f);
        StartCoroutine(DelayToInvoke.DelayToInvokeDo(() =>
        {
            transform.GetChild(1).gameObject.SetActive(false);
            getOrLostItem.gameObject.SetActive(false);
        }, 1f));

        if (interaction_type == 3)
        {
            CameraAndCharacterController cameraController = GameObject.Find("CameraAndCharacterController").GetComponent<CameraAndCharacterController>();
            cameraController.character_man.GetComponent<ShowAndHide>().Hide(1f);
            //Invoke("ShowChairMan", 1f);
            StartCoroutine(DelayToInvoke.DelayToInvokeDo(() =>
            {
                ShowChairMan();
            }, 1f));
        }


    }

    private void ShowChairMan()
    {
        CameraAndCharacterController cameraController = GameObject.Find("CameraAndCharacterController").GetComponent<CameraAndCharacterController>();
        GameObject characters = GameObject.Find("scene2-character");
        GameObject character_chair = characters.transform.Find("Character-chair").gameObject;
        character_chair.SetActive(true);
        character_chair.GetComponent<ShowAndHide>().Show(1f);
        cameraController.character_man = character_chair;
        GameObject.Find("Character-dog-growup").GetComponent<AutoFollow>().followed=character_chair;
        GameObject cameras = GameObject.Find("scene2-camera");
        GameObject camera_chair = cameras.transform.Find("CM vcam3").gameObject;
        cameraController.camera_man.SetActive(false);
        camera_chair.SetActive(true);
        cameraController.camera_man = camera_chair;
        Physics2D.IgnoreCollision(character_chair.GetComponent<Collider2D>(), cameraController.character_dog.GetComponent<Collider2D>(), true);
        
    }

   /* private void Cut()
    {
        GameObject.Find("漏电的电线").GetComponent<Animator>().SetTrigger("IsCut");
        GameObject.Find("ElectricLine").SetActive(false);
        Destroy(remind);
    }

    private void Back()
    {
        GameObject.Find("TrackCameraController").GetComponent<TrackCameraController>().Finished();
        Invoke("CanMove", 2.4f);
        
    }

    private void LoadSence()
    {
        GameController.LoadScene(4);
    }*/
    
    private void CanMove()
    {
        InputController.BanButton(false);
        InputController.BanMouse(false);
    }
}
