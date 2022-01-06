using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseCustomer : MonoBehaviour
{
    public int seed;//�������
    public int index;//�˿ͱ��
    public int food;//ʳ��
    public int water;//ˮ��
    public int mood;//����

    private int maxFood;//ʳ������
    private int maxWater;//ˮ������
    private const int maxMood = 10;//��������
    public CustomerState state;
    public float buyRate;//�����Ʒ�ĸ���

    public enum CustomerState
    {
        LookFor = 0,//Ѱ��ĳ����Ʒ��(�Ҳ����й�)
        OnGoing = 1,//ǰ��ĳ��������
        Buying = 2,//���ڸû��ܹ�����Ʒ
        HangOut = 3,//�������й�
        Leaving = 4,//������ϣ��뿪�̵���
    }

    public void OnInitCustomer(int _seed)
    {
        seed = _seed;
        buyRate = 1.0f;
        //ͨ��seed�Ӽ���index��

        //ͨ��index����GameData��ȡʳ���ˮ�����ޣ���ֵ

        //ͨ�����гɾ���ֵ��ʼ��mood

        //�˿ͽ���
        state = CustomerState.LookFor;
    }

    public void DestroySelf()
    {

        Destroy(this);
    }

    public void BuyDish()
    {

    }
    
}
