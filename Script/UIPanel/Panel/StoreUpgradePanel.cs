using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//�̵��������棬��ʾ������������
public class StoreUpgradePanel : BasePanel
{
    GameStoreData sData;
    public override void OnEnter(params object[] list)
    {
        base.OnEnter(list);
        sData = (GameStoreData)list[0];

    }
}
