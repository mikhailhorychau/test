using System;
using UIScripts.Observable;
using UnityEngine;

namespace UIScripts.Screens.V2.BonusRequirement
{
    [AddComponentMenu("UI/Requirements/RequirementsButtonPresenter")]
    public class RequirementsButtonPresenter : MonoBehaviour
    {
        [SerializeField] 
        [RequireInterface(typeof(IRequirementButton))]
        private GameObject reqButton;

        private IRequirementButton _reqButton;
        private RequirementButtonData _data;

        public event Action OnClick
        {
            add => Button.OnButtonClick += value;
            remove => Button.OnButtonClick -= value;
        }
        public IRequirementButton Button => _reqButton ??= reqButton.GetComponent<IRequirementButton>();
        
        public void Initialize(RequirementButtonData data)
        {
            RemoveListeners();
            _data = data;

            Button.Title = data.Title;
            Button.RequirementsDone = data.RequirementsDone;
            Button.InitializeRequirements(data.Requirements);
            
            _data.RequirementsDone.AddViewSubscriber(Button, Button.SetRequirementsDone);
        }

        private void RemoveListeners()
        {
            if (_data != null)
            {
                _data.RequirementsDone.OnValueChange -= Button.SetRequirementsDone;
            }
        }
    }
}