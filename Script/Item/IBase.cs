using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 通用基础接口，绝大部分obj应该继承并使用
/// </summary>
public interface IBase
{
    //初始化
    public void Init(params object[] list);
    //逻辑帧
    public void LogicUpdate();
    //动画帧
    public void AnimationUpdate();
}
