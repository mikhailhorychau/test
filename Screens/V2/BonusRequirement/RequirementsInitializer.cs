using System;
using System.Collections.Generic;

namespace UIScripts.Screens.V2.BonusRequirement
{
    public class RequirementsInitializer
    {
        public void Initialize(IRequirementsContainer container, List<RequirementModel> requirements)
        {
            container.Initialize(requirements);

            requirements.ForEach(requirement =>
            {
                var view = container.GetRequirementView(requirement.ID);
                view.Icon = requirement.Icon;
                view.Value = requirement.Value;

                view.CanBeUsed = requirement.CanBeUsed.Value;

                Action<bool> canBeUsedAction = (value) => view.CanBeUsed = value;
                requirement.CanBeUsed.OnValueChange += canBeUsedAction.Invoke;

                Action<string> valueAction = (value) => view.Value = value;
                requirement.OnValueChange += valueAction.Invoke;
                
                view.OnViewDestroy += () =>
                {
                    requirement.CanBeUsed.OnValueChange -= canBeUsedAction.Invoke;
                    requirement.OnValueChange -= valueAction.Invoke;
                };
            });
            
        }
    }
}