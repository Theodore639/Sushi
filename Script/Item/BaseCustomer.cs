using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseCustomer : MonoBehaviour
{
    public int seed;//随机种子
    public int index;//顾客编号
    public int food;//食物
    public int water;//水分
    public int mood;//心情

    private int maxFood;//食物上限
    private int maxWater;//水分上限
    private const int maxMood = 10;//心情上限
    public CustomerState state;
    public float buyRate;//购买菜品的概率

    public enum CustomerState
    {
        LookFor = 0,//寻找某个物品中(找不到闲逛)
        OnGoing = 1,//前往某个货架中
        Buying = 2,//正在该货架购买商品
        HangOut = 3,//无意义闲逛
        Leaving = 4,//购买完毕，离开商店中
    }

    public void OnInitCustomer(int _seed)
    {
        seed = _seed;
        buyRate = 1.0f;
        //通过seed从计算index，

        //通过index计算GameData获取食物和水分上限，赋值

        //通过现有成就数值初始化mood

        //顾客进店
        state = CustomerState.LookFor;
    }

    public void DestroySelf()
    {

        Destroy(this);
    }

    public void BuyDish()
    {

    }
    
}
