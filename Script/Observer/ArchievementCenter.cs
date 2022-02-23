using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//编号  成就说明	                成就奖励
//1	    完成成就个数	            一次性钻石
//2	    累计招揽XX个顾客            有概率发现稀有顾客
//3	    累计卖出XX个面包	        售价提高
//5	    累计卖出XX杯果汁	        制作速度提高
//4	    累计卖出XX个寿司	        顾客移动速度提高
//6	    累计卖出XX个冰淇淋	        顾客携带金币提高
//7	    累计打开XX个宝箱	        打开宝箱时间降低
//8	    观看XX次广告	            每次观看广告额外获得钻石
//9	    升到XX级	                招揽能量上限提高
//10	累计获得XX张卡牌	        招揽能量恢复速度提高
//11	使用XX次商店技能	        商店能量恢复速度提高
//12	将XX个货架升到满级	        货架堆叠数量提高
//13	XX个顾客购买10个以上商品	顾客初始好感度提高
//14	完成段位1的每日挑战次数	    顾客移动速度提高
//15	商店里同时有XX个稀有顾客	每次招揽多1个可选择顾客
//16	1分钟内完成任务个数XX	    打开宝箱时间降低
//17	让XX个顾客离开时金币少于10	每次招揽有概率多选择1个顾客
//18	完成任务XX个	            提高每日挑战奖励荣誉点数
public static class ArchievementCenter
{
    //商店升级
    public static void StoreUpgrade(int level)
    {
        GameAchievementData data = GameData.achievements.Find(delegate (GameAchievementData d) { return d.id == 9; });
        foreach (int i in data.requireParams)
            if (level == i)
            {
                PlayerData.AddArchievementData(9);
                break;
            }

    }
    //卖出一个菜品
    public static void SellDish(BaseDish dish)
    {
        switch (dish.gameDishData.type)
        {
            case DishType.Cake:
                PlayerData.AddArchievementData(3);
                break;
            case DishType.Juice:
                PlayerData.AddArchievementData(4);
                break;
            case DishType.Sushi:
                PlayerData.AddArchievementData(5);
                break;
            case DishType.Icecream:
                PlayerData.AddArchievementData(6);
                break;
        }
    }

    //顾客离店
    public static void CustomerLeave(BaseCustomer customer)
    {
        //成就：顾客离开商店时剩余金币小于10
        if (customer.playerCustomerData.money < 10)
            PlayerData.AddArchievementData(17);
        //成就：累计购买商品数量大于等于10
        if (customer.buyCount >= 10)
            PlayerData.AddArchievementData(13);
    }

    //顾客进店
    public static void AddCustomer()
    {
        PlayerData.AddArchievementData(2);

    }

    //完成一个任务
    public static void FinishTask()
    {
        PlayerData.AddArchievementData(18);
    }

    //使用一个商店技能
    public static void UseStoreSkill()
    {
        PlayerData.AddArchievementData(11);
    }

    //打开一个箱子
    public static void OpenBox()
    {
        PlayerData.AddArchievementData(7);
    }

    //观看完成一个广告
    public static void ShowAds()
    {
        PlayerData.AddArchievementData(8);
    }
}
