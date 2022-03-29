using System;
using TMPro;
using UnityEngine;

namespace UIScripts
{
    public class StyledText : MonoBehaviour, IAlphaChanger
    {
    
        public TextStyle textStyle;

        public string Text
        {
            get => TextMesh.text;
            set => TextMesh.text = value;
        }

        public TextMeshProUGUI TextMesh
        {
            get
            {
                if (!_text) _text = GetComponent<TextMeshProUGUI>();
                return _text;
            }
        }
        
        private TextMeshProUGUI _text;
    
        public void UpdateUI()
        {

            var color = UISettings.Instance.colors.Pick(textStyle.colorStyle);
            color.a = textStyle.alpha;
            TextMesh.color = color;
            TextMesh.font = UISettings.Instance.fonts.Pick(textStyle.fontStyle);
        }

        public void SwapColor(UIColorStyle color)
        {
            textStyle.colorStyle = color;
            UpdateUI();
        }
        

        public void SetAlpha(float alpha)
        {
            textStyle.alpha = alpha;
            var color = TextMesh.color;
            color.a = alpha;
            TextMesh.color = color;
        }

        public void SetFontStyle(UIFontStyle style)
        {
            textStyle.fontStyle = style;
            UpdateUI();
        }

        public void SwapStyle(TextStyle style)
        {
            textStyle = style;
            UpdateUI();
        }
    
        private void Start()
        {
            UpdateUI();
        }

        private void OnValidate()
        {
            UpdateUI();
        }
    }
}