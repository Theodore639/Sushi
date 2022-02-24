using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

/// <summary>
/// 箱子
/// </summary>
public class Box : MonoBehaviour
{
    public enum BoxType
    {
        Junior = 1,//初级，仅限新手期间
        Wood = 2,
        Copper = 3,
        Silver = 4,
        Gold = 5,
        Crystal = 6,
        Diamond = 7,
    }

    public BoxType type;
    public GameBoxData gameBoxData;

    //初始化箱子
    public void InitBox(BoxType _type)
    {
        type = _type;
        gameBoxData = GameData.boxes.Find(delegate (GameBoxData data) { return data.id == (int)type; });
    }

    //放弃该箱子，直接兑换少量奖励
    public void AbandonBox()
    {

    }

    //将箱子放到空的箱子位
    public void PutToLocation()
    {

    }

    //开始开箱子
    public void StartOpenBox()
    {

    }

    //根据箱子类型生成奖励，并一一给出
    public void OpenBox()
    {

    }
    
}
