using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseCustomer : MonoBehaviour, IBase
{
    public int seed;//随机种子
    public int index;//顾客编号
    public int food;//食物
    public int water;//水分
    public int mood;//心情

    private int maxFood;//食物上限
    private int maxWater;//水分上限
    private const int maxMood = 100;//心情上限
    public CustomerState state;
    public List<DishType> favorateDishes;//偏好的菜品类型
    public float buyRate;//购买菜品的概率
    private float tempRate;//如果上一个菜品有购买技能，则计算tempRate

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
        seed = (int)list[0];
        buyRate = 1.0f;
        tempRate = 0;
        //通过seed从计算index，

        //通过index计算GameData获取食物和水分上限，赋值

        //通过现有成就数值初始化mood

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
        buyRate -= 0.3f;
        //if(dish.dishData.skillList.Contains())
        //{
            
        //    tempRate = dish.dishData.skillList[0]
        //}
    }

    /// <summary>
    /// 计算真实点菜概率
    /// 初始概率100%，每随机到一个喜好加20%，点了该类型的喜好菜品后，20%加成消失。
    /// 每点心情加1%，每1点缺少的饥饿度和饥渴度加1%
    /// 每点1个菜，减30%
    /// 每点1个菜，如果该菜品有增加概率的技能，直接加上概率，仅一次
    /// </summary>
    /// <returns></returns>
    private float GetBuyRate()
    {
        float result = buyRate;
        result += favorateDishes.Count * 0.2f;
        result += (mood + (maxFood - food) + (maxWater - water)) * 0.01f;
        return result;
    }

    /// <summary>
    /// 计算点具体哪一个菜品
    /// 考虑喜好、种类加成、已完成制作数量等因素
    /// </summary>
    /// <returns></returns>
    private int GetOrderDishIndex()
    {
        return 0;
    }
}
