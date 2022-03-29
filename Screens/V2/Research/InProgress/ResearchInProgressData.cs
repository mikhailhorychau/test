using UIScripts.Observable;

namespace UIScripts.Screens.V2.Research.InProgress
{
    public class ResearchInProgressData
    {
        public ObservableProperty<ResearchInProgressCurrentData> Current { get; } =
            new ObservableProperty<ResearchInProgressCurrentData>();
        
        public string ChooseParamForWorking { get; set; }
    }
}