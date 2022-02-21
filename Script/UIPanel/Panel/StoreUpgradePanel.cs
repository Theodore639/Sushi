using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//商店升级界面，显示各种升级奖励
public class StoreUpgradePanel : BasePanel
{
    GameStoreData sData;
    public override void OnEnter(params object[] list)
    {
        base.OnEnter(list);
        sData = (GameStoreData)list[0];

    }
}
