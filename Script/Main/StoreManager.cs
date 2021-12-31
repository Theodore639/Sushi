using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoreManager : MonoBehaviour
{
    public enum StoreSkill
    {       
        AddCustomer = 0,    //招揽一定数量顾客
        DoubleMoney = 1,    //双倍金币持续一定时间
        AddMood = 2,        //全场加心情一定值
        SpeedUpCooking = 3, //全场增加制作速度一定值
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AddPower(int count = 1)
    {
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
