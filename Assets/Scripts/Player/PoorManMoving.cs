using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoorManMoving : MonoBehaviour
{
    [Tooltip("水平移动速度")]
    public float m_speed;
    [Tooltip("间隔多久咳嗽一次")]
    public float interval_time = 5f;

    private bool is_lookingRight = false;
    //咳嗽动作持续多久
    private float cough_time=2.3f;
    //距离上一次咳嗽过了多久
    private float last_time;
    private GameObject child;
    private bool is_Cough;

    private void Start()
    {
        last_time = 0f;
        is_Cough = false;
        child = transform.GetChild(0).gameObject;
    }

    void Update()
    {
        if (!is_Cough)
        {
            last_time += Time.deltaTime;
        }
        if (last_time > interval_time )
        {
            StopMoving();
            child.GetComponent<Animator>().SetBool("IsCough", true);
            InputController.BanButton(true);
            last_time = 0f;
            is_Cough = true;
            Invoke("FinishCough", cough_time);
        }
    }

    private void FinishCough()
    {
        InputController.BanButton(false);
        child.GetComponent<Animator>().SetBool("IsCough", false);
        last_time = 0f;
        is_Cough = false;
        InputController.GetKey();
    }

    private void MoveLeft()
    {
        child.GetComponent<Animator>().SetBool("IsMoving", true);
        is_lookingRight = false;
        Transform spriteTransform = transform.GetChild(0);
        if (is_lookingRight)
        {
            Quaternion look = new Quaternion(spriteTransform.rotation.x, 0.0f, spriteTransform.rotation.z, spriteTransform.rotation.w);
            spriteTransform.rotation = look;
        }
        else
        {
            Quaternion look = new Quaternion(spriteTransform.rotation.x, 180.0f, spriteTransform.rotation.z, spriteTransform.rotation.w);
            spriteTransform.rotation = look;
        }
        GetComponent<Rigidbody2D>().velocity = new Vector2(-1 * m_speed, 0f);
    }

    private void MoveRight()
    {
        child.GetComponent<Animator>().SetBool("IsMoving", true);
        is_lookingRight = true;
        Transform spriteTransform = transform.GetChild(0);
        if (is_lookingRight)
        {
            Quaternion look = new Quaternion(spriteTransform.rotation.x, 0.0f, spriteTransform.rotation.z, spriteTransform.rotation.w);
            spriteTransform.rotation = look;
        }
        else
        {
            Quaternion look = new Quaternion(spriteTransform.rotation.x, 180.0f, spriteTransform.rotation.z, spriteTransform.rotation.w);
            spriteTransform.rotation = look;
        }
        GetComponent<Rigidbody2D>().velocity = new Vector2(m_speed, 0f);
    }

    private void StopMoving()
    {
        GetComponent<Rigidbody2D>().velocity = new Vector2(0f, 0f);
        child.GetComponent<Animator>().SetBool("IsMoving", false);
    }
}
