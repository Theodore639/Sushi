using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class LoadingPanel : BasePanel
{
    private static LoadingPanel instance;
    public static LoadingPanel Instance
    {
        get
        {
            if (!instance)
            {
                instance = FindObjectOfType(typeof(LoadingPanel)) as LoadingPanel;
            }

            return instance;
        }
    }

    public Scrollbar scrollbar;
    public Text str, value;

    public override void OnEnter(params object[] list)
    {
        base.OnEnter(list);

    }

    public void SetProcess(int _value, string _str)
    {
        scrollbar.size = _value / 100.0f;
        value.text = _value + "%";
        str.text = _str;
    }

}
