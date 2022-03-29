using UnityEngine;
using UnityEngine.UI;

namespace UIScripts
{
    public abstract class ProgressBar : MonoBehaviour
    {
        [SerializeField] private UIColorStyle goodColor;
        [SerializeField] private UIColorStyle middleColor;
        [SerializeField] private UIColorStyle badColor;

        [Range(0, 100)]
        [SerializeField] private int goodBound;
        [Range(0, 100)]
        [SerializeField] private int middleBound;

        [Range(0, 100)] [SerializeField] private int value;

        public int Value
        {
            get => value;
            set
            {
                this.value = value;
                UpdateUI();
            }
        }

        private Slider _slider;

        public void UpdateUI()
        {
            if (!_slider) _slider = GetComponent<Slider>();

            _slider.value = value;
            if (_slider.value > goodBound) 
                SwapColor(goodColor);
            else if (_slider.value > middleBound) 
                SwapColor(middleColor);
            else 
                SwapColor(badColor);
        }
        private void Awake()
        {
            UpdateUI();
        }

        protected abstract void SwapColor(UIColorStyle color);
        private void OnValidate()
        {
            UpdateUI();
        }
    }
}