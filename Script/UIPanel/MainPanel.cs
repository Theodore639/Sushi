using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainPanel : BasePanel
{
    private static MainPanel instance;

    public static MainPanel Instance
    {
        get
        {
            if (!instance)
            {
                instance = FindObjectOfType(typeof(MainPanel)) as MainPanel;
            }

            return instance;
        }
    }

    public Button warehouse, shop;
    public Text expText, goldText, cloverText, powerText;

    public void SetValue(int index, int value)
    {
        switch(index)
        {
            case GameData.EXP:

                break;
            case GameData.MONEY:

                break;
            case GameData.DIAMOND:

                break;
            case GameData.POWER:

                break;
        }
    }
    IEnumerator ISetValue(Text text, int value)
    {
        int steps = 6;
        int cValue = int.Parse(text.text);
        int step = (value - cValue) / steps;
        for (int i = 1; i <= steps; i++)
        {
            text.text = (cValue + step * i).ToString();
            yield return new WaitForSeconds(0.06f);
        }
        text.text = value.ToString();
    }

    public void OnButtonClick(string name)
    {
        switch(name)
        {
            case "shop":
                UIPanelManager.Instance.PushPanel(typeof(ShopPanel));
                break;
        }
    }

    private void ChangeScene()
    {

    }

}
