using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PauseUI : MonoBehaviour
{
    bool m_IsPause = false; //是否处于暂停状态
    AudioSourceController m_AudioSourceController;

    private void Update()
    {
        if (SceneManager.GetActiveScene().buildIndex != 0) //场景不为主菜单时
        {
            if (Input.GetKeyDown(KeyCode.Escape) && !m_IsPause) //开始暂停
            {
                //音效
                if (m_AudioSourceController == null) m_AudioSourceController = AudioSourcesManager.ApplyAudioSourceController();
                m_AudioSourceController.Play("按钮", transform);

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
                //音效
                if (m_AudioSourceController == null) m_AudioSourceController = AudioSourcesManager.ApplyAudioSourceController();
                m_AudioSourceController.Play("按钮", transform);

                m_IsPause = false;
                transform.Find("PauseCanvas").gameObject.SetActive(false);

                Time.timeScale = 1f;
            }
        }
    }

    public void HelpButton()
    {
        //音效
        if (m_AudioSourceController == null) m_AudioSourceController = AudioSourcesManager.ApplyAudioSourceController();
        m_AudioSourceController.Play("按钮", transform);

        transform.Find("PauseCanvas/BackgroundPanel/BasicPanel").gameObject.SetActive(false);
        transform.Find("PauseCanvas/BackgroundPanel/HelpPanel").gameObject.SetActive(true);
    }

    public void MusicButton()
    {
        //音效
        if (m_AudioSourceController == null) m_AudioSourceController = AudioSourcesManager.ApplyAudioSourceController();
        m_AudioSourceController.Play("按钮", transform);

        transform.Find("PauseCanvas/BackgroundPanel/BasicPanel").gameObject.SetActive(false);
        transform.Find("PauseCanvas/BackgroundPanel/MusicPanel").gameObject.SetActive(true);
    }

    public void MenuButton()
    {
        //音效
        if (m_AudioSourceController == null) m_AudioSourceController = AudioSourcesManager.ApplyAudioSourceController();
        m_AudioSourceController.Play("按钮", transform);

        if (SceneManager.GetActiveScene().buildIndex == 0) //场景为主菜单时，直接返回
        {
            m_IsPause = false;
            transform.Find("PauseCanvas").gameObject.SetActive(false);
            transform.parent.Find("MainCanvas/MainPanel").gameObject.SetActive(true);
        }
        else //场景不为主菜单时，加载主菜单场景
        {
            Time.timeScale = 1f;
            SceneManager.LoadScene(0);
        }
    }

    public void ReturnButton()
    {
        //音效
        if (m_AudioSourceController == null) m_AudioSourceController = AudioSourcesManager.ApplyAudioSourceController();
        m_AudioSourceController.Play("按钮", transform);

        transform.Find("PauseCanvas/BackgroundPanel/HelpPanel").gameObject.SetActive(false);
        transform.Find("PauseCanvas/BackgroundPanel/MusicPanel").gameObject.SetActive(false);
        transform.Find("PauseCanvas/BackgroundPanel/BasicPanel").gameObject.SetActive(true);
    }

    public void ContinueButton()
    {
        //音效
        if (m_AudioSourceController == null) m_AudioSourceController = AudioSourcesManager.ApplyAudioSourceController();
        m_AudioSourceController.Play("按钮", transform);

        m_IsPause = false;
        transform.Find("PauseCanvas").gameObject.SetActive(false);

        Time.timeScale = 1f;
    }

    public void SetBGM()
    {
        AudioDataManager.BGMVolumn = transform.Find("PauseCanvas/BackgroundPanel/MusicPanel/BGMPanel/Slider").GetComponent<Slider>().value;
        GameObject.Find("Audio/AudioSourcesManager").GetComponent<AudioSource>().volume = AudioDataManager.GetAudioDataByName("bgm1").volumn * AudioDataManager.BGMVolumn;
    }

    public void SetSound()
    {
        AudioDataManager.soundVolumn = transform.Find("PauseCanvas/BackgroundPanel/MusicPanel/SoundPanel/Slider").GetComponent<Slider>().value;
        Debug.Log("全局音效音量 = " + AudioDataManager.soundVolumn);
    }
}
