using System;
using System.Collections.Generic;
using UIScripts.Utils.UI;

namespace UIScripts.Screens.V2.BonusRequirement
{
    public interface IRequirementButton : IDestroyableView
    {
        public event Action OnButtonClick;
        public string Title { set; }
        public bool RequirementsDone { set; }

        public void SetRequirementsDone(bool isDone);
        public void InitializeRequirements(List<RequirementModel> requirements);
    }
}