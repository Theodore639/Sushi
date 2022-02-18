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
        Junior = 100,//���������������ڼ�
        Wood = 101,
        Copper = 102,
        Silver = 103,
        Gold = 104,
        Crystal = 105,
        Diamond = 106,
    }

    public BoxType type;
    public GameBoxData gameBoxData;

    //��ʼ������
    public void InitBox(BoxType _type, int _seed)
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
