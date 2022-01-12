using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseCustomer : MonoBehaviour, IBase
{
    public int seed;//�������
    public int index;//�˿ͱ��
    public int food;//ʳ��
    public int water;//ˮ��
    public int mood;//����

    private int maxFood;//ʳ������
    private int maxWater;//ˮ������
    private const int maxMood = 100;//��������
    public CustomerState state;
    public List<DishType> favorateDishes;//ƫ�õĲ�Ʒ����
    public float buyRate;//�����Ʒ�ĸ���
    private float tempRate;//�����һ����Ʒ�й����ܣ������tempRate

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
        seed = (int)list[0];
        buyRate = 1.0f;
        tempRate = 0;
        //ͨ��seed�Ӽ���index��

        //ͨ��index����GameData��ȡʳ���ˮ�����ޣ���ֵ

        //ͨ�����гɾ���ֵ��ʼ��mood

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
        buyRate -= 0.3f;
        //if(dish.dishData.skillList.Contains())
        //{
            
        //    tempRate = dish.dishData.skillList[0]
        //}
    }

    /// <summary>
    /// ������ʵ��˸���
    /// ��ʼ����100%��ÿ�����һ��ϲ�ü�20%�����˸����͵�ϲ�ò�Ʒ��20%�ӳ���ʧ��
    /// ÿ�������1%��ÿ1��ȱ�ٵļ����Ⱥͼ��ʶȼ�1%
    /// ÿ��1���ˣ���30%
    /// ÿ��1���ˣ�����ò�Ʒ�����Ӹ��ʵļ��ܣ�ֱ�Ӽ��ϸ��ʣ���һ��
    /// </summary>
    /// <returns></returns>
    private float GetBuyRate()
    {
        float result = buyRate;
        result += favorateDishes.Count * 0.2f;
        result += (mood + (maxFood - food) + (maxWater - water)) * 0.01f;
        return result;
    }

    /// <summary>
    /// ����������һ����Ʒ
    /// ����ϲ�á�����ӳɡ��������������������
    /// </summary>
    /// <returns></returns>
    private int GetOrderDishIndex()
    {
        return 0;
    }
}
