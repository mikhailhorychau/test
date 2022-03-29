using UnityEngine;

namespace UIScripts
{
    public class SmallProgressBar : ProgressBar
    {
        [SerializeField] private StyledImage targetImage;
        protected override void SwapColor(UIColorStyle color) => targetImage.SwapColor(color, 1f);
        public void SwapAlpha(float alpha) => targetImage.SetAlpha(alpha);
    }
}