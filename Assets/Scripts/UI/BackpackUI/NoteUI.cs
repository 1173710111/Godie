using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

[RequireComponent(typeof(ShowAndHideUI))]
public class NoteUI : MonoBehaviour
{
    private AudioSourceController m_AudioSourceController;

    public void CloseButton()
    {
        //音效
        if (m_AudioSourceController == null) m_AudioSourceController = AudioSourcesManager.ApplyAudioSourceController();
        m_AudioSourceController.Play("阅读字条",transform);

        StopCoroutine(IE_ShowButton());
        GetComponent<ShowAndHideUI>().Hide(0.5f);
        GameObject.Find("EventSystem").GetComponent<EventSystem>().enabled = true;
    }

    //显示字条。 itemName道具名，showTime淡入/淡出时间，间隔waitTime后再显示关闭按钮
    public void Show(string itemName,float showTime=0.5f,float waitTime = 2f)
    {
        //音效
        if (m_AudioSourceController == null) m_AudioSourceController = AudioSourcesManager.ApplyAudioSourceController();
        m_AudioSourceController.Play("阅读字条", transform);

        //禁用输入
        InputController.BanButton(true);
        InputController.BanMouse(true);
        GameObject.Find("EventSystem").GetComponent<EventSystem>().enabled = false;

        transform.Find("Canvas/Close Button").gameObject.SetActive(false);
        transform.Find("Canvas/Note Text").GetComponent<Text>().text = GameObject.Find("ItemsData").GetComponent<ItemsData>().GetItemByItemName(itemName).introduce;
        GetComponent<ShowAndHideUI>().Show(showTime,delegate() { StartCoroutine(IE_ShowButton()); });
    }
    IEnumerator IE_ShowButton(float waitTime = 1f)
    { 
        yield return new WaitForSeconds(waitTime);
        transform.Find("Canvas/Close Button").GetComponent<ShowAndHideUI>().Show(0.2f);
        //解除禁用输入
        InputController.BanButton(false);
        InputController.BanMouse(false);
        GameObject.Find("EventSystem").GetComponent<EventSystem>().enabled = true;
        //使用回车来关闭该界面
        while (true)
        {
            if (Input.anyKey)
            {
                CloseButton();
                break;
            }
            yield return 0;
        }
    }
}
