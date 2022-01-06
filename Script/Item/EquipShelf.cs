using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 货架
/// 自动制作菜品，提供菜品供顾客选择
/// </summary>
public class EquipShelf : MonoBehaviour
{
    public const int MAX_GRID_COUNT = 6;
    public int index;
    public List<BaseDish> dishes;//上架的菜品列表
    public float priceInc = 1.0f;//全货架提价百分比

    //初始化
    public void OnInit()
    {
        
    }

    public void ShelfUpdate()
    {
        foreach(BaseDish dish in dishes)
        {
            dish.DishUpdate();
        }
    }


    public void OnClick()
    {
        UIPanelManager.Instance.PushPanel(typeof(ShelfPanel), index);
    }

    //解锁一个新格子
    public void UnLockGrid()
    {

    }

    //下架一个商品
    public void OffShelfDish(int gridIndex)
    {

    }

    //上架一个商品
    public void PutOnDish(int gridIndex, int dishIndex)
    {

    }

    //更新所有商品的售价
    public void UpdatePrice()
    {

    }
}
