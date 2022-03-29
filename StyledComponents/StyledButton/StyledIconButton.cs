using System;
using UIScripts.Abstract;
using UIScripts.Utils.UI;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

namespace UIScripts
{
    public class StyledIconButton : 
        MonoBehaviour,
        IButton,
        IInteractable,
        IPointerEnterHandler,
        IPointerExitHandler,
        IPointerDownHandler,
        IPointerUpHandler,
        IPointerClickHandler
    {
        [Serializable]
        public class SpriteSettings
        {
            public Sprite sprite;
            public UIColorStyle color;
            public float alpha = 1f;
        }
    
        [Serializable]
        public class StateStyle
        {
            public SpriteSettings background;
            public SpriteSettings border;
            public SpriteSettings icon;
        }

        [SerializeField] private StyledImage background;
        [SerializeField] private StyledImage border;
        [SerializeField] private StyledImage icon;
    
        [SerializeField] private UIState<StateStyle> stateStyles;

        public UIState<StateStyle> StateStyles => stateStyles;


        public bool disabled;
        public UnityEvent onClick;
        public UnityEvent onPointerEnter;
        public UnityEvent onPointerExit;
        public UnityEvent onPointerDown;
        public UnityEvent onPointerUp;
        public UnityEvent<bool> onDisableChange;

        private bool _pressed;
        
        private StateStyle _currentStyle;
        private bool _mouseIsOver;

        public bool Interactable { get; set; } = true;
        public string Text { get; set; }

        public UnityEvent OnClick => onClick;
        public StateStyle CurrentStyle => _currentStyle;

        private void Awake()
        {
            if (disabled)
            {
                SwitchStyle(stateStyles.disabled);
                return;
            }
            SwitchStyle(stateStyles.common);
        }

        private void OnValidate()
        {
            if (disabled)
            {
                SwitchStyle(stateStyles.disabled);
                return;
            }
            SwitchStyle(stateStyles.common);
        }

        private void UpdateUI()
        {
            UpdateImage(background, _currentStyle.background);
            
            if (border)
                UpdateImage(border, _currentStyle.border);
            
            UpdateImage(icon, _currentStyle.icon);
        }

        public void SetDisabled(bool isDisabled)
        {
            onDisableChange.Invoke(isDisabled);
            disabled = isDisabled;
            SwitchStyle(isDisabled ? stateStyles.disabled : stateStyles.common);
            
            if (isDisabled && _mouseIsOver)
                UISettings.Instance.GameCursor.SetCursorType(CursorType.Common);
        }

        public void SwitchIconSprite(Sprite sprite)
        {
            stateStyles.common.icon.sprite = sprite;
            stateStyles.hover.icon.sprite = sprite;
            stateStyles.pressed.icon.sprite = sprite;
            stateStyles.disabled.icon.sprite = sprite;
            SwitchStyle(disabled ? stateStyles.disabled : stateStyles.common);
        }

        private void UpdateImage(StyledImage styledImage, SpriteSettings spriteSettings)
        {
            if (styledImage == null) return;
            styledImage.image.overrideSprite = spriteSettings.sprite;
            styledImage.SwapColor(spriteSettings.color, spriteSettings.alpha);
        }

        public void SwitchStyle(StateStyle stateStyle)
        {
            _currentStyle = stateStyle;
            UpdateUI();
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            _mouseIsOver = true;
            
            if (disabled) return;
            
            UISettings.Instance.GameCursor.SetCursorType(CursorType.Link);
            
            if (!Interactable) return;
            
            if (_pressed) return;
            
            SwitchStyle(stateStyles.hover);
            onPointerEnter.Invoke();
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            _mouseIsOver = false;

            if (disabled) return;
            
            UISettings.Instance.GameCursor.SetCursorType(CursorType.Common);

            if (!Interactable) return;
            
            if (_pressed) return;
            
            SwitchStyle(stateStyles.common);
            onPointerExit.Invoke();
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            if (disabled) return;
            
            _pressed = true;
            SwitchStyle(stateStyles.pressed);
            onPointerDown.Invoke();
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            if (disabled) return;
            
            _pressed = false;
            SwitchStyle(stateStyles.common);
            onPointerUp.Invoke();
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            if (disabled) return;
            onClick.Invoke();
        }

        public void SetInteractivity(bool interactive)
        {
            SetDisabled(!interactive);
            Interactable = interactive;
        }
    }
}