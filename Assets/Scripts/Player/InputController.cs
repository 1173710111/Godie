using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using UnityEngine;

public static class InputController 
{
    private static KeyCode[] leftButtons= { KeyCode.A, KeyCode.LeftArrow };
    private static KeyCode[] rightButtons = { KeyCode.D, KeyCode.RightArrow };
    private static KeyCode[] interactionButtons = { KeyCode.W, KeyCode.UpArrow };
    private static KeyCode[] skillButtons = { KeyCode.LeftShift, KeyCode.RightShift };
    private static bool button_ban = false;
    private static bool mouse_ban = false;
    public static Vector3 hitPoint;
    public static bool interaction, left, right,mouseDown,right_mouseDown,left_mouseDown,buttonDown,anyDown;
    public static bool rush;

    public static void BanButton(bool flag)
    {
        button_ban = flag;
    }

    public static void BanMouse(bool flag)
    {
        mouse_ban = flag;
    }

    public static void GetKey()
    {
        buttonDown = false;
        mouseDown = false;
        anyDown = false;
        left_mouseDown = false;
        right_mouseDown = false;
        left = false;
        right = false;
        interaction = false;
        rush = false;
        if (!button_ban)
        {
            interaction = GetInteractionKey();
            left = GetLeftKey();
            right = GetRightKey();
            if (left && right)
            {
                left = false;
                right = false;
            }
            string skill = GetSkillKey();
            if (skill.Equals("rush"))
            {
                rush = true;
            }
            
        }
        if (!mouse_ban)
        {
            if (Input.GetMouseButtonDown(0))
            {
                left_mouseDown = true;
            }
            if (Input.GetMouseButtonDown(1))
            {
                right_mouseDown = true;
            }
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

    private static string GetSkillKey()
    {
        foreach (KeyCode keyCode in skillButtons)
        {
            if (Input.GetKey(keyCode))
            {
                switch (keyCode)
                {
                    case KeyCode.LeftShift:
                        return "rush";
                    case KeyCode.RightShift:
                        return "rush";
                }
            }
        }
        return "null";
    }
}
