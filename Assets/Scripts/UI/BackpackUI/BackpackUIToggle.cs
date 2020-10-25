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
        else if (itemsData.GetItemByItemName(label).isNewPanel)
        {
            if(label == "收音机")
            {
                if (Note.parent.parent.GetComponent<BackpackUI>().HasItem("电池")) //收音机有电池
                {
                    GameObject.Find("UI/n格漫画UI").transform.GetComponent<CartoonUI>().Show(3);
                }
                else //收音机没电池
                {
                    GameObject.Find("UI/字幕UI").GetComponent<ZimuUI>().Show("没有反应......难道是没电了吗？");
                }
            }
            if(label == "零件")
            {
                /*GameObject newPanel;
                if (Note.parent.Find("零件安装UI (clone)") == null) { newPanel = Instantiate(itemsData.GetItemByItemName(label).newPanelPrefab, GameObject.Find("UI").transform); }
                else newPanel = GameObject.Find("UI/零件安装UI (clone)").gameObject;
                newPanel.GetComponent<ShowAndHideUI>().Show();*/
                GameObject.Find("UI/技能UI/Canvas/Panel/技能1").GetComponent<Skill>().GetSkill();
                GameObject.Find("UI/字幕UI").GetComponent<ZimuUI>().Show("将零件安装到轮椅上了\n轮椅可以短暂的冲刺了，也许可以跨越某些地形了...");
                Note.parent.parent.GetComponent<BackpackUI>().RemoveItem("零件");
                
            }
            if(label == "收据1")
            {
                GameObject newPanel;
                if (GameObject.Find("UI/收据(Clone)") == null) { newPanel = Instantiate(GameObject.Find("ItemsData").GetComponent<ItemsData>().GetItemByItemName("收据1").newPanelPrefab, GameObject.Find("UI").transform); }
                else newPanel = GameObject.Find("UI/收据UI(Clone)").gameObject;
                newPanel.gameObject.SetActive(true);
                GameObject.Find("UI").transform.Find("收据UI(Clone)/Note").GetComponent<NoteUI>().Show("收据1", 0.5f, 2f, 2);
            }
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
