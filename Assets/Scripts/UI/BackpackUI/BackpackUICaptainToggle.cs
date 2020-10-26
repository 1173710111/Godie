using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Toggle))]
public class BackpackUICaptainToggle : MonoBehaviour
{
    [Tooltip("Toggle开启时")]public Sprite on;
    [Tooltip("Toggle关闭时")] public Sprite off;

    AudioSourceController audioSourceController;

    private void Start()
    {
        transform.GetComponent<Toggle>().targetGraphic.GetComponent<Image>().sprite = off;
        transform.GetComponent<Toggle>().targetGraphic.GetComponent<Image>().color = new Color(0.5f, 0.5f, 0.5f, 1);
    }

    public void BackpackToggle()
    {
        if (transform.GetComponent<Toggle>().isOn)
        {
            transform.GetComponent<Toggle>().targetGraphic.GetComponent<Image>().sprite = on;
            transform.GetComponent<Toggle>().targetGraphic.GetComponent<Image>().color = new Color(1, 1, 1, 1);
        }
        else
        {
            transform.GetComponent<Toggle>().targetGraphic.GetComponent<Image>().sprite = off;
            transform.GetComponent<Toggle>().targetGraphic.GetComponent<Image>().color = new Color(0.5f, 0.5f, 0.5f, 1);
        }

        if(audioSourceController==null) audioSourceController = AudioSourcesManager.ApplyAudioSourceController();
        audioSourceController.Play("按钮",transform);
    }
}
