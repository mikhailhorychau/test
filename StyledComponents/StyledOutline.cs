using System;
using UnityEngine;
using UnityEngine.UI;

namespace UIScripts
{
    [RequireComponent(typeof(Outline))]
    public class StyledOutline : MonoBehaviour
    {
        [SerializeField] private UIColorStyle color;
        
        [Range(0, 1)][SerializeField] private float alpha = 1f;

        [SerializeField] private Outline outline;

        private Outline Outline
        {
            get
            {
                if (outline == null)
                    outline = GetComponent<Outline>();

                return outline;
            }
        }
        
        private void Awake()
        {
            UpdateUI();
        }

        public void UpdateColor(UIColorStyle newColor, float newAlpha = 1f)
        {
            color = newColor;
            alpha = newAlpha;
            
            UpdateUI();
        }

        private void UpdateUI()
        {
            var rgbColor = UISettings.Instance.colors.Pick(color);
            rgbColor.a = alpha;
            Outline.effectColor = rgbColor;
        }

#if UNITY_EDITOR
        private void OnValidate()
        {
            UpdateUI();
        }
#endif
    }
}