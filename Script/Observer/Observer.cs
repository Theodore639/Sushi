using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �۲���
/// </summary>
public static class Observer
{
    //�̵�����
    public static void StoreUpgrade(int level)
    {
        ArchievementCenter.StoreUpgrade(level);
        StatisticsCenter.StroeUpgrade(level);
    }

    //�˿ͽ���
    public static void AddCustomer(BaseCustomer customer)
    {
        ArchievementCenter.AddCustomer();

    }

    //�˿��뿪
    public static void CustomerLeave(BaseCustomer customer)
    {
        ArchievementCenter.CustomerLeave(customer);
        TaskCenter.CustomerLeave(customer);
    }

    //����һ����Ʒ
    public static void SellDish(BaseDish dish)
    {
        ArchievementCenter.SellDish(dish);
        TaskCenter.SellDish(dish);
        ChallengeCenter.SellDish(dish);
    }

    //���һ������
    public static void FinishTask()
    {
        PlayerData.AddArchievementData(18);
    }

    //ʹ��һ���̵꼼��
    public static void UseStoreSkill(int index)
    {
        ArchievementCenter.UseStoreSkill();
    }

    //��һ������
    public static void OpenBox(Box.BoxType type)
    {
        ArchievementCenter.OpenBox();
    }

    //�ۿ����һ�����
    public static void ShowAds(string position)
    {
        ArchievementCenter.ShowAds();
        StatisticsCenter.ShowAd(position);
    }

    //���һ���ڹ�
    public static void IAPFinish(string position, int value)
    {

    }
}
