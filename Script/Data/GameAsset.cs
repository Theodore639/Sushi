using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameAsset : ScriptableObject
{
    public GameData.GlobalData globalData;
    public List<GameDishData> dishes;
    public List<GameEquipData> equips;
    public List<GameBoxData> boxes;

}
