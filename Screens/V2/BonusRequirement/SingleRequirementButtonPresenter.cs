using System;
using UnityEngine;

namespace UIScripts.Screens.V2.BonusRequirement
{
    public class SingleRequirementButtonPresenter : MonoBehaviour
    {
        [RequireInterface(typeof(ISingleRequirementButton))] 
        [SerializeField]
        private GameObject reqButton;

        private SingleRequirementModel _model;
        private ISingleRequirementButton _reqButton;

        public event Action OnClick
        {
            add => ReqButton.Button.OnClick.AddListener(value.Invoke);
            remove => ReqButton.Button.OnClick.RemoveListener(value.Invoke);
        }
        
        public ISingleRequirementButton ReqButton => _reqButton ??= reqButton.GetComponent<ISingleRequirementButton>();

        public void Initialize(SingleRequirementModel model)
        {
            RemoveListeners();
            _model = model;
            
            ReqButton.Requirement.Initialize(model.Requirement);
            ReqButton.Title = model.Title;
            ReqButton.SetRequirementsDone(model.RequirementsDone);

            AddListeners();
        }

        private void AddListeners()
        {
            if (_model != null)
            {
                _model.RequirementsDone.OnValueChange += ReqButton.SetRequirementsDone;
            }
        }

        private void RemoveListeners()
        {
            if (_model != null)
            {
                _model.RequirementsDone.OnValueChange -= ReqButton.SetRequirementsDone;
            }
        }

        private void OnDestroy()
        {
            RemoveListeners();
        }
    }
}