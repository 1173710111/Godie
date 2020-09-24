using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DogMoving : MonoBehaviour
{
    [Tooltip("狗子的水平移动速度")]
    public float dog_speed;

    private bool is_lookingRight = true;
    private bool is_moving = false;
    private Vector3 target_pos;

    void Start()
    {
        
    }

    private void LateUpdate()
    {
        if (is_moving)
        {
            Vector3 offset = target_pos - transform.position;
            if (offset.x >= 0f)
            {
                LookRight();
            }else
            {
                LookLeft();
            }
            //transform.position += offset.normalized * 20 * Time.deltaTime;
            transform.Translate(new Vector3(transform.position.x+ offset.normalized.x * dog_speed * Time.deltaTime , transform.position.y , transform.position.z));
            if (offset.x < 1f)
            {
                transform.position = target_pos;
                is_moving = false;
            }
        }
    }

    private void MoveTo(Vector3 target_position)
    {
        this.is_moving = true;
        this.target_pos = target_position;
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
}
