using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class TaskCenter
{
    public static void CreateTask()
    {
        GlobalData.Task taskData = GameData.global.task;
        //������������
        List<int> taskRate = new List<int>();
        for(int i = 0; i < GameData.tasks.Count; i++)
        {
            taskRate.Add(GameData.tasks[i].rate);
        }
        GameTaskData gData = GameData.tasks[RandomHelper.GetRandomId(taskRate)];
        PlayerTaskData pData = new PlayerTaskData();
        pData.index = gData.id;
        //�����Ƿ�Ϊ��ʱ�����̵�С�ڵ���3������̶�����ʱ��������ʱ�ͷ���ʱ�ȸ���
        if (PlayerData.Level <= 3)
            pData.isLimit = false;
        else
            pData.isLimit = Random.Range(0, 2) == 0 ? true : false;
        //���������Ѷ�
        pData.diffcult = Random.Range(taskData.intiDiff, taskData.intiDiff + taskData.diffRange) + PlayerData.Level + taskData.perLevelDiff;        
        if (!pData.isLimit && pData.diffcult > taskData.maxDiff)
            pData.diffcult = taskData.maxDiff;
        if (pData.isLimit && pData.diffcult > taskData.limitMaxDiff)
            pData.diffcult = taskData.limitMaxDiff;
        //���㱦��
        if (PlayerData.GetArchievementData(18) <= gData.initBox.Count)
            pData.boxIndex = gData.initBox[PlayerData.GetArchievementData(18)];
        else
        {
            List<int> rate = new List<int>();
            for(int i = 0; i < GameData.boxes.Count; i++)
            {
                rate.Add(GameData.boxes[i].rate);
            }
            pData.boxIndex = GameData.boxes[RandomHelper.GetRandomId(rate)].id;
        }
        //���������ѶȺ����ͣ���������ʵ�������ֵ

    }

    public static void SellDish(BaseDish dish)
    {

    }

    public static void CustomerLeave(BaseCustomer customer)
    {

    }

    public static void AddMood()
    {

    }
}
