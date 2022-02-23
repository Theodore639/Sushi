using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//���  �ɾ�˵��	                �ɾͽ���
//1	    ��ɳɾ͸���	            һ������ʯ
//2	    �ۼ�����XX���˿�            �и��ʷ���ϡ�й˿�
//3	    �ۼ�����XX�����	        �ۼ����
//5	    �ۼ�����XX����֭	        �����ٶ����
//4	    �ۼ�����XX����˾	        �˿��ƶ��ٶ����
//6	    �ۼ�����XX�������	        �˿�Я��������
//7	    �ۼƴ�XX������	        �򿪱���ʱ�併��
//8	    �ۿ�XX�ι��	            ÿ�ιۿ�����������ʯ
//9	    ����XX��	                ���������������
//10	�ۼƻ��XX�ſ���	        ���������ָ��ٶ����
//11	ʹ��XX���̵꼼��	        �̵������ָ��ٶ����
//12	��XX��������������	        ���ܶѵ��������
//13	XX���˿͹���10��������Ʒ	�˿ͳ�ʼ�øж����
//14	��ɶ�λ1��ÿ����ս����	    �˿��ƶ��ٶ����
//15	�̵���ͬʱ��XX��ϡ�й˿�	ÿ��������1����ѡ��˿�
//16	1����������������XX	    �򿪱���ʱ�併��
//17	��XX���˿��뿪ʱ�������10	ÿ�������и��ʶ�ѡ��1���˿�
//18	�������XX��	            ���ÿ����ս������������
public static class ArchievementCenter
{
    //�̵�����
    public static void StoreUpgrade(int level)
    {
        GameAchievementData data = GameData.achievements.Find(delegate (GameAchievementData d) { return d.id == 9; });
        foreach (int i in data.requireParams)
            if (level == i)
            {
                PlayerData.AddArchievementData(9);
                break;
            }

    }
    //����һ����Ʒ
    public static void SellDish(BaseDish dish)
    {
        switch (dish.gameDishData.type)
        {
            case DishType.Cake:
                PlayerData.AddArchievementData(3);
                break;
            case DishType.Juice:
                PlayerData.AddArchievementData(4);
                break;
            case DishType.Sushi:
                PlayerData.AddArchievementData(5);
                break;
            case DishType.Icecream:
                PlayerData.AddArchievementData(6);
                break;
        }
    }

    //�˿����
    public static void CustomerLeave(BaseCustomer customer)
    {
        //�ɾͣ��˿��뿪�̵�ʱʣ����С��10
        if (customer.playerCustomerData.money < 10)
            PlayerData.AddArchievementData(17);
        //�ɾͣ��ۼƹ�����Ʒ�������ڵ���10
        if (customer.buyCount >= 10)
            PlayerData.AddArchievementData(13);
    }

    //�˿ͽ���
    public static void AddCustomer()
    {
        PlayerData.AddArchievementData(2);

    }

    //���һ������
    public static void FinishTask()
    {
        PlayerData.AddArchievementData(18);
    }

    //ʹ��һ���̵꼼��
    public static void UseStoreSkill()
    {
        PlayerData.AddArchievementData(11);
    }

    //��һ������
    public static void OpenBox()
    {
        PlayerData.AddArchievementData(7);
    }

    //�ۿ����һ�����
    public static void ShowAds()
    {
        PlayerData.AddArchievementData(8);
    }
}
