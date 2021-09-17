using UnityEngine;
using UnityEngine.UI;

namespace LightGive
{
    /// <summary>
    /// 转场方式: 图片填充
    /// </summary>
    public class TransitionImageFill : TransitionInterface
    {
        /// <summary>
        /// 转场Image
        /// </summary>
        private Image m_transImage;

        /// <summary>
        /// 转场Image上Sprite
        /// </summary>
        private Sprite m_transitionSprite;

        /// <summary>
        /// 图片填充方式
        /// </summary>
        private EffectType m_defaultEffectType = EffectType.Fade;

        private Color m_defaultEffectColor = Color.black;

        public TransitionImageFill(EffectType type, Color color)
        {
            m_defaultEffectType = type;
            m_defaultEffectColor = color;
        }

        public TransitionImageFill()
        {
        }

        public void Init(Canvas canvas)
        {
            m_transImage = CreateImage(canvas);
            Texture2D plainTex = CreateTexture2D();
            m_transitionSprite = Sprite.Create(plainTex, new Rect(0, 0, 32, 32), Vector2.zero);
            m_transitionSprite.name = "TransitionSpeite";
            m_transImage.sprite = m_transitionSprite;
            m_transImage.type = Image.Type.Filled;
            m_transImage.fillAmount = 1.0f;

            SceneTransitionInit(m_defaultEffectColor, m_defaultEffectType);
        }

        public void Direction(float lp)
        {
            switch (m_defaultEffectType)
            {
                case EffectType.Fade:
                    m_transImage.color = new Color(m_transImage.color.r, m_transImage.color.g, m_transImage.color.b,
                        lp);
                    break;
                default:
                    m_transImage.fillAmount = lp;
                    break;
            }
        }

        public void End()
        {
            m_transImage.fillAmount = 0.0f;
            GameObject gameObject;
            (gameObject = m_transImage.gameObject).SetActive(false);
            Object.Destroy(gameObject);
        }

        void SceneTransitionInit(Color effectColor, EffectType effectType)
        {
            switch (effectType)
            {
                case EffectType.Fade:
                    m_transImage.fillAmount = 1.0f;
                    break;
                case EffectType.Horizontal_Right:
                    m_transImage.fillMethod = Image.FillMethod.Horizontal;
                    m_transImage.fillOrigin = (int) Image.OriginHorizontal.Right;
                    break;
                case EffectType.Horizontal_Left:
                    m_transImage.fillMethod = Image.FillMethod.Horizontal;
                    m_transImage.fillOrigin = (int) Image.OriginHorizontal.Left;
                    break;
                case EffectType.Vertical_Top:
                    m_transImage.fillMethod = Image.FillMethod.Vertical;
                    m_transImage.fillOrigin = (int) Image.OriginVertical.Top;
                    break;
                case EffectType.Vertical_Bottom:
                    m_transImage.fillMethod = Image.FillMethod.Vertical;
                    m_transImage.fillOrigin = (int) Image.OriginVertical.Bottom;
                    break;
                case EffectType.Radial90_TopRight:
                    m_transImage.fillMethod = Image.FillMethod.Radial90;
                    m_transImage.fillOrigin = (int) Image.Origin90.TopRight;
                    break;
                case EffectType.Radial90_TopLeft:
                    m_transImage.fillMethod = Image.FillMethod.Radial90;
                    m_transImage.fillOrigin = (int) Image.Origin90.TopLeft;
                    break;
                case EffectType.Radial90_BottomRight:
                    m_transImage.fillMethod = Image.FillMethod.Radial90;
                    m_transImage.fillOrigin = (int) Image.Origin90.BottomRight;
                    break;
                case EffectType.Radial90_BottomLeft:
                    m_transImage.fillMethod = Image.FillMethod.Radial90;
                    m_transImage.fillOrigin = (int) Image.Origin90.BottomLeft;
                    break;
                case EffectType.Radial180_Right:
                    m_transImage.fillMethod = Image.FillMethod.Radial180;
                    m_transImage.fillOrigin = (int) Image.Origin180.Right;
                    break;
                case EffectType.Radial180_Left:
                    m_transImage.fillMethod = Image.FillMethod.Radial180;
                    m_transImage.fillOrigin = (int) Image.Origin180.Left;
                    break;
                case EffectType.Radial180_Top:
                    m_transImage.fillMethod = Image.FillMethod.Radial180;
                    m_transImage.fillOrigin = (int) Image.Origin180.Top;
                    break;
                case EffectType.Radial180_Bottom:
                    m_transImage.fillMethod = Image.FillMethod.Radial180;
                    m_transImage.fillOrigin = (int) Image.Origin180.Bottom;
                    break;
                case EffectType.Radial360_Right:
                    m_transImage.fillMethod = Image.FillMethod.Radial360;
                    m_transImage.fillOrigin = (int) Image.Origin360.Right;
                    break;
                case EffectType.Radial360_Left:
                    m_transImage.fillMethod = Image.FillMethod.Radial360;
                    m_transImage.fillOrigin = (int) Image.Origin360.Left;
                    break;
                case EffectType.Radial360_Top:
                    m_transImage.fillMethod = Image.FillMethod.Radial360;
                    m_transImage.fillOrigin = (int) Image.Origin360.Top;
                    break;
                case EffectType.Radial360_Bottom:
                    m_transImage.fillMethod = Image.FillMethod.Radial360;
                    m_transImage.fillOrigin = (int) Image.Origin360.Bottom;
                    break;
            }

            switch (effectType)
            {
                case EffectType.Fade:
                    m_transImage.fillAmount = 1.0f;
                    m_transImage.color = new Color(effectColor.r, effectColor.g, effectColor.b, 0.0f);
                    break;
                default:
                    m_transImage.fillAmount = 0.0f;
                    m_transImage.color = effectColor;
                    break;
            }

            m_transImage.gameObject.SetActive(true);
        }

        /// <summary>
        /// 创建图片
        /// </summary>
        /// <param name="canvas"></param>
        /// <returns></returns>
        Image CreateImage(Canvas canvas)
        {
            GameObject transImageObj;
            transImageObj = new GameObject("Transition Image");
            transImageObj.transform.SetParent(canvas.transform);
            transImageObj.transform.position = Vector3.zero;
            transImageObj.transform.localPosition = Vector3.zero;
            transImageObj.transform.localScale = Vector3.one;
            transImageObj.transform.rotation = Quaternion.identity;
            var i = transImageObj.AddComponent<Image>();
            i.color = m_defaultEffectColor;
            i.sprite = null;
            RectTransform transImageRectTransfrm;
            transImageRectTransfrm = transImageObj.GetComponent<RectTransform>();
            transImageRectTransfrm.anchorMin = Vector2.zero;
            transImageRectTransfrm.anchorMax = Vector2.one;
            transImageRectTransfrm.pivot = new Vector2(0.5f, 0.5f);
            transImageRectTransfrm.localPosition = Vector3.zero;
            transImageRectTransfrm.sizeDelta = Vector3.zero;
            transImageObj.SetActive(false);
            return i;
        }

        Texture2D CreateTexture2D()
        {
            var tex = new Texture2D(32, 32, TextureFormat.RGB24, false);
            for (int i = 0; i < tex.width; i++)
            {
                for (int ii = 0; ii < tex.height; ii++)
                {
                    tex.SetPixel(i, ii, Color.white);
                }
            }

            return tex;
        }

        /// <summary>
        /// EffectType
        /// Custom is a transition method using rule images
        /// </summary>
        public enum EffectType
        {
            Fade = 0,

            Horizontal_Right = 1,
            Horizontal_Left = 2,

            Vertical_Top = 3,
            Vertical_Bottom = 4,

            Radial90_TopRight = 5,
            Radial90_TopLeft = 6,
            Radial90_BottomRight = 7,
            Radial90_BottomLeft = 8,

            Radial180_Right = 9,
            Radial180_Left = 10,
            Radial180_Top = 11,
            Radial180_Bottom = 12,

            Radial360_Right = 13,
            Radial360_Left = 14,
            Radial360_Top = 15,
            Radial360_Bottom = 16
        }
    }
}