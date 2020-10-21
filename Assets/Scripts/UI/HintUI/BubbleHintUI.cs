using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BubbleHintUI : MonoBehaviour
{
    [Header("相对于人物的位置")]
    [Tooltip("人物的gameobject")] public Transform character;
    [Tooltip("x轴偏移量")] public float xOffset;
    [Tooltip("y轴偏移量")] public float yOffset;

    private bool m_IsShowing = false;
    private ItemsData itemsData;

    //渐入并淡出，name提示内容的名称（对应道具类中的名称）,showTime渐入时长，holdTime完全显示的保持时长，alpha完全显示的alpha值
    public void ShowAndHide(string name, float showTime = 1f, float holdTime = 0.2f,float alpha = 1f)
    {
        
        transform.Find("内容").GetComponent<Image>().sprite = itemsData.GetSpriteByItemName(name);
        GetComponent<CanvasGroup>().alpha = 0;
        GetComponent<RectTransform>().position = character.position + new Vector3(xOffset, yOffset, 0);
        m_IsShowing = true;
        gameObject.SetActive(m_IsShowing);
        StartCoroutine(IE_ShowAndHide(showTime,holdTime, alpha));
    }
    IEnumerator IE_ShowAndHide(float showTime, float holdTime, float alpha)
    {
        //淡入
        float delta = Time.deltaTime / showTime; //showTime时间内渐入/淡出
        while (GetComponent<CanvasGroup>().alpha != alpha)
        {
            GetComponent<RectTransform>().position = character.position + new Vector3(xOffset, yOffset, 0);
            GetComponent<CanvasGroup>().alpha += delta;
            if (GetComponent<CanvasGroup>().alpha > alpha) GetComponent<CanvasGroup>().alpha = alpha;
            yield return 0;
        }

        //保持完全显示
        float timer = 0f;
        while (timer < holdTime)
        {
            GetComponent<RectTransform>().position = character.position + new Vector3(xOffset, yOffset, 0);
            timer += Time.deltaTime;
            yield return 0;
        }

        //淡出
        Hide(showTime);
    }

    //渐入，name提示内容的名称（对应道具类中的名称）,showTime渐入时长，alpha完全显示的alpha值
    public void Show(string name,float showTime = 1f,float alpha = 1f)
    {
        transform.Find("内容").GetComponent<Image>().sprite = itemsData.GetSpriteByItemName(name);
        GetComponent<CanvasGroup>().alpha = 0;
        GetComponent<RectTransform>().position = character.position + new Vector3(xOffset, yOffset, 0);
        m_IsShowing = true;
        gameObject.SetActive(m_IsShowing);
        StartCoroutine(IE_Show(showTime,alpha));
    }
    IEnumerator IE_Show(float showTime,float alpha)
    {
        float delta = Time.deltaTime / showTime; //showTime时间内渐入/淡出
        while (GetComponent<CanvasGroup>().alpha != alpha)
        {
            GetComponent<RectTransform>().position = character.position + new Vector3(xOffset, yOffset, 0);
            GetComponent<CanvasGroup>().alpha += delta;
            if (GetComponent<CanvasGroup>().alpha > alpha) GetComponent<CanvasGroup>().alpha = alpha;
            yield return 0;
        }
    }

    //淡出，showTime淡出时长，alpha完全显示的alpha值
    public void Hide(float hideTime = 1f)
    {
        m_IsShowing = false;
        GetComponent<RectTransform>().position = character.position + new Vector3(xOffset, yOffset, 0);
        StartCoroutine(IE_Hide(hideTime));
    }
    IEnumerator IE_Hide(float hideTime)
    {
        float delta = Time.deltaTime / hideTime; //showTime时间内渐入/淡出
        while (GetComponent<CanvasGroup>().alpha != 0)
        {
            GetComponent<RectTransform>().position = character.position + new Vector3(xOffset, yOffset, 0);
            GetComponent<CanvasGroup>().alpha -= delta;
            if (GetComponent<CanvasGroup>().alpha < 0) GetComponent<CanvasGroup>().alpha = 0;
            yield return 0;
        }
        gameObject.SetActive(m_IsShowing);
    }

    private void Awake()
    {
        transform.GetComponent<CanvasGroup>().alpha = 0;
        itemsData = GameObject.Find("ItemsData").GetComponent<ItemsData>();
        
    }
}
