using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 颜色管理
/// </summary>
public static class ColorHelper
{
    public static Color BrownDark = GetColorFromHex("5D4417");
    public static Color BrownLight = GetColorFromHex("A38A5E");
    public static Color Green = GetColorFromHex("6B8732");
    public static Color LightGreen = GetColorFromHex("BCFFB3");
    public static Color Red = GetColorFromHex("DE6A37");
    public static Color Gray = GetColorFromHex("6A6B6F");
    public static Color LightGray = GetColorFromHex("BFBFBF");

    public static Color GetTransParentColor(Color color)
    {
        return new Color(color.r, color.g, color.b, 0);
    }

    public static Color GetNonTransParentColor(Color color)
    {
        return new Color(color.r, color.g, color.b, 1);
    }

    public static Color GetColorFromHex(string hex, float a = 1)
    {
        Color color = new Color();
        try
        {
            color.r = (GetIntFromHex(hex[0]) * 16 + GetIntFromHex(hex[1])) / 256.0f;
            color.g = (GetIntFromHex(hex[2]) * 16 + GetIntFromHex(hex[3])) / 256.0f;
            color.b = (GetIntFromHex(hex[4]) * 16 + GetIntFromHex(hex[5])) / 256.0f;
            color.a = a;
        }
        catch
        {
            Debug.Log("颜色字符格式不正确");
        }
        return color;
    }

    private static int GetIntFromHex(char c)
    {
        if (c >= '0' && c <= '9')
            return c - 48;
        if (c >= 'A' && c <= 'F')
            return c - 55;
        if (c >= 'a' && c <= 'f')
            return c - 87;
        return 0;
    }
}


