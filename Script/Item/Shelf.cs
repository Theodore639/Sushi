using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ����
/// �Զ�������Ʒ���ṩ��Ʒ���˿�ѡ��
/// </summary>
public class Shelf : MonoBehaviour, IBase
{
    public int index;
    [HideInInspector] public PlayerShelfData playerShelfData;
    public BaseDish dish;//�ϼܵĲ�Ʒ

    //��ʼ��
    public void Init(params object[] list)
    {
        index = (int)list[0];
        playerShelfData = (PlayerShelfData)list[1];
    }

    public void LogicUpdate()
    {
        dish.LogicUpdate();
        
    }

    public void AnimationUpdate()
    {
        dish.AnimationUpdate();
    }


    public void OnClick()
    {
        UIPanelManager.Instance.PushPanel(typeof(ShelfPanel), index);
    }

    //�¼ܲ�Ʒ
    public void OffShelfDish()
    {

    }

    //�ϼ�һ����Ʒ
    public void PutOnDish(int dishIndex)
    {

    }

    //����������Ʒ���ۼ�
    public void UpdatePrice()
    {

    }

    //���»�������
    public void UpdateShelfData()
    {

    }
}
