using UnityEngine;

namespace LightGive
{
    /// <summary>
    /// 转场方式接口
    /// </summary>
    public interface TransitionInterface
    {
        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="canvas">转场临时画布</param>
        void Init(Canvas canvas);

        /// <summary>
        /// 转场执行中
        /// </summary>
        /// <param name="lp">进度</param>
        void Direction(float lp);

        /// <summary>
        /// 转场结束
        /// </summary>
        void End();
    }
}