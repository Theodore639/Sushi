using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class BaseDish : MonoBehaviour, IBase
{
    public GameDishData dishData;
    public int currentlevel;//当前等级
    public DateTime finishTime;//预期制作完成的时间

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
