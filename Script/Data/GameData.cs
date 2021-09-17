using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameAsset : ScriptableObject
{
    public List<GameSeafoodData> seafoods;
    public List<GameDishData> dishes;
    public List<GameEquipData> equips;
}

public static class GameData
{
    public static List<GameSeafoodData> seafoods;
    public static List<GameDishData> dishes;
    public static List<GameEquipData> equips;
}

public struct GameSeafoodData
{
    public int id;
    public string name;
    public string des;
    public ItemColor color;
    public int refPrice;//参考价格
    public int requireLevel;//学习加工的要求等级
    public int price;//学习加工的价格
    public float minWeight, maxWeight;//重量区间
    public int stacked;//堆叠数量
    public List<int> sliceItem;//切割后的物品Index
    public List<float> sliceWeight;//切割重量，用于计算切割数量
    public List<float> sliceRate;//切割后产出的物品概率，第一个必为1
    public int depth;//捕获深度
    public int rareValue;//稀有度，影响捕捞数量
}

public struct GameDishData
{
    public int id;
    public string name;
    public string des;
    public ItemColor color;
    public int stacked;//堆叠数量
    public int requireLevel;//需求等级
    public List<int> priceIndex, priceCount;//售价的货币种类和数量
}

public struct GameEquipData
{
    public int id;
    public string name;
    public string des;
    public int requireLevel;//需求等级
    public Location location;//设备方位
    public List<int> exp;//购买获得的经验
    public List<int> opExp;//操作获得的经验
    public List<int> params0, params1;//参数
    public List<int> priceIndex, priceCount;//购买的货币种类和数量
}

public enum ItemColor
{
    white = 0,
    green = 1,
    blue = 2,
    purple = 3,
    orange = 4,
}

public enum Location
{
    Middle = 0,
    Left = 1,
    Right = 2,
    Up = 3,
    Down = 4,
}