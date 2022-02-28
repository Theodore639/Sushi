using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseCustomer : MonoBehaviour, IBase
{
    public PlayerCustomerData playerCustomerData;
    public GameCustomerData gameCustomerData;
    public CustomerState state;
    public int buyCount;//�ۼƹ����Ʒ

    public enum CustomerState
    {
        LookFor = 0,//Ѱ��ĳ����Ʒ��(�Ҳ����й�)
        OnGoing = 1,//ǰ��ĳ��������
        Buying = 2,//���ڸû��ܹ�����Ʒ
        HangOut = 3,//�������й�
        Leaving = 4,//������ϣ��뿪�̵���
    }

    public void Init(params object[] list)
    {
        playerCustomerData = (PlayerCustomerData)list[0];
        gameCustomerData = GameData.customers.Find(delegate (GameCustomerData d) { return d.id == playerCustomerData.index; });
        buyCount = 0;
        //�˿ͽ���
        state = CustomerState.LookFor;
    }


    public void LogicUpdate()
    {

    }

    
    public void AnimationUpdate()
    {

    }

    public void DestroySelf()
    {

        Destroy(this);
    }

    public void BuyDish(BaseDish dish)
    {
        playerCustomerData.rate -= GameData.global.customer.buyRate;
        buyCount++;
        //if(dish.dishData.skillList.Contains())
        //{

        //    tempRate = dish.dishData.skillList[0]
        //}
    }

    /// <summary>
    /// ����������һ����Ʒ
    /// �ȸ���ƫ�����������
    /// �ٸ��ݽ�������ų���Ҳ����Ĳ�Ʒ
    /// �ٸ���������������ѡ��һ��
    /// ���ƣ��˿Ͳ��Ṻ��ͬһ����Ʒ����
    /// </summary>
    /// <returns></returns>
    private int GetOrderDishIndex()
    {
        return 0;
    }

    //��������
    public void AddMood(int value)
    {
        playerCustomerData.mood += value;
    }
}
