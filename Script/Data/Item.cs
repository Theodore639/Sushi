using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseItem 
{
    public int index;
    public int seed;//随机种子
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
            //70%概率在0~30%之间，无牌
            if (seed < 700000)
            {
                _weight = RandomHelper.Clamp(min, min + step * 3, RandomHelper.Next(_seed));
            }
            //25%概率在30~60%之间，铜牌
            else if (seed < 950000)
            {
                _weight = RandomHelper.Clamp(min + step * 3, min + step * 6, RandomHelper.Next(_seed));
            }
            //4.5%概率在60~80%之间，银牌
            else if (seed < 995000)
            {
                _weight = RandomHelper.Clamp(min + step * 6, min + step * 8, RandomHelper.Next(_seed));
            }
            //0.5%概率在80~100%之间，金牌
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
