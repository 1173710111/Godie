using System.Collections;
using System.Collections.Generic;
using UnityEngine;

class MonoStub : MonoBehaviour
{

}

public static class TransitionUI
{
    private static GameObject m_TransitionPrefab;
    private static GameObject transition;
    private static GameObject monoStub;

    //黑屏淡入,action淡入后执行的函数体
    public static void FadeIn(float showTime = 2f,System.Action action = null)
    {
        if (m_TransitionPrefab==null) m_TransitionPrefab = Resources.Load("Transition") as GameObject;
        if(transition==null)transition = GameObject.Instantiate(m_TransitionPrefab,GameObject.Find("UI").transform);
        transition.GetComponent<CanvasGroup>().alpha = 0;
        transition.GetComponent<ShowAndHideUI>().Show(showTime,action);
    }

    //从黑屏淡出,action淡出后执行的函数体
    public static void FadeOut(float hideTime = 2f, System.Action action = null)
    {
        if (m_TransitionPrefab == null) m_TransitionPrefab = Resources.Load("Transition") as GameObject;
        if (transition == null) transition = GameObject.Instantiate(m_TransitionPrefab, GameObject.Find("UI").transform);
        transition.GetComponent<CanvasGroup>().alpha = 1;
        transition.GetComponent<ShowAndHideUI>().m_IsShowing = true;
        transition.GetComponent<ShowAndHideUI>().Hide(hideTime, action);
    }

    //黑屏淡出并淡出,action淡出后执行的函数体
    public static void Fade(float showTime = 2f,float hideTime = 2f ,float holdTime = 0.5f,System.Action action = null)
    {
        if (m_TransitionPrefab == null) m_TransitionPrefab = Resources.Load("Transition") as GameObject;
        if (transition == null) transition = GameObject.Instantiate(m_TransitionPrefab, GameObject.Find("UI").transform);
        transition.GetComponent<CanvasGroup>().alpha = 0;
        transition.GetComponent<ShowAndHideUI>().Show(showTime,delegate() {
            if (monoStub == null) monoStub = new GameObject();
            monoStub.AddComponent<MonoStub>();
            monoStub.GetComponent<MonoStub>().StartCoroutine(IE_Hold(hideTime,holdTime,action));
        });
    }
    static IEnumerator IE_Hold(float hideTime,float holdTime,System.Action action)
    {
        float m_Timer = 0f;
        while (m_Timer < holdTime)
        {
            m_Timer += Time.deltaTime;
            yield return 0;
        }
        transition.GetComponent<ShowAndHideUI>().Hide(hideTime, action);
    }

}
