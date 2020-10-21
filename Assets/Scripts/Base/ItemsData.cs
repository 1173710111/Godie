using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ItemsData : MonoBehaviour
{
    public Sprite GuanTou;
    public Sprite PingZhuangShui;
    public Sprite ShouJu1;

    [NonSerialized] public List<Item> items;

    void Awake()
    {
        items = new List<Item>();
        items.Add(new Item("罐头","罐头",GuanTou));
        items.Add(new Item("瓶装水", "瓶装水", PingZhuangShui));
        items.Add(new Item("收据1", "收据1", ShouJu1));
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
    private string _name;
    private string _introduce;
    private Sprite _sprite;
    private bool _isNote;

    public Item(string name,string introduce,Sprite sprite,bool isNote = false)
    {
        this._name = name;
        this._sprite = sprite;
        this._introduce = introduce;
        this._isNote = isNote;
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
}