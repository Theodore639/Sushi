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
    public GameStoreData stroeData;
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
        stroeData = GameData.store.Find(delegate (GameStoreData sData) { return sData.id == PlayerData.Level; });
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

    #region 店铺自身功能，升级&技能等
    //店铺升级
    public void StoreUpgrade()
    {        
        UpdateCustomerList();

        //增加菜品货架
        GameStoreData data = GameData.store.Find(delegate (GameStoreData sData) { return sData.id == PlayerData.Level; });
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

        //弹升级界面
        if(PlayerData.Level > 1)
        {
            UIPanelManager.Instance.PushPanel(typeof(StoreUpgradePanel), data);
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

    #endregion

    #region 顾客相关
    
    private List<int> normalCustomerList, normalRate;
    private List<int> rareCustomerList, rateRate;

    //随机获得一个顾客index
    private int GetRandomCustomer(bool isRare)
    {
        if (isRare)
            return rareCustomerList[RandomHelper.GetRandomId(rateRate)];
        else
            return normalCustomerList[RandomHelper.GetRandomId(normalRate)];
    }

    //更新普通顾客和稀有顾客列表
    private void UpdateCustomerList()
    {
        normalCustomerList = new List<int>();
        rareCustomerList = new List<int>();
        foreach (GameCustomerData cData in GameData.customers)
        {
            if (cData.requireLevel <= PlayerData.Level)
            {
                if (cData.isRare == 0)
                {
                    normalCustomerList.Add(cData.id);
                    normalRate.Add(cData.rate);
                }
                else
                {
                    rareCustomerList.Add(cData.id);
                    rateRate.Add(cData.rate);
                }
            }
        }
    }

    //生成一个顾客的数据
    public PlayerCustomerData CreateCustomer()
    {
        PlayerCustomerData pData = new PlayerCustomerData();
        //稀有顾客
        bool isRare = PlayerData.GetArchievementRewardValue(2) > Random.Range(0, 100);

        //生成顾客编号、心情、初始金币，计算购买欲望
        pData.index = GetRandomCustomer(isRare);
        pData.mood = Random.Range(0, 3) + PlayerData.GetArchievementRewardValue(13);
        int customerMoney = stroeData.customerMoney;
        pData.money = Random.Range(customerMoney, customerMoney * 3) * (PlayerData.GetArchievementRewardValue(6) + 100) / 100;
        pData.rate = GameData.global.customer.initRate + pData.mood * GameData.global.customer.moodRate;

        return pData;
    }

    //一个顾客进店
    public void AddCustomer(PlayerCustomerData pData)
    {

    }

    //多个顾客进店
    public void AddCustomer(List<PlayerCustomerData> pData)
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

    #endregion
}
