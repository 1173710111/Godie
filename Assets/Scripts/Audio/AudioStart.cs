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
    }
}
