using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ил╣Й
/// </summary>
public class ShopPanel : SubPanel
{
    public void OnBuyDiamondClick(int index)
    {
        int count = 0;
        switch(index)
        {
            case 0:
                count = 60;
                break;
            case 1:
                count = 120;
                break;
            case 2:
                count = 300;
                break;
            case 3:
                count = 900;
                break;
            case 4:
                count = 3000;
                break;
            case 5:
                count = 6000;
                break;
        }
        PlayerData.AddItemData(CONST.DIAMOND, count);
    }

    public override void Deactive()
    {
        base.Deactive();
        bg.transform.localPosition = new Vector3(-3000, 0, 0);
    }

    public override void Active()
    {
        base.Active();
        bg.transform.localPosition = Vector3.zero;
    }
}
