using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;
    public static GameManager Instance
    {
        get
        {
            if (!instance)
            {
                instance = FindObjectOfType(typeof(GameManager)) as GameManager;
            }

            return instance;
        }
    }
    [HideInInspector] public int frame;
    [HideInInspector] public float gameTime;
    [HideInInspector] public bool isGamePause = false, isPrepareDone = false;

    private float logicTime, animationTime, tickTime;
    public const float LOGIC_FRAME_TIME = 0.1f, ANIMATION_FRAME_TIME = 0.033f, TICK_TIME = 1;

    private void Awake()
    {
        
    }

    // Start is called before the first frame update
    void Start()
    {
        frame = 0;
        gameTime = 0;
        logicTime = 0;
        animationTime = 0;
        tickTime = 0;

        StartCoroutine(Prepare());
    }

    // Update is called once per frame
    void Update()
    {
        frame++;
        if (isPrepareDone)
        {
            gameTime += Time.deltaTime;
            logicTime += Time.deltaTime;
            animationTime += Time.deltaTime;
            tickTime += Time.deltaTime;
            if (!isGamePause)
            {
                //逻辑帧
                if (logicTime >= LOGIC_FRAME_TIME)
                {
                    logicTime -= LOGIC_FRAME_TIME;
                    StoreManager.Instance.LogicUpdate();
                }
                //表现帧
                if (animationTime >= ANIMATION_FRAME_TIME)
                {
                    animationTime -= ANIMATION_FRAME_TIME;
                    //StoreManager.Instance.StoreUpdate();
                }
                //心跳帧
                if (tickTime >= TICK_TIME)                        
                {
                    tickTime -= TICK_TIME;
                    UIPanelManager.Instance.Tick();
                }
            }
        }

    }

    IEnumerator Prepare()
    {
        //初始化PanelManager,加载LoadingPanel        
        UIPanelManager.Instance.InitAllPanel();
        UIPanelManager.Instance.PushPanel(typeof(LoadingPanel)); 
        LoadingPanel.Instance.SetProcess(20, I2.Loc.LocalizationManager.GetTranslation("C_Loading_01"));
        yield return 0;

        //初始化GameData
        GameData.InitGameData();
        LoadingPanel.Instance.SetProcess(35, I2.Loc.LocalizationManager.GetTranslation("C_Loading_01"));
        yield return 0;
        
        //添加核心组件
        gameObject.AddComponent<StoreManager>();        
        yield return 0;

        //初始化商店及场景
        Instantiate(Resources.Load<GameObject>("Store"));
        yield return 0;
        StoreManager.Instance.Init();
        LoadingPanel.Instance.SetProcess(75, I2.Loc.LocalizationManager.GetTranslation("C_Loading_01"));
        yield return 0;

        //加载主界面
        UIPanelManager.Instance.PopPanel();
        UIPanelManager.Instance.PushPanel(typeof(MainPanel));
        yield return 0;
        isPrepareDone = true;

    }


}
