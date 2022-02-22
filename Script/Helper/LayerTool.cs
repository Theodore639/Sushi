using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class LayerTool
{
    /// <summary>
    /// 通过位置设置sortingOrder
    /// </summary>
    /// <param name="go"></param>
    /// <param name="offset">偏移</param>
    /// <returns></returns>
    public static int SetOrderByPosition(GameObject go, int offset = 0)
    {
        if (go.GetComponent<Renderer>() == null)
        {
            Debug.Log("SetLayerByPosition error, the GameObjet " + go.name + " has no SpriteRenderer Component");
        }

        var order = GetOrder(go, offset);
        go.GetComponent<Renderer>().sortingOrder = order;
        return order;
    }

    /// <summary>
    /// 获取sortingOrder
    /// </summary>
    /// <param name="go"></param>
    /// <param name="offset">偏移</param>
    /// <returns></returns>
    public static int GetOrder(GameObject go, int offset)
    {
        float y = go.transform.position.y;

        return GetOrder(y, offset);
    }

    /// <summary>
    /// 通过位置设置sortingOrder（包含子对象）
    /// </summary>
    /// <param name="list"></param>
    /// <param name="positionY"></param>
    /// <param name="offset">偏移</param>
    public static void SetOrderByPosition(List<Renderer> list, float positionY, int offset = 0)
    {
        int order = GetOrder(positionY, offset);
        foreach (var go in list)
        {
            go.sortingOrder = order;
        }
    }

    /// <summary>
    /// 直接设置sortingOrder
    /// </summary>
    /// <param name="list"></param>
    /// <param name="layer"></param>
    public static void SetOrder(List<Renderer> list, int layer)
    {
        foreach (var go in list)
        {
            go.sortingOrder = layer;
        }
    }

    /// <summary>
    /// 直接设置sortingOrder
    /// </summary>
    /// <param name="go"></param>
    /// <param name="layer"></param>
    public static void SetOrder(GameObject go, int layer)
    {
        if (go.GetComponent<Renderer>() == null)
        {
            Debug.Log("SetLayerByPosition error, the GameObjet " + go.name + " has no SpriteRenderer Component");
            return;
        }

        go.GetComponent<Renderer>().sortingOrder = layer;
    }

    /// <summary>
    /// 设置Layer层级,0默认，1甜品阴影，2表情（其他后续讨论增加）
    /// </summary>
    /// <param name="go"></param>
    /// <param name="layerId"></param>
    public static void SetSortingLayer(GameObject go, int layerId)
    {
        if (go.GetComponent<Renderer>() == null)
        {
            Debug.Log("SetSortingLayer error, the GameObjet " + go.name + " has no SpriteRenderer Component");
            return;
        }

        go.GetComponent<Renderer>().sortingOrder = layerId;
    }

    /// <summary>
    /// 获取sortingOrder
    /// </summary>
    /// <param name="positionY"></param>
    /// <param name="offset"></param>
    /// <returns></returns>
    public static int GetOrder(float positionY, int offset = 0)
    {
        //Y值取反扩大10倍，加200防止为负，再乘100使动物位于不同层级，避免其零部件交错显示
        int order = (int) (positionY * -10 + 200) * 100 + offset;
        if (order <= 0) order = 0;
        return order;
    }
    
    /// <summary>
    /// 设置物品的SortingOrder
    /// </summary>
    /// <param name="go"></param>
    /// <param name="offset">偏移量</param>
    /// <param name="useLayerOrder">是否使用游戏对象原本设置的sortorder</param>
    public static void SetAdventureOrderByPosition(GameObject go, int offset = 0,bool useLayerOrder = false)
    {
        if (go.transform.childCount > 0)//递归设置子节点的 LayerOrder
        {
            for (int i = 0; i < go.transform.childCount; i++)
            {
                SetAdventureOrderByPosition(go.transform.GetChild(i).gameObject,offset,useLayerOrder);
            }
        }
        if (go.GetComponent<SpriteRenderer>() == null)
        {
            Debug.Log("SetLayerByPosition error, the GameObjet " + go.name + " has no SpriteRenderer Component");
            return;
        }
        var render = go.GetComponent<SpriteRenderer>();
        var positonY = render.bounds.min.y;
        int oldOrder = 0;
        var order = (int) (positonY * -10 + 200) + offset;
        if (useLayerOrder)
        {
            oldOrder = go.GetComponent<Renderer>().sortingOrder;//原先设置好的order
            order = offset + oldOrder;
        }
        go.GetComponent<Renderer>().sortingOrder = order;
    }
}