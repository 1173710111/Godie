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
    private AudioSourceController m_AudioSourceController;


    private void Awake()
    {
        transform.GetComponent<CanvasGroup>().alpha = 0;
        itemsData = GameObject.Find("ItemsData").GetComponent<ItemsData>();
    }

    //显示获得道具的提示：道具名、渐入并淡出时长、上下偏移量、完全显示的停留时长、完全显示时的alpha值
    public void GetShow(string itemName, float showTime = 2f, float offset = 0.4f, float holdTime = 0.2f, System.Action action = null, float alpha = 1f)
    {
        transform.Find("道具").GetComponent<Image>().sprite = itemsData.GetSpriteByItemName(itemName);
        transform.Find("符号").GetComponent<Image>().sprite = getIron;
        ShowFromTop(showTime, offset, holdTime, action, alpha);

        //音效
        if (m_AudioSourceController == null) m_AudioSourceController = AudioSourcesManager.ApplyAudioSourceController();
        m_AudioSourceController.Play("获得道具", transform);
    }

    //显示失去道具的提示：道具名、渐入并淡出时长、上下偏移量、完全显示的停留时长、完全显示时的alpha值
    public void LostShow(string itemName, float showTime = 2f, float offset = 0.4f, float holdTime = 0.2f, System.Action action = null, float alpha = 1f)
    {
        transform.Find("道具").GetComponent<Image>().sprite = itemsData.GetSpriteByItemName(itemName);
        transform.Find("符号").GetComponent<Image>().sprite = lostIron;
        ShowFromButtom(showTime, offset, holdTime, action, alpha);
    }




    private void ShowFromTop(float showTime, float offset, float holdTime, System.Action action, float alpha) //showTime时间内渐入并淡出（从上渐入至下淡出）,holdTime是完全显示的保持时间,offset是上下的偏移量
    {
        if (m_IsShowing) return;
        m_IsShowing = true;
        gameObject.SetActive(m_IsShowing);
        StartCoroutine(IE_ShowFromTop(showTime, offset, holdTime, action, alpha));
    }
    IEnumerator IE_ShowFromTop(float showTime, float offset, float holdTime, System.Action action, float alpha) //showTime时间内渐入并淡出（从上渐入至下淡出）,offset是上下的偏移量
    {
        //渐入
        float recentOffset = 0f;
        transform.GetComponent<CanvasGroup>().alpha = 0;
        transform.GetComponent<RectTransform>().position = new Vector3(character.position.x + xOffset, character.position.y + yOffset + offset, character.position.z);
        float deltaOffset = offset * Time.deltaTime / (showTime / 2f);
        float deltaAlpha = alpha * Time.deltaTime / (showTime / 2f);
        while (true)
        {
            transform.GetComponent<CanvasGroup>().alpha += deltaAlpha;
            if (transform.GetComponent<CanvasGroup>().alpha > alpha) transform.GetComponent<CanvasGroup>().alpha = alpha;
            recentOffset += deltaOffset;
            transform.GetComponent<RectTransform>().position = new Vector3(character.position.x + xOffset, character.position.y + yOffset + offset - recentOffset, character.position.z);
            if (transform.GetComponent<RectTransform>().position.y < character.position.y + yOffset) transform.GetComponent<RectTransform>().position = new Vector3(character.position.x + xOffset, character.position.y + yOffset, character.position.z);
            if (Mathf.Abs(transform.GetComponent<CanvasGroup>().alpha - alpha) < 0.01f && Mathf.Abs(transform.GetComponent<RectTransform>().position.y - (character.position.y + yOffset)) < 0.01f) break;
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
            transform.GetComponent<RectTransform>().position = new Vector3(character.position.x + xOffset, character.position.y + yOffset - recentOffset, character.position.z);
            if (transform.GetComponent<RectTransform>().position.y < character.position.y + yOffset - offset) transform.GetComponent<RectTransform>().position = new Vector3(character.position.x + xOffset, character.position.y + yOffset - offset, character.position.z);
            if (Mathf.Abs(transform.GetComponent<CanvasGroup>().alpha) < 0.01f && Mathf.Abs(transform.GetComponent<RectTransform>().position.y - (character.position.y + yOffset - offset)) < 0.01f) break;
            yield return 0;
        }
        m_IsShowing = false;
        gameObject.SetActive(m_IsShowing);
        if (action != null)
        {
            action.Invoke();
        }
    }

    private void ShowFromButtom(float showTime, float offset, float holdTime, System.Action action, float alpha) //showTime时间内渐入并淡出（从下渐入至上淡出）,holdTime是完全显示的保持时间,offset是上下的偏移量
    {
        if (m_IsShowing) return;
        m_IsShowing = true;
        gameObject.SetActive(m_IsShowing);
        StartCoroutine(IE_ShowFromButtom(showTime, offset, holdTime, action, alpha));
    }
    IEnumerator IE_ShowFromButtom(float showTime, float offset, float holdTime, System.Action action, float alpha) //showTime时间内渐入并淡出（从下渐入至上淡出）,offset是上下的偏移量
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
            if (Mathf.Abs(transform.GetComponent<CanvasGroup>().alpha - alpha) < 0.01f && Mathf.Abs(transform.GetComponent<RectTransform>().position.y - (character.position.y + yOffset)) < 0.01f) break;
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
            if (Mathf.Abs(transform.GetComponent<CanvasGroup>().alpha) < 0.01f && Mathf.Abs(transform.GetComponent<RectTransform>().position.y - (character.position.y + yOffset + offset)) < 0.01f) break;
            yield return 0;
        }
        m_IsShowing = false;
        gameObject.SetActive(m_IsShowing);
        if (action != null)
        {
            action.Invoke();
        }
    }
}
