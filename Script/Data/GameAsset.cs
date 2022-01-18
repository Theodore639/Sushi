using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//¶ÁÈ¡AssetµÄÁÙÊ±ÈİÆ÷
public class GameAsset : ScriptableObject
{
    public GlobalData global;
    public List<GameDishData> dishes;
    public List<GameEquipData> equips;
    public List<GameBoxData> boxes;
    public List<GameLevelData> levels;
    public List<GameCustomerData> customers;
    public List<GameSkillData> skills;
}
