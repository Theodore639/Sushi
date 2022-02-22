using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class UIPanelManager
{
    private static UIPanelManager _instance;

    public static UIPanelManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new UIPanelManager();
            }

            return _instance;
        }
    }

    //要求场景中必须有且仅有一个Canvas
    public Transform canvasTransform, uiBgCanvasTransform;

    /// <summary>
    /// 构造函数，常驻panel需要到这里手动注册，注册内容包括panel类型和prefab路径
    /// panelPathDict:保存需要常驻的panel
    /// </summary>
    public Dictionary<Type, string> panelPathDict;

    private List<BasePanel> panelList;

    private UIPanelManager()
    {
        panelList = new List<BasePanel>();
        //需要永久保存的Panel才在这里注册
        panelPathDict = new Dictionary<Type, string>()
        {
            { typeof(MainPanel), "PrefabPanel/MainPanel" },
            
        };
        try
        {
            canvasTransform = GameObject.Find("Canvas").transform;
            uiBgCanvasTransform = GameObject.Find("UIBgCanvas").transform;
        }
        catch
        {
            Debug.Log("Cannot find Canvas GameObject!");
        }
    }

    /// <summary>
    /// 根据panel类型实例化panel，并返回
    /// </summary>
    /// <param name="panelType"></param>
    /// <returns></returns>
    private BasePanel GetPanel(Type panelType)
    {
        BasePanel panel = panelList.Find(delegate(BasePanel p) { return p.GetType() == panelType; });
        if (panel != null)
            return panel;
        string path = "PrefabPanel/" + panelType.Name.ToString();

        Transform panelTransform = GameObject.Instantiate(Resources.Load<GameObject>(path), canvasTransform).transform;
        if (panelTransform == null)
        {
            Debug.Log("UIPanelManager.GetPanel error, the path is incorrect, path=" + path);
            return null;
        }

        return panelTransform.GetComponent<BasePanel>();
    }

    //panel堆栈
    private Stack<BasePanel> panelStack;

    /// <summary>
    /// 实例化永久性Panel
    /// </summary>
    public void InitAllPanel()
    {
        float i = 0;
        foreach (KeyValuePair<Type, string> kv in panelPathDict)
        {
            i += 1;
            GameObject panel = GameObject.Instantiate(Resources.Load<GameObject>(kv.Value), canvasTransform);
            panel.GetComponent<BasePanel>().OnInit();
            panelList.Add(panel.GetComponent<BasePanel>());
        }
    }

    /// <summary>
    /// 根据panel类型实例化panel，并入栈
    /// </summary>
    /// <param name="panelType"></param>
    /// <param name="list">panel初始化的各种参数</param>
    public void PushPanel(Type panelType, params object[] list)
    {
        if (panelStack == null)
        {
            panelStack = new Stack<BasePanel>();
        }

        ////如果是非debug模式，则不push DebugPanel
        //if (panelType == typeof(DebugPanel) && !GameManager.isDebug)
        //    return;

        //停止上一个界面
        if (panelStack.Count > 0)
        {
            BasePanel topPanel = panelStack.Peek();
            topPanel.OnPause();
        }

        BasePanel panel = GetPanel(panelType);
        panelStack.Push(panel);
        panel.OnEnter(list);
    }

    /// <summary>
    /// panel出栈
    /// </summary>
    public void PopPanel()
    {
        if (panelStack == null)
        {
            panelStack = new Stack<BasePanel>();
        }

        //当只有MainPanel的时候不pop
        //if (panelStack.Count <= 0 || GetTopPanel().GetType() == typeof(MainPanel))
        //{
        //    return;
        //}

        //退出栈顶面板
        BasePanel topPanel = panelStack.Pop();
        topPanel.OnExit();
        //SoundManager.Instance.PlayButton2();

        //恢复上一个面板
        if (panelStack.Count > 0)
        {
            BasePanel panel = panelStack.Peek();
            panel.OnResume();
        }
    }

    //隐藏Panel
    public void HidePopPanel()
    {
        if (panelStack == null)
        {
            panelStack = new Stack<BasePanel>();
        }
        //当只有MainPanel的时候不hide
        //if (panelStack.Count <= 0 || GetTopPanel().GetType() == typeof(MainPanel))
        //{
        //    return;
        //}

        BasePanel topPanel = panelStack.Pop();
        topPanel.OnHide();
        //SoundManager.Instance.PlayButton2();

        //恢复上一个面板
        if (panelStack.Count > 0)
        {
            BasePanel panel = panelStack.Peek();
            panel.OnResume();
        }
    }

    //显示已经隐藏的Panel
    public void ShowHiddenPanel(BasePanel panel)
    {
        if (panelStack == null)
        {
            panelStack = new Stack<BasePanel>();
        }
        
        //停止上一个界面
        if (panelStack.Count > 0)
        {
            BasePanel topPanel = panelStack.Peek();
            topPanel.OnPause();
        }

        panel.OnShow();
        panelStack.Push(panel);
    }

    /// <summary>
    /// 返回到MainPanel
    /// </summary>
    public void BackToMainPanel()
    {
        int maxTimes = 10;
        while (GetTopPanel().GetType() != typeof(MainPanel) && maxTimes > 0)
        {
            PopPanel();
            maxTimes--;
        }
    }

    public void Tick()
    {
        foreach(BasePanel panel in panelStack)
        {
            panel.Tick();
        }
    }

    /// <summary>
    /// 获取panel堆栈顶部的panel
    /// </summary>
    /// <returns></returns>
    public BasePanel GetTopPanel()
    {
        if (panelStack == null)
        {
            panelStack = new Stack<BasePanel>();
        }

        if (panelStack.Count <= 0)
        {
            return null;
        }

        return panelStack.Peek();
    }
}