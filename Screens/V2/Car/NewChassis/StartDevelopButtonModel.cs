using System.Collections.Generic;
using UIScripts.Observable;
using UIScripts.Screens.V2.BonusRequirement;

namespace UIScripts.Screens.V2.Car.NewChassis
{
    public class StartDevelopButtonModel
    {
        public ObservableBool InDevelop { get; } = new ObservableBool();
        public ObservableBool RequirementsComplete { get; } = new ObservableBool();
        public List<RequirementModel> Requirements { get; set; }
        public string InDevelopTitle { get; set; }
        public string StartTitle { get; set; }
        
        public string Duration { get; set; }
    }
}