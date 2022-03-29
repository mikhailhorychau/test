using System.Collections.Generic;
using UIScripts.Abstract;
using UIScripts.Utils;
using UnityEngine;
using UnityEngine.UI;

namespace UIScripts
{
    public class SkillBar : MonoBehaviour, ISkillValue
    {
        [SerializeField] private RectTransform fillMask;
        [SerializeField] private Image fill;

        [SerializeField] private int maxValue = 10;
        [SerializeField] private int value = 1;
        [SerializeField] private int bannedValue = 0;
        [SerializeField] private int prevValue = 0;

        [SerializeField] private List<Image> bannedIndicators;
        public List<Image> BannedIndicators => bannedIndicators;
        [SerializeField] private Sprite bannedFilled;
        [SerializeField] private Sprite bannedEmpty;

        [SerializeField] private List<StyledImage> changesIndicators;
        public List<StyledImage> ChangesIndicators => changesIndicators;

        [SerializeField] private Gradient gradient;
        [SerializeField] private UIColorStyle startColor;
        [SerializeField] private UIColorStyle middleColor;
        [SerializeField] private UIColorStyle endColor;

        private RectTransform _fillRect;
        
        public int Value
        {
            get => value;
            set
            {
                if (value < 1) this.value = 1;
                else if (value > maxValue) this.value = maxValue;
                else this.value = value;
                
                ResizeFill();
            }
        }

        public int BannedValue
        {
            get => bannedValue;
            set
            {
                bannedValue = value;
                ResizeFill();
            }
        }

        public int PrevValue
        {
            get => prevValue;
            set
            {
                prevValue = value;
                ResizeFill();
            }
        }

        private void ResizeFill()
        {
            if (!fillMask) return;
            if (value > maxValue) value = maxValue;
            var width = 18 * value + value - 1;
            fillMask.sizeDelta = new Vector2(width, fillMask.sizeDelta.y);
            
            if (bannedIndicators.Count == 0) return;
            if (bannedValue > 0 && bannedValue <= maxValue)
            {
                for (int i = 0; i < bannedValue - 1; i++)
                {
                    bannedIndicators[i].color = Color.clear;
                }
                for (int i = bannedValue - 1; i < maxValue; i++)
                {
                    bannedIndicators[i].color = Color.white;
                    bannedIndicators[i].overrideSprite = i + 1 <= value ? bannedFilled : bannedEmpty;
                }
            }
            else
            {
                bannedIndicators.ForEach(indicator => indicator.color = Color.clear);
            }
            UpdateChangesIndicators();
        }

        private void UpdateChangesIndicators()
        {
            if (changesIndicators.Count == 0) return;
            if (prevValue > maxValue) prevValue = maxValue;
            if (prevValue < 0) prevValue = 0;
            
            if (prevValue == 0)
            {
                changesIndicators.ForEach(indicator => indicator.SetAlpha(0f));
                return;
            }

            if (prevValue == value)
            {
                changesIndicators.ForEach(indicator => indicator.SetAlpha(0f));
                return;
            }

            if (prevValue < value)
            {
                for (int i = 0; i < prevValue; i++)
                {
                    changesIndicators[i].SetAlpha(0f);
                }

                for (int i = prevValue; i < value; i++)
                {
                    changesIndicators[i].SetAlpha(1f);
                    changesIndicators[i].SwapColor(UIColorStyle.TextGreen);
                }

                for (int i = value; i < maxValue; i++)
                {
                    changesIndicators[i].SetAlpha(0f);
                }
            }

            if (prevValue > value)
            {
                for (int i = 0; i < value ; i++)
                {
                    changesIndicators[i].SetAlpha(0f);
                }

                for (int i = value; i < prevValue; i++)
                {
                    changesIndicators[i].SetAlpha(1f);
                    changesIndicators[i].SwapColor(UIColorStyle.TextRed);
                }

                for (int i = prevValue; i < maxValue; i++)
                {
                    changesIndicators[i].SetAlpha(0f);
                }
            }
        }

        private void InitGradient()
        {
            if (!_fillRect) _fillRect = fill.GetComponent<RectTransform>();
            var gradientSettings = new GradientSprites.GradientSettings()
            {
                StartColor = startColor,
                MiddleColor = middleColor,
                EndColor = endColor,
                Size = _fillRect.sizeDelta
            };
            var sprite = GradientSprites.GetGradientSprite(gradientSettings);
            fill.sprite = sprite;
            fill.overrideSprite = sprite;
        }

        private void Start()
        {
            if (gradient != null)
                InitGradient();
            ResizeFill();
        }

        private void OnValidate()
        {
            if (gradient != null)
                InitGradient();
            ResizeFill();
        }
    }
    
    
}