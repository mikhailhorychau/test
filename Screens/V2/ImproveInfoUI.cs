using TMPro;
using UnityEngine;

namespace UIScripts.Screens.V2
{
    public class ImproveInfoUI : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI upgrade;
        [SerializeField] private TextMeshProUGUI duration;
        [SerializeField] private TextMeshProUGUI perWeekTitle;
        [SerializeField] private TextMeshProUGUI perWeekValue;
        [SerializeField] private TextMeshProUGUI leftTitle;
        [SerializeField] private TextMeshProUGUI leftValue;

        public void Initialize(ImproveInfoData data)
        {
            upgrade.text = $"{data.Upgrade}({data.Level})";
            duration.text = data.Duration;
            perWeekTitle.text = data.PerWeekTitle;
            perWeekValue.text = data.PerWeekValue;
            leftTitle.text = data.LeftTitle;
            leftValue.text = data.LeftValue;
        }
    }

    public struct ImproveInfoData
    {
        public string Upgrade { get; set; }
        public int Level { get; set; }
        public string Duration { get; set; }
        public string PerWeekTitle { get; set; }
        public string PerWeekValue { get; set; }
        public string LeftTitle { get; set; }
        public string LeftValue { get; set; }
    }
}