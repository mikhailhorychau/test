using UIScripts.Observable;
using UIScripts.Screens.V2.BonusRequirement;

namespace UIScripts.Screens.V2.Car.CFD.Components
{
    public class CarComponentModel
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public int Level { get; set; }
        public string Duration { get; set; }
        public string InProgressTitle { get; set; }
        
        public string ConfirmationTitle { get; set; }
        public ObservableBool InProgress { get; } = new ObservableBool();
        public RequirementButtonData RequirementsData { get; set; }
    }
}