using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public static class GameData
{
    #region GlobalData
    [Serializable]
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

    private static void InitGlobalData()
    {
        globalData = new GlobalData();
        globalData.dish = new GlobalData.Dish();
        globalData.dish.upgradeCount = new List<int>() { 1, 2, 4, 8, 16, 32, 64, 128, 256, 512, 1024, 2048 };//每升一级需要多少张卡牌
        globalData.dish.maxLevel = new List<int>() { 12, 10, 9, 8, 7 };//不同颜色的卡牌最高等级
        globalData.dish.upgradePrice = 100;//每消耗一张卡牌需要的金币
        globalData.dish.priceInc = 20;//每次升级提升的价格比例

        globalData.power = new GlobalData.Power();
        globalData.power.solictCount = 20;//招揽顾客技能招揽的个数
        globalData.power.addMood = 5;//全场顾客增加心情
        globalData.power.doubleMoney = 10;//双倍金币持续时间（分钟）
        globalData.power.maxPower = 200;//触发技能需要的能量
    }
    #endregion
    public static GlobalData globalData;
    public const int EXP = 0, MONEY = 1, DIAMOND = 2, POWER = 3, SOLICT = 4;  

    public static List<GameDishData> items;
    public static List<GameEquipData> equips;
    public static List<GameBoxData> boxes;

    public static void InitGameData()
    {
        InitGlobalData();
    }
}
[Serializable]
public struct GameEquipData
{
    public int id;
    public string name;
    public string des;
    public int requireLevel;//需求等级
    public List<int> exp;//购买获得的经验
    public List<int> opExp;//操作获得的经验
    public List<int> params0, params1;//参数
    public List<int> priceIndex, priceCount;//购买的货币种类和数量
}
[Serializable]
public struct GameBoxData
{
    public int id;
    public string name;
    public int openTime;//开箱时间  分钟
    public int rate;
    public int money;
    public int white, green, blue, purple, gold;
    public int dishIndex, dishCount;//限定卡牌的id和数量
}
[Serializable]
public struct GameDishData
{
    public int id;
    public string name;
    public string des;
    public ItemColor color;
    public DishType type;
    public int price;
    public float time;
    public int food, water, mood;
    public List<int> skillList;
    public int rareValue;    
}

//菜品类型
public enum DishType
{
    Cake = 0,
    Juice = 1,
    Sushi = 2,
    Icecream = 3,
    Speacial = 999,
}

//颜色等级
public enum ItemColor
{
    white = 0,
    green = 1,
    blue = 2,
    purple = 3,
    gold = 4,
}

