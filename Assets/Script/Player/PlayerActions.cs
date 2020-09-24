using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerActions : MonoBehaviour
{
    [Tooltip("当前主视角，0表示人，1表示狗子")]
    public int mainCamera;
    [Tooltip("当前人的状态，0表示生病且无轮椅，1表示有轮椅，2表示健康，3表示狗子")]
    public int m_state;

    // Start is called before the first frame update
    void Start()
    {
        mainCamera = 0;
    }

    // Update is called once per frame
    void Update()
    {
        InputController.GetKey();
        
        #region 切换视角
        if (mainCamera == 0 && InputController.mouseDown)
        {
            LookAtDog();
            mainCamera = 1;
            return;
        }else if(mainCamera==1 && InputController.buttonDown)
        {
            LookAtMan();
            mainCamera = 0;
            return;
        }
        #endregion

        #region 轮椅的移动
        else if (m_state == 1)
        {
            gameObject.GetComponent<ChairMoving>().enabled = true;
        }
        #endregion


        #region 健康人的移动
        else if (m_state == 2)
        {
            gameObject.GetComponent<PlayerMoving>().enabled = true;
            if (InputController.interaction)
            {
                gameObject.GetComponent<PlayerMoving>().SendMessage("Interaction");
                return;
            }
            else if (InputController.left)
            {
                gameObject.GetComponent<PlayerMoving>().SendMessage("MoveLeft");
            }
            else if (InputController.right)
            {
                gameObject.GetComponent<PlayerMoving>().SendMessage("MoveRight");
            }
        }
        #endregion

        #region 狗子的移动
        if (m_state == 3 && mainCamera == 1 && InputController.mouseDown)
        {
            GetComponent<DogMoving>().SendMessage("MoveTo", InputController.hitPoint);
        }
        #endregion
    }

    private void LookAtDog()
    {

    }

    private void LookAtMan()
    {

    }
}
