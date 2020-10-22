using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MouseCursorChange : MonoBehaviour
{
    public MouseCursorController mouseCursor;

    private void Update()
    {
        Vector3 playerWordDir = Camera.main.WorldToScreenPoint(new Vector3(0, 0, 0f));
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, playerWordDir.z));
        int state=mouseCursor.GetState();
        if (GetComponent<Collider2D>().bounds.Contains(mousePosition) && state == 1)
        {
            mouseCursor.EnterPointPlane();
        }
        else if (!GetComponent<Collider2D>().bounds.Contains(mousePosition) && state == 0)
        {
            mouseCursor.ExitPointPlane();
        }
    }

}
