using System;
using TMPro;
using UnityEngine;

namespace UIScripts.TitleValue
{
    public class TitleStringValue : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI title;
        [SerializeField] private TextMeshProUGUI value;

        [SerializeField] private TitleValueProps<string, string> props;

        public void SwapColor(UIColorStyle color, bool onTitle = true, bool onValue = true)
        {
            if (onTitle)
                title.GetComponent<StyledText>().SwapColor(color);
            
            if (onValue)
                value.GetComponent<StyledText>().SwapColor(color);
        }

        public TitleValueProps<string, string> Props
        {
            get => props;
            set
            {
                gameObject.SetActive(value != null);
                if (value == null) return;
                props = value;
                Initialize();
            }
        }

        public string Title
        {
            get => title.text;
            set => title.text = value;
        }

        public string Value
        {
            get => value.text;
            set => this.value.text = value;
        }

        private void Initialize()
        {
            if (title != null)
                title.text = props.Title;
            
            if (value != null)
                value.text = props.Value;
        }

#if UNITY_EDITOR
        private void OnValidate()
        {
            Initialize();
        }
#endif
    }
}