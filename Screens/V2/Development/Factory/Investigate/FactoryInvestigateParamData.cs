using UIScripts.Observable;
using UIScripts.Screens.V2.DurationButton;

namespace UIScripts.Screens.V2.Development.Factory.Investigate
{
    public class FactoryInvestigateParamData
    {
        public string Name { get; set; }
        public ObservableInt Level { get; } = new ObservableInt();
        public int BannedLevel { get; set; }
        public ObservableBool InInvestigate { get; } = new ObservableBool();
        public ProgressReqButtonData ProgressData { get; set; } 
        
        public string ConfirmationTitle { get; set; }
    }
}