using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseCursorController : MonoBehaviour
{
    [Tooltip("默认图标，表示鼠标在允许点击的范围内")]
    public Texture2D normal;
    [Tooltip("禁止点击的图标，表示鼠标在允许点击的范围外")]
    public Texture2D cannot;
    [Tooltip("交互提示的图标，表示可以点击进行交互")]
    public Texture2D interaction;

    private int state = 0;
    public int GetState()
    {
        return state;
    }

    public void Awake()
    {
        Cursor.SetCursor(normal, Vector2.zero, CursorMode.Auto);
        state = 0;
    }


    public void EnterPointPlane()
    {
        state = 0;
        Cursor.SetCursor(normal, Vector2.zero, CursorMode.Auto);
    }

    public void ExitPointPlane()
    {
        state = 1;
        Cursor.SetCursor(cannot, Vector2.zero, CursorMode.Auto);
        
    }

    public void InterationPrompt()
    {
        state = 2;
        Cursor.SetCursor(interaction, Vector2.zero, CursorMode.Auto);
    }
}
