using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

public class BaseDish : MonoBehaviour, IBase
{
    public Text price, count;
    public SpriteRenderer dishSpriteRenderer;
    [HideInInspector] public GameDishData gameDishData;
    [HideInInspector] public PlayerDishData playerDishData;
    [HideInInspector] public int currentlevel;//��ǰ�ȼ�
    [HideInInspector] public int priceInc, timeDec;//�Ǽ۰ٷֱȺͼ�ʱ��ٷֱ�
    [HideInInspector] public int x, y;//����
    [HideInInspector] public DateTime finishTime;//Ԥ��������ɵ�ʱ��

    //��ʼ������PlayerData��ȡ����
    public void Init(params object[] list)
    {
        
    }

    public void LogicUpdate()
    {

    }

    public void AnimationUpdate()
    {

    }

    //����
    public void Upgrade()
    {

    }
}
