using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

public class BaseDish : MonoBehaviour, IBase
{
    public Text price, count;
    public SpriteRenderer dishSpriteRenderer;
    [HideInInspector] public GameDishData gameDishData;
    [HideInInspector] public PlayerDishData playerDishData;
    [HideInInspector] public int currentlevel;//当前等级
    [HideInInspector] public int priceInc, timeDec;//涨价百分比和减时间百分比
    [HideInInspector] public int x, y;//坐标
    [HideInInspector] public DateTime finishTime;//预期制作完成的时间

    //初始化，从PlayerData获取参数
    public void Init(params object[] list)
    {
        
    }

    public void LogicUpdate()
    {

    }

    public void AnimationUpdate()
    {

    }

    //升级
    public void Upgrade()
    {

    }
}
