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
    [HideInInspector] public PlayerSettingData settingData;//玩家缓存的游戏数据，避免频繁读取

    private float logicTime, animationTime, tickTime;
    public const float LOGIC_FRAME_TIME = 0.1f, ANIMATION_FRAME_TIME = 0.033f, TICK_TIME = 1;

    private void Awake()
    {
        
    }

    //游戏入口
    void Start()
    {
        frame = 0;
        gameTime = 0;
        logicTime = 0;
        animationTime = 0;
        tickTime = 0;

        StartCoroutine(Prepare());
    }

    //主循环
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
                //逻辑帧，用于游戏核心逻辑循环
                if (logicTime >= LOGIC_FRAME_TIME)
                {
                    logicTime -= LOGIC_FRAME_TIME;
                    StoreManager.Instance.LogicUpdate();
                }
                //表现帧，用于游戏动画循环
                if (animationTime >= ANIMATION_FRAME_TIME)
                {
                    animationTime -= ANIMATION_FRAME_TIME;
                    StoreManager.Instance.AnimationUpdate();
                }
                //心跳帧，主要用于红点判断，UI更新
                if (tickTime >= TICK_TIME)                        
                {
                    tickTime -= TICK_TIME;
                    UIPanelManager.Instance.Tick();
                }
            }
        }

    }

    //游戏进入准备
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
        Instantiate(Resources.Load<GameObject>("PrefabObj/SoundManager"), transform);//音效管理
        yield return 0;

        //初始化各种第三方SDK，Admob、FB、Pangel、IAP等

        //实例化商店场景
        Instantiate(Resources.Load<GameObject>("Store"));
        LoadingPanel.Instance.SetProcess(45, I2.Loc.LocalizationManager.GetTranslation("C_Loading_01"));
        yield return 0;

        //判断是否首次进入，是则配置首次进入数据
        if (PlayerData.Level == 0)
        {
            StoreManager.Instance.StoreUpgrade();
            //TODO 玩家初始配置及登陆信息，待完善
            PlayerSettingData data = new PlayerSettingData();
            data.musicSwitch = true;
            data.soundSwitch = true;
            //data.language = ??
            data.notifySwitch = true;
            //data.userName = ??
            data.loginInfo = 0;
            PlayerData.SetSettingData(data);
        }
        yield return 0;

        //初始化商店场景
        StoreManager.Instance.Init();
        LoadingPanel.Instance.SetProcess(75, I2.Loc.LocalizationManager.GetTranslation("C_Loading_01"));
        yield return 0;

        //TODO 从Loading界面到主界面的转场动画

        //加载主界面
        UIPanelManager.Instance.PopPanel();
        UIPanelManager.Instance.PushPanel(typeof(MainPanel));
        yield return 0;
        isPrepareDone = true;
    }


}
