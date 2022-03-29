using UIScripts.Screens.V2.BonusRequirement;
using UIScripts.Screens.V2.Research.Items.Reward;

namespace UIScripts.Screens.V2.Research.Items
{
    public class ResearchItemData
    {
        public int ID { get; set; }
        public string Start { get; set; }
        public string Name { get; set; }
        public string ConfirmationTitle { get; set; }
        public RequirementButtonData ButtonData { get; set; }
        public RewardData RewardData { get; set; }
    }
}