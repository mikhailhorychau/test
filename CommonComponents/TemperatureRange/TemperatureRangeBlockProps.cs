using System;
using UnityEngine;

namespace UIScripts.CommonComponents.TemperatureRange
{
    [Serializable]
    public class TemperatureRangeBlockProps
    {
        [SerializeField] private float value;
        [SerializeField] private int startValue;
        [SerializeField] private int endValue;
        [SerializeField] private string description;
        [SerializeField] private string startValueText;
        [SerializeField] private string endValueText;

        public float Value
        {
            get => value;
            set => this.value = value;
        }

        public int StartValue
        {
            get => startValue;
            set => startValue = value;
        }

        public int EndValue
        {
            get => endValue;
            set => endValue = value;
        }

        public string Description
        {
            get => description;
            set => description = value;
        }

        public string StartValueText
        {
            get => startValueText;
            set => startValueText = value;
        }

        public string EndValueText
        {
            get => endValueText;
            set => endValueText = value;
        }
    }
}