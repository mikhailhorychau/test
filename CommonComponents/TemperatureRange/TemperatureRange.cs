using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

namespace UIScripts.CommonComponents.TemperatureRange
{
    public class TemperatureRange : MonoBehaviour
    {
        [SerializeField] private TemperatureRangeBlock tempBlockPrefab;
        [SerializeField] private Transform container;

        [SerializeField] private TextMeshProUGUI startTemperatureTitle;
        [SerializeField] private TextMeshProUGUI endTemperatureTitle;
        
        [SerializeField] private Sprite leftSprite;
        [SerializeField] private Sprite middleSprite;
        [SerializeField] private Sprite rightSprite;
        [SerializeField] private Sprite fullSprite;

        [SerializeField] private float minTemperature = 0f;
        [SerializeField] private float maxTemperature = 40f;

        [SerializeField] private List<TemperatureRangeBlock> preloadedBlocks;

        public float MINTemperature
        {
            set => minTemperature = value;
        }

        public float MAXTemperature
        {
            set => maxTemperature = value;
        }

        private Vector2 FullRangeSize => GetComponent<RectTransform>()?.sizeDelta ?? Vector2.zero;
        private float TemperatureDiff => maxTemperature - minTemperature;

        public void Initialize(List<TemperatureRangeBlockProps> initProps, string minTemperatureText, string maxTemperatureText)
        {
            if (initProps.Count == 1) 
                InitRangeObject(initProps[0], FullRangeSize, fullSprite);
            else
            {
                initProps.ForEach(item => InitRangeObject(initProps, item));
            }

            startTemperatureTitle.text = minTemperatureText;
            endTemperatureTitle.text = maxTemperatureText;
        }


        private void InitRangeObject(List<TemperatureRangeBlockProps> initProps, TemperatureRangeBlockProps itemProps) =>
            InitRangeObject(itemProps, GetSpriteByItemPosition(initProps, itemProps));

        private void InitRangeObject(TemperatureRangeBlockProps itemProps, Sprite sprite) =>
            InitRangeObject(itemProps, GetCalculatedSize(itemProps.StartValue, itemProps.EndValue), sprite);
        
        private void InitRangeObject(TemperatureRangeBlockProps itemProps, Vector2 rangeSize, Sprite sprite, 
            string rangeName = "Range")
        {
            var tempBlockClass = Instantiate(tempBlockPrefab, container);
            tempBlockClass.name = rangeName;
            tempBlockClass.Initialize(itemProps, sprite, rangeSize);
        }

        private Vector2 GetCalculatedSize(float startValue, float endValue)
        {
            // Usually ranges be like [0, 12] - [13, 24], incrementValue fix this gap between ranges
            var incrementValue = endValue.Equals(maxTemperature) ? 0 : 1;
            var width = (endValue - startValue + incrementValue) * FullRangeSize.x / TemperatureDiff;
            return new Vector2(width, FullRangeSize.y);
        }

        private Sprite GetSpriteByItemPosition(List<TemperatureRangeBlockProps> itemsList, TemperatureRangeBlockProps item) 
            => item == itemsList.First() ? leftSprite 
                : item == itemsList.Last() ? rightSprite 
                : middleSprite;
        
    }
}