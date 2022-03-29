using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace UIScripts.CommonComponents
{
    public class CircleToggle : MonoBehaviour
    {
        [SerializeField] private RectTransform root;
        [SerializeField] private RectTransform fillRect;
        [SerializeField] private RectTransform hoverRect;
        [SerializeField] private Toggle toggle;
        [SerializeField] private GameObject fill;
        [SerializeField] private float paddingDiff = 6f;

        private bool _isMouseOver;

        private void Awake()
        {
            var padding = root.sizeDelta.x / paddingDiff;
            fillRect.offsetMax = new Vector2(-padding, -padding);
            fillRect.offsetMin = new Vector2(padding, padding);

            hoverRect.offsetMax = new Vector2(-padding, -padding);
            hoverRect.offsetMin = new Vector2(padding, padding);
            
            // toggle.onValueChanged.AddListener(ToggleChangeListener);
        }

        // private void ToggleChangeListener(bool selected)
        // {
        //     if (!_isMouseOver) return;
        //
        //     var cursorType = selected ? CursorType.Common : CursorType.Link;
        //     
        //     UISettings.Instance.GameCursor.SetCursorType(cursorType);
        // }
        //
        // public void OnPointerEnter(PointerEventData eventData)
        // {
        //     _isMouseOver = true;
        //     if (!toggle.interactable || toggle.isOn) return;
        //     
        //     UISettings.Instance.GameCursor.SetCursorType(CursorType.Link);
        // }
        //
        // public void OnPointerExit(PointerEventData eventData)
        // {
        //     _isMouseOver = false;
        //     UISettings.Instance.GameCursor.SetCursorType(CursorType.Common);
        // }
    }
}