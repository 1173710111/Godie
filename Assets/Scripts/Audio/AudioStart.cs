using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioStart : MonoBehaviour
{
    void Start()
    {
        TransitionUI.FadeOut(3f);
        AudioDataManager.Init();
        AudioSourcesManager.Init();
        GameObject.Find("AudioSourcesManager").GetComponent<AudioSource>().volume = AudioDataManager.GetAudioDataByName("bgm1").volumn * AudioDataManager.BGMVolumn;
    }
}
