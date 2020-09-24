using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStart : MonoBehaviour
{
    private float wait_time = 3f;

    private void Awake()
    {
        Invoke("YouCanMove", wait_time);
    }

    private void YouCanMove()
    {
        Debug.Log("CanMove");
        GameController.AnyDownStart();
    }
}
