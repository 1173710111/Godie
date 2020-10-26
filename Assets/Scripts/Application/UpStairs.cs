using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpStairs : MonoBehaviour
{
    [Tooltip("是上楼还是下楼")]
    public bool is_up=true;
    [Tooltip("另一层人的起始位置")]
    public Transform position_man;
    [Tooltip("另一层狗的起始位置")]
    public Transform position_dog;
    public GameObject environment1;
    public GameObject environment2;

    private bool is_inBounds=false;
    private GameObject player;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            is_inBounds = true;
            player = collision.gameObject;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            is_inBounds = false;
            player = null;
        }
    }

    private void Update()
    {
        if (is_inBounds&&player!=null)
        {
            
            if (player.GetComponent<PlayerActions>().GetInteraction())
            {
                
                TransitionUI.FadeIn(3f,()=>{
                    GameObject.Find("Character-chair").transform.position = position_man.position;
                    GameObject.Find("Character-dog-growup").transform.position = position_dog.position;
                    if (is_up)
                    {
                        environment1.SetActive(false);
                        environment2.SetActive(true);
                    }
                    else
                    {
                        environment1.SetActive(true);
                        environment2.SetActive(false);
                    }
                    TransitionUI.FadeOut(3f);
                });
            }
        }
    }
}
