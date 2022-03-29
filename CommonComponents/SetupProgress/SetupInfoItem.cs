using TMPro;
using UIScripts.Utils;
using UnityEngine;

namespace UIScripts.CommonComponents.SetupProgress
{
    public class SetupInfoItem : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI title;
        [SerializeField] private TextMeshProUGUI sunValue;
        [SerializeField] private TextMeshProUGUI rainValue;
        [SerializeField] private StyledImage styledImage;

        public StyledImage StyledImage => styledImage;
        
        public void Initialize(SetupInfoItemProps props, string objPrefix = "")
        {
            title.text = props.Title;
            sunValue.gameObject.name = $"{objPrefix}_sunValue{props.ID}";
            rainValue.gameObject.name = $"{objPrefix}_rainValue{props.ID}";
            sunValue.text = GetColoredString(props.SunValue);
            rainValue.text = GetColoredString(props.RainValue);
        }

        private string GetColoredString(double value)
        {
            var color = UIColorStyle.Text;
            if (value > 0)
                color = UIColorStyle.TextGreen;
            if (value < 0)
                color = UIColorStyle.TextRed;

            var str = value == 0 ? "-" : $"{value.ToSignedString()}%";

            return str.ToColorString(color);
        }
    }
}