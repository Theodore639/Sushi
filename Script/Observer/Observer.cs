using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 观察者
/// </summary>
public static class Observer
{
    //顾客进店
    public static void AddCustomer(BaseCustomer customer)
    {

    }

    //顾客离开
    public static void CustomerLeave(BaseCustomer customer)
    {

    }

    //卖出一个菜品
    public static void SellDish(BaseCustomer customer, BaseDish dish)
    {

    }

    //完成一个任务
    public static void FinishTask()
    {

    }

    //使用一个商店技能
    public static void UseStoreSkill(int index)
    {

    }

    //打开一个箱子
    public static void OpenBox(Box.BoxType type)
    {

    }

    //观看完成一个广告
    public static void WatchAds(string position)
    {

    }

    //完成一次内购
    public static void IAPFinish(string position, int value)
    {

    }
}
