using System.Collections.Generic;
using UIScripts.Screens.V2.BonusRequirement;
using UnityEngine;
using UnityEngine.UI;

namespace UIScripts.CommonComponents.NewSkillBar
{
    public class LvlBar : MonoBehaviour
    {
        [SerializeField] private List<Image> lvlIndicators;
        [SerializeField] private List<Image> researchIndicators;

        [RequireInterface(typeof(IButton))] 
        [SerializeField] private GameObject startButton;
        
        [RequireInterface(typeof(IButton))] 
        [SerializeField] private GameObject cancelButton;

        [SerializeField] private SingleRequirementButtonPresenter singleReqButton;

        private IButton _startBtn;
        private IButton _cancelBtn;

        public SingleRequirementButtonPresenter RequirementButton => singleReqButton;
        
        public int Lvl { get; set; }
        public bool InResearch { get; set; }
        public bool CanBeResearched { get; set; }
        
        private IButton StartButton => _startBtn ??= startButton.GetComponent<IButton>();
        private IButton CancelButton => _cancelBtn ??= cancelButton.GetComponent<IButton>();
    }
}