using System;

namespace UIScripts.Screens.V2.Car.CFD.Components
{
    public class CarComponentsPresenter
    {
        public event Action<int> OnUpgradeAccept; 

        private StaticPopup _popup;
        public CarComponentsPresenter(StaticPopup popup)
        {
            _popup = popup;
        }
        
        public void Initialize(ICarComponentView view, CarComponentModel model)
        {
            view.Button.InitializeRequirements(model.RequirementsData.Requirements);
            view.Button.Title = model.RequirementsData.Title;
            view.Button.RequirementsDone = model.RequirementsData.RequirementsDone.Value;

            Action<bool> requirementsDoneAction = (value) => view.Button.RequirementsDone = value;
            model.RequirementsData.RequirementsDone.OnValueChange += requirementsDoneAction.Invoke;
            
            view.Button.OnViewDestroy += () =>
                model.RequirementsData.RequirementsDone.OnValueChange -= requirementsDoneAction.Invoke;

            view.Name = model.Name;
            view.Level = model.Level;
            view.Duration = model.Duration;
            view.InProgress = model.InProgress.Value;
            view.InProgressTitle = model.InProgressTitle;

            Action<bool> inProgressChangeAction = (value) => view.InProgress = value;
            model.InProgress.OnValueChange += inProgressChangeAction.Invoke;

            view.Button.OnButtonClick += () => BtnClickListener(model);
            view.OnViewDestroy += () => model.InProgress.OnValueChange -= inProgressChangeAction.Invoke;

        }
        
        private void BtnClickListener(CarComponentModel model) 
        {
            _popup.gameObject.SetActive(true);
            _popup.onAccept.AddListener(() => OnUpgrade(model));
            _popup.Title = model.ConfirmationTitle;
        }

        private void OnUpgrade(CarComponentModel model)
        {
            OnUpgradeAccept?.Invoke(model.ID);
            model.InProgress.Value = true;
        }
    }
}