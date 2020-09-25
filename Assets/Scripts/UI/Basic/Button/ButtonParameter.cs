using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonParameter : MonoBehaviour
{
    [Tooltip("正常时文本大小")] public int fontNormalSize = 50;
    [Tooltip("悬浮时文本大小")] public int fonthighlightSize = 70;
    [Tooltip("正常时文本颜色")] public Color fontNormalColor = Color.white;
    [Tooltip("悬浮时文本颜色")] public Color fontHighlightedColor = Color.black;
}
