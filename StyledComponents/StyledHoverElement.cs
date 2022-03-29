using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace UIScripts
{
    public class StyledHoverElement : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        [SerializeField] private Image borderImage;
        [SerializeField] private Image backgroundImage;
        [SerializeField] private StyledText text;
        [SerializeField] private ButtonStyle common;
        [SerializeField] private ButtonStyle hover;

        public UnityEvent onPointerEnter;
        public UnityEvent onPointerExit;

        public void UpdateUI(ButtonStyle style)
        {
            if (!backgroundImage) backgroundImage = GetComponent<Image>();
            
            backgroundImage.overrideSprite = style.backgroundSprite;
            backgroundImage.color = UISettings.Instance.colors.Pick(style.backgroundColor);

            if (borderImage)
            {
                borderImage.overrideSprite = style.borderSprite;
                borderImage.color = UISettings.Instance.colors.Pick(style.borderColor);
            }

            if (text)
            {
                text.textStyle = style?.textStyle;
                text.UpdateUI();
            }
        }

        private void Start()
        {
            UpdateUI(common);
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            onPointerEnter.Invoke();
            UpdateUI(hover);
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            onPointerExit.Invoke();
            UpdateUI(common);
        }

        private void OnValidate()
        {
            UpdateUI(common);
        }
    }
}