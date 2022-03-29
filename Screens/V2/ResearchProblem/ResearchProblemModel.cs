using UIScripts.Observable;
using UIScripts.Screens.V2.BonusRequirement;

namespace UIScripts.Screens.V2.ResearchProblem
{
    public class ResearchProblemModel
    {
        public int ID { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string InProgressTitle { get; set; }
        public string Duration { get; set; }
        public ObservableBool InProgress { get; } = new ObservableBool();
        public RequirementButtonData RequirementsData { get; set; }
    }
}