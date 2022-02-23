using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FindCustomerPanel : BasePanel
{
    int selectCount;//可供选择顾客数量
    Dictionary<int, PlayerCustomerData> pDataDic;
    List<PlayerCustomerData> selectList;

    public override void OnEnter(params object[] list)
    {
        base.OnEnter(list);
        pDataDic = new Dictionary<int, PlayerCustomerData>();
        //每次可选择顾客，默认为3个，若完成成就17，则有概率多选择1个顾客
        selectCount = 3 + PlayerData.GetArchievementRewardValue(17) > Random.Range(0, 100) ? 1 : 0;
        //可供选择顾客数量，默认为5个，若完成成就15，则多1个顾客供选择
        for (int i = 0; i < 5 + PlayerData.GetArchievementRewardValue(15); i++)
        {
            pDataDic.Add(i, StoreManager.Instance.CreateCustomer());
        }
        //TODO 根据pDataDic的信息显示顾客信息，并显示还可以选择的顾客数量：selectCount
        //TODO 进入动画，顾客以卡牌的形式展示，带有翻转，由小变大，从中间产生，从左到右依次展示动画并排列

    }

    //选择1个顾客
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
        //TODO 退出动画：未选择的顾客淡出，已选择顾客向右飞出；动画播放完之后再执行PopPanel；

        Timer.Schedule(this, 1, () => { UIPanelManager.Instance.PopPanel(); });
        
    }

}
