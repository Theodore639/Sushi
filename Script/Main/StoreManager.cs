using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

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

    public Transform stroeCanvas, dishParent, customerParent, shelfParent, other;
    public GameStoreData gameStroeData;
    const int MaxX = 4, MaxY = 4;
    Vector3 customerInitPosition = new Vector3(-5, -5, 0);
    public enum StoreSkill
    {
        AddCustomer = 0,    //招揽一定数量顾客
        DoubleMoney = 1,    //双倍金币持续一定时间
        AddMood = 2,        //全场加心情一定值
        SpeedUpCooking = 3, //全场增加制作速度一定值
    }

    public void Init(params object[] list)
    {
        gameStroeData = GameData.store.Find(delegate (GameStoreData sData) { return sData.level == PlayerData.Level; });
        
        for(int i = 0; i < MaxX; i++)
            for(int j = 0; j < MaxX; j++)
            {
                int index = i * MaxX + j;
                PlayerShelfData shelfData = PlayerData.GetShelfData(index);
                if (shelfData.level > 0)
                {
                    GameObject shelf = Instantiate(Resources.Load<GameObject>("PrefabObj/Shelf"), shelfParent);
                    shelf.GetComponent<Shelf>().Init(index, shelfData);
                    shelf.transform.localPosition = new Vector3(j * 2 - 3, i * 2.5f - 3, 0);//更新位置信息
                }
            }
    }

    public void LogicUpdate()
    {
        foreach(Transform child in shelfParent)
        {
            if(child.GetComponent<Shelf>() != null)
                child.GetComponent<Shelf>().LogicUpdate();
        }
        foreach (Transform child in dishParent)
        {
            if (child.GetComponent<BaseDish>() != null)
                child.GetComponent<BaseDish>().LogicUpdate();
        }
        foreach (Transform child in customerParent)
        {
            if (child.GetComponent<BaseCustomer>() != null)
                child.GetComponent<BaseCustomer>().LogicUpdate();
        }
    }

    public void AnimationUpdate()
    {
        foreach (Transform child in shelfParent)
        {
            if (child.GetComponent<Shelf>() != null)
                child.GetComponent<Shelf>().AnimationUpdate();
        }
        foreach (Transform child in dishParent)
        {
            if (child.GetComponent<BaseDish>() != null)
                child.GetComponent<BaseDish>().AnimationUpdate();
        }
        foreach (Transform child in customerParent)
        {
            if (child.GetComponent<BaseCustomer>() != null)
                child.GetComponent<BaseCustomer>().AnimationUpdate();
        }
    }

    #region 店铺自身功能，升级&技能等
    //店铺升级
    public void StoreUpgrade()
    {
        PlayerData.Level++;
        Observer.StoreUpgrade(PlayerData.Level);
        //更新顾客列表
        UpdateCustomerList();
        //增加菜品货架
        gameStroeData = GameData.store.Find(delegate (GameStoreData sData) { return sData.level == PlayerData.Level; });
        if (gameStroeData.shelf.Count > 0)
        {
            for(int i = 0; i < MaxX; i++)
                for(int j = 0; j < MaxY; j++)
                {
                    int index = i * MaxX + j;
                    if (gameStroeData.shelf.Contains(i * MaxX + j))
                    {
                        PlayerShelfData shelfData = PlayerData.GetShelfData(index);
                        shelfData.level = 1;                        
                        PlayerData.SetShelfData(index, shelfData);
                    }
                }
        }

        //弹升级界面
        if(PlayerData.Level > 1)
        {
            UIPanelManager.Instance.PushPanel(typeof(StoreUpgradePanel), gameStroeData);
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
                List<PlayerCustomerData> customerList = new List<PlayerCustomerData>();
                for(int i = 0; i < GameData.global.store.solictCount; i++)
                {
                    customerList.Add(CreateCustomerData());
                }
                StartCoroutine(AddCustomers(customerList));
                break;
            case StoreSkill.DoubleMoney:
                TimeSpan time = new TimeSpan(0, GameData.global.store.doubleMoney, 0);
                AddDoubleMoneyBuff(time);
                break;
            case StoreSkill.AddMood:
                foreach(Transform child in customerParent)
                {
                    child.GetComponent<BaseCustomer>().AddMood(GameData.global.store.addMood);
                }
                break;
            case StoreSkill.SpeedUpCooking:
                break;
        }
        PlayerData.Power -= GameData.global.store.maxPower;
    }

    public void AddDoubleMoneyBuff(TimeSpan time)
    {

    }
    #endregion

    #region 顾客相关
    
    private List<int> normalCustomerList, normalRate;
    private List<int> rareCustomerList, rareRate;

    //随机获得一个顾客index
    private int GetRandomCustomer(bool isRare)
    {
        if (isRare)
            return rareCustomerList[RandomHelper.GetRandomId(rareRate)];
        else
            return normalCustomerList[RandomHelper.GetRandomId(normalRate)];
    }

    //更新普通顾客和稀有顾客列表
    private void UpdateCustomerList()
    {
        normalCustomerList = new List<int>();
        rareCustomerList = new List<int>();
        normalRate = new List<int>();
        rareRate = new List<int>();
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
                    rareRate.Add(cData.rate);
                }
            }
        }
    }

    //生成一个顾客的数据
    public PlayerCustomerData CreateCustomerData()
    {
        PlayerCustomerData pData = new PlayerCustomerData();
        //稀有顾客
        bool isRare = PlayerData.GetArchievementRewardValue(2) > UnityEngine.Random.Range(0, 100);
        //生成顾客编号
        pData.index = GetRandomCustomer(isRare);
        //计算顾客心情，稀有顾客初始拥有更高的心情
        float[] moodRate;
        if(isRare) 
            moodRate = new float[] { 20, 100, 60, 30, 10, 6, 3, 1 };
        else
            moodRate = new float[] { 0, 0, 0, 100, 60, 25, 8, 4 };
        pData.mood = RandomHelper.GetRandomId(moodRate) + PlayerData.GetArchievementRewardValue(13);
        //计算顾客初始金币，稀有顾客金币翻倍
        int customerMoney = gameStroeData.customerMoney;
        pData.money = UnityEngine.Random.Range(customerMoney, customerMoney * 3) * (PlayerData.GetArchievementRewardValue(6) + 100) / 100;
        if (isRare)
            pData.money *= 2;
        //根据顾客心情计算顾客初始购买欲望
        pData.rate = GameData.global.customer.initRate + pData.mood * GameData.global.customer.moodRate;

        return pData;
    }

    //根据顾客数据创建一个顾客实体，并开始进店
    public void CreateCustomer(PlayerCustomerData pData)
    {
        GameObject customer = Instantiate(Resources.Load("PrefabObj/Customer/" + pData.index.ToString()) as GameObject, customerParent);
        customer.transform.localPosition = customerInitPosition;
        customer.GetComponent<BaseCustomer>().Init(pData);
    }

    //多个顾客进店
    public IEnumerator AddCustomers(List<PlayerCustomerData> dataList)
    {
        foreach(PlayerCustomerData data in dataList)
        {
            CreateCustomer(data);
            yield return new WaitForSeconds(0.5f);
        }
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
