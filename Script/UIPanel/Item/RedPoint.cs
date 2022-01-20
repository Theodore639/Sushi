using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class RedPoint : MonoBehaviour
{
    public RedPoint parent;
    public List<RedPoint> children;
    public bool isShow;
    Color hideColor = new Color(1, 1, 1, 0); 

    public void CheckChildren()
    {
        bool isHide = true;
        foreach(RedPoint r in children)
        {
            if (r.isShow)
                isHide = false;
        }
        //如果所有子红点均为隐藏状态
        if(isHide)
        {
            Hide();
        }
    }

    public void Show()
    {
        isShow = true;
        if(parent != null)
            parent.Show();        
        GetComponent<Image>().color = Color.white;
    }

    public void Hide()
    {
        isShow = false;        
        if(parent != null)
            parent.CheckChildren();
        GetComponent<Image>().color = hideColor;
    }

    public void SetParent(RedPoint _parent)
    {
        parent = _parent;
        parent.children.Add(this);
    }

}
