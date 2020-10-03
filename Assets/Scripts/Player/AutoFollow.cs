using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoFollow : MonoBehaviour
{
    [Tooltip("被跟随的物体")]
    public GameObject followed;
    [Tooltip("间隔距离")]
    public float follow_distance;
    [Tooltip("距离多远时停止跟随")]
    public float stop_distance;
    [Tooltip("跟随速度")]
    public float velocity;
    private bool is_following;


    // Update is called once per frame
    void Update()
    {
        InputController.GetKey();
        if (InputController.mouseDown)
        {
            is_following = false;
        }
        Vector3 offset = followed.transform.position - transform.position;
        if (Math.Abs(offset.x) > follow_distance)
        {
            is_following = true;
        }
        if (Math.Abs(offset.x) <= (stop_distance + 0.1) && is_following)
        {
            gameObject.GetComponent<DogMoving>().SendMessage("IsMoving", false);
            is_following = false;
        }
        #region 跟随某物体移动
        if (is_following)
        {
            gameObject.GetComponent<DogMoving>().SendMessage("IsMoving", true);
            Vector3 target;
            if (offset.x > 0)
            {
                target = new Vector3(followed.transform.position.x - stop_distance, followed.transform.position.y, followed.transform.position.z);
            }
            else
            {
                target = new Vector3(followed.transform.position.x + stop_distance, followed.transform.position.y, followed.transform.position.z);
            }
            GetComponent<DogMoving>().SendMessage("MoveTo", target);
        }
        
        #endregion
    }
}
