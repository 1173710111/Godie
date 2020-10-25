using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpStairs : MonoBehaviour
{
    [Tooltip("是上楼还是下楼")]
    public bool is_up=true;
    public Transform position_another_floor;

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
                TransitionUI.FadeIn(3f);
                StartCoroutine(DelayToInvoke.DelayToInvokeDo(() =>
                {
                    GameObject.Find("scene4-characters").transform.position = position_another_floor.position;
                    TransitionUI.FadeOut(3f);
                }, 3f));
            }
        }
    }
}
