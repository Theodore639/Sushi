using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ChallengeCenter
{
    static PlayerChallengeData playerChallengeData;

    public static void SellDish(BaseDish dish)
    {
        if (playerChallengeData.challengeState != TaskState.Ongoing)
            return;
        if(playerChallengeData.dishType == dish.gameDishData.type)
        {
            playerChallengeData.completePoint += GameData.global.challenge.basePoint[(int)dish.gameDishData.color] + dish.playerDishData.level;
            PlayerData.SetChallengeData(playerChallengeData);
        }
    }

    //需要每日更新
    public static void CreateChallenge()
    {
        playerChallengeData = new PlayerChallengeData();
        playerChallengeData.count = 0;
        playerChallengeData.challengeState = TaskState.Ready;
        playerChallengeData.completePoint = 0;
        playerChallengeData.dishType = (DishType)Random.Range((int)DishType.Cake, (int)DishType.Icecream + 1);
        PlayerData.SetChallengeData(playerChallengeData);
    }

    //开始挑战
    public static void StartChallenge()
    {
        playerChallengeData.challengeState = TaskState.Ongoing;
        playerChallengeData.count++;
        playerChallengeData.completePoint = 0;
        playerChallengeData.startTime = System.DateTime.Now;//时间待统一管理
        PlayerData.SetChallengeData(playerChallengeData);
    }

    //挑战时间到，根据积分计算奖励等级
    public static void ChallengeTimeUp()
    {
        int rewardLevel;
        for(rewardLevel = 0; rewardLevel < GameData.challenge.Count; rewardLevel++)
        {
            if (playerChallengeData.completePoint < GameData.challenge[rewardLevel].point)
                break;
        }

        //奖励rewardLevel级别的内容
    }
}
