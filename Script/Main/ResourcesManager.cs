using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ResourcesManager
{
    public static Sprite GetItemSprite(int index, int level = -1)
    {
        string name = index.ToString();
        if (level >= 0)
            name += "_" + level.ToString();
        return Resources.Load<Sprite>("Sprite/ItemSprite/" + name);
    }
}
