using TMPro;
using UnityEngine;

namespace UIScripts.CommonComponents
{
    public class PopupWithInput : StaticPopup
    {
        [SerializeField] private StyledTextInput input;
        [SerializeField] private TextMeshProUGUI label;

        public string Label
        {
            set => label.text = value;
        }
        public StyledTextInput Field => input;
    }
}