using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class TaskCenter
{
    public static void CreateTask()
    {
        GlobalData.Task taskData = GameData.global.task;
        //计算任务类型
        List<int> taskRate = new List<int>();
        for(int i = 0; i < GameData.tasks.Count; i++)
        {
            taskRate.Add(GameData.tasks[i].rate);
        }
        GameTaskData gData = GameData.tasks[RandomHelper.GetRandomId(taskRate)];
        PlayerTaskData pData = new PlayerTaskData();
        pData.index = gData.id;
        //计算是否为限时任务，商店小于等于3级任务固定不限时，后面限时和非限时等概率
        if (PlayerData.Level <= 3)
            pData.isLimit = false;
        else
            pData.isLimit = Random.Range(0, 2) == 0 ? true : false;
        //计算任务难度
        pData.diffcult = Random.Range(taskData.intiDiff, taskData.intiDiff + taskData.diffRange) + PlayerData.Level + taskData.perLevelDiff;        
        if (!pData.isLimit && pData.diffcult > taskData.maxDiff)
            pData.diffcult = taskData.maxDiff;
        if (pData.isLimit && pData.diffcult > taskData.limitMaxDiff)
            pData.diffcult = taskData.limitMaxDiff;
        //计算宝箱
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
        //根据任务难度和类型，计算任务实际需求的值

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
