using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI.CoroutineTween;

public class BackpackUIDropdown : Dropdown
{

    protected override GameObject CreateDropdownList(GameObject template)
    {
        transform.Find("Toggle").GetComponent<Toggle>().isOn = true;
        return base.CreateDropdownList(template);
    }

    protected override void DestroyDropdownList(GameObject dropdownList)
    {
        transform.Find("Toggle").GetComponent<Toggle>().isOn = false;
        base.DestroyDropdownList(dropdownList);
    }

    protected override void Start()
    {
        for(int i = 0; i < options.Count; i++)
        {
            options[i].image = GameObject.Find("ItemsData").GetComponent<ItemsData>().GetItemByItemName(options[i].text).sprite;
        }
    }
}
