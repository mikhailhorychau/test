using UIScripts.Utils;
using UnityEngine;
using UnityEngine.UI;

namespace UIScripts.CommonComponents
{
    public class GradientImage : MonoBehaviour
    {
        [SerializeField] private RectTransform rectTransform;
        [SerializeField] private Image fill;
        [SerializeField] private GradientSprites.GradientSettings gradientColors;
        
        private Vector2 RectSize => rectTransform.sizeDelta;

        private void Awake()
        {
            ChangeSprite();
        }

        private void ChangeSprite()
        {
            if (!rectTransform) return;
            if (!fill) return;
            
            var gradConfig = gradientColors;
            gradConfig.Size = RectSize;
            var sprite = GradientSprites.GetGradientSprite(gradConfig);

            fill.overrideSprite = sprite;
        }

        private void OnValidate()
        {
            ChangeSprite();
        }
    }
}