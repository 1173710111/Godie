using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class CartoonUI : MonoBehaviour
{
    [Header("漫画图集（需按顺序）")]
    [Tooltip("漫画1")] public Sprite[] cartoon1;
    [Tooltip("漫画2")] public Sprite[] cartoon2;
    [Tooltip("漫画3")] public Sprite[] cartoon3;
    [Tooltip("漫画4")] public Sprite[] cartoon4;
    [Tooltip("漫画5")] public Sprite[] cartoon5;
    [Tooltip("漫画6")] public Sprite[] cartoon6;

    private List<Sprite[]> list;
    private string m_CurCartoon; //"n格漫画"

    private AudioSourceController m_AudioSourceController;

    private void Awake()
    {
        list = new List<Sprite[]>();
        list.Add(cartoon1);
        list.Add(cartoon2);
        list.Add(cartoon3);
        list.Add(cartoon4);
        list.Add(cartoon5);
        list.Add(cartoon6);
    }

    //显示对应id的漫画,intervalTime每张漫画显示的间隔时间，showTime单张漫画渐入的时长
    public void Show(int id, float intervalTime = 3f, float showTime = 0.5f)
    {
        //初始化
        transform.Find("Canvas/Panel/4格漫画").gameObject.SetActive(false);
        transform.Find("Canvas/Panel/4格漫画/Image1").gameObject.SetActive(false);
        transform.Find("Canvas/Panel/4格漫画/Image2").gameObject.SetActive(false);
        transform.Find("Canvas/Panel/4格漫画/Image3").gameObject.SetActive(false);
        transform.Find("Canvas/Panel/4格漫画/Image4").gameObject.SetActive(false);
        transform.Find("Canvas/Panel/4格漫画/Arrow").gameObject.SetActive(false);
        transform.Find("Canvas/Panel/3格漫画").gameObject.SetActive(false);
        transform.Find("Canvas/Panel/3格漫画/Image1").gameObject.SetActive(false);
        transform.Find("Canvas/Panel/3格漫画/Image2").gameObject.SetActive(false);
        transform.Find("Canvas/Panel/3格漫画/Image3").gameObject.SetActive(false);
        transform.Find("Canvas/Panel/3格漫画/Arrow").gameObject.SetActive(false);
        transform.Find("Canvas").GetComponent<ShowAndHideUI>().Show(0.5f);

        //。。。。。。禁用玩家输入的函数放这里
        InputController.BanButton(true);
        InputController.BanMouse(true);
        GameObject.Find("EventSystem").GetComponent<EventSystem>().enabled = false;

        Sprite[] sprites = list[id-1];
        if (sprites.Length == 4) m_CurCartoon = "4格漫画";
        else if (sprites.Length == 3) m_CurCartoon = "3格漫画";
        else return;
        transform.Find("Canvas/Panel/" + m_CurCartoon).gameObject.SetActive(true);
        for (int i = 1; i <= sprites.Length; i++)
        {
            transform.Find("Canvas/Panel/" + m_CurCartoon + "/Image" + i).GetComponent<Image>().sprite = sprites[i-1];
        }
        StartCoroutine(IE_Show(id,sprites.Length, intervalTime, showTime));
    }
    IEnumerator IE_Show(int id,int n, float intervalTime, float showTime )
    {
        yield return new WaitForSeconds(1f);
        for (int i = 1; i <= n; i++)
        {
            //音效
            if (m_AudioSourceController == null) m_AudioSourceController = AudioSourcesManager.ApplyAudioSourceController();
            if(id==2&&i==1)m_AudioSourceController.Play("四格漫画2-1", transform);
            else if (id == 2 && i == 2) m_AudioSourceController.Play("修理轮椅", transform);
            else if (id == 2 && i == 3) m_AudioSourceController.Play("四格漫画2-3", transform);
            else if (id == 2 && i == 4) m_AudioSourceController.Play("四格漫画2-4", transform);
            else if (id == 1 && i == 1) m_AudioSourceController.Play("开罐头", transform);
            else if(id == 1 && i==2) m_AudioSourceController.Play("四格漫画1-2", transform);
            else if (id == 1 && i == 3) m_AudioSourceController.Play("四格漫画1-3", transform);
            else if (id == 1 && i == 4) m_AudioSourceController.Play("四格漫画1-4", transform);
            else if (id == 3 && i == 2) m_AudioSourceController.Play("收音机杂音", transform);
            else if (id == 3 && i == 3) m_AudioSourceController.Play("修理轮椅", transform);
            else if (id == 3 && i == 4) m_AudioSourceController.Play("四格漫画2-3", transform);
            else if(id==4 && i==1) m_AudioSourceController.Play("倒水", transform);
            else if (id == 4 && i == 2) m_AudioSourceController.Play("四格漫画2-3", transform);
            else if (id == 4 && i == 3) m_AudioSourceController.Play("花盆倒下", transform);
            else if (id == 5 && i == 1) m_AudioSourceController.Play("火花", transform);
            else if (id == 5 && i == 2) m_AudioSourceController.Play("爆炸", transform);
            else if (id == 5 && i == 3) m_AudioSourceController.Play("风", transform);
            else if (id == 6 && i == 1) m_AudioSourceController.Play("轮椅移动", transform);
            else if (id == 6 && i == 2) m_AudioSourceController.Play("人倒下", transform);
            else if (id == 6 && i == 3) m_AudioSourceController.Play("跑步", transform);

            transform.Find("Canvas/Panel/" + m_CurCartoon + "/Image" + i).GetComponent<ShowAndHideUI>().Show();
            yield return new WaitForSeconds(intervalTime);
        }
        transform.Find("Canvas/Panel/" + m_CurCartoon + "/Arrow").GetComponent<ShowAndHideUI>().Show();
        GameObject.Find("EventSystem").GetComponent<EventSystem>().enabled = true;
        //以下是解除禁用输入后，玩家点击任意键，使得漫画UI淡出
        while (true)
        {
            if (Input.anyKey)
            {
                //。。。。。。解除禁用玩家输入的函数放这里
                InputController.BanButton(false);
                InputController.BanMouse(false);
                Hide(id);
                break;
            }
            yield return 0;
        }
    }


    //淡出漫画UI，hideTime淡出时长
    public void Hide(int id,float hideTime = 0.5f)
    {
        transform.Find("Canvas").GetComponent<ShowAndHideUI>().Hide(hideTime,delegate() {
            if (id == 3)
            {
                GameObject newPanel;
                if (GameObject.Find("UI/收音机播放UI(Clone)") == null) { newPanel = Instantiate(GameObject.Find("ItemsData").GetComponent<ItemsData>().GetItemByItemName("收音机").newPanelPrefab, GameObject.Find("UI").transform); }
                else newPanel = GameObject.Find("UI/收音机播放UI(Clone)").gameObject;
                newPanel.gameObject.SetActive(true);
                GameObject.Find("UI").transform.Find("收音机播放UI(Clone)/Note").GetComponent<NoteUI>().Show("收音机");
            }
            if (id == 4) { /*这里加获得日记的逻辑*/ }
            if (id == 5) Show(6);
            if (id == 6)
            {
                EndUI.Play();
            }
        });
    }

}