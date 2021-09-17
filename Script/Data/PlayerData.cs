using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class PlayerData
{
    public static void SetItemData(int id, int value)
    {
        if(id < 1000)
            PlayerPrefs.SetString(id.ToString(), EncriptInt(value));
        else
            PlayerPrefs.SetInt(id.ToString(), value);
    }

    public static int GetItemData(int id)
    {
        if (id < 1000)
            return DecriptInt(PlayerPrefs.GetString(id.ToString(), "0"));
        else
            return PlayerPrefs.GetInt(id.ToString(), 0);
    }

    /// <summary>
    /// 加解密
    /// </summary>
    static int[] box = new int[] {17, -13, 401, -349, 49681, -38923, 7612589, -6198747};
    private static string EncriptInt(int value)
    {
        try
        {
            string result = "";
            int a = 0;
            for (int i = 0; i < box.Length; i++)
            {
                int r = Random.Range(0, 10);
                a += r * box[i];
                result += r.ToString();
            }
            int b = int.Parse(result) % 997;
            if (b < 100) result += "0";
            if (b < 10) result += "0";
            result += b.ToString();
            result += (value - a).ToString();
            Debug.Log(result);
            return result;
        }
        catch
        {
            //TODO 提示数据异常
            return "0";
        }
    }

    private static int DecriptInt(string value)
    {
        try
        {
            int result = 0;
            for (int i = 0; i < box.Length; i++)
            {
                int r = int.Parse(value[i].ToString());
                result += r * box[i];
            }
            int a = int.Parse(value.Substring(0, box.Length));
            int b = int.Parse(value.Substring(box.Length, 3));
            if (a % 997 != b)
            {
                //TODO 提示数据异常
                return 0;
            }
            result += int.Parse(value.Substring(box.Length + 3));
            Debug.Log(result);
            return result > 0 ? result : 0;
        }
        catch
        {
            //TODO 提示数据异常
            return 0;
        }
    }
}
