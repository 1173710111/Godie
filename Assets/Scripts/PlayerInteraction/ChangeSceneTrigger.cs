using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeSceneTrigger : MonoBehaviour
{
    [Tooltip ("是否需要玩家交互后再切换场景")]
    public bool Need_Interaction=false;
    public int next_scene;

    private bool is_inBounds = false;
    private GameObject player;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (!Need_Interaction)
            {
                GameController.LoadScene(next_scene);
            }
            else
            {
                is_inBounds = true;
                player = collision.gameObject;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            is_inBounds = false;
        }
    }

    private void Update()
    {
        if (is_inBounds)
        {
            if (player.GetComponent<PlayerActions>().GetInteraction() && next_scene != 0)
            {
                GameController.LoadScene(next_scene);
            }
        }
    }
}
