using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CanvasGroup))]
public class ShowAndHideUI : MonoBehaviour
{
    public bool m_IsShowing = false;


    //showTime时间内渐入,alpha完全显示时的alpha值，action执行完渐入后执行的函数
    public void Show(float showTime = 1f,System.Action action = null,float alpha =1f)
    {
        GetComponent<CanvasGroup>().alpha = 0;//初始化alpha为0
        m_IsShowing = true;
        gameObject.SetActive(m_IsShowing);
        StartCoroutine(IE_Show(showTime,alpha,action));
    }
    IEnumerator IE_Show(float showTime,float alpha, System.Action action)
    {
            float delta = alpha * Time.deltaTime / showTime; //showTime时间内渐入/淡出
            while (GetComponent<CanvasGroup>().alpha < alpha)
            {
                if (!m_IsShowing) break;
                GetComponent<CanvasGroup>().alpha += delta;
                if (GetComponent<CanvasGroup>().alpha > alpha) GetComponent<CanvasGroup>().alpha = alpha;
                yield return 0;
            }
            if (action != null)
            {
                action.Invoke();
            }
    }

    //showTime时间内淡出
    public void Hide(float showTime = 1f,System.Action action = null)
    {
        if (m_IsShowing)
        {
            m_IsShowing = false;
            StartCoroutine(IE_Hide(showTime,action));
        }  
    }
    IEnumerator IE_Hide(float showTime, System.Action action)
    {
        float delta = Time.deltaTime / showTime; //showTime时间内渐入/淡出
        while (true)
        {
            float cur = GetComponent<CanvasGroup>().alpha;
            GetComponent<CanvasGroup>().alpha = cur - delta;
            if (GetComponent<CanvasGroup>().alpha <= 0)
            {
                GetComponent<CanvasGroup>().alpha = 0;
                break;
            }
            yield return 0;
        }
        gameObject.SetActive(m_IsShowing);
        if (action != null)
        {
            action.Invoke();
        }
    }
}
