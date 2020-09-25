using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuUI : MonoBehaviour
{
    bool m_HasSave; //是否有存档

    private void Awake()
    {
        //初始化存档
        m_HasSave = false; 
    }

    private void Start()
    {
        if (!m_HasSave)  //无存档时，不显示“继续游戏”
        {
            transform.Find("MainCanvas/MainPanel/ContinueButton").gameObject.SetActive(false);
        }
    }

    //Button开始新游戏
    public void NewStartButton()
    {
        SceneManager.LoadScene(1); //加载场景1
        Debug.Log("NewStart");
    }

    //Button继续游戏
    public void ContinueButton()
    {
        Debug.Log("Continue");
    }

    //Button设置
    public void SettingButton()
    {
        transform.Find("MainCanvas").gameObject.SetActive(false);
        transform.Find("PauseUI/PauseCanvas").gameObject.SetActive(true);
        Debug.Log("Setting");
    }

    //Button退出
    public void ExitButton()
    {
        Application.Quit();
        Debug.Log("Exit");
    }
}
