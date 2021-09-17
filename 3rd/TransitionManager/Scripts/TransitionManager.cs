#region License

//Copyright(c) 2017 Akase Matsuura
//https://github.com/LightGive/TransitionManager

//Permission is hereby granted, free of charge, to any person obtaining a
//copy of this software and associated documentation files (the
//"Software"), to deal in the Software without restriction, including
//without limitation the rights to use, copy, modify, merge, publish, 
//distribute, sublicense, and/or sell copies of the Software, and to
//permit persons to whom the Software is furnished to do so, subject to
//the following conditions:

//The above copyright notice and this permission notice shall be
//included in all copies or substantial portions of the Software.

//THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, 
//EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF
//MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND
//NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE
//LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION
//OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION
//WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.

#endregion

using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using System.Collections;
using LightGive;

/// <summary>
/// 转场管理类
/// At the time of scene loading, it is possible to produce using Image fillMethod and rule images.
/// Also, you can produce directing such as flash
/// </summary>
public class TransitionManager : SingletonMonoBehaviour<TransitionManager>
{
    //Transition
    [SerializeField] private float m_defaultTransDuration = 1.0f;
    [SerializeField] private AnimationCurve m_animCurve = AnimationCurve.Linear(0, 0, 1, 1);


    private int m_texCount = 0;
    private bool m_isTransition = false;
    private bool m_isFlash = false;


    // private CanvasScaler m_baseCanvasScaler;
    public Canvas m_baseCanvas;

    #region Prop

    public float defaultTransDuration
    {
        get { return m_defaultTransDuration; }
    }

    #endregion


    protected override void Awake()
    {
        base.Awake();
        Init();
    }

    void Init()
    {
        CreateCanvas();
    }

    void CreateCanvas()
    {
        if (m_baseCanvas == null)
            m_baseCanvas = gameObject.GetComponent<Canvas>() != null
                ? gameObject.GetComponent<Canvas>()
                : gameObject.AddComponent<Canvas>();

        m_baseCanvas.renderMode = RenderMode.ScreenSpaceOverlay;
        m_baseCanvas.sortingOrder = 999;
        m_baseCanvas.pixelPerfect = false;

        // if (m_baseCanvas.gameObject.GetComponent<CanvasScaler>() != null)
        //     m_baseCanvasScaler = m_baseCanvas.gameObject.GetComponent<CanvasScaler>();
        // else
        //     m_baseCanvasScaler = m_baseCanvas.gameObject.AddComponent<CanvasScaler>();
        //
        // m_baseCanvasScaler.uiScaleMode = CanvasScaler.ScaleMode.ScaleWithScreenSize;
        // m_baseCanvasScaler.referenceResolution = new Vector2(Screen.width, Screen.height);
        // m_baseCanvasScaler.screenMatchMode = CanvasScaler.ScreenMatchMode.Shrink;
        // m_baseCanvasScaler.referencePixelsPerUnit = 100.0f;
    }


    private IEnumerator TransitionAction(UnityAction _act, float _transtime, TransitionInterface transitionInterface)
    {
        if (m_isTransition)
            yield break;

        m_isTransition = true;
        transitionInterface.Init(m_baseCanvas);

        float t = Time.time;
        float lp;
        while (Time.time - t < _transtime)
        {
            lp = m_animCurve.Evaluate((Time.time - t) / _transtime);
            transitionInterface.Direction(lp);
            yield return null;
        }

        _act.Invoke();

        t = Time.time;
        lp = 0.0f;
        m_animCurve = FlipCurve(m_animCurve);

        while (Time.time - t < _transtime)
        {
            lp = m_animCurve.Evaluate((Time.time - t) / _transtime);
            transitionInterface.Direction(lp);
            yield return null;
        }

        m_animCurve = FlipCurve(m_animCurve);

        transitionInterface.End();

        m_isTransition = false;
    }

    private AnimationCurve FlipCurve(AnimationCurve _curve)
    {
        AnimationCurve newCurve = new AnimationCurve();
        for (int i = 0; i < _curve.length; i++)
        {
            Keyframe key = _curve[i];
            key.time = 1f - key.time;
            key.inTangent = key.inTangent * -1f;
            key.outTangent = key.outTangent * -1f;
            newCurve.AddKey(key);
        }

        return newCurve;
    }


    #region LoadScene

    /// <summary>
    /// Load a scene with Transition.
    /// (All default parameter)
    /// </summary>
    /// <param name="_sceneName">Name of scene to load.</param>
    public void LoadScene(string _sceneName)
    {
        StartCoroutine(TransitionAction(
            () => SceneManager.LoadScene(_sceneName),
            m_defaultTransDuration,
            new TransitionImageFill()));
    }

    /// <summary>
    /// Load a scene with Transition.
    /// </summary>
    /// <param name="_sceneName">Name of scene to load.</param>
    /// <param name="_duration">Transition duration.</param>
    public void LoadScene(string _sceneName, float _duration)
    {
        StartCoroutine(TransitionAction(() => SceneManager.LoadScene(_sceneName), _duration,
            new TransitionImageFill()));
    }

    /// <summary>
    /// Load a scene with Transition.
    /// </summary>
    /// <param name="_sceneName">场景名称</param>
    /// <param name="_duration">转场耗时</param>
    /// <param name="transitionInterface">转场方式</param>
    /// <param name="_effectColor">Effect color.</param>
    public void LoadScene(string _sceneName, float _duration, TransitionInterface transitionInterface)
    {
        StartCoroutine(TransitionAction(
            () =>
                SceneManager.LoadScene(_sceneName),
            _duration,
            transitionInterface));
    }

    #endregion

    #region ReLoadScene

    /// <summary>
    /// ReLoad a scene with Transition.
    /// (All default parameter)
    /// </summary>
    public void ReLoadScene()
    {
        StartCoroutine(TransitionAction(
            () =>
                SceneManager.LoadScene(SceneManager.GetActiveScene().name),
            m_defaultTransDuration,
            new TransitionImageFill()));
    }

    /// <summary>
    /// ReLoad a scene with Transition.
    /// </summary>
    /// <param name="_duration">Transition duration.</param>
    public void ReLoadScene(float _duration)
    {
        StartCoroutine(TransitionAction(
            () =>
                SceneManager.LoadScene(SceneManager.GetActiveScene().name),
            _duration,
            new TransitionImageFill()));
    }

    /// <summary>
    /// ReLoad a scene with Transition
    /// </summary>
    /// <param name="_duration">Transition duration.</param>
    /// <param name="iInterface">转场方式</param>
    public void ReLoadScene(float _duration, TransitionInterface iInterface)
    {
        StartCoroutine(TransitionAction(
            () =>
                SceneManager.LoadScene(SceneManager.GetActiveScene().name),
            _duration,
            iInterface));
    }

    #endregion

    /// <summary>
    /// 非常转场
    /// </summary>
    /// <param name="duration"></param>
    /// <param name="iInterface"></param>
    /// <param name="action"></param>
    public void Transition(float duration, TransitionInterface iInterface, UnityAction action)
    {
        StartCoroutine(TransitionAction(
            action,
            duration,
            iInterface));
    }
}