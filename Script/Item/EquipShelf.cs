using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ����
/// �Զ�������Ʒ���ṩ��Ʒ���˿�ѡ��
/// </summary>
public class EquipShelf : MonoBehaviour
{
    public const int MAX_GRID_COUNT = 6;
    public int index;
    public List<BaseDish> dishes;//�ϼܵĲ�Ʒ�б�
    public float priceInc = 1.0f;//ȫ������۰ٷֱ�

    //��ʼ��
    public void OnInit()
    {
        
    }

    public void ShelfUpdate()
    {
        foreach(BaseDish dish in dishes)
        {
            dish.DishUpdate();
        }
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
