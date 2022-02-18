using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public static class PlayerData
{
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
        MainPanel.Instance.SetValue(id, GetItemData(id) + value);
    }

    public static int GetItemData(int id)
    {
        if (id < 1000)
            return DecriptInt(PlayerPrefs.GetString(id.ToString(), "0"));
        else
            return PlayerPrefs.GetInt(id.ToString(), 0);
    }

    //设置某一个具体的值，必须通过此函数设置值
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
            if (!stringKeys.Contains(key))
                stringKeys.Add(key);
            PlayerPrefs.SetString(key, value.ToString());
        }
    }


    #region BaseParam 基础参数
    public static int Money
    {
        set { SetItemData(CONST.MONEY, value); }
        get { return GetItemData(CONST.MONEY); }
    }
    public static int Diamond
    {
        set { SetItemData(CONST.DIAMOND, value); }
        get { return GetItemData(CONST.DIAMOND); }
    }
    public static int Level
    {
        set { SetItemData(CONST.LEVEL, value); }
        get { return GetItemData(CONST.MONEY); }
    }
    public static int Exp
    {
        set 
        {
            int exp = value;
            if(exp >= GameData.store[Level].exp)
            {
                exp -= GameData.store[Level].exp;
                Level++;
                StoreManager.Instance.StoreUpgrade();
            }
            SetItemData(CONST.EXP, exp);
        }
        get { return GetItemData(CONST.EXP); }
    }
    public static int Power
    {
        set { SetItemData(CONST.POWER, value); }
        get { return GetItemData(CONST.POWER); }
    }
    public static int Solict
    {
        set { SetItemData(CONST.SOLICT, value); }
        get { return GetItemData(CONST.SOLICT); }
    }
    public static int SolictExtra
    {
        set { SetItemData(CONST.SOLICTEXTRA, value); }
        get { return GetItemData(CONST.SOLICTEXTRA); }
    }

    #endregion

    #region Dish 菜品相关数据

    public static void SetDishData(int index, PlayerDishData dishData)
    {
        string result = dishData.level + "_" + dishData.cardCount + "_" + Timer.ConvertDateTimeToLong(dishData.startCookingTime); 
        SetValue("Dish" + index, result);
    }

    public static PlayerDishData GetDishData(int index)
    {
        PlayerDishData dishData = new PlayerDishData();
        try
        {
            string[] reslut = PlayerPrefs.GetString("Shelf" + index, "0_0_0_0_0").Split('_');
            dishData.level = int.Parse(reslut[0]);
            dishData.cardCount = int.Parse(reslut[1]);
            dishData.startCookingTime = Timer.ConvertLongToDateTime(long.Parse(reslut[2]));
        }
        catch (Exception e)
        {
            LogManager.ShowLog("GetDishData Error, index =" + index + " reason = " + e.ToString(), true);
        }
        return dishData;
    }

    #endregion

    #region Shelf 货架相关数据
    public static void SetShelfData(int index, PlayerShelfData shelfData)
    {
        string result = shelfData.level + "_" + shelfData.priceIncLevel + "_" + shelfData.speedIncLevel + 
            "_" + shelfData.stackLevel + "_" + shelfData.dishIndex;
        SetValue("Shelf" + index, result);
    }

    public static PlayerShelfData GetShelfData(int index)
    {
        PlayerShelfData shelfData = new PlayerShelfData();
        try
        {
            string[] reslut = PlayerPrefs.GetString("Shelf" + index, "0_0_0_0_0").Split('_');
            shelfData.level = int.Parse(reslut[0]);
            shelfData.priceIncLevel = int.Parse(reslut[1]);
            shelfData.speedIncLevel = int.Parse(reslut[2]);
            shelfData.stackLevel = int.Parse(reslut[3]);
            shelfData.dishIndex = int.Parse(reslut[4]);
        }
        catch(Exception e)
        {
            LogManager.ShowLog("GetShelfData Error, index =" + index + " reason = " + e.ToString(), true);
        }
        return shelfData;
    }
    #endregion

    #region TaskArchievement 各种任务成就相关数据
    public static void SetAichievementData(int index, int value)
    {
        SetItemData(CONST.ARCHIEVEMENT + index, value);
    }

    public static int GetAichievementData(int index)
    {
        return GetItemData(CONST.ARCHIEVEMENT + index);
    }

    #endregion

    #region Customer 各种顾客相关数据，存放临时顾客信息

    #endregion

    #region Box 各种宝箱相关数据

    #endregion

    #region Function 基础功能，打包/加密等
    public static string PackPlayerData()
    {
        string result = "";
        for (int i = 0; i < intKeys.Count; i++)
            result += intKeys[i] + "#" + PlayerPrefs.GetInt(intKeys[i], 0) + "&";
        result += "$";
        for (int i = 0; i < stringKeys.Count; i++)
            result += stringKeys[i] + "#" + PlayerPrefs.GetString(stringKeys[i], " ") + "&";
        return result;
    }

    public static void UnPackPlayerData()
    {

    }

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
                int r = UnityEngine.Random.Range(0, 10);
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

#region Struct

public struct PlayerDishData
{
    public int level;
    public int cardCount;
    public DateTime startCookingTime;
}

public struct PlayerShelfData
{
    public int level;
    public int priceIncLevel;
    public int speedIncLevel;
    public int stackLevel;
    public int dishIndex;
}

public struct PlayerCustomerData
{

}
#endregion