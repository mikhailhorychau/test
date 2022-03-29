using UnityEngine;
using UnityEngine.UI;

namespace UIScripts
{
    public class StyledImage : MonoBehaviour, IAlphaChanger
    {
        public Image image;
        public UIColorStyle colorStyle;
        public float colorAlpha = 1f;

        private RectTransform _rect;

        public void UpdateUI()
        {
            if (!image) image = GetComponent<Image>();
            if (!image) return;

            var color = UISettings.Instance.colors.Pick(colorStyle);
            color.a = colorAlpha;

            image.color = color;
        }

        public void UpdateUI(Sprite sprite, UIColorStyle color)
        {
            image.overrideSprite = sprite;
            image.sprite = sprite;
            var clr = UISettings.Instance.colors.Pick(color);
            image.color = clr;
            colorAlpha = clr.a;
        }

        public void UpdateUI(SpriteStyle style)
        {
            image.sprite = style.sprite;
            image.overrideSprite = style.sprite;
            var color = UISettings.Instance.colors.Pick(style.color);
            color.a = style.alpha;
            image.color = color;
        }
        
        public void SwapColor(UIColorStyle color, float alpha)
        {
            if (colorStyle == color && colorAlpha.CompareTo(alpha) == 0)
                return;
            
            colorStyle = color;
            colorAlpha = alpha;
            
            UpdateUI();
        }

        public void SwapColor(UIColorStyle color) => SwapColor(color, 1f);

        public void SetAlpha(float alpha) => SwapColor(colorStyle, alpha);

        public void SetScale(float scale)
        {
            if (!_rect) _rect = GetComponent<RectTransform>();
            _rect.localScale = new Vector3(scale, scale, scale);
        }
    
        private void OnValidate()
        {
            UpdateUI();
        }

        private void Awake()
        {
            UpdateUI();
        }
    }
}