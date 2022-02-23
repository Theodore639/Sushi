using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public static class PlayerData
{
    #region 基础参数
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
        get { return GetItemData(CONST.LEVEL); }
    }
    public static int Exp
    {
        set 
        {
            int exp = value;
            if(exp >= GameData.store[Level].exp)
            {
                exp -= GameData.store[Level].exp;
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

    #region 常用物体数据，包括店铺，菜品，货架，顾客等
    ////商店
    //public static void SetStoreData(PlayerStoreData data)
    //{
    //    SetValue("Store", SerializeObjToStr(data));
    //}
    //public static PlayerStoreData GetStoreData()
    //{
    //    object result = DeserializeStrToObj(PlayerPrefs.GetString("Store"));
    //    if (result == null)
    //        return new PlayerStoreData();
    //    return (PlayerStoreData)result;
    //}
    
    //菜品
    public static void SetDishData(int index, PlayerDishData data)
    {
        SetValue("Dish" + index, SerializeObjToStr(data));
    }
    public static PlayerDishData GetDishData(int index)
    {
        object result = DeserializeStrToObj(PlayerPrefs.GetString("Dish" + index));
        if (result == null)
            return new PlayerDishData();
        return (PlayerDishData)result;
    }

    //货架
    public static void SetShelfData(int index, PlayerShelfData data)
    {
        SetValue("Shelf" + index, SerializeObjToStr(data));
    }
    public static PlayerShelfData GetShelfData(int index)
    {
        object result = DeserializeStrToObj(PlayerPrefs.GetString("Shelf" + index));
        if (result == null)
            return new PlayerShelfData();
        return (PlayerShelfData)result;
    }

    //顾客
    public static void SetCustomerData(List<PlayerCustomerData> data)
    {
        SetValue("CustomerList", SerializeObjToStr(data));
    }
    public static List<PlayerCustomerData> GetCustomerData()
    {
        object result = DeserializeStrToObj(PlayerPrefs.GetString("CustomerList"));
        if (result == null)
            return new List<PlayerCustomerData>();
        return (List<PlayerCustomerData>)result;
    }
    #endregion

    #region 各种任务成就相关数据
    //增加成就的值（默认+1）和等级（只能+1）
    public static void AddArchievementData(int index, int value = 1)
    {
        SetItemData(CONST.ARCHIEVEMENT + index, GetArchievementData(index) + value);
    }
    public static void AddArchievementLevel(int index)
    {
        SetItemData(CONST.ARCHIEVEMENT_LEVEL + index, GetArchievementLevel(index) + 1);
    }
    //获得成就的值
    public static int GetArchievementData(int index)
    {
        return GetItemData(CONST.ARCHIEVEMENT + index);
    }
    //获得成就的等级
    public static int GetArchievementLevel(int index)
    {
        return GetItemData(CONST.ARCHIEVEMENT_LEVEL + index);
    }
    //获得成就当前等级增益的值
    public static int GetArchievementRewardValue(int index)
    {
        return GameData.achievements.Find(delegate (GameAchievementData data) 
        { return data.id == index; }).rewardParams[GetArchievementLevel(index)];
    }

    //设置任务数据
    public static void SetTaskData(PlayerTaskData data)
    {
        SetValue("Task", SerializeObjToStr(data));
    }
    //获取任务数据
    public static PlayerTaskData GetTaskData()
    {
        object result = DeserializeStrToObj(PlayerPrefs.GetString("Task"));
        if (result == null)
            return new PlayerTaskData();
        return (PlayerTaskData)result;
    }
    #endregion

    #region 辅助功能：设置数据，打包/加密，序列化反序列化等
    public static void DeleteAll()
    {
        PlayerPrefs.DeleteAll();
        intKeys = new List<string>();
        stringKeys = new List<string>();
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
            if (value.GetType() == typeof(string))
                PlayerPrefs.SetString(key, value.ToString());
            else
                PlayerPrefs.SetString(key, SerializeObjToStr(value));
        }
    }

    static List<string> intKeys = new List<string>();//保存所有int型的key
    static List<string> stringKeys = new List<string>();//保存所有string型的key

    //序列化和反序列化结构体
    public static string SerializeObjToStr(object obj)
    {
        string result = "";
        try
        {
            MemoryStream memoryStream = new MemoryStream();
            BinaryFormatter binaryFormatter = new BinaryFormatter();
            binaryFormatter.Serialize(memoryStream, obj);
            result = Convert.ToBase64String(memoryStream.ToArray());
        }
        catch
        {
            LogCenter.ShowLog("SerializeObjToStr Error, object is " + obj.ToString());
        }
        return result;
    }
    public static object DeserializeStrToObj(string serializedStr)
    {
        object deserializedObj = null;
        try
        {
            if (string.IsNullOrEmpty(serializedStr))
                return null;
            byte[] restoredBytes = System.Convert.FromBase64String(serializedStr);
            MemoryStream restoredMemoryStream = new MemoryStream(restoredBytes);
            BinaryFormatter binaryFormatter = new BinaryFormatter();
            deserializedObj = binaryFormatter.Deserialize(restoredMemoryStream);
        }
        catch
        {
            LogCenter.ShowLog("DeserializeStrToObj Error, serializedStr is " + serializedStr);
        }
        return deserializedObj;
    }

    //压缩所有Player数据
    public static string PackPlayerData()
    {
        string result = "";
        for (int i = 0; i < intKeys.Count; i++)
            result += intKeys[i] + "#" + PlayerPrefs.GetInt(intKeys[i], 0) + "&";
        result += "$";
        for (int i = 0; i < stringKeys.Count; i++)
            result += stringKeys[i] + "#" + PlayerPrefs.GetString(stringKeys[i], " ") + "&";
        return SerializeObjToStr(result);
    }
    //清空当前Player数据，解压传入的压缩Player数据，并应用
    public static void UnPackPlayerData(string str)
    {
        DeleteAll();
        string[] result = ((string)DeserializeStrToObj(str)).Split('$');
        string[] intList = result[0].Split('&');
        foreach(string s in intList)
        {
            string[] value = s.Split('#');
            SetValue(value[0], int.Parse(value[1]));
        }
        string[] strList = result[1].Split('&');
        foreach (string s in strList)
        {
            string[] value = s.Split('#');
            SetValue(value[0], value[1]);
        }
    }

    //加解密int数据
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
            return result > 0 ? result : 0;
        }
        catch
        {
            //TODO 提示数据异常
            return 0;
        }
    }
    #endregion
}

#region 玩家数据结构
[Serializable]
public struct PlayerDishData
{
    public int level;//菜品等级
    public int cardCount;//剩余卡牌数量
    public DateTime startCookingTime;//开始制作时间
}
[Serializable]
public struct PlayerShelfData
{
    public int level;//货架等级
    public int priceIncLevel;//提高售价技能等级
    public int speedIncLevel;//提高制作速度技能等级
    public int stackLevel;//提高堆叠数量技能等级
    public int dishIndex;//摆放的菜品index
}
[Serializable]
public struct PlayerCustomerData
{
    public int index;//顾客编号
    public int mood;//当前心情
    public int money;//当前剩余金币
    public int rate;//购买欲望
    //顾客buff离线时暂不考虑，不做保存处理
}
[Serializable]
public struct PlayerTaskData
{
    public int index;//任务类型
    public int diffcult;//任务难度
    public int boxIndex;//箱子
    public bool isLimit;//是否限时任务
    public List<int> completeValue;//已完成任务数值
    public List<int> requireValue;//任务要求的数值
    public int taskState;//任务状态，0未启动，1进行中，2已完成，3失败
    public DateTime startTime;//限时任务开始时间
    public List<DishType> dishTypes;//任务涉及的菜品种类
}
#endregion