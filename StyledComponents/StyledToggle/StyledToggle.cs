using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace UIScripts
{
    public class StyledToggle : 
        MonoBehaviour,
        IPointerEnterHandler,
        IPointerExitHandler
    {
        [Serializable]
        private class ImageStyle
        {
            public UIColorStyle color;
            public float alpha = 1f;
            public Sprite sprite;
        }
    
        [Serializable]
        private class ToggleStyle
        {
            public ImageStyle background;
            public ImageStyle border;
            public ImageStyle icon;
        }
    
        [SerializeField] private StyledImage background;
        [SerializeField] private StyledImage border;
        [SerializeField] private StyledImage icon;
        [SerializeField] private Toggle toggle;

        [SerializeField] private UIState<ToggleStyle> toggleStyles;

        public bool disabled;
        private ToggleStyle _currentStyle;
    

        private void Start()
        {
            if (!toggle) toggle = GetComponent<Toggle>();
            if (!toggle) return;

            if (disabled)
            {
                SwapStyle(toggleStyles.disabled);
                return;
            }
            SwapStyle(toggle.isOn ? toggleStyles.pressed : toggleStyles.common);
        }
        public void UpdateUI()
        {
            background.image.overrideSprite = _currentStyle.background.sprite;
            background.SwapColor(_currentStyle.background.color, _currentStyle.background.alpha);

            border.image.overrideSprite = _currentStyle.border.sprite;
            border.SwapColor(_currentStyle.border.color, _currentStyle.border.alpha);

            icon.image.overrideSprite = _currentStyle.icon.sprite;
            icon.SwapColor(_currentStyle.icon.color, _currentStyle.icon.alpha);
        }

        public void ValueChangeHandler(Toggle tgl)
        {
            if (disabled) return;
            SwapStyle(tgl.isOn ? toggleStyles.pressed : toggleStyles.common);
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            if (disabled) return;
            UISettings.Instance.GameCursor.SetCursorType(CursorType.Link);
            SwapStyle(toggleStyles.hover);
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            if (disabled) return;
            UISettings.Instance.GameCursor.SetCursorType(CursorType.Common);
            if (!toggle) toggle = GetComponent<Toggle>();
            SwapStyle(toggle.isOn ? toggleStyles.pressed : toggleStyles.common);
        }

        private void SwapStyle(ToggleStyle toggleStyle)
        {
            if (disabled) return;
            _currentStyle = toggleStyle;
            UpdateUI();
        }

        private void OnValidate()
        {
            if (disabled) return;
            if (!toggle) toggle = GetComponent<Toggle>();
            SwapStyle(toggle.isOn ? toggleStyles.pressed : toggleStyles.common);
        }
    }
}