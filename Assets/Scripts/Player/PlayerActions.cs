using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerActions : MonoBehaviour
{
    
    [Tooltip("当前人的状态，0表示生病且无轮椅，1表示有轮椅，2表示健康，3表示狗子")]
    public int m_state;

    public GameObject CameraController;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        InputController.GetKey();
        if (!GameController.GetCanMove())
        {
            return;
        }

        #region 切换视角
        if (GameController.GetCharacterNumber() == 2)
        {
            int mainCamera = CameraController.GetComponent<CameraAndCharacterController>().mainCamera;
            if (mainCamera == 0 && InputController.mouseDown)
            {
                CameraController.GetComponent<CameraAndCharacterController>().mainCamera = 1;
                CameraController.GetComponent<CameraAndCharacterController>().SendMessage("ChangeCamera");
            }
            else if (mainCamera == 1 && InputController.buttonDown)
            {
                CameraController.GetComponent<CameraAndCharacterController>().mainCamera = 0;
                CameraController.GetComponent<CameraAndCharacterController>().SendMessage("ChangeCamera");
            }
        }
        #endregion

        #region 轮椅的移动
        if (m_state == 1)
        {
            gameObject.GetComponent<ChairMoving>().enabled = true;
        }
        #endregion


        #region 健康人的移动
        else if (m_state == 2)
        {
            if (InputController.left)
            {
                gameObject.GetComponent<PlayerMoving>().SendMessage("IsMoving", true);
                gameObject.GetComponent<PlayerMoving>().SendMessage("MoveLeft");
            }
            else if (InputController.right)
            {
                gameObject.GetComponent<PlayerMoving>().SendMessage("IsMoving", true);
                gameObject.GetComponent<PlayerMoving>().SendMessage("MoveRight");
            }
            else if (InputController.interaction)
            {
                gameObject.GetComponent<PlayerMoving>().SendMessage("IsMoving", false);
                gameObject.GetComponent<PlayerMoving>().SendMessage("Interaction");
                return;
            }
            else
            {
                gameObject.GetComponent<PlayerMoving>().SendMessage("IsMoving", false);
                return;
            }

        }
        #endregion

        #region 狗子的移动
        if (m_state == 3)
        {
            GameObject plane = GameObject.FindGameObjectWithTag("Plane");
            Vector3 target = new Vector3(InputController.hitPoint.x, InputController.hitPoint.y, plane.transform.position.z);
            if (InputController.right_mouseDown)
            {

                GetComponent<DogMoving>().SendMessage("JumpTo", target);
            }
            if (plane.GetComponent<Collider2D>().bounds.Contains(target))
            {
                if (InputController.left_mouseDown)
                {
                    GetComponent<DogMoving>().SendMessage("MoveTo", target);
                }
            }
        }
        #endregion
    }

}
