using UIScripts.Screens.V2.BonusRequirement;
using UIScripts.Utils.UI;

namespace UIScripts.Screens.V2.Car.CFD.Components
{
    public interface ICarComponentView : IDestroyableView
    {
        public string Name { set; }
        public int Level { set; }
        public IRequirementButton Button { get; }
        
        public string Duration { set; }
        public string InProgressTitle { set; }
        
        public bool InProgress { set; }
    }
}