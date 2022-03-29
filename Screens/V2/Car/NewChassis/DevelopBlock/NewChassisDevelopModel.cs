using UIScripts.Observable;

namespace UIScripts.Screens.V2.Car.NewChassis.DevelopBlock
{
    public class NewChassisDevelopModel
    {
        public NewChassisDevelopStatic Static { get; set; }
        public ObservableFloat Angle { get; } = new ObservableFloat();
        public ObservableFloat Clearance { get; } = new ObservableFloat();
        public ObservableFloat Length { get; } = new ObservableFloat();
        public ObservableBool InDevelop { get; } = new ObservableBool();
    }
}