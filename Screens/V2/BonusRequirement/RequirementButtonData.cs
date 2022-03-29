using System.Collections.Generic;
using UIScripts.Observable;

namespace UIScripts.Screens.V2.BonusRequirement
{
    public class RequirementButtonData
    {
        public string Title { get; set; }
        public string ReqNotDoneTitle { get; set; }
        public ObservableBool RequirementsDone { get; } = new ObservableBool();
        public List<RequirementModel> Requirements { get; set; } = new List<RequirementModel>();
    }
}