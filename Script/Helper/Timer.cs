using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using I2.Loc;
using UnityEngine;
using Object = UnityEngine.Object;

public class Timer
{
    private static MonoBehaviour _behaviour;
    private static DateTime _currentDateTime;
    public delegate void Task();
    private static DateTime GlobalStartTime = new DateTime(1970, 1, 1);

    /// <summary>
    /// 延时调用代码逻辑
    /// </summary>
    /// <param name="monoBehaviour"></param>
    /// <param name="delay"></param>
    /// <param name="task"></param>
    public static void Schedule(MonoBehaviour monoBehaviour, float delay, Task task)
    {
        Timer._behaviour = monoBehaviour;
        Timer._behaviour.StartCoroutine(DoTask(task, delay));
    }
    private static IEnumerator DoTask(Task task, float delay)
    {
        yield return new WaitForSeconds(delay);
        task();
    }
    
    /// <summary>
    /// 获取当前日期
    /// </summary>
    /// <param name="monoBehaviour"></param>
    /// <returns></returns>
    public static DateTime GetCurrentDateTime(MonoBehaviour monoBehaviour)
    {
        // monoBehaviour.StartCoroutine(GetCurrentNetTime());
        return DateTime.Now;
    }

    private IEnumerator GetTime(MonoBehaviour monoBehaviour)
    {
        yield return monoBehaviour.StartCoroutine(GetCurrentNetTime());
    }


    /// <summary>
    /// 获取当前网络时间
    /// </summary>
    /// <returns></returns>
    private static IEnumerator GetCurrentNetTime()
    {
        WWW www = new WWW("http://www.hko.gov.hk/cgi-bin/gts/time5a.pr?a=1");
        yield return www;
        try
        {
            if (!string.IsNullOrEmpty(www.text))
            {
                string timeStr = www.text.Substring(2);
                DateTime time = DateTime.MinValue;
                // Debug.Log(timeStr);
                DateTime startTime = new DateTime(1970, 1, 1, 0, 0, 0);
                time = startTime.AddMilliseconds(Convert.ToDouble(timeStr));
                _currentDateTime = time;
            }
        }
        catch (Exception e)
        {
            _currentDateTime = DateTime.Now;
            Debug.Log(e.ToString());
            // Console.WriteLine(e);
            // throw;
        }
        
    }
    
    /// <summary>
    /// 通过服务器获取时间
    /// </summary>
    /// <returns></returns>
    //public static double GetServerTime () {
    //    //服务器时间世界标准时区的当前时间（比北京时间少8小时），为了兼容老版本数据，服务器时间要加上8小时时间
    //    return GameManager.Instance.curStartServerTime == 0 ? GetCurUtcTimeInSeconds() : GameManager.Instance.curStartServerTime + Time.realtimeSinceStartup;
    //}
    
    public static string getNowToNextDay6AM ()
    {
        var time = "";
        var next = new DateTime(DateTime.Now.Year,DateTime.Now.Month,DateTime.Now.Day + 1,6,0,0);
        var TotalSeconds = (next - DateTime.Now).TotalSeconds;
        var hour = (int) (TotalSeconds / 60 / 60);
        var minute = (int) (TotalSeconds / 60 % 60);
        var second = (int) (TotalSeconds % 60);
        time = Get2Str(hour) + ":" + Get2Str(minute) + ":" + Get2Str(second);
        return time;
    }

    private static string Get2Str(int value)
    {
        if (value > 10)
            return value.ToString();
        else
            return "0" + value.ToString();
    }
    

    public static DateTime ConvertLongToDateTime(long timeStamp)
    {
        //DateTime dtStart = TimeZone.CurrentTimeZone.ToLocalTime(GlobalStartTime);
        DateTime dtStart = TimeZoneInfo.ConvertTime(GlobalStartTime, TimeZoneInfo.Local);
        long lTime = long.Parse(timeStamp + "0000000");
        TimeSpan toNow = new TimeSpan(lTime);
        return dtStart.Add(toNow);
    }

    public static long ConvertDateTimeToLong(DateTime time)
    {
        DateTime startTime = TimeZoneInfo.ConvertTime(GlobalStartTime, TimeZoneInfo.Local);
        return (long)(time - startTime).TotalSeconds;
    }
    
    /// <summary>
    /// 计算时分秒
    /// </summary>
    /// <param name="time">总共多少秒</param>
    /// <returns>返回格式 HH:MM:SS</returns>
    public static string GetTimeString(float time)
    {
        string hour;
        string minutes;
        string seconds;
        string timeStr="";
        float h = Mathf.FloorToInt(time / 3600f);
        if (h<=0)
        {
            
        }else if (h<10)
        {
            hour = h.ToString("00");
            timeStr += hour + ":";
        }
        else
        {
            hour = h.ToString("00");
            timeStr += hour + ":";
        }

        float m = Mathf.FloorToInt(time / 60f - h * 60f);
        if (m<0)
        {
            
        }else if (m<10)
        {
            minutes = m.ToString("00");
            timeStr += minutes + ":";
        }
        else
        {
            minutes = m.ToString("00");
            timeStr += minutes + ":";
        }
        float s = Mathf.FloorToInt(time - m * 60f - h * 3600f);
        if (s<0)
        {
            
        }else if (s<10)
        {
            seconds = s.ToString("00");
            timeStr += seconds;
        }
        else
        {
            seconds = s.ToString("00");
            timeStr += seconds;
        }
        return   timeStr;
    }

    public static string GetMinTime2String(int min)
    {
        var strHour = LocalizationManager.GetTranslation("hour");
        var strMin = LocalizationManager.GetTranslation("min");
        var time = "";
        if (min > 60)
        {
            time = min / 60 + strHour + min % 60 + strMin;
        }
        else
        {
            time = min + strMin;
        }

        var ret  = LocalizationManager.GetTranslation("nee_to_com").Replace("{[time]}", time);
        return ret;
    }
}
