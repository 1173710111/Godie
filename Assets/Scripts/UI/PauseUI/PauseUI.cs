using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseUI : MonoBehaviour
{
    bool m_IsPause = false; //是否处于暂停状态

    private void Update()
    {
        if (SceneManager.GetActiveScene().buildIndex != 0) //场景不为主菜单时
        {
            if (Input.GetKeyDown(KeyCode.Escape) && !m_IsPause) //开始暂停
            {
                m_IsPause = true;
                /*GameObject[] UIs = GameObject.FindGameObjectsWithTag("UI");
                foreach (GameObject ui in UIs)
                {
                    ui.SetActive(false);
                }*/
                transform.Find("PauseCanvas").gameObject.SetActive(true);
                transform.Find("PauseCanvas/BackgroundPanel/HelpPanel").gameObject.SetActive(false);
                transform.Find("PauseCanvas/BackgroundPanel/MusicPanel").gameObject.SetActive(false);
                transform.Find("PauseCanvas/BackgroundPanel/BasicPanel").gameObject.SetActive(true);
                ButtonOverride[] buttonOverrides = transform.GetComponentsInChildren<ButtonOverride>();
                foreach (ButtonOverride buttonOverride in buttonOverrides)
                {
                    buttonOverride.Init();
                }

                Time.timeScale = 0f;
            }
            else if (Input.GetKeyDown(KeyCode.Escape) && m_IsPause) //取消暂停
            {
                m_IsPause = false;
                transform.Find("PauseCanvas").gameObject.SetActive(false);

                Time.timeScale = 1f;
            }
        }
    }

    public void HelpButton()
    {
        Debug.Log("Help");
        transform.Find("PauseCanvas/BackgroundPanel/BasicPanel").gameObject.SetActive(false);
        transform.Find("PauseCanvas/BackgroundPanel/HelpPanel").gameObject.SetActive(true);
    }

    public void MusicButton()
    {
        Debug.Log("Music");
        transform.Find("PauseCanvas/BackgroundPanel/BasicPanel").gameObject.SetActive(false);
        transform.Find("PauseCanvas/BackgroundPanel/MusicPanel").gameObject.SetActive(true);
    }

    public void MenuButton()
    {
        if(SceneManager.GetActiveScene().buildIndex == 0) //场景为主菜单时，直接返回
        {
            m_IsPause = false;
            transform.Find("PauseCanvas").gameObject.SetActive(false);
            transform.parent.Find("MainCanvas").gameObject.SetActive(true);
        }
        else //场景不为主菜单时，加载主菜单场景
        {
            Time.timeScale = 1f;
            SceneManager.LoadScene(0);
        }

        Debug.Log("Menu");
    }

    public void ReturnButton()
    {
        transform.Find("PauseCanvas/BackgroundPanel/HelpPanel").gameObject.SetActive(false);
        transform.Find("PauseCanvas/BackgroundPanel/MusicPanel").gameObject.SetActive(false);
        transform.Find("PauseCanvas/BackgroundPanel/BasicPanel").gameObject.SetActive(true);
    }

    public void ContinueButton()
    {
        m_IsPause = false;
        transform.Find("PauseCanvas").gameObject.SetActive(false);

        Time.timeScale = 1f;
    }
}
