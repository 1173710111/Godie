using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class InputController 
{
    private static KeyCode[] leftButtons= { KeyCode.A, KeyCode.LeftArrow };
    private static KeyCode[] rightButtons = { KeyCode.D, KeyCode.RightArrow };
    private static KeyCode[] interactionButtons = { KeyCode.W, KeyCode.UpArrow };
    
    public static Vector3 hitPoint;
    public static bool interaction, left, right,mouseDown,right_mouseDown,left_mouseDown,buttonDown,anyDown;

    public static void GetKey()
    {
        buttonDown = false;
        mouseDown = false;
        anyDown = false;
        left_mouseDown = false;
        right_mouseDown = false;

        interaction = GetInteractionKey();
        left = GetLeftKey();
        right = GetRightKey();

        if (left && right)
        {
            left = false;
            right = false;
        }
        if (Input.GetMouseButtonDown(0))
        {
            left_mouseDown = true;
        }
        if (Input.GetMouseButtonDown(1))
        {
            right_mouseDown = true;
        }

        buttonDown = left | right | interaction;
        mouseDown = left_mouseDown | right_mouseDown;
        anyDown = mouseDown | buttonDown;

        if (mouseDown)
        {
            Vector3 playerWordDir = Camera.main.WorldToScreenPoint(new Vector3(0, 0, 0f));
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, playerWordDir.z));
            hitPoint = mousePosition;
        }        
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
