using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 任务中心，控制创建任务，管理任务进度等
///1   出售XX种类的菜品获得XX金币
///2   出售XX个XX种类的菜品
///3   让XX个顾客至少购买3个商品
///4   让XX个顾客至少购买5个商品
///5   让XX个顾客至少购买7个商品
///6   让XX个顾客离开商店时心情达到6
///7   让XX个顾客离开商店时心情达到8
///8   让XX个顾客离开商店时金币少于50
///9   累计为顾客增加X点心情
/// </summary>
public static class TaskCenter
{
    static PlayerTaskData playerTaskData;
    public static void CreateTask()
    {
        GlobalData.Task taskData = GameData.global.task;
        //计算任务类型
        List<int> taskRate = new List<int>();
        for(int i = 0; i < GameData.tasks.Count; i++)
        {
            taskRate.Add(GameData.tasks[i].rate);
        }
        GameTaskData gameTaskData = GameData.tasks[RandomHelper.GetRandomId(taskRate)];
        playerTaskData = new PlayerTaskData();
        playerTaskData.index = gameTaskData.id;
        //计算是否为限时任务，商店小于等于3级任务固定不限时，后面限时和非限时等概率
        if (PlayerData.Level <= 3)
            playerTaskData.isLimit = false;
        else
            playerTaskData.isLimit = Random.Range(0, 2) == 0 ? true : false;
        //配置任务状态
        playerTaskData.taskState = playerTaskData.isLimit ? TaskState.Ready : TaskState.Ongoing;
        //计算任务难度
        playerTaskData.diffcult = taskData.intiDiff + PlayerData.Level + taskData.perLevelDiff;
        if (playerTaskData.isLimit)
        {
            int limitLevel = Random.Range(0, taskData.limitDiffList.Count);
            playerTaskData.diffcult = (int)(playerTaskData.diffcult * taskData.limitDiffList[limitLevel]);
            playerTaskData.medal = taskData.limitMedal[limitLevel];
        }
        //计算宝箱
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
        //根据任务难度和类型，计算任务实际需求的值

        PlayerData.SetTaskData(playerTaskData);
    }

    //开始限时任务,清空历史数据
    public static void StartTask()
    {
        playerTaskData.taskState = TaskState.Ongoing;
        playerTaskData.startTime = System.DateTime.Now;//TODO 时间统一处理
        for (int i = 0; i < playerTaskData.completeValue.Count; i++)
            playerTaskData.completeValue[i] = 0;
        PlayerData.SetTaskData(playerTaskData);
    }

    //检查任务是否完成，当任务值有变化时调用
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

    //任务时间到
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
