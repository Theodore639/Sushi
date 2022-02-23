using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using I2.Loc;

//显示所有Dish的界面
public class DishPanel : SubPanel
{
    [HideInInspector] public List<int> dishIndexList;
    public Transform dishes;
    public RedPoint redPoint;

    public override void Active()
    {
        base.Active();
        bg.transform.localPosition = Vector3.zero;
        UpdateAll();
    }

    public void UpdateAll()
    {
        dishIndexList = new List<int>();
        redPoint.Hide();
        foreach (GameDishData dishData in GameData.dishes)
        {
            PlayerDishData pDishData = PlayerData.GetDishData(dishData.id);
            if (pDishData.level > 0)
            {
                GameObject dishObj = (GameObject)Instantiate(Resources.Load("PrefabUI/Dish"), dishes);
                dishIndexList.Add(dishData.id);
                dishObj.name = dishData.id.ToString();
            }
        }
        Timer.Schedule(this, 0.1f, () => {
            foreach (int id in dishIndexList)
                UpdateDish(id);
        });
    }

    public void UpdateDish(int index)
    {
        Transform dish = dishes.Find(index.ToString());
        GameDishData gameDishData = GameData.dishes.Find(delegate (GameDishData data) { return data.id == index; });
        PlayerDishData playerDishData = PlayerData.GetDishData(index);
        if (dish == null)
        {
            Debug.Log("DishPanel UpdateDish error, cannot find index " + index.ToString());
            return;
        }
        dish.Find("Name").GetComponent<Text>().text = LocalizationManager.GetTranslation(gameDishData.name);
        dish.Find("Image").GetComponent<Image>().sprite = ResourcesCenter.GetDishSprite(index);
        dish.Find("Level").GetComponent<Text>().text = "Lv " + playerDishData.level.ToString();
        
        dish.Find("Count").GetComponent<RedPoint>().SetParent(redPoint);
        if (playerDishData.cardCount > 0)
        {
            dish.Find("Count").GetComponent<RedPoint>().Show();
            dish.Find("Count/Text").GetComponent<Text>().text = playerDishData.cardCount.ToString();
        }
        else
        {
            dish.Find("Count").GetComponent<RedPoint>().Hide();
            dish.Find("Count/Text").GetComponent<Text>().text = "";
        }
    }

    public void OnClickDish(int index)
    {
        UIPanelManager.Instance.PushPanel(typeof(DishDetailPanel), index);
    }


    public override void Deactive()
    {
        base.Deactive();
        bg.transform.localPosition = new Vector3(-1800, 0, 0);
    }
}
