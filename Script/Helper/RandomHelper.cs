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

    /// <summary>
    /// 随机返回一个重量，，越接近max，概率越低
    /// 保留2位小数点
    /// </summary>
    public static float GetRandomWeight(float min, float max)
    {
        float step = (max - min) / 10;        
        float r = Random.Range(0, 1.0f);
        float result = min;
        //90%概率在0~40%之间
        if (r < 0.8f)
        {
            result = Random.Range(min, min + step * 4);
        }
        //15%概率在40~70%之间
        else if (r < 0.95f)
        {
            result = Random.Range(min + step * 4, min + step * 7);
        }
        //4%概率在70~90%之间
        else if (r < 0.99f)
        {
            result = Random.Range(min + step * 7, min + step * 9);
        }
        //1%概率在90~100%之间
        else if (r < 0.95f)
        {
            result = Random.Range(min + step * 9, max);
        }
        return ((int)(result * 100)) / 100.0f;
    }

}