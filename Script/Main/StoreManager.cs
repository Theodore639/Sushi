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

    public Transform dishParent, customerParent, other;

    [HideInInspector] public Shelf[,] shelves;
    public const int MaxX = 4, MaxY = 4;
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
            //PlayerData.AddDishCard(101, 1);
            //PlayerData.AddDishCard(201, 1);
            StoreUpgrade();
        }
        shelves = new Shelf[MaxX, MaxY];
        for(int i = 0; i < MaxX; i++)
            for(int j = 0; j < MaxX; j++)
            {
                int index = i * MaxX + j;
                PlayerShelfData shelfData = PlayerData.GetShelfData(index);
                if (shelfData.level > 0)
                {
                    shelves[i, j] = Instantiate(Resources.Load<GameObject>("PrefabObj/Shelf")).GetComponent<Shelf>();
                    shelves[i, j].Init(index, shelfData);
                }
            }

    }

    public void LogicUpdate()
    {
        foreach(Shelf dish in shelves)
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

        if (data.shelf.Count > 0)
        {
            for(int i = 0; i < MaxX; i++)
                for(int j = 0; j < MaxY; j++)
                {
                    int index = i * MaxX + j;
                    if (data.shelf.Contains(i * MaxX + j))
                    {
                        PlayerShelfData shelfData = PlayerData.GetShelfData(index);
                        shelfData.level = 1;
                        shelves[i, j] = Instantiate(Resources.Load<GameObject>("PrefabObj/Shelf")).GetComponent<Shelf>();
                        shelves[i, j].Init(index, shelfData);
                    }
                }
        }
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
        if (PlayerData.Power + count > GameData.global.store.maxPower)
            count = GameData.global.store.maxPower - PlayerData.Power;
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
        PlayerData.Power -= GameData.global.store.maxPower;
    }

    //一个顾客进店
    public void AddCustomer(BaseCustomer customer)
    {

    }

    //手动招揽顾客
    public void SolictCustomer()
    {
        UIPanelManager.Instance.PushPanel(typeof(FindCustomerPanel));
    }

    /// <summary>
    /// 自动招揽逻辑：
    /// 只有店里的顾客少于一定比例才会自动招揽，详细待设计
    /// </summary>
    public void AutoSolict()
    {

    }
}
