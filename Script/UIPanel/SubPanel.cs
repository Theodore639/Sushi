using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SubPanel : MonoBehaviour
{
    public GameObject bg;
    public bool isShow;
    public MainPanel.MainSubPanel subPanelType;

    //激活Panel，在MainPanel中显示
    public virtual void Active() 
    {
        isShow = true;
    }

    //去激活Panel，隐藏，但实体存在
    public virtual void Deactive() 
    {
        isShow = false;
    }

    public virtual void Tick() { }
}
