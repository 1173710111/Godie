using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bubble : MonoBehaviour
{
    public GameObject bubble_prefab;
    public Transform playerStartPosition;
    public float fadeTime = 0.8f;
    public float showTime = 3f;

    private GameObject bubble;
    void Start()
    {
        InputController.BanButton(true);
        InputController.BanMouse(true);
        bubble= Instantiate(bubble_prefab, new Vector3(playerStartPosition.position.x, playerStartPosition.position.y + 2.6f, playerStartPosition.position.z), Quaternion.identity);
        bubble.GetComponent<BubbleHintUI>().character = playerStartPosition;
        bubble.GetComponent<BubbleHintUI>().xOffset = 0f;
        bubble.GetComponent<BubbleHintUI>().yOffset = 2.6f;
        bubble.GetComponent<BubbleHintUI>().ShowAndHide("罐头",fadeTime,showTime,1f);
        Invoke("CanMove", showTime+fadeTime*2f);
    }


    void CanMove()
    {
        InputController.BanButton(false);
        InputController.BanMouse(false);
    }
}