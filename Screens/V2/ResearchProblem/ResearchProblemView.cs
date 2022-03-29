using System;
using TMPro;
using UIScripts.Screens.V2.BonusRequirement;
using UnityEngine;

namespace UIScripts.Screens.V2.ResearchProblem
{
    public class ResearchProblemView : MonoBehaviour, IResearchProblemView
    {
        [SerializeField] private TextMeshProUGUI title;
        [SerializeField] private TextMeshProUGUI description;
        [SerializeField] private TextMeshProUGUI inProgress;
        [SerializeField] private TextMeshProUGUI duration;
        [SerializeField] private GameObject inProgressObj;
        [SerializeField] private StyledImage background;
        
        [RequireInterface(typeof(IRequirementButton))]
        [SerializeField] private GameObject reqButton;

        public event Action OnViewDestroy;
        public IRequirementButton Button => reqButton.GetComponent<IRequirementButton>();

        public string Title
        {
            set => title.text = value;
        }

        public string Description
        {
            set => description.text = value;
        }

        public string InProgressTitle
        {
            set => inProgress.text = value;
        }

        public string Duration
        {
            set => duration.text = value;
        }

        public bool InProgress
        {
            set
            {
                inProgressObj.SetActive(value);
                reqButton.SetActive(!value);
            }
        }

        public StyledImage Background => background;

        private void OnDestroy()
        {
            OnViewDestroy?.Invoke();
        }
    }
}