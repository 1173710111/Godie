using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public static class EndUI 
{
    static public float[] intervalTimes = { 4f,4f,7f,4f};
    static public string[] texts = {
        "一人一狗，在战争中艰难生存",
        "但，他们并不孤独",
        "感谢游玩"
    };
    

    static public void Play(float showTime = 3f,float intervalTime = 2f)
    {
        //禁用输入
        InputController.BanButton(true);
        InputController.BanMouse(true);
        GameObject.Find("EventSystem").GetComponent<EventSystem>().enabled = false;

        GameObject prefab = GameObject.Instantiate(Resources.Load<GameObject>("EndUI"),GameObject.Find("UI").transform);
        prefab.transform.Find("BackgroundPanel/Text").GetComponent<Text>().text = texts[0];
        prefab.transform.Find("BackgroundPanel").GetComponent<ShowAndHideUI>().Show(showTime,delegate()
        {
            GameObject.Find("Audio").GetComponentInChildren<AudioStart>().StartCoroutine(IE_ChangeContent(prefab, intervalTime));
        });

    }
    static IEnumerator IE_ChangeContent(GameObject prefab,float intervalTime)
    {
        int i;
        for(i = 0; i < texts.Length-1; i++)
        {
            yield return new WaitForSeconds(intervalTimes[i]);
            Text text = prefab.transform.Find("BackgroundPanel/Text").GetComponent<Text>();
            text.text = texts[i+1];
            if (i + 1 == 2)
            {
                text.fontSize = text.resizeTextMaxSize = 70;
                text.GetComponent<TextSpacing>().spacing_x = 20;
            }
            text.GetComponent<ShowAndHideUI>().Show();
        }
        yield return new WaitForSeconds(intervalTimes[i]);

        //解除禁用输入
        InputController.BanButton(true);
        InputController.BanMouse(true);

        SceneManager.LoadScene(0);
    }
}
