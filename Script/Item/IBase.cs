using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ͨ�û����ӿڣ����󲿷�objӦ�ü̳в�ʹ��
/// </summary>
public interface IBase
{
    //��ʼ��
    public void Init(params object[] list);
    //�߼�֡
    public void LogicUpdate();
    //����֡
    public void AnimationUpdate();
}
