using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(CanvasGroup))]
public class GetOrLostItem : MonoBehaviour
{
    [Tooltip("获得道具的符号")] public Sprite getIron;
    [Tooltip("失去道具的符号")] public Sprite lostIron;

    [Header("相对于人物的位置")]
    [Tooltip("人物的gameobject")] public Transform character;
    [Tooltip("x轴偏移量")] public float xOffset;
    [Tooltip("y轴偏移量")] public float yOffset;

    private bool m_IsShowing = false;
    private ItemsData itemsData; 

    private void Awake()
    {
        transform.GetComponent<CanvasGroup>().alpha = 0;
        itemsData = GameObject.Find("ItemsData").GetComponent<ItemsData>();
    }

    //显示获得道具的提示：道具名、渐入并淡出时长、上下偏移量、完全显示的停留时长、完全显示时的alpha值
    public void GetShow(string itemName,float showTime = 2f, float offset = 0.4f, float holdTime = 0.2f, float alpha = 1f) 
    {
        transform.Find("道具").GetComponent<Image>().sprite = itemsData.GetSpriteByItemName(itemName);
        transform.Find("符号").GetComponent<Image>().sprite = getIron;
        ShowFromTop(showTime, offset, holdTime, alpha);
    }

    //显示失去道具的提示：道具名、渐入并淡出时长、上下偏移量、完全显示的停留时长、完全显示时的alpha值
    public void LostShow(string itemName,float showTime = 2f, float offset = 0.4f, float holdTime = 0.2f, float alpha = 1f) 
    {
        transform.Find("道具").GetComponent<Image>().sprite = itemsData.GetSpriteByItemName(itemName);
        transform.Find("符号").GetComponent<Image>().sprite = lostIron;
        ShowFromButtom(showTime, offset, holdTime, alpha);
    }

    //设置该提示的位置（世界坐标，只需x，y轴，z默认为0）
    public void SetPosition(float x,float y)
    {
        transform.GetComponent<RectTransform>().position = new Vector3(x, y, 0);
    }



    private void ShowFromTop(float showTime,float offset,float holdTime,float alpha) //showTime时间内渐入并淡出（从上渐入至下淡出）,holdTime是完全显示的保持时间,offset是上下的偏移量
    {
        if (m_IsShowing) return;
        m_IsShowing = true;
        gameObject.SetActive(m_IsShowing);
        StartCoroutine(IE_ShowFromTop(showTime,offset,holdTime,alpha));
    }
    IEnumerator IE_ShowFromTop(float showTime, float offset,float holdTime ,float alpha) //showTime时间内渐入并淡出（从上渐入至下淡出）,offset是上下的偏移量
    {
        //渐入
        float recentOffset = 0f;
        transform.GetComponent<CanvasGroup>().alpha = 0;
        transform.GetComponent<RectTransform>().position = new Vector3(character.position.x + xOffset, character.position.y + yOffset + offset, character.position.z);
        float deltaOffset = offset * Time.deltaTime / (showTime/2f);
        float deltaAlpha = alpha * Time.deltaTime / (showTime/2f);
        
        while (true) 
        {
            transform.GetComponent<CanvasGroup>().alpha += deltaAlpha;
            if (transform.GetComponent<CanvasGroup>().alpha > alpha) transform.GetComponent<CanvasGroup>().alpha = alpha;
            recentOffset += deltaOffset;
            transform.GetComponent<RectTransform>().position = new Vector3(character.position.x + xOffset, character.position.y + yOffset + offset - recentOffset, character.position.z);
            if (transform.GetComponent<RectTransform>().position.y < character.position.y + yOffset) transform.GetComponent<RectTransform>().position = new Vector3(character.position.x + xOffset, character.position.y + yOffset, character.position.z) ;
            if (transform.GetComponent<CanvasGroup>().alpha == alpha && transform.GetComponent<RectTransform>().position.y == character.position.y + yOffset) break;
            
            yield return 0;
        }
        Debug.Log(transform.Find("道具").GetComponent<Image>().sprite.name);
        //完全显示时保持不变
        float timer = 0f;
        while (timer<holdTime)
        {
            transform.GetComponent<RectTransform>().position = new Vector3(character.position.x + xOffset, character.position.y + yOffset, character.position.z);
            timer += Time.deltaTime;
            yield return 0;
        }
        

        //淡出
        recentOffset = 0f;
        transform.GetComponent<CanvasGroup>().alpha = alpha;
        transform.GetComponent<RectTransform>().position = new Vector3(character.position.x + xOffset, character.position.y + yOffset, character.position.z);
        while (true)
        {
            transform.GetComponent<CanvasGroup>().alpha -= deltaAlpha;
            if (transform.GetComponent<CanvasGroup>().alpha < 0f) transform.GetComponent<CanvasGroup>().alpha = 0f;
            recentOffset += deltaOffset;
            transform.GetComponent<RectTransform>().position = new Vector3(character.position.x + xOffset, character.position.y + yOffset - recentOffset, character.position.z);
            if (transform.GetComponent<RectTransform>().position.y < character.position.y + yOffset - offset) transform.GetComponent<RectTransform>().position = new Vector3(character.position.x + xOffset, character.position.y + yOffset - offset, character.position.z);
            if (transform.GetComponent<CanvasGroup>().alpha == 0 && transform.GetComponent<RectTransform>().position.y == (float)(character.position.y + yOffset - offset)) break;
            yield return 0;
        }
        m_IsShowing = false;
        gameObject.SetActive(m_IsShowing);
    }

    private void ShowFromButtom(float showTime, float offset, float holdTime, float alpha) //showTime时间内渐入并淡出（从下渐入至上淡出）,holdTime是完全显示的保持时间,offset是上下的偏移量
    {
        if (m_IsShowing) return;
        m_IsShowing = true;
        gameObject.SetActive(m_IsShowing);
        StartCoroutine(IE_ShowFromButtom(showTime, offset, holdTime, alpha));
    }
    IEnumerator IE_ShowFromButtom(float showTime, float offset, float holdTime, float alpha) //showTime时间内渐入并淡出（从下渐入至上淡出）,offset是上下的偏移量
    {
        //渐入
        float recentOffset = 0f;
        transform.GetComponent<CanvasGroup>().alpha = 0;
        transform.GetComponent<RectTransform>().position = new Vector3(character.position.x + xOffset, character.position.y + yOffset - offset, character.position.z);
        float deltaOffset = offset * Time.deltaTime / (showTime / 2f);
        float deltaAlpha = alpha * Time.deltaTime / (showTime / 2f);
        while (true)
        {
            transform.GetComponent<CanvasGroup>().alpha += deltaAlpha;
            if (transform.GetComponent<CanvasGroup>().alpha > alpha) transform.GetComponent<CanvasGroup>().alpha = alpha;
            recentOffset += deltaOffset;
            transform.GetComponent<RectTransform>().position = new Vector3(character.position.x + xOffset, character.position.y + yOffset - offset + recentOffset, character.position.z);
            if (transform.GetComponent<RectTransform>().position.y > character.position.y + yOffset) transform.GetComponent<RectTransform>().position = new Vector3(character.position.x + xOffset, character.position.y + yOffset, character.position.z);
            if (transform.GetComponent<CanvasGroup>().alpha == alpha && transform.GetComponent<RectTransform>().position.y == character.position.y + yOffset) break;
            yield return 0;
        }

        //完全显示时保持不变
        float timer = 0f;
        while (timer < holdTime)
        {
            transform.GetComponent<RectTransform>().position = new Vector3(character.position.x + xOffset, character.position.y + yOffset, character.position.z);
            timer += Time.deltaTime;
            yield return 0;
        }

        //淡出
        recentOffset = 0f;
        transform.GetComponent<CanvasGroup>().alpha = alpha;
        transform.GetComponent<RectTransform>().position = new Vector3(character.position.x + xOffset, character.position.y + yOffset, character.position.z);
        while (true)
        {
            transform.GetComponent<CanvasGroup>().alpha -= deltaAlpha;
            if (transform.GetComponent<CanvasGroup>().alpha < 0f) transform.GetComponent<CanvasGroup>().alpha = 0f;
            recentOffset += deltaOffset;
            transform.GetComponent<RectTransform>().position = new Vector3(character.position.x + xOffset, character.position.y + yOffset + recentOffset, character.position.z);
            if (transform.GetComponent<RectTransform>().position.y > character.position.y + yOffset + offset) transform.GetComponent<RectTransform>().position = new Vector3(character.position.x + xOffset, character.position.y + yOffset + offset, character.position.z);
            if (transform.GetComponent<CanvasGroup>().alpha == 0 && transform.GetComponent<RectTransform>().position.y == (float)(character.position.y + yOffset + offset)) break;
            yield return 0;
        }
        m_IsShowing = false;
        gameObject.SetActive(m_IsShowing);
    }
}
