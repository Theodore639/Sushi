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
        set
        {
            SetItemData(GameData.MONEY, value);
            MainPanel.Instance.SetValue(GameData.MONEY, GetItemData(GameData.MONEY));
        }
        get { return GetItemData(GameData.MONEY);}
    }
    public static int Diamond
    {
        set
        {
            SetItemData(GameData.DIAMOND, value);
            MainPanel.Instance.SetValue(GameData.DIAMOND, GetItemData(GameData.DIAMOND));
        }
        get { return GetItemData(GameData.DIAMOND); }
    }
    public static int Exp
    {
        set 
        {
            SetItemData(GameData.EXP, value);
            MainPanel.Instance.SetValue(GameData.EXP, GetItemData(GameData.EXP));
        }
        get { return GetItemData(GameData.EXP); }
    }
    public static int Power
    {
        set 
        { 
            SetItemData(GameData.POWER, value); 
            MainPanel.Instance.SetValue(GameData.POWER, GetItemData(GameData.POWER)); 
        }
        get { return GetItemData(GameData.POWER); }
    }
    public static int Solict
    {
        set
        {
            SetItemData(GameData.SOLICT, value);
            MainPanel.Instance.SetValue(GameData.SOLICT, GetItemData(GameData.SOLICT));
        }
        get { return GetItemData(GameData.SOLICT); }
    }

    public static int SolictExtra
    {
        set
        {
            SetItemData(GameData.SOLICTEXTRA, value);
            MainPanel.Instance.SetValue(GameData.SOLICTEXTRA, GetItemData(GameData.SOLICTEXTRA));
        }
        get { return GetItemData(GameData.SOLICTEXTRA); }
    }

    public static int Vip
    {
        set
        {
            SetItemData(GameData.VIP, value);
            MainPanel.Instance.SetValue(GameData.VIP, GetItemData(GameData.VIP));
        }
        get { return GetItemData(GameData.VIP); }
    }

    public static int Tips
    {
        set
        {
            SetItemData(GameData.TIPS, value);
            MainPanel.Instance.SetValue(GameData.TIPS, GetItemData(GameData.TIPS));
        }
        get { return GetItemData(GameData.TIPS); }
    }

    #endregion

    #region Dish 各种菜品相关数据

    public static void AddDishCard(int index, int count)
    {
        SetValue("DishCount" + index.ToString(), count + GetDishCardCount(index));
    }

    public static void DishUpgrade(int index)
    {
        int oldLevel = GetDishCardLevel(index);
        SetValue("DishLevel" + index.ToString(), oldLevel + 1);
        SetValue("DishCount" + index.ToString(), GetDishCardCount(index) - GameData.global.dish.upgradeCount[oldLevel]);
    }

    public static int GetDishCardCount(int index)
    {
        return PlayerPrefs.GetInt("DishCount" + index.ToString(), 0);
    }

    public static int GetDishCardLevel(int index)
    {
        return PlayerPrefs.GetInt("DishLevel" + index.ToString(), 0);
    }

    public static void SetDishTime(int index, DateTime time)
    {
        SetValue("DishTime" + index.ToString(), Timer.ConvertDateTimeToLong(time));
    }

    public static DateTime GetDishTime(int index)
    {
        return Timer.ConvertLongToDateTime(PlayerPrefs.GetInt("DishTime" + index.ToString()));
    }

    //获取和保存菜品在商店中的摆放位置
    public static void SetDishLocation(BaseDish[,] dishArray)
    {
        string result = "";
        foreach(BaseDish dish in dishArray)
        {
            result += dish.dishData.id + "_" + dish.x + "_" + dish.y + "!";
        }
        SetValue("DishArray", result);
    }

    public static int[,] GetDishLocation()
    {
        string reslut = PlayerPrefs.GetString("DishArray", "");
        //默认值处理
        if (reslut.Length == 0)
        {
            for (int i = 0; i < StoreManager.MaxX; i++)
                for (int j = 0; j < StoreManager.MaxY; j++)
                    reslut += "-1_" + i + "_" + j + "!";            
        }
        string[] dishes = reslut.Split('!');
        int[,] dishArray = new int[StoreManager.MaxX, StoreManager.MaxY];
        foreach(string dish in dishes)
        {
            string[] value = dish.Split('_');
            if(value.Length == 3)
                dishArray[int.Parse(value[1]), int.Parse(value[2])] = int.Parse(value[0]);
        }
        return dishArray;
    }
    #endregion

    #region TaskArchievement 各种任务成就相关数据
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
