using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//¶ÁÈ¡AssetµÄÁÙÊ±ÈİÆ÷
public class GameAsset : ScriptableObject
{
    public GlobalData global;
    public List<GameDishData> dishes;
    public List<GameShelfData> shelf;
    public List<GameBoxData> boxes;
    public List<GameStoreData> store;
    public List<GameCustomerData> customers;
    public List<GameAchievementData> achievements;
    public List<GameTaskData> tasks;
    public List<GameChallengeData> challenge;
}
