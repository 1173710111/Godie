using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BottomCheck : MonoBehaviour
{
    public Transform left_point;
    public Transform right_point;
    [Tooltip("地面所在层")]
    public LayerMask groundLayer;
    //人物底部与地面的垂直距离
    private float distance;
    private Vector3[] points;
    private bool is_inAir;
    private float deta;
    private void Awake()
    {
        distance = 0.05f;
        points = new Vector3[30];
        float length = right_point.position.x - left_point.transform.position.x;
        deta = length / 30f;
    }

    private void Update()
    {
        for (int i = 0; i < 30; i++)
        {
            points[i] = new Vector3(left_point.position.x + i * deta, left_point.position.y, left_point.position.z);
        }
        is_inAir = true;
        foreach(Vector3 point in points)
        {
            RaycastHit2D hitInfo = Physics2D.Raycast(point, Vector2.down, distance, groundLayer);
            if (hitInfo.collider != null)
            {
                is_inAir = false;
                break;
            }
        }
    }

    public bool IsInAir()
    {
        return this.is_inAir;
    }
}
