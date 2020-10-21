using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionTriggerDog : MonoBehaviour
{
    [Tooltip("交互类型,0表示推门")]
    public int interaction_type;
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

    private void CanMove()
    {
        InputController.BanButton(false);
        InputController.BanMouse(false);
    }
}
