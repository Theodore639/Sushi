using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameAsset : ScriptableObject
{
    public List<GameItemData> items;
    public List<GameEquipData> equips;
    public List<GameBoxData> boxes;
}

public static class GameData
{
    #region GlobalData
    public struct GolobData
    {
        public Dish dish;
        public Power power;

        public struct Dish
        {
            public List<int> upgradeCount;
            public List<int> maxLevel;
            public int upgradePrice;
            public int priceInc;
        }
        
        public struct Power
        {
            public int solictCount;
            public int addMood;
            public int doubleMoney;
            public int maxPower;
        }
    }

    private static void InitGlobalData()
    {
        globalData = new GolobData();
        globalData.dish = new GolobData.Dish();
        globalData.dish.upgradeCount = new List<int>() { 1, 2, 4, 8, 16, 32, 64, 128, 256, 512, 1024, 2048 };//每升一级需要多少张卡牌
        globalData.dish.maxLevel = new List<int>() { 12, 10, 9, 8, 7 };//不同颜色的卡牌最高等级
        globalData.dish.upgradePrice = 100;//每消耗一张卡牌需要的金币
        globalData.dish.priceInc = 20;//每次升级提升的价格比例

        globalData.power = new GolobData.Power();
        globalData.power.solictCount = 20;//招揽顾客技能招揽的个数
        globalData.power.addMood = 5;//全场顾客增加心情
        globalData.power.doubleMoney = 10;//双倍金币持续时间（分钟）
        globalData.power.maxPower = 200;//触发技能需要的能量
    }
    #endregion
    public static GolobData globalData;
    public const int EXP = 0, MONEY = 1, DIAMOND = 2, POWER = 3, SOLICT = 4;  

    public static List<GameItemData> items;
    public static List<GameEquipData> equips;
    public static List<GameBoxData> boxes;

    public static void InitGameData()
    {
        InitGlobalData();
    }
}

public struct GameItemData
{
    public int id;
    public string name;
    public string des;
    public ItemColor color;
    public int price;
    public float minWeight, maxWeight;//重量区间
    public int rareValue;//稀有度
}

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

public enum ItemColor
{
    white = 0,
    green = 1,
    blue = 2,
    purple = 3,
    gold = 4,
}

