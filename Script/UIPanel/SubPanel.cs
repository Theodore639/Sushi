using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SubPanel : MonoBehaviour
{
    public GameObject bg;
    public bool isActive;
    public MainPanel.MainSubPanel subPanelType;

    public virtual void Active() 
    {
        isActive = true;
    }

    public virtual void Deactive() 
    {
        isActive = false;
    }

    public virtual void Tick() { }
}
