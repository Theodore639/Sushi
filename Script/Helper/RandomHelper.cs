using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class RandomHelper
{
    /// <summary>
    /// 乱序排列一个数组
    /// </summary>
    public static void GetDisorderArray(List<int> intArray)
    {
        int min = 1;
        int max = intArray.Count;
        int inx = 0;
        int b;
        while (min != max)
        {
            int r = Random.Range(min++, max);
            b = intArray[inx];
            intArray[inx] = intArray[r];
            intArray[r] = b;
            inx++;
        }
    }

    /// <summary>
    /// 根据概率序列返回一个随机值
    /// </summary>
    /// <param name="rateList"></param>
    /// <returns></returns>
    public static int GetRandomId(float[] rateList)
    {
        float sum = 0;
        for (int i = 0; i < rateList.Length; i++)
        {
            sum += rateList[i];
        }
        float r = Random.Range(0, sum);
        int result = 0;//随机序号
        for (int i = 0; i < rateList.Length; i++)
        {
            r -= rateList[i];
            if (r < 0)
            {
                result = i;
                break;
            }
        }
        return result;
    }
    public static int GetRandomId(List<int> rateList)
    {
        float[] f = new float[rateList.Count];
        for (int i = 0; i < rateList.Count; i++)
            f[i] = rateList[i];
        return GetRandomId(f);
    }

    //根据种子返回一个伪随机数，范围0~999999
    const int MAXSEED = 1000000;
    public static int Next(int seed)
    {
        if (seed % 2 == 0)
            return (seed * 1877 + 132707) % MAXSEED;
        else
            return (seed * 1471 + 329717) % MAXSEED;
    }

    public static int CreateSeed()
    {
        return Random.Range(0, MAXSEED);
    }

}