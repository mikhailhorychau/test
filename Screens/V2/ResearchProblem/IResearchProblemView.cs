using UIScripts.Screens.V2.BonusRequirement;
using UIScripts.Utils.UI;

namespace UIScripts.Screens.V2.ResearchProblem
{
    public interface IResearchProblemView : IDestroyableView
    {
        public IRequirementButton Button { get; }
        public string Title { set; }
        public string Description { set; }
        public string InProgressTitle { set; }
        public string Duration { set; }
        public bool InProgress { set; }
        
        public StyledImage Background { get; }
    }
}