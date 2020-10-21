using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

[RequireComponent(typeof(Light2D))]
public class FlickerLight : MonoBehaviour
{
    [Header("功能：让Light2D自动闪烁")]

    [Tooltip("最小闪烁间隔时间")] public float minIntervalTime = 0f;
    [Tooltip("最大闪烁间隔时间")] public float maxIntervalTime = 0f;
    [Tooltip("最小闪烁持续时间")] public float minHoldTime = 1f;
    [Tooltip("最大闪烁持续时间")] public float maxHoldTime = 1f;
    [Tooltip("最小闪烁亮度")] public float minIntensity = 1f;
    [Tooltip("最大闪烁亮度")] public float maxIntensity =1f;

    private Light2D m_light2D;
    private float m_IntervalTime;
    private float m_HoldTime;
    private float m_Intensity;
    private float m_Timer;

    private void Start()
    {
        m_light2D = GetComponent<Light2D>();
        m_HoldTime = Random.Range(minHoldTime, maxHoldTime);
        m_Intensity = Random.Range(minIntensity, maxIntensity);
        m_IntervalTime = Random.Range(minIntervalTime, maxIntervalTime);
        m_Timer = 0f;

        m_light2D.intensity = m_Intensity;
    }

    private void Update()
    {
        m_Timer += Time.deltaTime;
        if (m_Timer > m_HoldTime && m_Timer < m_HoldTime + m_IntervalTime)
        {
            m_light2D.intensity = 0;
        }
        else if (m_Timer >= m_IntervalTime + m_HoldTime)
        {
            m_Timer = 0f;
            m_HoldTime = Random.Range(minHoldTime, maxHoldTime);
            m_Intensity = Random.Range(minIntensity, maxIntensity);
            m_IntervalTime = Random.Range(minIntervalTime, maxIntervalTime);
            m_light2D.intensity = m_Intensity;
        }
    }
}
