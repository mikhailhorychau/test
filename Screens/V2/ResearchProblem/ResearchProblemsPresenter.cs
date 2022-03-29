using System;
using System.Collections.Generic;

namespace UIScripts.Screens.V2.ResearchProblem
{
    public class ResearchProblemsPresenter
    {
        public void InitializeProblems(IResearchProblemView view, List<ResearchProblemModel> models) =>
            models.ForEach(model => InitializeProblem(view, model));

        public IResearchProblemView InitializeProblem(IResearchProblemView view, ResearchProblemModel model)
        {
            view.Button.Title = model.RequirementsData.Title;
            view.Button.RequirementsDone = model.RequirementsData.RequirementsDone.Value;
            view.Button.InitializeRequirements(model.RequirementsData.Requirements);
            
            view.Button.OnButtonClick += () => model.InProgress.Value = true;

            view.Title = model.Title;
            view.Description = model.Description;
            view.InProgressTitle = model.InProgressTitle;
            view.Duration = model.Duration;
            view.InProgress = model.InProgress.Value;

            Action<bool> inProgressAction = value => view.InProgress = value;

            model.InProgress.OnValueChange += inProgressAction.Invoke;

            Action<bool> reqDoneAction = value => view.Button.RequirementsDone = value;
            model.RequirementsData.RequirementsDone.OnValueChange += reqDoneAction.Invoke;

            view.OnViewDestroy += () => model.InProgress.OnValueChange -= inProgressAction.Invoke;
            view.OnViewDestroy += () => model.RequirementsData.RequirementsDone.OnValueChange -= reqDoneAction.Invoke;

            return view;
        }
    }
}