using System;
using UnityEngine;

namespace UIScripts.CommonComponents.SetupProgress
{
    public class SetupProgress : MonoBehaviour
    {
        [SerializeField] private RectTransform background;

        [SerializeField] private RectTransform greenBackground;
        [SerializeField] private StyledImage fill;

        [Range(0, 100)]
        [SerializeField] private int greenZoneBound = 1;
        
        [Range(0, 100)]
        [SerializeField] private float value = 1;

        public int GreenZoneBound
        {
            get => greenZoneBound;
            set
            {
                if (greenZoneBound == value) return;
                if (value > 100)
                    greenZoneBound = 100;
                else if (value < 1)
                    greenZoneBound = 1;
                else
                    greenZoneBound = value;
                
                UpdateValues();
            }
        }

        public float Value
        {
            get => value;
            set
            {
                if (this.value == value) return;
                if (value > 100)
                    this.value = 100;
                else if (value < 1)
                    this.value = 1;
                else
                    this.value = value;
                
                UpdateValues();
            }
        }

        private void UpdateValues()
        {
            if (greenBackground)
                greenBackground.sizeDelta = GetRectSize(greenZoneBound);

            if (!fill) return;
            fill.image.rectTransform.sizeDelta = GetRectSize(value);
            fill.SwapColor(GetColorFill());
        }

        private Vector2 GetRectSize(float sizeValue) => 
            new Vector2(background.sizeDelta.x * sizeValue / 100, background.sizeDelta.y);

        private UIColorStyle GetColorFill() => 100 - value > greenZoneBound ? UIColorStyle.Title : UIColorStyle.ProgressGreen;

#if UNITY_EDITOR
        private void OnValidate()
        {
            UpdateValues();
        }
#endif
        
    }
}