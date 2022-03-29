using System;
using TMPro;
using UIScripts.Screens.V2.BonusRequirement;
using UnityEngine;

namespace UIScripts.Screens.V2.Car.CFD.Components
{
    public class CarComponent : MonoBehaviour, ICarComponentView
    {
        private const int MAX_LVL = 10;

        public event Action OnViewDestroy;
        
        [SerializeField] private TextMeshProUGUI nameTitle;
        [SerializeField] private SkillBar skillBar;
        [SerializeField] private GameObject interactObj;
        [SerializeField] private GameObject inProgressObj;
        [SerializeField] private TextMeshProUGUI inProgress;
        [SerializeField] private TextMeshProUGUI durationValue;

        [RequireInterface(typeof(IRequirementButton))] 
        [SerializeField] private GameObject reqButton;


        public string Name
        {
            set => nameTitle.text = value;
        }

        public int Level
        {
            set => SetLevel(value);
        }

        public string InProgressTitle
        {
            set => inProgress.text = value;
        }

        public string Duration
        {
            set => durationValue.text = value;
        }

        public bool InProgress
        {
            set
            {
                inProgressObj.SetActive(value);
                reqButton.SetActive(!value);
            }
        }

        public IRequirementButton Button => reqButton.GetComponent<IRequirementButton>();

        public void OnDestroy()
        {
            OnViewDestroy?.Invoke();
        }

        private void SetLevel(int lvl)
        {
            skillBar.Value = lvl;

            var isMaxLvl = lvl == MAX_LVL;
            
            interactObj.SetActive(!isMaxLvl);
        }
        
    }
}