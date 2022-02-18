using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoreManager : MonoBehaviour, IBase
{
    private static StoreManager instance;
    public static StoreManager Instance
    {
        get
        {
            if (!instance)
            {
                instance = FindObjectOfType(typeof(StoreManager)) as StoreManager;
            }

            return instance;
        }
    }

    public Transform dishParent, customerParen, other;
    [HideInInspector] public BaseDish[,] dishArray;
    public const int MaxX = 5, MaxY = 5;
    public enum StoreSkill
    {
        AddCustomer = 0,    //招揽一定数量顾客
        DoubleMoney = 1,    //双倍金币持续一定时间
        AddMood = 2,        //全场加心情一定值
        SpeedUpCooking = 3, //全场增加制作速度一定值
    }

    public void Init(params object[] list)
    {
        //首次进入初始化操作
        if(PlayerData.Level == 0)
        {
            PlayerData.Level++;
            PlayerData.AddDishCard(101, 1);
            PlayerData.AddDishCard(201, 1);
            StoreUpgrade();
        }
        dishArray = new BaseDish[MaxX, MaxY];
        int[,] dishLocation = PlayerData.GetDishLocation();
        for(int i = 0; i < MaxX; i++)
            for(int j = 0; j < MaxX; j++)
            {
                if(dishLocation[i, j] >= 0)
                {
                    dishArray[i, j] = Instantiate(Resources.Load<GameObject>("PrefabObj/Dish")).GetComponent<BaseDish>();
                    dishArray[i, j].Init(dishLocation[i, j]);
                }
            }

    }

    public void LogicUpdate()
    {
        foreach(BaseDish dish in dishArray)
        {
            if(dish != null)
                dish.LogicUpdate();
        }
    }

    public void AnimationUpdate()
    {

    }

    //店铺升级
    public void StoreUpgrade()
    {
        //增加菜品货架
        GameStoreData data = GameData.store[PlayerData.Level];
        int[,] dishLocation = PlayerData.GetDishLocation();
        if (data.shelf.Count > 0)
        {
            for(int i = 0; i < MaxX; i++)
                for(int j = 0; j < MaxY; j++)
                {
                    if (data.shelf.Contains(i * MaxX + j))
                    {
                        dishLocation[i, j] = 0;
                        dishArray[i, j] = Instantiate(Resources.Load<GameObject>("PrefabObj/Dish")).GetComponent<BaseDish>();
                        dishArray[i, j].Init(dishLocation[i, j]);
                    }
                }
        }
        PlayerData.SetDishLocation(dishLocation);
        //奖励
        PlayerData.Diamond += data.diamond;
        PlayerData.Solict += data.solict;

        //弹升级界面
        if(PlayerData.Level > 1)
        {

        }
    }

    public void AddPower(int count = 1)
    {
        if (PlayerData.Power + count > GameData.global.power.maxPower)
            count = GameData.global.power.maxPower - PlayerData.Power;
        PlayerData.Power += count;
    }

    //使用商店技能
    public void UseStoreSkill(StoreSkill type)
    {
        switch(type)
        {
            case StoreSkill.AddCustomer:
                break;
            case StoreSkill.DoubleMoney:
                break;
            case StoreSkill.AddMood:
                break;
            case StoreSkill.SpeedUpCooking:
                break;
        }
        PlayerData.Power -= GameData.global.power.maxPower;
    }

    //一个顾客进店
    public void AddCustomer(BaseCustomer customer)
    {

    }

    //寻找顾客
    public void FindCustomer(int count)
    {
        for(int i = 0; i < count; i++)
        {

        }
    }
}
