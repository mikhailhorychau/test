using System;
using System.Collections.Generic;
using UIScripts.Observable;

namespace UIScripts.Screens.V2.Car.Chassis.ReadyToProd
{
    public interface IReadyToProductionView
    {
        public event Action<bool> OnDontShowAgainChanged;
        public event Action OnStartDevelop;
        public event Action OnViewDestroy;
        public InfoPopup Popup { get; }
        public StaticPopup ConfirmationPopup { get; }
        public string ReadyToProductionTitle { set; }
        public string NoProblemsTitle { set; }
        public string MakeImprovementTitle { set; }
        public void InitializeProblems(List<string> problems, string decided, bool dontShowAgain = false);
    }

    public class TestM
    {
        private ObservableString _value = new ObservableString();

        public string Value
        {
            get => _value;
            set => _value.Value = value;
        }
    }
}