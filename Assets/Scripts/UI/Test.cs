﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Test : MonoBehaviour
{
    public ZimuUI zimuUI;
    public GameObject Hint;
    public GetOrLostItem getOrLostItem;
    public BubbleHintUI bubbleHintUI;
    public Skill skill01;
    public Skill skill02;

    private void Start()
    {

    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.N))
        {
            skill01.GetSkill();
        }
        if (Input.GetKeyDown(KeyCode.B))
        {
            skill01.UseSkill();
        }
        if (Input.GetKeyDown(KeyCode.V))
        {
            skill02.GetSkill();
        }
        if (Input.GetKeyDown(KeyCode.K))
        {
            zimuUI.Show("甲:“我给你讲个冷笑话啊!”\n乙:“好啊。”\n甲:“这个笑话很冷!”\n乙:“我知道了你讲吧。”\n沉默中……\n乙:“你讲啊!”\n甲:“我讲完了啊。”");
        }
        /*if (Input.GetKeyDown(KeyCode.O))
        {
            getOrLostItem.GetShow("瓶装水",5f);
        }
        if (Input.GetKeyDown(KeyCode.P))
        {
            getOrLostItem.LostShow("罐头");
        }
        if (Input.GetKeyDown(KeyCode.U))
        {
            bubbleHintUI.ShowAndHide("罐头",5f);
        }*/
        if (Input.GetKeyDown(KeyCode.L))
        {
            GameObject.Find("BackpackUI/Canvas/Note").transform.GetComponent<NoteUI>().Show("罐头",5f);
        }
        if (Input.GetKeyDown(KeyCode.M))
        {
            Debug.Log("Input M");
            Debug.Log(Time.timeScale);
            GameObject.Find("n格漫画UI").transform.GetComponent<CartoonUI>().Show(1);
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            GameObject.Find("BackpackUI").GetComponent<BackpackUI>().AddItem("炸弹");
        }
        if (Input.GetKeyDown(KeyCode.T))
        {
            GameObject.Find("BackpackUI").GetComponent<BackpackUI>().RemoveItem("炸弹");
        }
        if (Input.GetKeyUp(KeyCode.Q))
        {
            Debug.Log("Input Q");
            TransitionUI.FadeOut();
        }
    }
}