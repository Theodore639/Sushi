using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public static class GameData
{
    static public GlobalData global;
    static public List<GameDishData> dishes;
    static public List<GameShelfData> shelf;
    static public List<GameBoxData> boxes;
    static public List<GameStoreData> store;
    static public List<GameCustomerData> customers;
    static public List<GameAchievementData> achievements;
    static public List<GameTaskData> tasks;

    public static void InitGameData()
    {
        GameAsset asset = Resources.Load<GameAsset>("GameData");
        global = asset.global;
        dishes = asset.dishes;
        shelf = asset.shelf;
        boxes = asset.boxes;
        store = asset.store;
        customers = asset.customers;
        achievements = asset.achievements;
        tasks = asset.tasks;
    }
}

#region Struct
[Serializable]//全局数据
public struct GlobalData
{
    public Dish dish;
    public Store store;
    public Challenge challenge;
    public Customer customer;
    public Task task;

    [Serializable]
    public struct Dish
    {
        public List<int> upgradeCount;//每升一级需要的卡牌数量
        public List<int> upgradePrice; //不同颜色升级消耗，每张卡牌
    }
    [Serializable]
    public struct Store
    {
        public int solictCount;//招揽顾客技能招揽的个数
        public int addMood;//全场顾客增加心情
        public int doubleMoney;//双倍金币持续时间（分钟）
        public int maxPower;//触发技能需要的能量
        public int maxSolict;//最大招揽能量个数
        public int solictTime;//招揽能量基础回复时间（分钟）
        public int incomeVIP;//每个会员每小时收益
        public int incomeMaxTime;//会员收益最大时间（小时）
        public int expPrice; //每多少金币换取1点商店经验       
    }

    [Serializable]
    public struct Challenge
    {
        public int unlockLevel;//挑战解锁等级
        public int basePoint;//每出售一个物品的基础分数
        public List<int> cardCount;//挑战奖励卡牌数量       
        public List<int> requirePoint; //完成挑战需要的分数
    }


    [Serializable]
    public struct Customer
    {
        public int initRate;//顾客初始购买欲望
        public int buyRate;//每买一个商品下降的购买欲望
        public int moodRate;//每1点好感度增加的购买欲望
        public int maxMood;//心情上限
    }

    [Serializable]
    public struct Task
    {
        public int intiDiff;//初始难度
        public int perLevelDiff;//每升一级难度提升
        public int diffRange;//难度波动范围
        public int maxDiff;//非限时任务难度上限
        public int limitMaxDiff;//限时任务难度上限
        public int limitTime;// 限时任务时间（分钟）
    }
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
    public int level;
    public int exp;//升级要求经验
    public int diamond, solict, dish;//升级奖励：钻石、招揽能量、菜品卡牌
    public List<int> shelf;//升级奖励：解锁的货架编号
    public int maxCustomer, maxCustomerPrice;//最大容纳顾客数量，以及升级价格
    public int maxVIP, maxVIPPrice;//最大容纳会员数量，以及升级价格
    public int customerMoney, customerMoneyPrice;//顾客初始金币，以及升级价格
}

[Serializable]//顾客数据
public struct GameCustomerData
{
    public int id;
    public string name;
    public int requireLevel;
    public int rate;
    public int isRare;
    public int bread, juice, sushi, icecream;//不同种类的菜品偏好度
}

[Serializable]//成就数据
public struct GameAchievementData
{
    public int id;
    public string name, des;
    public List<int> requireParams, rewardParams;
}

[Serializable]//货架等级数据
public struct GameShelfData
{
    public int level;
    public int requireLevel;
    public int upgradePrice;
    public int speedInc, speedIncPrice;
    public int stack, stackPrice;
    public int priceInc, priceIncPrice;
}

[Serializable]//货架等级数据
public struct GameTaskData
{
    public int id;
    public string des;
    public int rate;
    public int requireLevel;
    public int minParam, maxParam;
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

//颜色等级
public enum ItemColor
{
    Green = 0,
    Blue = 1,
    Purple = 2,
    Gold = 3,
}

#endregion