using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionTriggerDog : MonoBehaviour
{
    [Tooltip("交互类型,0表示推门")]
    public int interaction_type;

    private bool dog_inBounds=false;
    private GameObject m_dog;
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

    private void Update()
    {
        if (!dog_inBounds)
        {
            return;
        }
        Vector3 playerWordDir = Camera.main.WorldToScreenPoint(new Vector3(0, 0, 0f));
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, playerWordDir.z));
        if (transform.parent.GetComponent<Collider2D>().bounds.Contains(mousePosition))
        {
            InputController.GetKey();
            if (m_dog.GetComponent<PlayerActions>().GetInteraction())
            {
                Transform stone = transform.parent;
                stone.GetChild(1).GetComponent<ShowAndHide>().Hide(3f);
                stone.GetChild(2).gameObject.SetActive(true);
                stone.GetChild(2).GetComponent<ShowAndHide>().Show(3f);
                StartCoroutine(DelayToInvoke.DelayToInvokeDo(() =>
                {
                    m_dog.GetComponent<DogMoving>().StopMoving();
                    gameObject.SetActive(false);
                    stone.GetComponent<Collider2D>().isTrigger=true;
                }, 3f));
            }
        }
    }
}
