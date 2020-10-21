using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reminding : MonoBehaviour
{
    public GameObject arrow_prefab;
    public GameObject getOrLose_prefab;
    public GameObject button_prefab;
    public Transform arrow_position;
    public Transform button_position;

    [Tooltip("交互的类型，0表示捡起纸条，1表示捡起罐头，3表示摸狗头，4表示清理轮椅，5表示关闭电源开关")]
    public int interaction_type;
    [Tooltip("捡起的物品的图片")]
    public Sprite get_object;
    

    private GameObject arrow;
    private GameObject button;
    private bool is_inBounds;
    private GameObject player;
    private GameObject getOrLose;
    private GetOrLostItem getOrLostItem;
    private ItemsData itemsData;

    private void Awake()
    {
        getOrLose = Instantiate(getOrLose_prefab, transform);
        getOrLostItem = getOrLose.GetComponent<GetOrLostItem>();
        itemsData = GameObject.Find("ItemsData").GetComponent<ItemsData>();

    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (gameObject.transform.GetChild(2).gameObject.activeInHierarchy)
            {
                arrow = Instantiate(arrow_prefab, arrow_position);
                button = Instantiate(button_prefab, button_position);
            }
            player = collision.gameObject;
            is_inBounds = true;
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Destroy(arrow);
            Destroy(button);
            player = null;
            is_inBounds = false;
        }
    }

    private void Update()
    {
        if (!gameObject.transform.GetChild(2).gameObject.activeInHierarchy)
        {
            
            return;
        }
        if (is_inBounds && player != null)
        {
            
            if (player.GetComponent<PlayerActions>().GetInteraction())
            {
                
                switch (interaction_type)
                {
                    case 0:
                        break;
                    case 1:
                        GetSomething("罐头");
                        break;
                    case 3:
                        break;
                    case 4:
                        GameObject.Find("零件堆").GetComponent<Animator>().SetTrigger("Disappear");
                        itemsData.items.Add(new Item("轮椅","", get_object));
                        GetSomething("轮椅");
                        transform.parent .Find("SwitchTrigger").gameObject.SetActive(true);
                        break;
                    case 5:
                        GameObject.Find("漏电的电线").GetComponent<Animator>().SetTrigger("IsCut");
                        break;
                    default:
                        break;
                }
            }
        }
    }

    private void GetSomething(string name)
    {
        
        getOrLostItem.character = player.transform;
        getOrLostItem.xOffset = 0f;
        getOrLostItem.yOffset = 2.6f;
        gameObject.transform.GetChild(2).gameObject.SetActive(false);
        Destroy(arrow);
        Destroy(button);
        getOrLostItem.GetShow(name, 0.5f, 1f, 1f, 1f);
    }
}
