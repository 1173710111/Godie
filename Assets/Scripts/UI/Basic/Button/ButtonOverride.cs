using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[RequireComponent(typeof(ButtonParameter))]
public class ButtonOverride : Button
{
    private AudioSourceController m_AudioSourceController;

    public void Init()
    {
        Text text = transform.GetComponentInChildren<Text>();
        if (text)
        {
            text.fontSize = GetComponent<ButtonParameter>().fontNormalSize;
            text.color = GetComponent<ButtonParameter>().fontNormalColor;
        }
    }

    public override void OnSelect(BaseEventData eventData)
    {
        base.OnSelect(eventData);
    }

    public override void OnPointerEnter(PointerEventData eventData)
    {
        //音效
        if (m_AudioSourceController == null) m_AudioSourceController = AudioSourcesManager.ApplyAudioSourceController();
        m_AudioSourceController.Play("按钮悬浮", transform);

        base.OnPointerEnter(eventData);
        Text text = transform.GetComponentInChildren<Text>();
        if (text)
        {
            text.fontSize = GetComponent<ButtonParameter>().fonthighlightSize;
            text.color = GetComponent<ButtonParameter>().fontHighlightedColor;
        }
    }

    public override void OnPointerExit(PointerEventData eventData)
    {
        base.OnPointerExit(eventData);
        Text text = transform.GetComponentInChildren<Text>();
        if (text)
        {
            text.fontSize = GetComponent<ButtonParameter>().fontNormalSize;
            text.color = GetComponent<ButtonParameter>().fontNormalColor;
        }
    }
    public override void OnPointerDown(PointerEventData eventData)
    {
        base.OnPointerDown(eventData);
        Text text = transform.GetComponentInChildren<Text>();
        if (text)
        {
            text.fontSize = GetComponent<ButtonParameter>().fontNormalSize;
            text.color = GetComponent<ButtonParameter>().fontNormalColor;
        }
    }

}
