using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class ZimuUI : MonoBehaviour
{
    private bool m_IsShowing = false; //是否已显示
    private string[] m_Content = new string[] { }; //文字内容
    [Tooltip("文字变化的间隔时间")] public float m_IntervalTime = 1.5f;

    //显示UI,自动播放内容，最后UI淡出
    public void Show(string text)
    {
        //UI淡入
        m_IsShowing = true;
        transform.GetComponent<Animator>().enabled = false;
        transform.GetComponent<Animator>().enabled = true;
        transform.gameObject.SetActive(m_IsShowing);
        SetText(text);
        transform.GetComponent<Animator>().SetTrigger("show");

        //播放文字内容，最后UI淡出
        PlayContent();
    }

    //自动播放文字内容（若有多行，每隔m_IntervalTime秒变化一次），最后淡出UI
    private void PlayContent()
    {
        
        Text text = transform.GetComponentInChildren<Text>();
        Animator anim = transform.GetComponent<Animator>();
        StartCoroutine(IE_PlayContent(text,anim));
    }
    IEnumerator IE_PlayContent(Text text,Animator anim) //自动播放文本,最后淡出UI
    {
        for (int i = 1; i <= m_Content.Length; i++)
        {
            yield return new WaitForSeconds(m_IntervalTime);
            if(i == m_Content.Length)
            {
                anim.SetTrigger("hide");
                yield return new WaitForSeconds(1f);
                m_IsShowing = false;
                anim.gameObject.SetActive(m_IsShowing);
                break;
            }
            float oldAlpha = text.color.a;
            float delta = text.color.a * Time.deltaTime / 0.2f; //0.2f内渐入/淡出
            while (true) //淡出
            {
                text.color = new Color(text.color.r, text.color.g, text.color.b, (text.color.a - delta) >= 0f ? (text.color.a - delta) : 0f);
                if (text.color.a == 0) break;
                yield return 0;
            }
            text.text = m_Content[i];
            while (true) //淡入
            {
                text.color = new Color(text.color.r, text.color.g, text.color.b, (text.color.a + delta) <= oldAlpha ? (text.color.a + delta) : oldAlpha);
                if (text.color.a == oldAlpha) break;
                yield return 0;
            }
        }
        Debug.Log("finish IE_PlayContent");
    }

    //设置字幕内容 以\n为分隔（内容每隔m_IntervalTime秒变化一次）
    public void SetText(string text)
    {
        Array.Clear(m_Content, 0, m_Content.Length);
        m_Content = text.Split('\n');
        transform.GetComponentInChildren<Text>().text = m_Content[0];
    }

    //返回值 是否已显示
    public bool isShowing()
    {
        return m_IsShowing;
    }

}
