//音频播放源的管理器（动态申请音频播放源）
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class AudioSourcesManager
{
    public static Transform audioSourcesManager;
    public static GameObject audioSourceContollerPrefab;
    public static List<AudioSourceController> audioSourceControllers;

    public static void Init()
    {
        audioSourcesManager = GameObject.Find("AudioSourcesManager").transform;
        audioSourceContollerPrefab = Resources.Load("AudioSourceController") as GameObject;
        audioSourceControllers = new List<AudioSourceController>();
    }

    //申请音频播放源
    public static AudioSourceController ApplyAudioSourceController()
    {
        AudioSourceController result = AddAudioSourceController();
        return result;
    }

    //实例化新的AudioSourceController
    private static AudioSourceController AddAudioSourceController()
    {
        GameObject newASC = GameObject.Instantiate(audioSourceContollerPrefab,audioSourcesManager); 
        audioSourceControllers.Add(newASC.GetComponent<AudioSourceController>());
        return newASC.GetComponent<AudioSourceController>();
    }
}
