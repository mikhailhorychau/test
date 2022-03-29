using UIScripts.Observable;

namespace UIScripts.Screens.V2.BonusRequirement
{
    public class SingleRequirementModel
    {
        public ObservableBool RequirementsDone { get; } = new ObservableBool();
        public RequirementModel Requirement { get; set; } 
        public string Title { get; set; } 
    }
}