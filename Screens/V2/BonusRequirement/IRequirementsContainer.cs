using System.Collections.Generic;

namespace UIScripts.Screens.V2.BonusRequirement
{
    public interface IRequirementsContainer
    {
        public void Initialize(List<RequirementModel> requirements);
        public IRequirementView GetRequirementView(int id);
    }
}