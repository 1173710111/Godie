﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerActions : MonoBehaviour
{
    
    [Tooltip("当前人的状态，0表示生病且无轮椅，1表示有轮椅，2表示健康，3表示狗子")]
    public int m_state;

    public GameObject CameraController;
    //用来判断用户是否有交互键的输入；
    private bool interaction;
    AudioSourceController audioSourceController;

    public bool GetInteraction()
    {
        return interaction;
    }

    // Start is called before the first frame update
    void Start()
    {
        if (audioSourceController == null) audioSourceController = AudioSourcesManager.ApplyAudioSourceController();
    }

    // Update is called once per frame
    void Update()
    {
        interaction = false;
        InputController.GetKey();
        
        #region 切换视角
        if (CameraController!=null&&GameController.GetCharacterNumber() == 2)
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
        else if(GameController.GetCharacterNumber() !=2)
        {
            Debug.Log("这个场景的角色不是两个，所以不能切换视角！");
        }
        #endregion

        #region 虚弱的人的移动
        if (m_state == 0)
        {
            if (InputController.left)
            {
                gameObject.GetComponent<PoorManMoving>().SendMessage("MoveLeft");
                audioSourceController.Play("人走路", transform);
            }
            else if (InputController.right)
            {
                gameObject.GetComponent<PoorManMoving>().SendMessage("MoveRight");
                audioSourceController.Play("人走路", transform);
            }
            else if (InputController.interaction)
            {
                interaction = true;
                gameObject.GetComponent<PoorManMoving>().SendMessage("StopMoving");
                audioSourceController.Stop();
                audioSourceController = AudioSourcesManager.ApplyAudioSourceController();
                return;
            }
            else
            {
                gameObject.GetComponent<PoorManMoving>().SendMessage("StopMoving");
                audioSourceController.Stop();
                audioSourceController = AudioSourcesManager.ApplyAudioSourceController();
                return;
            }
        }
        #endregion

        #region 轮椅的移动
        if (m_state == 1)
        {
            if (InputController.rush)
            {
                Skill rushSkill = GameObject.Find("技能UI/Canvas/Panel").transform.Find("技能1").GetComponent<Skill>();
                if (rushSkill.IsAble())
                {
                    if (!rushSkill.isCD)
                    {
                        rushSkill.UseSkill();
                        //audioSourceController.Play("冲刺", transform);
                        gameObject.GetComponent<ChairMoving>().SendMessage("Rush");
                    }
                }
            }
            if (InputController.left)
            {
                audioSourceController.Play("轮椅移动", transform);
                gameObject.GetComponent<ChairMoving>().SendMessage("MoveLeft");
            }
            else if (InputController.right)
            {
                audioSourceController.Play("轮椅移动", transform);
                gameObject.GetComponent<ChairMoving>().SendMessage("MoveRight");
            }
            else if (InputController.interaction)
            {
                interaction = true;
                gameObject.GetComponent<ChairMoving>().SendMessage("StopMoving");
                audioSourceController.Stop();
                audioSourceController = AudioSourcesManager.ApplyAudioSourceController();
                return;
            }
            else
            {
                gameObject.GetComponent<ChairMoving>().SendMessage("StopMoving");
                audioSourceController.Stop();
                audioSourceController = AudioSourcesManager.ApplyAudioSourceController();
                return;
            }
        }
        #endregion


        #region 健康人的移动
        if (m_state == 2)
        {
            if (InputController.left)
            {
                gameObject.GetComponent<PlayerMoving>().SendMessage("IsMoving", true);
                gameObject.GetComponent<PlayerMoving>().SendMessage("MoveLeft");
                audioSourceController.Play("人走路", transform);
            }
            else if (InputController.right)
            {
                gameObject.GetComponent<PlayerMoving>().SendMessage("IsMoving", true);
                gameObject.GetComponent<PlayerMoving>().SendMessage("MoveRight");
                audioSourceController.Play("人走路", transform);
            }
            else if (InputController.interaction)
            {
                interaction = true;
                gameObject.GetComponent<PlayerMoving>().SendMessage("IsMoving", false);
                audioSourceController.Stop();
                audioSourceController= AudioSourcesManager.ApplyAudioSourceController();
                return;
            }
            else
            {
                gameObject.GetComponent<PlayerMoving>().SendMessage("IsMoving", false);
                audioSourceController.Stop();
                audioSourceController = AudioSourcesManager.ApplyAudioSourceController();
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
                    interaction = true;
                }
            }
        }
        #endregion
    }
}
