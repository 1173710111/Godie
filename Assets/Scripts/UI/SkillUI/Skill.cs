using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


[RequireComponent(typeof(ShowAndHideUI))]
[RequireComponent(typeof(Image))]
public class Skill : MonoBehaviour
{
    [Tooltip("技能冷却时间")] public float cdTime;
    [Tooltip("未冷却时技能图标")] public Sprite normalSprite;
    [Tooltip("冷却时技能图标")] public Sprite cdSprite;

    private bool m_IsCD = false;
    private bool m_Disable = true;
    private float m_Timer = 0f;

    public bool isCD
    {
        get { return m_IsCD; }
    }

    public bool IsAble()
    {
        return !m_Disable;
    }

    public void GetSkill()
    {
        AudioSourceController audioSourceController = AudioSourcesManager.ApplyAudioSourceController();
        audioSourceController.Play("修理轮椅", transform);
        m_Disable = false;
        GetComponent<ShowAndHideUI>().Show();
        GetComponent<Image>().sprite = normalSprite;
        GetComponent<Image>().color = new Color(1f, 1f, 1f, 1f);
        transform.GetChild(0).GetComponent<Image>().color = new Color(1, 1, 1, 0);
    }

    public void UseSkill()
    {
        if (!m_IsCD && !m_Disable)
        {
            m_IsCD = true;
            m_Timer = 0f;
            GetComponent<Image>().sprite = normalSprite;
            GetComponent<Image>().color = new Color(0.5f, 0.5f, 0.5f, 1f);
            transform.GetChild(0).GetComponent<Image>().sprite = cdSprite;
            transform.GetChild(0).GetComponent<Image>().color = new Color(0f, 0f,0f, 0.4f);
            AudioSourceController audioSourceController = AudioSourcesManager.ApplyAudioSourceController();
            audioSourceController.Play("冲刺", transform);
            StartCoroutine(IE_CD());
        }
    }
    IEnumerator IE_CD()
    {
        while (true)
        {
            if (m_Timer >= cdTime)
            {
                m_IsCD = false;
                GetComponent<Image>().sprite = normalSprite;
                GetComponent<Image>().color = new Color(1f, 1f, 1f, 1f);
                transform.GetChild(0).GetComponent<Image>().color = new Color(1, 1, 1, 0);
                break;
            }
            m_Timer += Time.deltaTime;
            transform.GetChild(0).GetComponent<Image>().fillAmount = (cdTime - m_Timer) / cdTime;
            yield return 0;
        } 
    }
}
