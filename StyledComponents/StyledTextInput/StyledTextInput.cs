using System;
using TMPro;
using UnityEngine;

namespace UIScripts
{
    public class StyledTextInput : MonoBehaviour
    {
        [Serializable]
        private class TextInputStyle
        {
            public UIColorStyle dividerColor;
            public TextStyle textStyle;
        }

        [SerializeField] private TextInputStyle commonStyle;
        [SerializeField] private TextInputStyle errorStyle;
    
        [SerializeField] private StyledText text;
        [SerializeField] private StyledImage divider;

        [SerializeField] private bool withValidate = false;
        [SerializeField] private bool isError = false;
        public bool IsError => isError;
        
        private TMP_InputField _textMeshInput;
        private TextMeshProUGUI _placeHolder;

        private TMP_InputField Input
        {
            get
            {
                if (_textMeshInput == null)
                    _textMeshInput = GetComponent<TMP_InputField>();

                return _textMeshInput;
            }
        }

        public string Placeholder
        {
            set
            {
                if (!_placeHolder)
                    _placeHolder = Input.placeholder.GetComponent<TextMeshProUGUI>();

                _placeHolder.text = value;
            }
        }

        public string Value
        {
            get => Input.text;
            set => Input.text = value;
        }

        private void Awake()
        {
            _textMeshInput = GetComponent<TMP_InputField>();
        }

        public void SetError(bool value)
        {
            if (!withValidate)
                return;
            isError = value;
            UpdateUI();
        } 

        public void Validate()
        {
            if (!_textMeshInput) _textMeshInput = GetComponent<TMP_InputField>();
            if (!_textMeshInput) return;

            isError = _textMeshInput.text.Length == 0;
            UpdateUI();
        } 
    
        public void UpdateUI()
        {
            var style = isError ? errorStyle : commonStyle;
        
            divider.colorStyle = style.dividerColor;
            divider.UpdateUI();
            text.textStyle = style.textStyle;
            text.UpdateUI();
        }

        private void OnValidate()
        {
            UpdateUI();
        }
    }
}