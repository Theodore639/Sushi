using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FindCustomerPanel : BasePanel
{
    int selectCount;//�ɹ�ѡ��˿�����
    Dictionary<int, PlayerCustomerData> pDataDic;
    List<PlayerCustomerData> selectList;

    public override void OnEnter(params object[] list)
    {
        base.OnEnter(list);
        pDataDic = new Dictionary<int, PlayerCustomerData>();
        //ÿ�ο�ѡ��˿ͣ�Ĭ��Ϊ3��������ɳɾ�17�����и��ʶ�ѡ��1���˿�
        selectCount = 3 + PlayerData.GetArchievementRewardValue(17) > Random.Range(0, 100) ? 1 : 0;
        //�ɹ�ѡ��˿�������Ĭ��Ϊ5��������ɳɾ�15�����1���˿͹�ѡ��
        for (int i = 0; i < 5 + PlayerData.GetArchievementRewardValue(15); i++)
        {
            pDataDic.Add(i, StoreManager.Instance.CreateCustomer());
        }
        //TODO ����pDataDic����Ϣ��ʾ�˿���Ϣ������ʾ������ѡ��Ĺ˿�������selectCount
        //TODO ���붯�����˿��Կ��Ƶ���ʽչʾ�����з�ת����С��󣬴��м����������������չʾ����������

    }

    //ѡ��1���˿�
    public void SelectCustomer(int index)
    {
        selectCount--;
        selectList.Add(pDataDic[index]);
        if (selectCount <= 0)
        {           
            StartCoroutine(StoreManager.Instance.AddCustomers(selectList));
            OnCloseClick();            
        }
    }

    public override void OnCloseClick()
    {
        //TODO �˳�������δѡ��Ĺ˿͵�������ѡ��˿����ҷɳ�������������֮����ִ��PopPanel��

        Timer.Schedule(this, 1, () => { UIPanelManager.Instance.PopPanel(); });
        
    }

}
