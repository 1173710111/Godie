using System.Collections;
using System.Collections.Generic;
using UnityEditor.MemoryProfiler;
using UnityEngine;

public class DogMoving : MonoBehaviour
{
    [Tooltip("狗子的水平移动速度")]
    public float dog_speed_x;
    [Tooltip("狗子的竖直移动速度")]
    public float dog_speed_y;
    [Tooltip("地面所在层")]
    public LayerMask groundLayer;
    //人物中心与地面的垂直距离
    private float distance;

    //此时狗子是不是在空中
    private bool is_inAir=false;
    private bool is_lookingRight = false;
    private bool is_moving = false;
    private Vector3 target_pos;
    private bool lookRight;

    void Start()
    {
        distance=transform.position.y-GameObject.Find("GroundSurface").transform.position.y+0.05f;
    }

    private void LateUpdate()
    {
        RaycastHit2D hitInfo=Physics2D.Raycast(transform.position,Vector2.down,distance,groundLayer);
        if (hitInfo.collider == null)
        {
            is_inAir = true;
        }
        else
        {
            is_inAir = false;
        }
        if (is_moving&&!is_inAir)
        {
            Vector3 offset = target_pos - transform.position;
            #region 水平位移
            if (offset.x < 0.1f&&offset.x>-0.1f)
            {
                IsMoving(false);
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
        IsMoving(true);
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
            
            IsMoving(false);
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

    private void IsMoving(bool isMoving)
    {
        is_moving = isMoving;
        GetComponentInChildren<Animator>().SetBool("IsMoving", isMoving);
    }

}
