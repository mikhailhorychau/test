using System.Collections.Generic;
using UIScripts.Observable;

namespace UIScripts.Screens.V2.Car.Chassis.ReadyToProd
{
    public class ReadyToProductionModel
    {
        public string ReadyToToProductionTitle { get; set; }
        public string DecidedTitle { get; set; }
        public string MakeImprovementTitle { get; set; }
        
        public string NoProblemsTitle { get; set; }

        public ObservableBool DontShowPopup { get; } = new ObservableBool();
        public ObservableProperty<List<string>> Problems { get; set; } 
    }
}