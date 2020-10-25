using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BackpackUI : MonoBehaviour
{
    private void Start()
    {

    }

    void Update()
    {
        if (Time.timeScale != 0 && Input.GetKeyUp(KeyCode.I))
        {
            bool flag = transform.Find("Canvas/Dropdown/Toggle").GetComponent<Toggle>().isOn;
            transform.Find("Canvas/Dropdown/Toggle").GetComponent<Toggle>().isOn = !flag;
            if (flag) transform.Find("Canvas/Dropdown").GetComponent<BackpackUIDropdown>().Hide();
            else transform.Find("Canvas/Dropdown").GetComponent<BackpackUIDropdown>().Show();
        }
    }

    //添加道具到背包：itemName道具名(对应ItemsData类中的道具名)
    public void AddItem(string itemName)
    {
        Sprite sprite = GameObject.Find("ItemsData").GetComponent<ItemsData>().GetItemByItemName(itemName).sprite;
        transform.Find("Canvas/Dropdown").GetComponent<BackpackUIDropdown>().options.Add(new Dropdown.OptionData(itemName,sprite));
    }

    //删除背包中的道具：itemName道具名(对应ItemsData类中的道具名)
    public void RemoveItem(string itemName)
    {
        foreach (var k in transform.Find("Canvas/Dropdown").GetComponent<BackpackUIDropdown>().options)
        {
            if(k.text==itemName)
            {
                transform.Find("Canvas/Dropdown").GetComponent<BackpackUIDropdown>().options.Remove(k);
                break;
            }
        }
    }

    //查找背包中是否有某道具：itemName道具名(对应ItemsData类中的道具名)
    public bool HasItem(string itemName)
    {
        foreach (var k in transform.Find("Canvas/Dropdown").GetComponent<BackpackUIDropdown>().options)
        {
            if (k.text == itemName)
            {
                return true;
            }
        }
        return false;
    }
}
