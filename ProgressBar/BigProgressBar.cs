using UnityEngine;

namespace UIScripts
{
    public class BigProgressBar : ProgressBar
    {
        [SerializeField] private UIGradient fillGradient;
        protected override void SwapColor(UIColorStyle color) => 
            fillGradient.m_color2 = UISettings.Instance.colors.Pick(color);
    }
}