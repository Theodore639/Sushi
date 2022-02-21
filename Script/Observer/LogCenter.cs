using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class LogCenter 
{
    public static void ShowLog(string log, bool isStatic = false)
    {
        Debug.Log(log);
        //上报事件统计
        if(isStatic)
        {

        }
    }
}
