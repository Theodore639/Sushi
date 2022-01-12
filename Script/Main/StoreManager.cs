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

    public enum StoreSkill
    {       
        AddCustomer = 0,    //招揽一定数量顾客
        DoubleMoney = 1,    //双倍金币持续一定时间
        AddMood = 2,        //全场加心情一定值
        SpeedUpCooking = 3, //全场增加制作速度一定值
    }

    public List<EquipShelf> shelfList;
    public int curentLevel;

    public void Init(params object[] list)
    {
        shelfList = new List<EquipShelf>();
    }

    public void LogicUpdate()
    {
        foreach(EquipShelf shelf in shelfList)
        {
            shelf.LogicUpdate();
        }
    }

    public void AnimationUpdate()
    {

    }

    public void AddPower(int count = 1)
    {
        if (PlayerData.Power + count > GameData.globalData.power.maxPower)
            count = GameData.globalData.power.maxPower - PlayerData.Power;
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
        PlayerData.Power -= GameData.globalData.power.maxPower;
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
