using TMPro;
using UIScripts.CommonComponents;
using UnityEngine;

namespace UIScripts.Screens.V2.Car.Chassis
{
    public class ChassisInfo : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI nameView;
        [SerializeField] private TextMeshProUGUI chassisTitle;
        [SerializeField] private ResearchSkillBar skillBar;

        public string Name
        {
            set => nameView.text = value;
        }

        public string ChassisTitle
        {
            set => chassisTitle.text = value;
        }

        public int Level
        {
            set => skillBar.Lvl = value;
        }

        public int ResearchLvl
        {
            set => skillBar.ResearchedLvl = value;
        }

        public bool InResearch
        {
            set => skillBar.InResearch = value;
        }

        public void Initialize(CurrentChassisInfoData data)
        {
            Name = data.Name;
            ChassisTitle = data.ChassisTitle;
            Level = data.Level;
            ResearchLvl = data.ResearchLvl;
            InResearch = data.InResearch;
        }
    }

    public struct CurrentChassisInfoData
    {
        public string Name { get; set; }
        public string ChassisTitle { get; set; }
        public int Level { get; set; }
        public int ResearchLvl { get; set; }
        public bool InResearch { get; set; }

        public CurrentChassisInfoData(string name, string chassisTitle, int level, int researchLvl, bool inResearch)
        {
            Name = name;
            ChassisTitle = chassisTitle;
            Level = level;
            ResearchLvl = researchLvl;
            InResearch = inResearch;
        }
    } 
}