using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class AudioData
{
    public string audioName; //音频名
    public AudioClip audioClip; //音频源
    public float volumn; //音量
    public bool isSound; //是否音效，否则为背景音
    public bool loop; //是否循环
    public bool is3D; //是否3D
    public float speed; //播放速度
    public string playMode; //播放模式Normal、FadeIn(淡入)、FadeOut(淡出)
    public float fadeFactor; //淡入/淡出的系数(x秒后为0)

    public AudioData(string audioName, AudioClip audioClip, float volumn, bool isSound, bool loop, bool is3D,float speed,string playMode, float fadeFactor = 0f)
    {
        this.audioName = audioName;
        this.audioClip = audioClip;
        this.volumn = volumn;
        this.isSound = isSound;
        this.loop = loop;
        this.is3D = is3D;
        this.playMode = playMode;
        this.fadeFactor = fadeFactor;
    }
}

public static class AudioDataManager
{
    public static float BGMVolumn = 0.5f;
    public static float soundVolumn = 0.5f; //全局音效音量

    public static List<AudioData> audioDatas;

    public static GameObject audioSourceManagerPrefab;
    public static List<AudioSourceController> audioSourceControllers;

    public static void Init()
    {
        //读取已有的存档（音量相关）

        audioDatas = new List<AudioData>();
        audioDatas.Add(new AudioData("按钮", Resources.Load<AudioClip>("music/1001"),0.8f,true,false,false,1f,"Normal"));
        audioDatas.Add(new AudioData("冲刺", Resources.Load<AudioClip>("music/1002"), 1.3f, true, false, false,1f, "Normal"));
        audioDatas.Add(new AudioData("电流", Resources.Load<AudioClip>("music/1003"), 2f, true, true, true,1f, "Normal"));
        audioDatas.Add(new AudioData("阅读字条", Resources.Load<AudioClip>("music/1005阅读纸条"), 3.5f, true, false, false,1f, "Normal"));
        audioDatas.Add(new AudioData("爆炸", Resources.Load<AudioClip>("music/1006"), 2f, true, false, false,1f, "Normal"));
        audioDatas.Add(new AudioData("开罐头", Resources.Load<AudioClip>("music/1007"), 2.5f, true, false, false,1f, "Normal"));
        audioDatas.Add(new AudioData("开关", Resources.Load<AudioClip>("music/1008"), 2.5f, true, false, false,1f, "Normal"));
        audioDatas.Add(new AudioData("抚摸", Resources.Load<AudioClip>("music/1009"), 5f, true, false, false,1f, "Normal"));
        audioDatas.Add(new AudioData("人走路", Resources.Load<AudioClip>("music/1010"), 2f, true, true, false, 1f, "Normal"));
        audioDatas.Add(new AudioData("轮椅移动", Resources.Load<AudioClip>("music/1011"), 1.5f, true, true, false, 1f, "Normal"));
        audioDatas.Add(new AudioData("修理轮椅", Resources.Load<AudioClip>("music/1012"), 1.8f, true, false, false,1f, "FadeOut", 5f));
        audioDatas.Add(new AudioData("倒水", Resources.Load<AudioClip>("music/1013"), 2f, true, false, false, 1f, "FadeOut",3f));
        audioDatas.Add(new AudioData("收音机杂音", Resources.Load<AudioClip>("music/1014"), 2f, true, false, false, 1f, "Normal"));
        audioDatas.Add(new AudioData("收音机杂音循环", Resources.Load<AudioClip>("music/1014"), 2f, true, true, false, 1f, "Normal"));
        audioDatas.Add(new AudioData("四格漫画1-2", Resources.Load<AudioClip>("music/四格漫画1-2"), 2.5f, true, false, false, 1f, "Normal"));
        audioDatas.Add(new AudioData("四格漫画1-3", Resources.Load<AudioClip>("music/四格漫画1-3"), 1.8f, true, false, false, 1f, "Normal"));
        audioDatas.Add(new AudioData("四格漫画1-4", Resources.Load<AudioClip>("music/四格漫画1-4 new"), 2.5f, true, false, false, 1f, "FadeOut",5f));
        audioDatas.Add(new AudioData("四格漫画2-1", Resources.Load<AudioClip>("music/四格漫画2-1"), 1.5f, true, false, false, 1f, "FadeOut", 4f));
        audioDatas.Add(new AudioData("四格漫画2-3", Resources.Load<AudioClip>("music/四格漫画2-3"), 2f, true, false, false, 1f, "Normal"));
        audioDatas.Add(new AudioData("四格漫画2-4", Resources.Load<AudioClip>("music/四格漫画2-4"), 2f, true, false, false, 1f, "FadeOut", 4f));
        audioDatas.Add(new AudioData("气泡提示", Resources.Load<AudioClip>("music/提示音效"), 2f, true, false, false, 1f, "FadeOut", 1f));
        audioDatas.Add(new AudioData("按钮悬浮", Resources.Load<AudioClip>("music/界面字体变大"), 0.6f, true, false, false, 1f, "Normal"));
        audioDatas.Add(new AudioData("获得道具", Resources.Load<AudioClip>("music/获得物品"), 1f, true, false, false, 1f, "Normal"));
        audioDatas.Add(new AudioData("开关门", Resources.Load<AudioClip>("music/铁丝网门"), 1.5f, true, false, false, 1f, "Normal"));
        audioDatas.Add(new AudioData("咳嗽", Resources.Load<AudioClip>("music/咳嗽音效"), 2.3f, true, false, false, 1f, "Normal"));
        audioDatas.Add(new AudioData("bgm1", Resources.Load<AudioClip>("music/bgm1"), 2f, false, true, false, 1f, "Normal"));
        audioDatas.Add(new AudioData("火花", Resources.Load<AudioClip>("music/提取音频-火花"), 2f, true, false, false, 1f, "Normal"));
        audioDatas.Add(new AudioData("风", Resources.Load<AudioClip>("music/提取音频-风"), 1.5f, true, false, false, 1f, "Normal"));
        audioDatas.Add(new AudioData("人倒下", Resources.Load<AudioClip>("music/提取音频-倒下"), 2f, true, false, false, 1f, "Normal"));
        audioDatas.Add(new AudioData("跑步", Resources.Load<AudioClip>("music/跑步"), 1.2f, true, false, false, 1f, "Normal"));
        audioDatas.Add(new AudioData("碎石", Resources.Load<AudioClip>("music/碎石"), 2f, true, false, false, 1f, "Normal"));
        audioDatas.Add(new AudioData("花盆倒下", Resources.Load<AudioClip>("music/提取音频-倒下"), 1.5f, true, false, false, 1f, "FadeOut",1f));


        audioSourceControllers = new List<AudioSourceController>();
        
    }

    public static AudioData GetAudioDataByName(string audioName)
    {
        foreach(AudioData audioData in audioDatas)
        {
            if (audioData.audioName == audioName) return audioData;
        }
        return null;
    }
}
