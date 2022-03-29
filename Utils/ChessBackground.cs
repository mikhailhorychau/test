using UnityEngine;

namespace UIScripts
{
    public class ChessBackground : UIElement
    {
        [SerializeField] private Transform container;

        [Space] [SerializeField] private UIColorStyle firstColor = UIColorStyle.Black;
        [SerializeField] private float firstColorAlpha = 0.7f;
    
        [Space] [SerializeField] private UIColorStyle secondColor = UIColorStyle.MainBackground;
        [SerializeField] private float secondColorAlpha = 0.9f;
    
        [Space] [SerializeField] private UIColorStyle additionalColor = UIColorStyle.Selection;
        [SerializeField] private float additionalColorAlpha = 0.9f;

        private void Start()
        {
            UpdateUI();
        }
    
        public override void UpdateUI()
        {
            if (!container) return;
            var index = 0;

            foreach (Transform element in container)
            {
                var styledImage = element.GetComponent<StyledImage>();
                if (!styledImage) return;

                index++;
                var additional = element.GetComponent<IChessBackgroundAdditionalItem>();
                if (additional != null && additional.IsAdditional())
                {
                    styledImage.SwapColor(additionalColor, additionalColorAlpha);
                }
                else
                {
                    var color = index % 2 != 0 ? firstColor : secondColor;
                    var alpha = index % 2 != 0 ? firstColorAlpha : secondColorAlpha;
                    styledImage.SwapColor(color, alpha);
                }
            }
        }

        private void OnValidate()
        {
            UpdateUI();
        }
    }
}