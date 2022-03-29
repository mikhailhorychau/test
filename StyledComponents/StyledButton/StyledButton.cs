using System;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace UIScripts
{
    public class StyledButton : 
        MonoBehaviour, 
        IButton,
        IPointerEnterHandler, 
        IPointerExitHandler, 
        IPointerDownHandler, 
        IPointerUpHandler,
        IPointerClickHandler
    {
        [Serializable]
        public enum ButtonStyleType
        {
            Primary,
            Secondary,
            Building
        }

        [Serializable]
        public class ButtonStyle
        {
            public Sprite backgroundSprite;
            public UIColorStyle backgroundColor;
            public Sprite borderSprite;
            public UIColorStyle borderColor;
            public TextStyle textStyle;
        }

        [Serializable]
        public class ButtonStylesSettings
        {
            public UIState<ButtonStyle> primary;
            public UIState<ButtonStyle> secondary;
            public UIState<ButtonStyle> building;
        }
    
        [SerializeField] private ButtonStyleType buttonStyle;
        [SerializeField] private ButtonStylesSettings buttonStylesSettings;

        [SerializeField] private Image backgroundImage;
        [SerializeField] private Image borderImage;
        [SerializeField] private StyledText styledText;
        [SerializeField] private TextMeshProUGUI textMesh;

        public string Text
        {
            get => textMesh.text;
            set => textMesh.text = value;
        }

        public event Action<ButtonEventData> OnButtonEvent;
        public ButtonEventData ButtonEventData(ButtonEventType type) => 
            new ButtonEventData(_disabled, _pressed, _isMouseOver, type);
        
        public UnityEvent onClick;
        public UnityEvent OnClick => onClick;

        private bool _pressed;
        [SerializeField] private bool _disabled;

        public bool Disabled
        {
            get => _disabled;
            set
            {
                _disabled = value;
                if (_currentStyleSettings.disabled == null)
                {
                    SwapStyle(buttonStyle);
                }
                UpdateUI(_disabled ? _currentStyleSettings.disabled : _currentStyleSettings.common);
            }
        }

        public void SetInteractivity(bool interactive) => Disabled = !interactive;

        private bool _isMouseOver = false;
        
        private UIState<ButtonStyle> _currentStyleSettings;

        private void Start()
        {
            SwapStyle(buttonStyle);
            UpdateUI(_disabled ? _currentStyleSettings.disabled : _currentStyleSettings.common);
        }

        private void Awake()
        {
            SwapStyle(buttonStyle);
            UpdateUI(_disabled ? _currentStyleSettings.disabled : _currentStyleSettings.common);
        }

        private void OnDisable()
        {
            if (_isMouseOver)
            {
                UISettings.Instance.GameCursor.SetCursorType(CursorType.Common);
            }
        }

        private void SwapStyle(ButtonStyleType buttonStyleType)
        {
            switch (buttonStyleType)
            {
                case ButtonStyleType.Primary : _currentStyleSettings = buttonStylesSettings.primary;
                    break;
                case ButtonStyleType.Secondary : _currentStyleSettings = buttonStylesSettings.secondary;
                    break;
                case ButtonStyleType.Building : _currentStyleSettings = buttonStylesSettings.building;
                    break;
            }
            UpdateUI(_currentStyleSettings.common);
        }

        public void DisableButton()
        {
            _disabled = true;
            UpdateUI(_currentStyleSettings.disabled);
        }
        
        private void UpdateUI(ButtonStyle settings)
        {
            if (!backgroundImage) backgroundImage = GetComponent<Image>();
            if (backgroundImage == null) return;
            backgroundImage.overrideSprite = settings.backgroundSprite;
            backgroundImage.color = UISettings.Instance.colors.Pick(settings.backgroundColor);

            if (!borderImage) borderImage = GetComponentInChildren<Image>();
            if (borderImage == null) return;
            borderImage.overrideSprite = settings.borderSprite;
            borderImage.color = UISettings.Instance.colors.Pick(settings.borderColor);

            if (!styledText) styledText = GetComponentInChildren<StyledText>();
            if (styledText == null) return;
            styledText.textStyle = settings.textStyle;
            styledText.UpdateUI();
        }

        public void UpdateUI() => UpdateUI(_currentStyleSettings.common);
    
        private void OnValidate()
        {
            SwapStyle(buttonStyle);
        }

        public void SwapBorderColor(UIColorStyle color)
        {
            _currentStyleSettings.common.borderColor = color;
            _currentStyleSettings.disabled.borderColor = color;
            _currentStyleSettings.hover.borderColor = color;
            _currentStyleSettings.pressed.borderColor = color;
            UpdateUI(_currentStyleSettings.common);
        }

        public void SwapTextColor(UIColorStyle color)
        {
            _currentStyleSettings.common.textStyle.colorStyle = color;
            _currentStyleSettings.disabled.textStyle.colorStyle = color;
            _currentStyleSettings.hover.textStyle.colorStyle = color;
            _currentStyleSettings.pressed.textStyle.colorStyle = color;
            UpdateUI(_currentStyleSettings.common);
        } 

        public void OnPointerEnter(PointerEventData eventData)
        {
            _isMouseOver = true;
            OnButtonEvent?.Invoke(ButtonEventData(ButtonEventType.Enter));
            if (_disabled) return;
            UISettings.Instance.GameCursor.SetCursorType(CursorType.Link);
            if (_pressed) return;
            UpdateUI(_currentStyleSettings.hover);
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            _isMouseOver = false;
            OnButtonEvent?.Invoke(ButtonEventData(ButtonEventType.Exit));
            if (_disabled) return;
            UISettings.Instance.GameCursor.SetCursorType(CursorType.Common);
            if (_pressed) return;
            UpdateUI(_currentStyleSettings.common);
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            OnButtonEvent?.Invoke(ButtonEventData(ButtonEventType.Press));
            if (_disabled) return;
            UpdateUI(_currentStyleSettings.pressed);
            _pressed = true;
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            OnButtonEvent?.Invoke(ButtonEventData(ButtonEventType.Up));
            if (_disabled) return;
            _pressed = false;
            UpdateUI(_currentStyleSettings.common);
        }
    
        public void OnPointerClick(PointerEventData eventData)
        {
            OnButtonEvent?.Invoke(ButtonEventData(ButtonEventType.Click));
            if (_disabled) return;
            onClick.Invoke();
        }
    }

    public struct ButtonEventData
    {
        public readonly bool disabled;
        public readonly bool pressed;
        public readonly bool hovered;
        public readonly ButtonEventType type;

        public ButtonEventData(bool disabled, bool pressed, bool hovered, ButtonEventType type)
        {
            this.disabled = disabled;
            this.pressed = pressed;
            this.hovered = hovered;
            this.type = type;
        }
    }

    public enum ButtonEventType
    {
        Enter,
        Exit,
        Press,
        Up,
        Click
    }
}