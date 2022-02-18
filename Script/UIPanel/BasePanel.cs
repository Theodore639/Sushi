using UnityEngine;

public class BasePanel : MonoBehaviour
{
    public Animator bgAnimator;//用于Panel动画，配置了即播放动画，否则不播放
    const float CloseTime = 0.33f;//关闭动画时间
    protected bool isAllowClose;//是否允许关闭
    const float ShowMinTime = 0.5f;//Panel最少展示时间

    /// <summary>
    /// 初始化方法，仅通过UIPanelManager调用
    /// </summary>
    /// <param name="list"></param>
    public virtual void OnInit()
    {
        gameObject.GetComponent<CanvasGroup>().alpha = 0;
        gameObject.GetComponent<CanvasGroup>().interactable = false;
        gameObject.GetComponent<CanvasGroup>().blocksRaycasts = false;
    }

    /// <summary>
    /// 显示方法，仅通过UIPanelManager调用
    /// </summary>
    /// <param name="list"></param>
    public virtual void OnEnter(params object[] list)
    {
        gameObject.GetComponent<CanvasGroup>().alpha = 1;
        gameObject.GetComponent<CanvasGroup>().interactable = true;
        gameObject.GetComponent<CanvasGroup>().blocksRaycasts = true;
        isAllowClose = false;
        Timer.Schedule(this, ShowMinTime, () => { isAllowClose = true; });
        if (bgAnimator != null)
        {
            bgAnimator.SetBool("isExit", false);
        }
    }

    /// <summary>
    /// 暂停方法，仅通过UIPanelManager调用
    /// </summary>
    public virtual void OnPause()
    {
        if (gameObject.GetComponent<CanvasGroup>() != null)
            gameObject.GetComponent<CanvasGroup>().blocksRaycasts = false;
    }

    /// <summary>
    /// 恢复方法，仅通过UIPanelManager调用
    /// </summary>
    public virtual void OnResume()
    {
        if (gameObject.GetComponent<CanvasGroup>() != null)
            gameObject.GetComponent<CanvasGroup>().blocksRaycasts = true;
    }

    /// <summary>
    /// 退出方法，仅通过UIPanelManager调用
    /// </summary>
    public virtual void OnExit()
    {
        if (UIPanelManager.Instance.panelPathDict.ContainsKey(GetType()))
        {
            gameObject.GetComponent<CanvasGroup>().alpha = 0;
            gameObject.GetComponent<CanvasGroup>().interactable = false;
            gameObject.GetComponent<CanvasGroup>().blocksRaycasts = false;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public virtual void OnCloseClick()
    {
        if (!isAllowClose)
            return;
        if (bgAnimator != null)
        {
            bgAnimator.SetBool("isExit", true);
            Timer.Schedule(this, CloseTime, () => {
                UIPanelManager.Instance.PopPanel();
            });
        }else
            UIPanelManager.Instance.PopPanel();
    }

    /// <summary>
    /// 隐藏方法，仅通过UIPanelManager调用
    /// </summary>
    public virtual void OnHide()
    {
        gameObject.GetComponent<CanvasGroup>().alpha = 0;
        gameObject.GetComponent<CanvasGroup>().interactable = false;
        gameObject.GetComponent<CanvasGroup>().blocksRaycasts = false;
    }

    /// <summary>
    /// 恢复隐藏方法，仅通过UIPanelManager调用
    /// </summary>
    public virtual void OnShow()
    {
        gameObject.GetComponent<CanvasGroup>().alpha = 1;
        gameObject.GetComponent<CanvasGroup>().interactable = true;
        gameObject.GetComponent<CanvasGroup>().blocksRaycasts = true;
    }

    public virtual void HidePanel()
    {
        UIPanelManager.Instance.HidePopPanel();
    }

    public virtual void Tick()
    {

    }
}