using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

/// <summary>
/// ����
/// </summary>
public class Box : MonoBehaviour
{
    public enum BoxType
    {
        Junior = 1,//���������������ڼ�
        Wood = 2,
        Copper = 3,
        Silver = 4,
        Gold = 5,
        Crystal = 6,
        Diamond = 7,
    }

    public BoxType type;
    public GameBoxData gameBoxData;

    //��ʼ������
    public void InitBox(BoxType _type)
    {
        type = _type;
        gameBoxData = GameData.boxes.Find(delegate (GameBoxData data) { return data.id == (int)type; });
    }

    //���������ӣ�ֱ�Ӷһ���������
    public void AbandonBox()
    {

    }

    //�����ӷŵ��յ�����λ
    public void PutToLocation()
    {

    }

    //��ʼ������
    public void StartOpenBox()
    {

    }

    //���������������ɽ�������һһ����
    public void OpenBox()
    {

    }
    
}
