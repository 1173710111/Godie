using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowAndHide : MonoBehaviour
{
    private SpriteRenderer[] renderers;
    private bool m_IsShowing = false;

    private void Awake()
    {
        renderers = transform.GetComponentsInChildren<SpriteRenderer>();
    }

    //showTime时间内渐入
    public void Show(float showTime = 1f, System.Action action = null)
    {
        //初始化alpha为0
        renderers = transform.GetComponentsInChildren<SpriteRenderer>();
        for (int i = 0; i < renderers.Length; i++)
        {
            renderers[i].color = new Color(renderers[i].color.r, renderers[i].color.g, renderers[i].color.b, 0f);
        }
        m_IsShowing = true;
        gameObject.SetActive(m_IsShowing);
        StartCoroutine(IE_Show(showTime, action));
    }
    IEnumerator IE_Show(float showTime, System.Action action = null)
    {
        float delta = Time.deltaTime / showTime; //showTime时间内渐入/淡出
        bool finish = false;
        while (!finish)
        {
            finish = true;
            for(int i = 0; i < renderers.Length; i++)
            {
                renderers[i].color = new Color(renderers[i].color.r, renderers[i].color.g, renderers[i].color.b, (renderers[i].color.a + delta) <= 1f ? (renderers[i].color.a + delta) : 1f);
                if (renderers[i].color.a != 1f) finish = false;
            }
            yield return 0;
        }
        if (action != null)
        {
            action.Invoke();
        }
    }

    //showTime时间内淡出
    public void Hide(float hideTime = 1f, System.Action action = null)
    {
        m_IsShowing = false;
        StartCoroutine(IE_Hide(hideTime,action));
    }
    IEnumerator IE_Hide(float hideTime, System.Action action)
    {
        float delta = Time.deltaTime / hideTime; //showTime时间内渐入/淡出
        bool finish = false;
        while (!finish)
        {
            finish = true;
            for (int i = 0; i < renderers.Length; i++)
            {
                renderers[i].color = new Color(renderers[i].color.r, renderers[i].color.g, renderers[i].color.b, (renderers[i].color.a - delta) >= 0f ? (renderers[i].color.a - delta) : 0f);
                if (renderers[i].color.a != 0f) finish = false;
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
