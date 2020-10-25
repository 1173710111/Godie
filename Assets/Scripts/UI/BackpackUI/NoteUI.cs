using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

[RequireComponent(typeof(ShowAndHideUI))]
public class NoteUI : MonoBehaviour
{
    private AudioSourceController m_AudioSourceController;

    public void CloseButton(string itemName = null)
    {
        //音效
        if (m_AudioSourceController == null) m_AudioSourceController = AudioSourcesManager.ApplyAudioSourceController();
        if (itemName != "收音机") m_AudioSourceController.Play("阅读字条", transform);
        else m_AudioSourceController.Stop();

        StopCoroutine(IE_ShowButton());
        GetComponent<ShowAndHideUI>().Hide(0.5f);
        GameObject.Find("EventSystem").GetComponent<EventSystem>().enabled = true;
    }

    //显示字条。 itemName道具名，showTime淡入/淡出时间，间隔waitTime后再显示关闭按钮
    public void Show(string itemName,float showTime=0.5f,float waitTime = 2f,int count = 2)
    {
        //音效
        if (m_AudioSourceController == null) m_AudioSourceController = AudioSourcesManager.ApplyAudioSourceController();
        if (itemName == "收音机") m_AudioSourceController.Play("收音机杂音循环", transform);
        else m_AudioSourceController.Play("阅读字条", transform); 

        //禁用输入
        InputController.BanButton(true);
        InputController.BanMouse(true);
        GameObject.Find("EventSystem").GetComponent<EventSystem>().enabled = false;

        transform.Find("Canvas/Close Button").gameObject.SetActive(false);
        if(itemName!="收音机" && itemName != "收据1")transform.Find("Canvas/Note Text").GetComponent<Text>().text = GameObject.Find("ItemsData").GetComponent<ItemsData>().GetItemByItemName(itemName).introduce;
        if (itemName == "纸条" || itemName == "日记") transform.Find("Canvas/Note Text").GetComponent<Text>().fontSize = 26;
        GetComponent<ShowAndHideUI>().Show(showTime,delegate() { StartCoroutine(IE_ShowButton(1f,itemName,count)); });
    }
    IEnumerator IE_ShowButton(float waitTime = 1f,string itemName = null,int count = 2)
    { 
        yield return new WaitForSeconds(waitTime);
        transform.Find("Canvas/Close Button").GetComponent<ShowAndHideUI>().Show(0.2f);
        //解除禁用输入
        InputController.BanButton(false);
        InputController.BanMouse(false);
        GameObject.Find("EventSystem").GetComponent<EventSystem>().enabled = true;
        //使用任意键来关闭该界面
        while (true)
        {
            if (Input.anyKey)
            {
                if (itemName == "收据1") {
                    if (count == 2) transform.Find("Canvas/Note Text").GetComponent<Text>().text = "威尔特超市购物清单\n\n单号：104351\n\n商品名      数量      单价      金额\n饮用水       5        4.0       20.0\n 苹果        1       10.0       10.0\n 罐头        2       20.0       40.0\n\n-----------------------------------------\n 合计：60.0 ";
                    else if (count == 3) transform.Find("Canvas/Note Text").GetComponent<Text>().text = "威尔特超市购物清单\n\n单号：105315\n\n商品名      数量      单价      金额\n 可乐         1       20.0       20.0\n饮用水       30       10.0       300.0\n 罐头        10       50.0       500.0\n\n-----------------------------------------\n 合计：820.0";
                    else if (count == 4) transform.Find("Canvas/Note Text").GetComponent<Text>().text = "威尔特超市购物清单\n\n单号：1067299\n\n商品名      数量      单价      金额\n 苹果         1       100.0      100.0\n饮用水       10       50.0       500.0\n\n-----------------------------------------\n 合计：600.0";
                    else { CloseButton(itemName); break; }
                    count++;
                    Show("收据1",0.5f,2f,count);
                }
                else CloseButton(itemName);
                break;
            }
            yield return 0;
        }
    }
}
