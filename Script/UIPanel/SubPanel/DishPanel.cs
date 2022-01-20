using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using I2.Loc;

public class DishPanel : SubPanel
{
    [HideInInspector] public List<int> dishIndexList;
    public Transform dishes;
    public RedPoint redPoint;

    public void UpdateAll()
    {
        dishIndexList = new List<int>();
        redPoint.Hide();
        foreach (GameDishData dishData in GameData.dishes)
        {
            if (PlayerData.GetDishCardLevel(dishData.id) > 0)
            {
                GameObject dish = (GameObject)Instantiate(Resources.Load("PrefabUI/Dish"), dishes);
                dishIndexList.Add(dishData.id);
                dish.name = dishData.id.ToString();
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
        GameDishData dishData = GameData.dishes.Find(delegate (GameDishData data) { return data.id == index; });
        if (dish == null)
        {
            Debug.Log("DishPanel UpdateDish error, cannot find index " + index.ToString());
            return;
        }
        dish.Find("Name").GetComponent<Text>().text = LocalizationManager.GetTranslation(dishData.name);
        dish.Find("Image").GetComponent<Image>().sprite = ResourcesCenter.GetDishSprite(index);
        dish.Find("Level").GetComponent<Text>().text = "Lv " + PlayerData.GetDishCardLevel(index).ToString();
        
        dish.Find("Count").GetComponent<RedPoint>().SetParent(redPoint);
        if (PlayerData.GetDishCardCount(index) > 0)
        {
            dish.Find("Count").GetComponent<RedPoint>().Show();
            dish.Find("Count/Text").GetComponent<Text>().text = PlayerData.GetDishCardCount(index).ToString();
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

    public override void Active()
    {
        base.Active();
        bg.transform.localPosition = Vector3.zero;
    }
}
