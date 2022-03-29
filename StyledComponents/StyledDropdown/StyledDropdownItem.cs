using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace UIScripts
{
    public class StyledDropdownItem : 
        MonoBehaviour,
        IPointerEnterHandler,
        IPointerExitHandler,
        IPointerClickHandler,
        IDeselectHandler
    {

        [SerializeField] private UIColorStyle commonStyle;
        [SerializeField] private UIColorStyle hoverStyle;
        [SerializeField] public Image target;

        public UnityEvent onClick;
        
        public void UpdateUI()
        {
            target.color = UISettings.Instance.colors.Pick(commonStyle);
        }

        private void OnValidate()
        {
            UpdateUI();
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            target.color = UISettings.Instance.colors.Pick(hoverStyle);
            UISettings.Instance.GameCursor.SetCursorType(CursorType.Link);
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            target.color = UISettings.Instance.colors.Pick(commonStyle);
            UISettings.Instance.GameCursor.SetCursorType(CursorType.Common);
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            target.color = UISettings.Instance.colors.Pick(commonStyle);
            onClick.Invoke();
        }

        public void OnDeselect(BaseEventData eventData)
        {
            target.color = UISettings.Instance.colors.Pick(commonStyle);
        }
    
    }
}