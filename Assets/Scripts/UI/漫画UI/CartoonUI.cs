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

    private List<Sprite[]> list;
    private string m_CurCartoon; //"n格漫画"

    private AudioSourceController m_AudioSourceController;

    private void Awake()
    {
        list = new List<Sprite[]>();
        list.Add(cartoon1);
        list.Add(cartoon2);
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
    IEnumerator IE_Show(int id,int n, float intervalTime, float showTime)
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
            
            transform.Find("Canvas/Panel/" + m_CurCartoon + "/Image" + i).GetComponent<ShowAndHideUI>().Show();
            yield return new WaitForSeconds(intervalTime);
        }
        transform.Find("Canvas/Panel/" + m_CurCartoon + "/Arrow").GetComponent<ShowAndHideUI>().Show();
        //。。。。。。解除禁用玩家输入的函数放这里
        InputController.BanButton(false);
        InputController.BanMouse(false);
        GameObject.Find("EventSystem").GetComponent<EventSystem>().enabled = true;
        //以下是解除禁用输入后，玩家点击任意键，使得漫画UI淡出
        while (true)
        {
            if (Input.anyKey)
            {
                Hide();
                break;
            }
            yield return 0;
        }
    }


    //淡出漫画UI，hideTime淡出时长
    public void Hide(float hideTime = 0.5f)
    {
        transform.Find("Canvas").GetComponent<ShowAndHideUI>().Hide(hideTime);
    }

}