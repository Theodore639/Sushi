using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class RedPoint : MonoBehaviour
{
    public RedPoint parent;
    public List<RedPoint> children;
    Color hideColor = new Color(1, 1, 1, 0); 

    public void Init()
    {
        
    }

    public void Show()
    {
        parent.Show();
        GetComponent<Image>().color = Color.white;
    }

    public void Hide()
    {
        GetComponent<Image>().color = hideColor;
    }

}
