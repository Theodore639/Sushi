using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseItem 
{
    public int index;
    public int seed;//�������
    public int price;
    public float weight;
    public float quality;

    public BaseItem(int _index)
    {
        index = _index;        
        GameItemData iData = GameData.items[index];
        float min = iData.minWeight;
        float max = iData.maxWeight;
        float step = (max - min) / 10;        
        int lucky = 1;
       
        weight = 0;
        float _weight;
        int _seed;
        for(int i = 0; i < lucky + 1; i++)
        {
            _seed = RandomHelper.CreateSeed();
            //70%������0~30%֮�䣬����
            if (seed < 700000)
            {
                _weight = RandomHelper.Clamp(min, min + step * 3, RandomHelper.Next(_seed));
            }
            //25%������30~60%֮�䣬ͭ��
            else if (seed < 950000)
            {
                _weight = RandomHelper.Clamp(min + step * 3, min + step * 6, RandomHelper.Next(_seed));
            }
            //4.5%������60~80%֮�䣬����
            else if (seed < 995000)
            {
                _weight = RandomHelper.Clamp(min + step * 6, min + step * 8, RandomHelper.Next(_seed));
            }
            //0.5%������80~100%֮�䣬����
            else
            {
                _weight = RandomHelper.Clamp(min + step * 8, max, RandomHelper.Next(_seed));
            }
            if(weight < _weight)
            {
                weight = _weight;
                seed = _seed;
            }
        }

       
        weight = ((int)(weight * 100)) / 100.0f;
        price = (int)(weight * iData.price);
    }

}
