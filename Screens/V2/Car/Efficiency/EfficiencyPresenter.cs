using System;

namespace UIScripts.Screens.V2.Car.Efficiency
{
    public class EfficiencyPresenter
    {
        public void Initialize(IEfficiencyView view, EfficiencyModel model)
        {
            view.Name = model.Name;
            view.Value = model.Value;

            Action<string> valueChangeAction = value => view.Value = value;

            model.Value.OnValueChange += valueChangeAction.Invoke;

            void ViewDestroyAction() => model.Value.OnValueChange -= valueChangeAction.Invoke;

            view.OnViewDestroy += ViewDestroyAction;
        }
    }
}