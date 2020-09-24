using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class InputController 
{
    private static KeyCode[] leftButtons= { KeyCode.A, KeyCode.LeftArrow };
    private static KeyCode[] rightButtons = { KeyCode.D, KeyCode.RightArrow };
    private static KeyCode[] interactionButtons = { KeyCode.W, KeyCode.UpArrow };
    
    private static float validTouchDistance=200; //200
    private static string layerName="Ground"; //"Ground"

    public static Vector3 hitPoint;
    public static bool interaction, left, right,mouseDown,buttonDown,anyDown;

    public static void GetKey()
    {
        buttonDown = false;
        mouseDown = false;
        anyDown = false;
        if (!GameController.GetCanMove())
            return;
        anyDown = GetAnyKeyOrMouse();
        interaction = GetInteractionKey();
        left = GetLeftKey();
        right = GetRightKey();
        if (left || right || interaction)
        {
            buttonDown = true;
        }

        if (left && right)
        {
            left = false;
            right = false;
        }
        if (Input.GetMouseButtonDown(0))
        {
            mouseDown = true;
            //摄像机需要设置MainCamera的Tag这里才能找到
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);  
            RaycastHit hitInfo;
            if (Physics.Raycast(ray, out hitInfo, validTouchDistance, LayerMask.GetMask(layerName)))
            {
                GameObject gameObj = hitInfo.collider.gameObject;
                hitPoint = hitInfo.point;
                Debug.Log("click object name is " + gameObj.name + " , hit point " + hitPoint.ToString());
            }
        }
    }

    private static bool GetAnyKeyOrMouse()
    {
        return Input.anyKeyDown;
    }

    private static bool GetInteractionKey()
    {
        foreach (KeyCode keyCode in interactionButtons)
        {
            if (Input.GetKey(keyCode))
            {
                return true;
            }
        }
        return false;
    }

    private static bool GetLeftKey()
    {
        foreach (KeyCode keyCode in leftButtons)
        {
            if (Input.GetKey(keyCode))
            {
                return true;
            }
        }
        return false;
    }

    private static bool GetRightKey()
    {
        foreach (KeyCode keyCode in rightButtons)
        {
            if (Input.GetKey(keyCode))
            {
                return true;
            }
        }
        return false;
    }
}
