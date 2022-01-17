using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ил╣Й
/// </summary>
public class ShopPanel : MonoBehaviour
{
    public void CreateGoods()
    {
        int seed = RandomHelper.CreateSeed();
        //int lucky = PlayerData.GetItemData(GameData.LUCKY);
    }

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
        PlayerData.AddItemData(GameData.DIAMOND, count);
    }
}
