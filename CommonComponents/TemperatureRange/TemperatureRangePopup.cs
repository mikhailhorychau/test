using TMPro;
using UnityEngine;

namespace UIScripts.CommonComponents.TemperatureRange
{
    public class TemperatureRangePopup : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI rangeValue;
        [SerializeField] private TextMeshProUGUI descriptionValue;

        public string RangeValue
        {
            get => rangeValue.text;
            set => rangeValue.text = value;
        }

        public string DescriptionValue
        {
            get => descriptionValue.text;
            set => descriptionValue.text = value;
        }
    }
}