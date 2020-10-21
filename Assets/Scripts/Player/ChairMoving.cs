using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChairMoving : MonoBehaviour
{
    [Tooltip("轮椅的水平移动速度")]
    public float m_speed;

    private bool is_lookingRight = false;
    private GameObject child;
    // Start is called before the first frame update
    void Start()
    {
        child = transform.GetChild(0).gameObject;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        
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
