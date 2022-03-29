using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace UIScripts.CommonComponents.Popup
{
    [DisallowMultipleComponent]
    public class ToolTipComponent : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
    {
        [SerializeField] private ToolTipPopup popup;
        [SerializeField] private string key;
        [SerializeField] private string description;

        public string Description
        {
            get => description;
            set => description = value;
        }

        public string Key
        {
            get => key;
            set => key = value;
        }

        public ToolTipPopup Popup
        {
            get => popup;
            set => popup = value;
        }
        

        public void OnPointerEnter(PointerEventData eventData)
        {
            popup.PointerEnterHandler(eventData, Description);
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            popup.PointerExitHandler(eventData);
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            popup.PointerClickHandler(eventData, gameObject, Description);
        }
    }
}