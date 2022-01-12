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

    private float logicTime, animationTime;
    public const float LOGIC_FRAME_TIME = 0.1f, ANIMATION_FRAME_TIME = 0.33f;

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
            }
        }

    }

    IEnumerator Prepare()
    {
        GameData.InitGameData();
        yield return 0;

        gameObject.AddComponent<StoreManager>();
        
        yield return 0;
        StoreManager.Instance.Init();
        yield return 0;
        UIPanelManager.Instance.InitAllPanel();
        //UIPanelManager.Instance.PushPanel(typeof(MainPanel));
        yield return 0;
        isPrepareDone = true;

    }


}
