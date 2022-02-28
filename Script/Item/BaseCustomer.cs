using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseCustomer : MonoBehaviour, IBase
{
    public PlayerCustomerData playerCustomerData;
    public GameCustomerData gameCustomerData;
    public CustomerState state;
    public int buyCount;//累计购买菜品

    public enum CustomerState
    {
        LookFor = 0,//寻找某个物品中(找不到闲逛)
        OnGoing = 1,//前往某个货架中
        Buying = 2,//正在该货架购买商品
        HangOut = 3,//无意义闲逛
        Leaving = 4,//购买完毕，离开商店中
    }

    public void Init(params object[] list)
    {
        playerCustomerData = (PlayerCustomerData)list[0];
        gameCustomerData = GameData.customers.Find(delegate (GameCustomerData d) { return d.id == playerCustomerData.index; });
        buyCount = 0;
        //顾客进店
        state = CustomerState.LookFor;
    }


    public void LogicUpdate()
    {

    }

    
    public void AnimationUpdate()
    {

    }

    public void DestroySelf()
    {

        Destroy(this);
    }

    public void BuyDish(BaseDish dish)
    {
        playerCustomerData.rate -= GameData.global.customer.buyRate;
        buyCount++;
        //if(dish.dishData.skillList.Contains())
        //{

        //    tempRate = dish.dishData.skillList[0]
        //}
    }

    /// <summary>
    /// 计算点具体哪一个菜品
    /// 先根据偏好随机出种类
    /// 再根据金币数量排除金币不够的菜品
    /// 再根据已完成数量随机选择一个
    /// 限制：顾客不会购买同一个菜品两次
    /// </summary>
    /// <returns></returns>
    private int GetOrderDishIndex()
    {
        return 0;
    }

    //增加心情
    public void AddMood(int value)
    {
        playerCustomerData.mood += value;
    }
}
