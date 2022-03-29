using TMPro;
using UIScripts.Screens.V2.BonusRequirement;
using UnityEngine;

namespace UIScripts.Screens.V2.Car.NewChassis
{
    public class StartDevelopButton : MonoBehaviour
    {
        [RequireInterface(typeof(IButton))]
        [SerializeField] private GameObject button;

        [SerializeField] private RequirementsContainer requirementsContainer;
        [SerializeField] private GameObject inDevelopObj;
        [SerializeField] private TextMeshProUGUI duration;

        private StartDevelopButtonModel _model;
        private RequirementsInitializer _reqInitializer = new RequirementsInitializer();

        private IButton Button => button.GetComponent<IButton>();

        public void Initialize(StartDevelopButtonModel model)
        {
            RemoveListeners();
            
            _model = model;
            
            _reqInitializer.Initialize(requirementsContainer, model.Requirements);
            
            duration.text = model.Duration;
            
            UpdateButtonActivity();
            UpdateDevelopActivity(model.InDevelop.Value);
            
            AddListeners();
        }

        private void OnDestroy()
        {
            RemoveListeners();
        }

        private void RemoveListeners()
        {
            if (_model != null)
            {
                _model.RequirementsComplete.OnValueChange -= RequirementsCompleteChangeListener;
                _model.InDevelop.OnValueChange -= InDevelopChangeListener;
                
                Button.OnClick.RemoveListener(BtnClickListener);
            }
        }
        
        private void AddListeners()
        {
            if (_model != null)
            {
                _model.RequirementsComplete.OnValueChange += RequirementsCompleteChangeListener;
                _model.InDevelop.OnValueChange += InDevelopChangeListener;
                
                Button.OnClick.AddListener(BtnClickListener);
            }
        }

        private void BtnClickListener()
        {
            if (_model != null)
            {
                _model.InDevelop.Value = true;
            }
        }

        private void InDevelopChangeListener(bool inDevelop)
        {
            UpdateButtonActivity();
            UpdateDevelopActivity(inDevelop);
        }

        private void RequirementsCompleteChangeListener(bool complete)
        {
            UpdateButtonActivity();
        }

        private void UpdateDevelopActivity(bool inDevelop)
        {
            inDevelopObj.gameObject.SetActive(inDevelop);
            requirementsContainer.gameObject.SetActive(!inDevelop);
        }

        private void UpdateButtonActivity()
        {
            Button.SetInteractivity(!IsDisabled());
            Button.Text = GetButtonTitle();
        }

        private bool IsDisabled() => _model.InDevelop.Value || !_model.RequirementsComplete.Value;
        private string GetButtonTitle() => _model.InDevelop.Value ? _model.InDevelopTitle : _model.StartTitle;
    }
}