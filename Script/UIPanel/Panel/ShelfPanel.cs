using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 货架Panel
/// 功能点1：显示货架上架的菜品，并提供上架下架按钮（加减号表示即可）
/// 功能点2：货架升级，提供货架升级按钮和货架功能解锁按钮
/// 功能点3：点击上架按钮后，下方弹出小panel，显示所有可供上架的菜品
/// </summary>
public class ShelfPanel : BasePanel
{
    public GameObject offButton, onButton;
    public Image dishImage;
    public Text dishLevel, dishTime, dishPrice;



    public void OffDishClick()
    {

    }

    public void OnDishClick()
    {

    }

    public void ShelfUpgradeClick()
    {

    }

    public void ShelfSkillClick(int id)
    {

    }
}
