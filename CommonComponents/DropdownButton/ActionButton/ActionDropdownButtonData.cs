using UIScripts.Screens.V2.BonusRequirement;

namespace UIScripts.CommonComponents.DropdownButton.ActionButton
{
    public struct ActionDropdownButtonData : IDropdownItemData
    {
        public ActionDropdownItemVariant Variant { get; set; }
        public int ID { get; set; }
        public string Title { get; set; }
        public string Subtitle { get; set; }
        public bool Disabled { get; set; }
        public RequirementModel Requirement { get; set; }
    }
}