using UnityEngine;

namespace UIScripts.Screens.V2.BonusRequirement
{
    public class RequirementPresenter : MonoBehaviour
    {
        [RequireInterface(typeof(IRequirementView))] 
        [SerializeField]
        private GameObject requirement;

        private IRequirementView _view;
        private RequirementModel Model { get; set; }
        public IRequirementView View => _view ??= requirement.GetComponent<IRequirementView>();
        
        public void Initialize(RequirementModel model)
        {
            RemoveListeners();

            Model = model;
            
            View.Icon = model.Icon;
            View.Value = model.Value;
            View.CanBeUsed = model.CanBeUsed;
            
            AddListeners();
        }

        private void AddListeners()
        {
            if (Model != null)
            {
                Model.CanBeUsed.OnValueChange += View.SetCanBeUsed;
                Model.OnValueChange += View.SetValue;
            }

            View.OnViewDestroy += ViewDestroyListener;
        }

        private void RemoveListeners()
        {
            if (Model != null)
            {
                Model.CanBeUsed.OnValueChange -= View.SetCanBeUsed;
                Model.OnValueChange -= View.SetValue;
            }
            
            View.OnViewDestroy -= ViewDestroyListener;
        }

        private void ViewDestroyListener()
        {
            RemoveListeners();
        }
    }
}