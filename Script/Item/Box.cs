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
        Junior = 100,//初级，仅限新手期间
        Wood = 101,
        Copper = 102,
        Silver = 103,
        Gold = 104,
        Crystal = 105,
        Diamond = 106,
    }

    public BoxType type;
    public int seed;//箱子的随机种子
    public GameBoxData gameBoxData;

    //初始化箱子
    public void InitBox(BoxType _type, int _seed)
    {
        type = _type;
        seed = _seed;
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

    //根据箱子类型和随机种子生成奖励，并一一给出
    public void OpenBox()
    {

    }
    
}
