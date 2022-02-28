using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// �������ģ����ƴ������񣬹���������ȵ�
///1   ����XX����Ĳ�Ʒ���XX���
///2   ����XX��XX����Ĳ�Ʒ
///3   ��XX���˿����ٹ���3����Ʒ
///4   ��XX���˿����ٹ���5����Ʒ
///5   ��XX���˿����ٹ���7����Ʒ
///6   ��XX���˿��뿪�̵�ʱ����ﵽ6
///7   ��XX���˿��뿪�̵�ʱ����ﵽ8
///8   ��XX���˿��뿪�̵�ʱ�������50
///9   �ۼ�Ϊ�˿�����X������
/// </summary>
public static class TaskCenter
{
    static PlayerTaskData playerTaskData;
    public static void CreateTask()
    {
        GlobalData.Task taskData = GameData.global.task;
        //������������
        List<int> taskRate = new List<int>();
        for(int i = 0; i < GameData.tasks.Count; i++)
        {
            taskRate.Add(GameData.tasks[i].rate);
        }
        GameTaskData gameTaskData = GameData.tasks[RandomHelper.GetRandomId(taskRate)];
        playerTaskData = new PlayerTaskData();
        playerTaskData.index = gameTaskData.id;
        //�����Ƿ�Ϊ��ʱ�����̵�С�ڵ���3������̶�����ʱ��������ʱ�ͷ���ʱ�ȸ���
        if (PlayerData.Level <= 3)
            playerTaskData.isLimit = false;
        else
            playerTaskData.isLimit = Random.Range(0, 2) == 0 ? true : false;
        //��������״̬
        playerTaskData.taskState = playerTaskData.isLimit ? TaskState.Ready : TaskState.Ongoing;
        //���������Ѷ�
        playerTaskData.diffcult = taskData.intiDiff + PlayerData.Level + taskData.perLevelDiff;
        if (playerTaskData.isLimit)
        {
            int limitLevel = Random.Range(0, taskData.limitDiffList.Count);
            playerTaskData.diffcult = (int)(playerTaskData.diffcult * taskData.limitDiffList[limitLevel]);
            playerTaskData.medal = taskData.limitMedal[limitLevel];
        }
        //���㱦��
        if (PlayerData.GetArchievementData(18) <= GameData.global.task.initBox.Count)
            playerTaskData.boxIndex = GameData.global.task.initBox[PlayerData.GetArchievementData(18)];
        else
        {
            List<int> rate = new List<int>();
            for(int i = 0; i < GameData.boxes.Count; i++)
            {
                rate.Add(GameData.boxes[i].rate);
            }
            playerTaskData.boxIndex = GameData.boxes[RandomHelper.GetRandomId(rate)].id;
        }
        //���������ѶȺ����ͣ���������ʵ�������ֵ

        PlayerData.SetTaskData(playerTaskData);
    }

    //��ʼ��ʱ����,�����ʷ����
    public static void StartTask()
    {
        playerTaskData.taskState = TaskState.Ongoing;
        playerTaskData.startTime = System.DateTime.Now;//TODO ʱ��ͳһ����
        for (int i = 0; i < playerTaskData.completeValue.Count; i++)
            playerTaskData.completeValue[i] = 0;
        PlayerData.SetTaskData(playerTaskData);
    }

    //��������Ƿ���ɣ�������ֵ�б仯ʱ����
    public static void CheckIsComplete()
    {
        bool isFinish = true;
        for(int i = 0; i < playerTaskData.requireValue.Count; i++)
        {
            if (playerTaskData.completeValue[i] < playerTaskData.requireValue[i])
                isFinish = false;
            if (playerTaskData.completeValue[i] > playerTaskData.requireValue[i])
                playerTaskData.completeValue[i] = playerTaskData.requireValue[i];
        }
        if (isFinish)
        {
            playerTaskData.taskState = TaskState.Finish;
            Observer.FinishTask();
        }
        PlayerData.SetTaskData(playerTaskData);
    }

    //����ʱ�䵽
    public static void TaskTimeUp()
    {
        playerTaskData.taskState = TaskState.Failure;
        PlayerData.SetTaskData(playerTaskData);
    }

    public static void SellDish(BaseDish dish)
    {
        if (playerTaskData.taskState != TaskState.Ongoing)
            return;
        switch(playerTaskData.index)
        {
            case 1:
                for (int i = 0; i < playerTaskData.dishTypes.Count; i++)
                {
                    if (playerTaskData.dishTypes[i] == dish.gameDishData.type)
                    {
                        playerTaskData.completeValue[i] += dish.finallyPrice;
                        CheckIsComplete();
                    }
                }
                break;
            case 2:
                for(int i = 0; i < playerTaskData.dishTypes.Count; i++)
                {
                    if(playerTaskData.dishTypes[i] == dish.gameDishData.type)
                    {
                        playerTaskData.completeValue[i]++;
                        CheckIsComplete();
                    }
                }
                break;
        }
    }

    public static void CustomerLeave(BaseCustomer customer)
    {
        if (playerTaskData.taskState != TaskState.Ongoing)
            return;
        Dictionary<int, int> buyCountRequire = new Dictionary<int, int>() { { 3, 3 }, { 4, 5 }, { 5, 7 } };
        Dictionary<int, int> moodRequire = new Dictionary<int, int>() { { 6, 6 }, { 7, 8 } };
        switch (playerTaskData.index)
        {
            case 3:
            case 4:
            case 5:
                if (customer.buyCount >= buyCountRequire[playerTaskData.index])
                {
                    playerTaskData.completeValue[0]++;
                    CheckIsComplete();
                }    
                break;
            case 6:
            case 7:
                if (customer.playerCustomerData.mood >= moodRequire[playerTaskData.index])
                {
                    playerTaskData.completeValue[0]++;
                    CheckIsComplete();
                }
                break;
            case 8:
                if (customer.playerCustomerData.money <= 50)
                {
                    playerTaskData.completeValue[0]++;
                    CheckIsComplete();
                }
                break;
        }
    }

    public static void AddMood()
    {
        if (playerTaskData.taskState != TaskState.Ongoing)
            return;
        if (playerTaskData.index == 9)
        {
            playerTaskData.completeValue[0]++;
            CheckIsComplete();
        }
    }
}
