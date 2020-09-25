using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpStairsTrigger : MonoBehaviour
{
    public int stairs_upOrDown;
    public int next_scene;
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Debug.Log("UpStairs");
            collision.GetComponent<PlayerMoving>().SendMessage("PI_ChangeOnStairs",stairs_upOrDown);
            collision.GetComponent<PlayerMoving>().SendMessage("PI_ChangeScene", next_scene);
        }
    }
}
