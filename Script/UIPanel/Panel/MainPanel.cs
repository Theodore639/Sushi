using System.Collections;
using System;
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

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
    public Transform subPanelParent;
    [HideInInspector]
    public MainSubPanel subPanel;
    public Dictionary<MainSubPanel, SubPanel> subPanels;

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
        subPanels = new Dictionary<MainSubPanel, SubPanel>();
        subPanel = MainSubPanel.Store;
        //初始化所有子界面
        foreach(Transform child in subPanelParent)
        {
            SubPanel sp = child.GetComponent<SubPanel>();
            if(sp != null)
                subPanels.Add(sp.subPanelType, sp);
            if (sp.subPanelType != subPanel)
                sp.Deactive();
        }
    }

    public override void Tick()
    {
        foreach (KeyValuePair<MainSubPanel, SubPanel> kv in subPanels)
        {
            kv.Value.Tick();
        }
    }

    public void SetValue(int index, int value)
    {
        switch (index)
        {
            case CONST.LEVEL:
                levelText.text = value.ToString();
                break;
            case CONST.EXP:
                StartCoroutine(ISetValue(expText, value, 6));
                break;
            case CONST.MONEY:
                StartCoroutine(ISetValue(moneyText, value, 10));
                break;
            case CONST.DIAMOND:
                StartCoroutine(ISetValue(diamondText, value, 5));
                break;
            case CONST.POWER:
                power.size = value * 1.0f / GameData.global.power.maxPower;
                break;
            case CONST.SOLICT:
                StartCoroutine(ISetValue(diamondText, value, 1));
                break;
            default:
                break;
        }
    }
    //协程设置参数值
    IEnumerator ISetValue(Text text, int value, int steps = 1)
    {
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
        foreach(KeyValuePair<MainSubPanel, SubPanel> kv in subPanels)
        {
            kv.Value.Deactive();
        }
        if(subPanel != MainSubPanel.Store)
            subPanels[subPanel].Active();
        
    }
}

