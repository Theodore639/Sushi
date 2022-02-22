using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 货架
/// 自动制作菜品，提供菜品供顾客选择
/// </summary>
public class Shelf : MonoBehaviour, IBase
{
    public int index;
    [HideInInspector] public PlayerShelfData playerShelfData;
    public BaseDish dish;//上架的菜品

    //初始化
    public void Init(params object[] list)
    {
        index = (int)list[0];
        playerShelfData = (PlayerShelfData)list[1];
    }

    public void LogicUpdate()
    {
        dish.LogicUpdate();
        
    }

    public void AnimationUpdate()
    {
        dish.AnimationUpdate();
    }


    public void OnClick()
    {
        UIPanelManager.Instance.PushPanel(typeof(ShelfPanel), index);
    }

    //下架菜品
    public void OffShelfDish()
    {

    }

    //上架一个商品
    public void PutOnDish(int dishIndex)
    {

    }

    //更新所有商品的售价
    public void UpdatePrice()
    {

    }

    //更新货架数据
    public void UpdateShelfData()
    {

    }
}
