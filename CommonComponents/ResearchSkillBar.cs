using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace UIScripts.CommonComponents
{
    public class ResearchSkillBar : MonoBehaviour
    {
        [SerializeField] private SkillBar skillBar;
        [SerializeField] private List<Image> researchedIndicators;
        [SerializeField] private List<Image> researchIndicators;

        private int _researchedLvl;
        private bool _inResearch;
        
        public int Lvl
        {
            get => skillBar.Value;
            set => skillBar.Value = value;
        }

        public int ResearchedLvl
        {
            get => _researchedLvl;
            set
            {
                _researchedLvl = value < Lvl ? Lvl : value;
                
                for (var i = 0; i < researchedIndicators.Count; i++)
                {
                    var isEnabled = i >= Lvl && i < _researchedLvl;
                    researchedIndicators[i].enabled = isEnabled;
                }
            }
        }

        public bool InResearch
        {
            set
            {
                if (!value)
                {
                    ClearResearchIndicators();
                    return;
                }
                
                for (var i = 0; i < researchIndicators.Count; i++)
                {
                    var isEnabled = i == _researchedLvl - 1;
                    researchIndicators[i].enabled = isEnabled;
                }
            }
        }

        private void ClearResearchIndicators()
        {
            researchIndicators.ForEach(indicator => indicator.enabled = false);
        }
    }
}