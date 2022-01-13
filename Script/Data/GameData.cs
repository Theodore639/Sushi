using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public static class GameData
{
    public const int EXP = 0, MONEY = 1, DIAMOND = 2, POWER = 3, SOLICT = 4;
    static public GlobalData global;
    static public List<GameDishData> dishes;
    static public List<GameEquipData> equips;
    static public List<GameBoxData> boxes;
    static public List<GameLevelData> levels;
    static public List<GameCustomerData> customers;
    static public List<GameSkillData> skills;

    public static void InitGameData()
    {
        GameAsset asset = Resources.Load<GameAsset>("GameData");
        global = asset.global;
        dishes = asset.dishes;
        equips = asset.equips;
        boxes = asset.boxes;
        levels = asset.levels;
        customers = asset.customers;
        skills = asset.skills;
    }
}

//读取Asset的临时容器
public class GameAsset : ScriptableObject
{
    public GlobalData global;
    public List<GameDishData> dishes;
    public List<GameEquipData> equips;
    public List<GameBoxData> boxes;
    public List<GameLevelData> levels;
    public List<GameCustomerData> customers;
    public List<GameSkillData> skills;
}


#region Struct
[Serializable]//全局数据
public struct GlobalData
{
    public Dish dish;
    public Power power;
    [Serializable]
    public struct Dish
    {
        public List<int> upgradeCount;//每升一级需要的卡牌数量
        public List<int> maxLevel;//不同颜色菜品的最高等级
        public int upgradePrice; //升级消耗（金币)，每张卡牌
        public int priceInc;//升级提升的售价比例
        public List<int> stackCount;//每一级对应的堆叠数量
    }
    [Serializable]
    public struct Power
    {
        public int solictCount;//招揽顾客技能招揽的个数
        public int addMood;//全场顾客增加心情
        public int doubleMoney;//双倍金币持续时间（分钟）
        public int maxPower;//触发技能需要的能量
    }
}

[Serializable]//设备数据
public struct GameEquipData
{
    public int id;
    public string name;
    public string des;
    public int requireLevel;//需求等级
    public List<int> exp;//购买获得的经验
    public List<int> priceIndex, priceCount;//购买的货币种类和数量
}

[Serializable]//宝箱数据
public struct GameBoxData
{
    public int id;
    public string name;
    public int time;//开箱时间  分钟
    public int rate;
    public int money;
    public int white, green, blue, purple;
    public int goldIndex, goldCount;//限定卡牌的id和数量
}

[Serializable]//菜品数据
public struct GameDishData
{
    public int id;
    public string name;
    public string des;
    public ItemColor color;
    public DishType type;
    public int price;
    public float time;
    public int requireLevel;
    public int food, water, mood;
    public List<int> skillList;
    public int rareValue;    
}

[Serializable]//商店等级数据
public struct GameLevelData
{
    public int id;
    public int exp;
    public int diamond;
    public int solict;
    public int dish;
    public int maxCustomer;
    public int maxVIP;
}

[Serializable]//商店等级数据
public struct GameCustomerData
{
    public int id;
    public string name;
    public int requireLevel;
    public int rate;
    public CustomerType type;
    public int baseMood, maxFood, maxWater;
}

[Serializable]//商店等级数据
public struct GameSkillData
{
    public int id;
    public string name, des;
    public int baseParam;
    public List<int> levelParams;
}

#endregion

#region Enum

//菜品类型
public enum DishType
{
    Cake = 0,
    Juice = 1,
    Sushi = 2,
    Icecream = 3,
    Speacial = 999,
}

//动物类型  
public enum CustomerType
{
    Rabbit = 0,
    Cat = 1,
    Dog = 2,
    Bear = 3,
    Speacial = 4,
}

//颜色等级
public enum ItemColor
{
    White = 0,
    Green = 1,
    Blue = 2,
    Purple = 3,
    Gold = 4,
}

#endregion