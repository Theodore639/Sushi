using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ����
/// �Զ�������Ʒ���ṩ��Ʒ���˿�ѡ��
/// </summary>
public class EquipShelf : MonoBehaviour, IBase
{
    public const int MAX_GRID_COUNT = 6;
    public int index;
    [HideInInspector] public List<BaseDish> dishes;//�ϼܵĲ�Ʒ�б�
    [HideInInspector] public float priceInc = 1.0f;//ȫ������۰ٷֱ�

    //��ʼ��
    public void Init(params object[] list)
    {
        dishes = new List<BaseDish>();
    }

    public void LogicUpdate()
    {
        foreach(BaseDish dish in dishes)
        {
            dish.LogicUpdate();
        }
    }

    public void AnimationUpdate()
    {

    }


    public void OnClick()
    {
        UIPanelManager.Instance.PushPanel(typeof(ShelfPanel), index);
    }

    //����һ���¸���
    public void UnLockGrid()
    {

    }

    //�¼�һ����Ʒ
    public void OffShelfDish(int gridIndex)
    {

    }

    //�ϼ�һ����Ʒ
    public void PutOnDish(int gridIndex, int dishIndex)
    {

    }

    //����������Ʒ���ۼ�
    public void UpdatePrice()
    {

    }
}
