using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;


public class BackpackUIToggle : Toggle
{
    private ItemsData itemsData;
    private string label;
    private float m_HangTime = 0f;
    private bool m_IsHang = false;
    private bool m_IntroduceShowing = false;

    protected override void Start()
    {
        itemsData = GameObject.Find("ItemsData").GetComponent<ItemsData>();
        label = transform.Find("Item Label").GetComponent<Text>().text;
    }

    public override void OnPointerEnter(PointerEventData eventData)
    {
        m_HangTime = 0f;
        m_IsHang = true;
        m_IntroduceShowing = false;
        base.OnPointerEnter(eventData);
    }

    public override void OnPointerExit(PointerEventData eventData)
    {
        m_IsHang = false;
        m_IntroduceShowing = false;
        transform.Find("Item Introduce").GetComponent<ShowAndHideUI>().Hide(0.1f);
        base.OnPointerExit(eventData);
    }

    public override void OnPointerUp(PointerEventData eventData)
    {
        Transform  Note = transform.parent.parent.parent.parent.parent.Find("Note");
        if (itemsData.GetItemByItemName(label).isNote)
        {
            Note.GetComponent<NoteUI>().Show(itemsData.GetItemByItemName(label).name);
        }
        base.OnPointerUp(eventData);
    }

    private void Update()
    {
        if (m_IsHang && !m_IntroduceShowing && !itemsData.GetItemByItemName(label).isNote)
        {
            m_HangTime += Time.deltaTime;
            if (m_HangTime > 0.5f)
            {
                m_IntroduceShowing = true;
                transform.Find("Item Introduce").GetComponentInChildren<Text>().text = itemsData.GetItemByItemName(label).introduce;
                transform.Find("Item Introduce").GetComponent<ShowAndHideUI>().Show(0.1f);
            }
        }
    }
}
