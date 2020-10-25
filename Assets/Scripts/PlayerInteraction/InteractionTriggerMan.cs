using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionTriggerMan : MonoBehaviour
{
    public GameObject remind_prefab;
    public GameObject getOrLose_prefab;
    public Transform remind_position;

    [Tooltip("交互的类型，0表示捡起纸条，1表示捡起罐头，2表示摸狗头，3表示清理轮椅，4表示关闭电源开关，5表示触发警示，" +
        "6表示捡起收据1，7表示捡起日记，8表示花盆交互，9表示捡瓶装水，10表示推箱子，11表示拿收音机，12表示拿到电池，" +
        "13表示捡到零件")]
    public int interaction_type;
    [Tooltip("捡起的物品，提供图片")]
    public GameObject get_object;

    private GameObject remind;
    private bool is_inBounds;
    private GameObject player;
    private GameObject getOrLose;
    private GetOrLostItem getOrLostItem;
    private ItemsData itemsData;
    private ZimuUI zimu;

    private void Awake()
    {
        
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            player = collision.gameObject;
            is_inBounds = true;
            if (gameObject.transform.GetChild(1).gameObject.activeInHierarchy)
            {
                remind = Instantiate(remind_prefab, remind_position);
                if (getOrLose_prefab != null)
                {
                    getOrLose = Instantiate(getOrLose_prefab);
                    getOrLostItem = getOrLose.GetComponent<GetOrLostItem>();
                }
                if (GameObject.Find("ItemsData") != null)
                {
                    itemsData = GameObject.Find("ItemsData").GetComponent<ItemsData>();
                }
                zimu = GameObject.Find("UI").transform.Find("字幕UI").GetComponent<ZimuUI>();
            }
            if (interaction_type == 5)
            {
                remind = Instantiate(remind_prefab);
                remind.transform.parent = player.transform;
                remind.transform.localPosition = new Vector3(0f, 3f, 0f);
                remind.transform.localScale = new Vector3(0.4f,0.4f,0.4f);
                remind.GetComponent<ShowAndHide>().Show(1f);
                ZimuUI zimu = GameObject.Find("UI").transform.Find("字幕UI").GetComponent<ZimuUI>();
                zimu.Show("太危险了！可是必须要过去。");
                return;
            }
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (interaction_type == 5)
            {
                remind.GetComponent<ShowAndHide>().Hide(1f);
                return;
            }
            Destroy(remind);
            Destroy(getOrLose);
            player = null;
            is_inBounds = false;
        }
    }

    private void Update()
    {
        if (!gameObject.transform.GetChild(1).gameObject.activeInHierarchy)
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
                        #region 纸条
                        GetSomething("纸条");
                        StartCoroutine(DelayToInvoke.DelayToInvokeDo(() =>
                        {
                            GameObject.Find("BackpackUI/Canvas/Note").transform.GetComponent<NoteUI>().Show("纸条", 5f);
                            GameObject.Find("BackpackUI").GetComponent<BackpackUI>().AddItem("纸条");
                        }, 2f));
                        #endregion
                        break;
                    case 1:
                        #region 罐头
                        GetSomething("罐头");
                        GameObject.Find("n格漫画UI").transform.GetComponent<CartoonUI>().Show(1);
                        #endregion
                        break;
                    case 2:
                        break;
                    case 3:
                        #region 轮椅
                        GameObject.Find("零件堆").GetComponent<Animator>().SetTrigger("Disappear");
                        itemsData.items.Add(new Item("轮椅","", get_object.GetComponent<SpriteRenderer>().sprite));
                        GetSomething("轮椅");
                        CameraAndCharacterController cameraController = GameObject.Find("CameraAndCharacterController").GetComponent<CameraAndCharacterController>();
                        cameraController.character_man.GetComponent<ShowAndHide>().Hide(2f);
                        StartCoroutine(DelayToInvoke.DelayToInvokeDo(() =>
                        {
                            ShowChairMan();
                        }, 2f));
                        transform.parent.Find("SwitchTrigger").gameObject.SetActive(true);
                        #endregion
                        break;
                    case 4:
                        #region 电线开关
                        InputController.BanButton(true);
                        InputController.BanMouse(true);
                        GameObject.Find("TrackCameraController").GetComponent<TrackCameraController>().StartMove();
                        transform.GetChild(1).gameObject.SetActive(false);
                        StartCoroutine(DelayToInvoke.DelayToInvokeDo(() =>
                        {
                            GameObject.Find("漏电的电线").GetComponent<Animator>().SetTrigger("IsCut");
                            GameObject.Find("ElectricLine").SetActive(false);
                            Destroy(remind);
                        }, 4f));
                        StartCoroutine(DelayToInvoke.DelayToInvokeDo(() =>
                        {
                            GameObject.Find("TrackCameraController").GetComponent<TrackCameraController>().Finished();
                            Invoke("CanMove", 2.4f);
                        }, 5.5f));
                        #endregion
                        break;
                    case 5:
                        break;
                    case 6:
                        #region 收据
                        GetSomething("收据1");
                        StartCoroutine(DelayToInvoke.DelayToInvokeDo(() =>
                        {
                            GameObject.Find("BackpackUI/Canvas/Note").transform.GetComponent<NoteUI>().Show("收据1", 5f);
                            GameObject.Find("BackpackUI").GetComponent<BackpackUI>().AddItem("收据1");
                        }, 2f));
                        #endregion
                        break;
                    case 7:
                        #region 日记
                        GetSomething("日记");
                        StartCoroutine(DelayToInvoke.DelayToInvokeDo(() =>
                        {
                            GameObject.Find("BackpackUI/Canvas/Note").transform.GetComponent<NoteUI>().Show("日记", 5f);
                            GameObject.Find("BackpackUI").GetComponent<BackpackUI>().AddItem("日记");
                        }, 2f));
                        #endregion
                        break;
                    case 8:
                        #region 花盆
                        
                        if (GameObject.Find("BackpackUI").GetComponent<BackpackUI>().Contains("瓶装水"))
                        {
                            zimu.Show("希望它可以快快发芽。");
                            GetComponent<Collider2D>().enabled = false;
                            LoseSomething("瓶装水");
                            GameObject.Find("BackpackUI").GetComponent<BackpackUI>().AddItem("瓶装水");
                        }
                        else
                        {
                            zimu.Show("这花盆里有颗种子，或许需要给它浇浇水。");
                        }
                        #endregion
                        break;
                    case 9:
                        #region 瓶装水
                        GetSomething("瓶装水");
                        GameObject.Find("BackpackUI").GetComponent<BackpackUI>().AddItem("瓶装水");
                        Collider2D[] waterColliders = transform.parent.GetComponentsInChildren<Collider2D>();
                        foreach (Collider2D water in waterColliders){
                            water.enabled = false;
                        }
                        #endregion
                        break;
                    case 10:
                        #region 箱子
                        /*Transform boxTransform = transform.GetChild(1);
                        boxTransform.parent = player.transform;
                        boxTransform.localPosition = new Vector3(-5.35f, 11.84f, 0f);
                        boxTransform.localScale = new Vector3(1f, 1f, 1f);*/
                        #endregion
                        break;
                    case 11:
                        #region 收音机
                        GetSomething("收音机");
                        GameObject.Find("BackpackUI").GetComponent<BackpackUI>().AddItem("收音机");
                        InputController.BanButton(true);
                        InputController.BanMouse(true);
                        zimu.Show("拿到了收音机，可还需要电池才行。");
                        GameObject.Find("TrackCameraController").GetComponent<TrackCameraController>().StartMove();
                        StartCoroutine(DelayToInvoke.DelayToInvokeDo(() =>
                        {
                            GameObject.Find("TrackCameraController").GetComponent<TrackCameraController>().Finished();
                            Invoke("CanMove", 2f);
                        }, 5f));
                        transform.parent.Find("电池Trigger").GetComponent<Collider2D>().enabled = true;
                        #endregion
                        break;
                    case 12:
                        #region 电池
                        GetSomething("电池");
                        GameObject.Find("BackpackUI").GetComponent<BackpackUI>().AddItem("电池");
                        zimu.Show("收音机可以正常工作了！");
                        StartCoroutine(DelayToInvoke.DelayToInvokeDo(() =>
                        {
                            GameObject.Find("n格漫画UI").transform.GetComponent<CartoonUI>().Show(2);
                        },2f));
                        
                        #endregion
                        break;
                    case 13:
                        #region 零件
                        GetSomething("零件");
                        GameObject.Find("BackpackUI").GetComponent<BackpackUI>().AddItem("零件");
                        zimu.Show("这零件刚好可以用来修理轮椅。");
                        StartCoroutine(DelayToInvoke.DelayToInvokeDo(() =>
                        {
                            GameObject.Find("n格漫画UI").transform.GetComponent<CartoonUI>().Show(0);
                        }, 2f));
                        #endregion
                        break;
                    case 14:
                        #region 炸弹
                        GetSomething("炸弹");
                        GameObject.Find("BackpackUI").GetComponent<BackpackUI>().AddItem("零件");
                        zimu.Show("这炸弹emm文案还没想好。");
                        #endregion
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
        gameObject.transform.GetChild(1).GetComponent<ShowAndHide>().Hide(2f);
        InputController.BanButton(true);
        getOrLostItem.GetShow(name, 1f, 1f, 1f,delegate(){
                Destroy(getOrLostItem);
                InputController.BanButton(false);
        }, 1f);
        Destroy(remind);
    }

    private void LoseSomething(string name)
    {
        getOrLostItem.character = player.transform;
        getOrLostItem.xOffset = 0f;
        getOrLostItem.yOffset = 2.6f;
        InputController.BanButton(true);
        getOrLostItem.LostShow(name, 1f, 1f, 1f, delegate () {
            Destroy(getOrLostItem);
            InputController.BanButton(false);
        }, 1f);
        Destroy(remind);
    }

    private void ShowChairMan()
    {
        CameraAndCharacterController cameraController = GameObject.Find("CameraAndCharacterController").GetComponent<CameraAndCharacterController>();
        GameObject characters = GameObject.Find("scene2-character");
        GameObject character_chair = characters.transform.Find("Character-chair").gameObject;
        character_chair.GetComponent<ShowAndHide>().Show(2f);
        StartCoroutine(DelayToInvoke.DelayToInvokeDo(() =>
        {
            cameraController.character_man = character_chair;
            GameObject.Find("Character-dog-growup").GetComponent<AutoFollow>().followed = character_chair;
            GameObject cameras = GameObject.Find("scene2-camera");
            GameObject camera_chair = cameras.transform.Find("CM vcam3").gameObject;
            cameraController.camera_man.SetActive(false);
            camera_chair.SetActive(true);
            cameraController.camera_man = camera_chair;
            Physics2D.IgnoreCollision(character_chair.GetComponent<Collider2D>(), cameraController.character_dog.GetComponent<Collider2D>(), true);
        }, 2f));
        
    }

    private void CanMove()
    {
        InputController.BanButton(false);
        InputController.BanMouse(false);
    }
}
