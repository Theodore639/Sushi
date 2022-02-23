using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 观察者
/// </summary>
public static class Observer
{
    //商店升级
    public static void StoreUpgrade(int level)
    {
        ArchievementCenter.StoreUpgrade(level);
        StatisticsCenter.StroeUpgrade(level);
    }

    //顾客进店
    public static void AddCustomer(BaseCustomer customer)
    {
        ArchievementCenter.AddCustomer();

    }

    //顾客离开
    public static void CustomerLeave(BaseCustomer customer)
    {
        ArchievementCenter.CustomerLeave(customer);
        TaskCenter.CustomerLeave(customer);
    }

    //卖出一个菜品
    public static void SellDish(BaseDish dish)
    {
        ArchievementCenter.SellDish(dish);
        TaskCenter.SellDish(dish);
        ChallengeCenter.SellDish(dish);
    }

    //完成一个任务
    public static void FinishTask()
    {
        PlayerData.AddArchievementData(18);
    }

    //使用一个商店技能
    public static void UseStoreSkill(int index)
    {
        ArchievementCenter.UseStoreSkill();
    }

    //打开一个箱子
    public static void OpenBox(Box.BoxType type)
    {
        ArchievementCenter.OpenBox();
    }

    //观看完成一个广告
    public static void ShowAds(string position)
    {
        ArchievementCenter.ShowAds();
        StatisticsCenter.ShowAd(position);
    }

    //完成一次内购
    public static void IAPFinish(string position, int value)
    {

    }
}
