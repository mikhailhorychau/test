using TMPro;
using UIScripts.Screens.V2.Car.Efficiency;
using UIScripts.Screens.V2.Car.NewChassis.DevelopBlock;
using UnityEngine;
using UnityEngine.UI;

namespace UIScripts.Screens.V2.Car.NewChassis
{
    public class NewChassisUI : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI chassisTitle;
        [SerializeField] private TextMeshProUGUI greenZoneTitle;
        [SerializeField] private SkillBar level;
        [SerializeField] private Slider greenZone;
        [SerializeField] private TextMeshProUGUI startDevDescription;
        [SerializeField] private NewChassisDevelopUI newChassisDevelop;
        [SerializeField] private EfficiencyContainer efficiencyContainer;
        [SerializeField] private StartDevelopButton startDevButton;

        public string ChassisTitle
        {
            set => chassisTitle.text = value;
        }

        public int Level
        {
            set => level.Value = value;
        }

        public float GreenZone
        {
            set => greenZone.value = value;
        }

        public string GreenZoneTitle
        {
            set => greenZoneTitle.text = value;
        }

        public string StartDevDescription
        {
            set => startDevDescription.text = value;
        }

        public NewChassisDevelopUI NewChassisDevelop => newChassisDevelop;

        public EfficiencyContainer EfficiencyContainer => efficiencyContainer;

        public StartDevelopButton StartDevButton => startDevButton;
    }
    
}