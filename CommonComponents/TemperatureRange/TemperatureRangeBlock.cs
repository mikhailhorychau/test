using TMPro;
using UIScripts.Constants;
using UIScripts.Utils;
using UnityEngine;
using UnityEngine.EventSystems;

namespace UIScripts.CommonComponents.TemperatureRange
{
    public class TemperatureRangeBlock : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        [SerializeField] private TextMeshProUGUI rangeValue;
        [SerializeField] private StyledImage styledImage;
        [SerializeField] private RectTransform rect;
        [SerializeField] private UIColorStyle goodColor;
        [SerializeField] private float goodColorBound;
        [SerializeField] private UIColorStyle middleColor;
        [SerializeField] private float middleColorBound;
        [SerializeField] private UIColorStyle badColor;

        [SerializeField] private TemperatureRangePopup rangePopup;
        [SerializeField] private TemperatureRangeBlockProps props;

        public void Initialize(TemperatureRangeBlockProps rangeProps, Sprite rangeSprite, Vector2 rangeSize)
        {
            props = rangeProps;
            
            rangePopup.gameObject.SetActive(false);
            rangePopup.DescriptionValue = rangeProps.Description;
            rangePopup.RangeValue = GetRangeTitle(rangeProps.StartValueText, rangeProps.EndValueText);
            
            rangeValue.text = $"{rangeProps.Value:0.0}%";
            
            // styledImage.image.sprite = rangeSprite;
            styledImage.image.overrideSprite = rangeSprite;
            styledImage.SwapColor(GetColorByValue(rangeProps.Value));

            rect.sizeDelta = rangeSize;
        }

        private UIColorStyle GetColorByValue(float value)
        {
            if (value >= goodColorBound) return goodColor;
            if (value >= middleColorBound) return middleColor;
            return badColor;
        }

        private string GetRangeTitle(string minValue, string maxValue) =>
            $"{minValue} ... {maxValue}";

        public void OnPointerEnter(PointerEventData eventData)
        {
            rangePopup.gameObject.SetActive(true);
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            rangePopup.gameObject.SetActive(false);
        }
    }
}