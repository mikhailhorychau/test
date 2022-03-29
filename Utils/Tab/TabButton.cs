using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace UIScripts.Tab
{
    public class TabButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler, IPointerUpHandler
    {
        [Serializable]
        private class TabStyle
        {
            public ImageStyle background;
            public ImageStyle border;
            public TextStyle text;
        }

        [Serializable]
        private class ImageStyle
        {
            public Sprite sprite;
            public UIColorStyle color;
        }

        [Serializable]
        private enum State
        {
            Common,
            Hover,
            Pressed,
            Active
        }

        [SerializeField] private Toggle toggle;

        public Toggle Toggle => toggle;

        [SerializeField] private StyledImage background;
        [SerializeField] private StyledImage border;
        [SerializeField] private StyledText text;

        [SerializeField] private TextMeshProUGUI title;

        [SerializeField] private ToggleState<TabStyle> stateStyles;

        [SerializeField] private State currentState;

        [SerializeField] private ContentSizeFitter fitter;
        
        private State CurrentState
        {
            get => currentState;
            set
            {
                currentState = value;
                UpdateState();
            }
        }

        private void Awake()
        {
            Initialize();
            toggle.onValueChanged.AddListener((active) => CurrentState = active ? State.Active : State.Common);
            CurrentState = toggle.isOn ? State.Active : State.Common;
        }
        
        private bool _isPressed;

        private void Initialize()
        {
            toggle.onValueChanged.AddListener((selected) =>
            {
                if (selected)
                    CurrentState = State.Active;
            });
            UpdateState();
        }

        private void UpdateState()
        {
            switch (CurrentState)
            {
                case State.Common : UpdateUI(stateStyles.common);
                    break;
                case State.Hover : UpdateUI(stateStyles.hover);
                    break;
                case State.Pressed : UpdateUI(stateStyles.pressed);
                    break;
                case State.Active : UpdateUI(stateStyles.active);
                    break;
            }
        }

        public RectTransform RectTransform
        {
            get
            {
                if (!_rectTransform)
                    _rectTransform = GetComponent<RectTransform>();

                return _rectTransform;
            }
        }

        private RectTransform _rectTransform;

        public void SetTextAndResize(string newText)
        {
            // fitter.enabled = true;
            text.Text = newText;
            // LayoutRebuilder.ForceRebuildLayoutImmediate(transform.parent.GetComponent<RectTransform>());
            // fitter.enabled = false;
            // if (isActiveAndEnabled)
            //     StartCoroutine(Resize());
            // Resize();
        }

        private Vector4 _defaultMargin = new Vector4(0, 8, 0, 8);
        
        private IEnumerator Resize()
        {
            yield return new WaitForEndOfFrame();

            LayoutRebuilder.ForceRebuildLayoutImmediate(text.TextMesh.rectTransform);
            var sizeDelta = text.TextMesh.rectTransform.sizeDelta;
            RectTransform.sizeDelta = new Vector2(sizeDelta.x, sizeDelta.y);
            text.GetComponent<ContentSizeFitter>().horizontalFit = ContentSizeFitter.FitMode.Unconstrained;
            text.TextMesh.margin = _defaultMargin;
        }

        private void OnValidate()
        {
            UpdateState();
        }

        private void UpdateUI(TabStyle style)
        {
            background.UpdateUI(style.background.sprite, style.background.color);
            border.UpdateUI(style.border.sprite, style.border.color);
            text.SwapStyle(style.text);
        }
        
        public void OnPointerEnter(PointerEventData eventData)
        {
            if (toggle.isOn) return;
            if (_isPressed) return;
            UISettings.Instance.GameCursor.SetCursorType(CursorType.Link);
            CurrentState = State.Hover;
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            if (toggle.isOn) return;
            if (_isPressed) return;
            UISettings.Instance.GameCursor.SetCursorType(CursorType.Common);
            CurrentState = State.Common;
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            if (toggle.isOn) return;
            _isPressed = true;
            CurrentState = State.Pressed;
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            if (toggle.isOn) return;
            _isPressed = false;
            CurrentState = toggle.isOn ? State.Active : State.Common;
            UISettings.Instance.GameCursor.SetCursorType(CursorType.Common);
        }
    }
}