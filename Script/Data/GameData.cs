using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public static class GameData
{
    static public GlobalData global;
    static public List<GameDishData> dishes;
    static public List<GameEquipData> equips;
    static public List<GameBoxData> boxes;
    static public List<GameStoreData> store;
    static public List<GameCustomerData> customers;
    static public List<GameAchievementData> achievements;

    public static void InitGameData()
    {
        GameAsset asset = Resources.Load<GameAsset>("GameData");
        global = asset.global;
        dishes = asset.dishes;
        equips = asset.equips;
        boxes = asset.boxes;
        store = asset.store;
        customers = asset.customers;
        achievements = asset.achievements;
    }
}

#region Struct
[Serializable]//全局数据
public struct GlobalData
{
    public Dish dish;
    public Power power;
    public Common common;

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

    [Serializable]
    public struct Common
    {
        public int maxSolict;//最大招揽能量个数
        public int solictTime;//招揽能量基础回复时间（分钟）
        public int incomeVIP;//每个会员每小时收益
        public int incomeMaxTime;//会员收益最大时间（小时）
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
    public int green, blue, purple, gold;//不同颜色卡牌数量
}

[Serializable]//菜品数据
public struct GameDishData
{
    public int id;
    public string name;
    public string des;
    public ItemColor color;
    public DishType type;
    public List<int> priceList;
    public List<int> skillParams;
    public float time;
    public int requireLevel;
    public float mood;
    public int rareValue;    
}

[Serializable]//商店等级数据
public struct GameStoreData
{
    public int id;
    public int exp;
    public int diamond;
    public int solict;
    public int dish;
    public int maxCustomer, maxCustomerPrice;
    public int maxVIP, maxVIPPrice;
    public int customerMoney, customerMoneyPrice;
    public List<int> shelf;
}

[Serializable]//商店等级数据
public struct GameCustomerData
{
    public int id;
    public string name;
    public int requireLevel;
    public int rate;
    public int isRare;
    public int bread, juice, sushi, icecream;//不同种类的菜品偏好度
}

[Serializable]//商店等级数据
public struct GameAchievementData
{
    public int id;
    public string name, des;
    public List<int> requireParams, rewardParams;
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
    All = 1000,
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

//菜品技能作用范围
public enum DishSkillType
{
    BuyCake,
    BuyJuice,
    BuySushi,
    BuyIcecream,
    RowInc,//整排涨价
    ColoumInc,//整列涨价
    GridInc,//3*3格子涨价
    AllInc,//全部涨价
    AllSpeedUp,//全部加速
    CakeInc,//所有蛋糕涨价
    JuiceInc,//所有果汁涨价
    SushiInc,//所有寿司涨价
    IcecreamInc,//所有冰淇淋涨价
    CakeSpeedUp,//所有蛋糕加速
    JuiceSpeedUp,//所有果汁加速
    SushiSpeedUp,//所有寿司加速
    IcecreamSpeedUp,//所有冰淇淋加速
    SelfInc,//自身永久涨价
    ExtraPay,//额外支付金币
    AddCustomer,//额外招揽顾客
    AddPower,//额外加能量
}
#endregion