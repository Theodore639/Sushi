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

    //��Ҫÿ�ո���
    public static void CreateChallenge()
    {
        playerChallengeData = new PlayerChallengeData();
        playerChallengeData.count = 0;
        playerChallengeData.challengeState = TaskState.Ready;
        playerChallengeData.completePoint = 0;
        playerChallengeData.dishType = (DishType)Random.Range((int)DishType.Cake, (int)DishType.Icecream + 1);
        PlayerData.SetChallengeData(playerChallengeData);
    }

    //��ʼ��ս
    public static void StartChallenge()
    {
        playerChallengeData.challengeState = TaskState.Ongoing;
        playerChallengeData.count++;
        playerChallengeData.completePoint = 0;
        playerChallengeData.startTime = System.DateTime.Now;//ʱ���ͳһ����
        PlayerData.SetChallengeData(playerChallengeData);
    }

    //��սʱ�䵽�����ݻ��ּ��㽱���ȼ�
    public static void ChallengeTimeUp()
    {
        int rewardLevel;
        for(rewardLevel = 0; rewardLevel < GameData.challenge.Count; rewardLevel++)
        {
            if (playerChallengeData.completePoint < GameData.challenge[rewardLevel].point)
                break;
        }

        //����rewardLevel���������
    }
}
