using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;


public class VideoLinked : MonoBehaviour
{
    public VideoClip[] VCs;

    private VideoPlayer m_VP1,m_VP2;
    private int index = 0;
    private bool to1, to2;

    private void Start()
    {
        to1 = false;
        to2 = false;

        int count = 0;
        foreach(VideoPlayer videoPlayer in GetComponentsInChildren<VideoPlayer>())
        {
            if (count == 0) m_VP1 = videoPlayer;
            else if (count == 1) m_VP2 = videoPlayer;
            count++;
        }
        m_VP1.clip = VCs[index++];
        m_VP1.Prepare();
        m_VP1.Play();
    }

    private void Update()
    {
        if(m_VP1.frame > (long)(m_VP1.frameCount - 300) && !to2 && m_VP1.isPlaying)
        {
            to2 = true;
            to1 = false;
            m_VP2.clip = VCs[index];
            m_VP2.targetCameraAlpha = 0.4f;
            m_VP2.Prepare();
            index = (index + 1) % VCs.Length;
        }
        else if(m_VP1.frame > (long)(m_VP1.frameCount - 120) && m_VP1.isPlaying)
        {
            if (!m_VP2.isPlaying) m_VP2.Play();
            m_VP1.targetCameraAlpha -= 0.6f *Time.deltaTime / 3f;
            m_VP2.targetCameraAlpha += 0.6f *Time.deltaTime / 3f;
        }
        else if(m_VP2.frame > (long)(m_VP2.frameCount -300) && !to1 && m_VP2.isPlaying)
        {
            to1 = true;
            to2 = false;
            m_VP1.clip = VCs[index];
            m_VP1.targetCameraAlpha = 0.4f;
            m_VP1.Prepare();
            index = (index + 1) % VCs.Length;
        }
        else if(m_VP2.frame > (long)(m_VP2.frameCount - 120) && m_VP2.isPlaying)
        {
            if (!m_VP1.isPlaying) m_VP1.Play();
            m_VP2.targetCameraAlpha -= 0.6f * Time.deltaTime / 3f;
            m_VP1.targetCameraAlpha += 0.6f * Time.deltaTime / 3f;
        }
    }

}
