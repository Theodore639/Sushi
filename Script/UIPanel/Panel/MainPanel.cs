using System.Collections;
using System;
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

    public Text levelText, expText, moneyText, diamondText, solictText;
    public Scrollbar power;
    public MainSubPanel subPanel;

    public enum MainSubPanel
    {
        Store = 0,//主界面
        Shop = 1,//商城界面
        Dish = 2,//菜品界面
        Box = 3,//宝箱界面
        Setting = 4,//设置界面
    }

    public override void OnEnter(params object[] list)
    {
        base.OnEnter(list);
        subPanel = MainSubPanel.Store;

    }

    public void SetValue(int index, int value)
    {
        switch (index)
        {
            case GameData.EXP:

                break;
            case GameData.MONEY:

                break;
            case GameData.DIAMOND:

                break;
            case GameData.POWER:

                break;
            case GameData.SOLICT:

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
        switch (name)
        {
            case "Shop":
            case "Dish":
            case "Store":
            case "Box":
            case "Setting":
                ChangeSubPanel((MainSubPanel)Enum.Parse(typeof(MainSubPanel), name));
                break;
        }
    }

    private void ChangeSubPanel(MainSubPanel _subPanel)
    {
        subPanel = _subPanel;
        switch (subPanel)
        {
            case MainSubPanel.Store:
                UIPanelManager.Instance.BackToMainPanel();

                break;
            case MainSubPanel.Shop:
                UIPanelManager.Instance.PushPanel(typeof(ShopPanel));
                break;

        }

    }
}

