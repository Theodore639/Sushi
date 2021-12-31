using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class PlayerData
{
    public static int Gold
    {
        set{ SetItemData(GameData.MONEY, value);}
        get{ return GetItemData(GameData.MONEY);}
    }
    public static int Clover
    {
        set { SetItemData(GameData.DIAMOND, value); }
        get { return GetItemData(GameData.DIAMOND); }
    }
    public static int Exp
    {
        set { SetItemData(GameData.EXP, value); }
        get { return GetItemData(GameData.EXP); }
    }
    public static int Power
    {
        set { SetItemData(GameData.POWER, value); }
        get { return GetItemData(GameData.POWER); }
    }
    public static int Solict
    {
        set { SetItemData(GameData.SOLICT, value); }
        get { return GetItemData(GameData.SOLICT); }
    }

    public static void AddItemData(int id, int value)
    {
        SetItemData(id, GetItemData(id) + value);
    }

    public static void SetItemData(int id, int value)
    {
        if (id < 1000)
            SetValue(id.ToString(), EncriptInt(value));
        else
            SetValue(id.ToString(), value);
    }

    public static int GetItemData(int id)
    {
        if (id < 1000)
            return DecriptInt(PlayerPrefs.GetString(id.ToString(), "0"));
        else
            return PlayerPrefs.GetInt(id.ToString(), 0);
    }


    private static void SetValue(string key, object value)
    {
        if (value.GetType() == typeof(int))
        {
            if (!intKeys.Contains(key))
                intKeys.Add(key);
            PlayerPrefs.SetInt(key, (int)value);
        }
        else
        {
            if(!stringKeys.Contains(key))
                stringKeys.Add(key);
            PlayerPrefs.SetString(key, value.ToString());
        }
    }

    #region Pack
    public static string PackPlayerData()
    {
        string result = "";
        for (int i = 0; i < intKeys.Count; i++)
            result += intKeys[i] + "_" + PlayerPrefs.GetInt(intKeys[i], 0) + "&";
        result += "$";
        for (int i = 0; i < stringKeys.Count; i++)
            result += stringKeys[i] + "_" + PlayerPrefs.GetString(stringKeys[i], "0") + "&";
        return result;
    }

    public static void UnPackPlayerData()
    {

    }
    #endregion

    #region Encript
    static List<string> intKeys;//保存所有int型的key
    static List<string> stringKeys;//保存所有string型的key

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
                Debug.Log("Warning：解密时校验失败 " + value);
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



    private static string EncriptString(string str)
    {
        return "";
    }

    private static string DecriptString(string str)
    {
        return "";
    }
    #endregion
}
