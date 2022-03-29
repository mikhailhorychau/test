using System;
using UnityEngine;

namespace UIScripts.CommonComponents
{
    public class ProgressIndicators : MonoBehaviour
    {
        [SerializeField] private PureSkillBar skillBar;
        [SerializeField] private StyledImage fill;

        public int Value
        {
            get => skillBar.Value;
            set => skillBar.Value = value;
        }
        
        public void SetType(ProgressType type)
        {
            var color = GetColorByType(type);
            fill.SwapColor(color);
        }

        public void SetValue(int value) => Value = value;

        private UIColorStyle GetColorByType(ProgressType type)
        {
            return type switch
            {
                ProgressType.Increased => UIColorStyle.ProgressGreen,
                ProgressType.Decreased => UIColorStyle.TextRed,
                _ => throw new ArgumentOutOfRangeException()
            };
        }
    }

    public enum ProgressType
    {
        Increased,
        Decreased
    }
}