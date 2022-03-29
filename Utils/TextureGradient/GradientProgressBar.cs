using System;
using ExtUI;
using UnityEngine;
using UnityEngine.UI;

namespace UIScripts
{
    public class GradientProgressBar : MonoBehaviour
    {
        public Gradient gradient;
        public Color emptyBlockColor;
        public Slider slider;
        public Image fill;
        public Image emptyBlock;
        private float _value;

        [SerializeField]
        public float Value
        {
            get => _value;
            set => SetValue(value);
        }

        private void Start()
        {
            Initialize();
        }

        public void Initialize()
        {
            fill.sprite = gradient.ToTexture(Convert.ToInt32(slider.GetComponent<RectTransform>().rect.width),
                Convert.ToInt32(slider.GetComponent<RectTransform>().rect.height)).ToSprite();

            if (emptyBlock != null)
            {
                emptyBlock.color = emptyBlockColor;
            }

            slider.onValueChanged.RemoveListener(OnValueChange);
            slider.onValueChanged.AddListener(OnValueChange);
            SetValue(_value);
        }

        private void OnValueChange(float progressValue)
        {
            _value = slider.maxValue - progressValue;
        }

        private void OnValidate()
        {
// #if UNITY_EDITOR
            // Initialize();
// #endif
        }

        public void SetValue(float progressValue)
        {
            _value = progressValue;
            slider.value = slider.maxValue - progressValue;
        }
    }
}