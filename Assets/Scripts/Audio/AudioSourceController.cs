using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class AudioSourceController : MonoBehaviour
{
    AudioSource audioSource;
    AudioData audioData;

    //播放audioName的音频，播放参数为AudioSourceManager中设定的默认值
    public void Play(string audioName, Transform user) 
    {
        audioSource = GetComponent<AudioSource>();
        audioData = AudioDataManager.GetAudioDataByName(audioName);
        transform.position = user.position;

        bool clipChange = false;
        if (audioSource.clip != audioData.audioClip)
        {
            audioSource.clip = audioData.audioClip;
            clipChange = true;
        }
        audioSource.volume = (audioData.playMode == "FadeIn" ? 0f : audioData.volumn) * (audioData.isSound ? AudioDataManager.soundVolumn : AudioDataManager.BGMVolumn);
        audioSource.loop = audioData.loop;
        audioSource.spatialBlend = audioData.is3D ? 1f : 0f;
        if (audioSource.loop == true && !clipChange && audioSource.isPlaying) { } //已在播放，无需再次播放
        else
        {
            audioSource.Play();
            if (audioData.playMode == "FadeIn") StartCoroutine(IE_FadeIn());
            else if (audioData.playMode == "FadeOut") StartCoroutine(IE_FadeOut());
            else if(audioData.playMode == "Normal") StartCoroutine(IE_Normal());
        }
    }

    IEnumerator IE_Normal() //用于实时适应全局音量的改变,并检测停止使用1s后释放
    {
        float gobalvolumn = audioData.isSound ? AudioDataManager.soundVolumn : AudioDataManager.BGMVolumn;
        bool flag = false;
        while (true)
        {
            if (!audioSource.isPlaying) flag = true;
            if(audioSource.volume > audioData.volumn * gobalvolumn) audioSource.volume = audioData.volumn * gobalvolumn;
            yield return new WaitForSeconds(1f);
            if (audioSource.isPlaying) flag = false;
            if(flag)
            {
                Destroy(gameObject);
                break;
            }
        }
    }

    IEnumerator IE_FadeIn()
    {
        float delta = Time.deltaTime / audioData.fadeFactor;
        float gobalvolumn = audioData.isSound ? AudioDataManager.soundVolumn : AudioDataManager.BGMVolumn;
        while (audioSource.volume < audioData.volumn * gobalvolumn)
        {
            audioSource.volume += delta;
            if (audioSource.volume >= audioData.volumn * gobalvolumn) break;
            yield return 0;
        }
        audioSource.volume = audioData.volumn * gobalvolumn;
        StartCoroutine(IE_Normal());
    }

    IEnumerator IE_FadeOut()
    {
        float delta = Time.deltaTime / audioData.fadeFactor;
        while (audioSource.volume > 0f)
        {
            audioSource.volume -= delta;
            if (audioSource.volume <= 0f) break;
            yield return 0;
        }
        audioSource.volume = 0f;
        audioSource.Stop();
    }

    //停止播放
    public void Stop()
    {
        audioSource.Stop();
    }

    //淡出音频
    public void FadeOut()
    {
        StartCoroutine(IE_FadeOut());
    }
}
