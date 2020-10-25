using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ItemsData : MonoBehaviour
{
    public Sprite GuanTou;
    public Sprite PingZhuangShui;
    public Sprite ShouJu1;
    public Sprite ZhaDan;
    public Sprite LingJian;
    public Sprite ZhiTiao;
    public Sprite Seed;
    public Sprite Diary;
    public Sprite ShouYinJi;
    public Sprite DianChi;

    [NonSerialized] public List<Item> items;

    void Awake()
    {
        items = new List<Item>();
        //items.Add(new Item("None", "", null,));
        items.Add(new Item("罐头", "一盒罐头。除了自己吃，也可以喂狗吧？", GuanTou));
        items.Add(new Item("瓶装水", "一瓶战争之前随处可见的饮用水。当然，它除了饮用之外还有很多的用途，比如注满水坑，充当导体，浇铸希望。", PingZhuangShui));
        items.Add(new Item("收据1", "一叠收据。翻开看看吧。", ShouJu1, false,true,Resources.Load<GameObject>("收据UI")));
        items.Add(new Item("炸弹", "一个特种炸弹。是苦味酸、TNT还是黑索金？其实没人在乎它是什么，只要在需要炸开点什么东西的时候不哑火就行。哦!点燃它还需要通电才行。", ZhaDan));
        items.Add(new Item("零件", "看上去像是轮椅的零件...", LingJian,false,true, Resources.Load<GameObject>("零件安装UI")));
        items.Add(new Item("纸条", "动乱，反叛，警报。\n火药，炸弹，枪炮。\n哭闹，嘶吼，嚎叫。\n轰鸣，炸响，雷暴。\n鲜血，白骨，撕咬。\n啊，战争，战争，战争。\n那自诩正义的恶魔发动了战争，\n然后战争把人们都拉入了地狱。\n\n署名：\n    在地狱中挣扎的可怜人", ZhiTiao,true));
        items.Add(new Item("种子", "如果能从一颗种子上看到参天大树，那你还有什么理由不去栽培和浇灌它呢？只需要花盆和水，以及一点点耐心就够了。", Seed));
        items.Add(new Item("日记", "起初，硝烟还很远，\n没有人把战争当回事，\n它只是我抬高物价的借口。\n后来，当枪炮声在周遭响起，\n那颗我最爱的橡树\n    被炸成了半截；\n那只常来偷罐头的猫\n    被压成了肉泥；\n那个来店里帮工的少年\n   满身鲜血的倒在路旁。\n我终于意识到了，\n    原来，这，就是战争。", Diary,true));
        items.Add(new Item("收音机", "收音机里会有什么消息吗？", ShouYinJi, false, true, Resources.Load<GameObject>("收音机播放UI")));
        items.Add(new Item("电池", "这是一节电池。\n也许，可以放到收音机里？", DianChi));
    }

    public Sprite GetSpriteByItemName(string name)
    {
        foreach (Item item in items)
        {
            if (String.Compare(name,item.name) == 0) return item.sprite;
        }
        Debug.LogError("No Found: Item's sprite by name");
        return null;
    }

    public Item GetItemByItemName(string name)
    {
        foreach (Item item in items)
        {
            if (String.Compare(name, item.name) == 0) return item;
        }
        Debug.LogError("No Found: Item sprite by name");
        return null;
    }
}

public class Item
{
    private string _name; //名字
    private string _introduce; //简介内容
    private Sprite _sprite; //图片
    private bool _isNote; //是否字条
    private bool _isNewPanel; //是否打开新的面板
    private GameObject _newPanelPrefab; //新面板的预制体

    public Item(string name, string introduce, Sprite sprite, bool isNote = false, bool isNewPanel = false, GameObject newPanelPrefab = null)
    {
        this._name = name;
        this._sprite = sprite;
        this._introduce = introduce;
        this._isNote = isNote;
        this._isNewPanel = isNewPanel;
        this._newPanelPrefab = newPanelPrefab;
    }

    public string name
    {
        get { return _name; }
    }

    public Sprite sprite
    {
        get { return _sprite; }
    }

    public string introduce
    {
        get { return _introduce; }
    }

    public bool isNote
    {
        get { return _isNote; }
    }

    public bool isNewPanel
    {
        get { return _isNewPanel; }
    }

    public GameObject newPanelPrefab
    {
        get { return _newPanelPrefab; }
    }
}