using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SubPanel : MonoBehaviour
{
    public GameObject bg;
    public bool isShow;
    public MainPanel.MainSubPanel subPanelType;

    //����Panel����MainPanel����ʾ
    public virtual void Active() 
    {
        isShow = true;
    }

    //ȥ����Panel�����أ���ʵ�����
    public virtual void Deactive() 
    {
        isShow = false;
    }

    public virtual void Tick() { }
}
