using System;
using System.Collections.Generic;
using UnityEngine;

namespace UIScripts.Screens.V2.BonusRequirement
{
    
    [AddComponentMenu("UI/Requirements/RequirementButton")]
    public class RequirementButton : MonoBehaviour, IRequirementButton
    {
        public event Action OnViewDestroy;
        
        [RequireInterface(typeof(IButton))]
        [SerializeField] private GameObject button;
        [RequireInterface(typeof(IRequirementsContainer))]
        [SerializeField] private GameObject container;

        private RequirementsInitializer _reqInitializer = new RequirementsInitializer();
        private IButton _button;

        public RequirementsInitializer ReqInitializer
        {
            get => _reqInitializer;
            set => _reqInitializer = value;
        }
        
        private IRequirementsContainer RequirementsContainer => container.GetComponent<IRequirementsContainer>();
        private IButton Button => _button ??= button.GetComponent<IButton>();
        
        public event Action OnButtonClick
        {
            add => Button.OnClick.AddListener(value.Invoke);
            remove => Button.OnClick.RemoveListener(value.Invoke);
        }

        public string Title
        {
            set => Button.Text = value;
        }

        public bool RequirementsDone
        {
            set => Button.SetInteractivity(value);
        }

        public void SetRequirementsDone(bool reqDone)
        {
            RequirementsDone = reqDone;
        }

        private void OnDestroy()
        {
            OnViewDestroy?.Invoke();
        }

        public void InitializeRequirements(List<RequirementModel> requirements)
        {
            _reqInitializer.Initialize(RequirementsContainer, requirements);
        }
        
    }
}