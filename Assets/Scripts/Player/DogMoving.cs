﻿
using UnityEngine;

public class DogMoving : MonoBehaviour
{
    [Tooltip("狗子的水平移动速度")]
    public float dog_speed_x;
    [Tooltip("狗子的竖直移动速度")]
    public float dog_speed_y;

    //此时狗子是不是在空中
    private bool is_inAir=false;
    private bool is_lookingRight = false;
    private bool is_moving = false;
    private Vector3 target_pos;
    private bool lookRight;


    private void LateUpdate()
    {
        is_inAir = GetComponent<BottomCheck>().IsInAir();
        if (is_moving&&!is_inAir)
        {
            Vector3 offset = target_pos - transform.position;
            #region 水平位移
            if (offset.x < 0.1f&&offset.x>-0.1f)
            {
                StopMoving();
                GetComponent<Rigidbody2D>().velocity = new Vector2(0f,0f);
                return;
            }
            if (offset.x > 0)
            {
                GetComponent<Rigidbody2D>().velocity = new Vector2(dog_speed_x, 0f);
            }else if (offset.x < 0)
            {
                
                GetComponent<Rigidbody2D>().velocity = new Vector2(-dog_speed_x, 0f);
            }
            #endregion
        }
    }

    private void MoveTo(Vector3 target_position)
    {
        StartMoving();
        this.target_pos = target_position;
        if (target_position.x > transform.position.x)
        {
            LookRight();
        }
        else if (target_position.x < transform.position.x)
        {
            LookLeft();
        }
    }

    private void JumpTo(Vector3 target_position)
    {
        if (!is_inAir)
        {
            
            StopMoving();
            if (target_position.x > transform.position.x)
            {
                LookRight();
                GetComponent<Rigidbody2D>().velocity = new Vector2(dog_speed_x, dog_speed_y);
            }
            else if (target_position.x < transform.position.x)
            {
                LookLeft();
                GetComponent<Rigidbody2D>().velocity = new Vector2(-dog_speed_x, dog_speed_y);
            }
            else
            {
                GetComponent<Rigidbody2D>().velocity = new Vector2(0f, dog_speed_y);
            }
        
        }
            
    }

    private void LookLeft()
    {
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
    }

    private void LookRight()
    {
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
    }

    public void StopMoving()
    {
        is_moving = false;
        GetComponentInChildren<Animator>().SetBool("IsMoving", false);
    }

    public void StartMoving()
    {
        is_moving = true;
        GetComponentInChildren<Animator>().SetBool("IsMoving", true);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.contacts[0].normal.x>=-1&&collision.contacts[0].normal.x<=1)
        {
            StopMoving();
        }
            
    }
}
