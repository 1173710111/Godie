using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpStairsTrigger : MonoBehaviour
{
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Debug.Log("UpStairs");
            collision.GetComponent<PlayerMoving>().SendMessage("PI_ChangeOnStairs", 1);
        }
    }
}
