using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class BaseDish : MonoBehaviour, IBase
{
    public GameDishData dishData;
    public int currentlevel;//��ǰ�ȼ�
    public DateTime finishTime;//Ԥ��������ɵ�ʱ��

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
