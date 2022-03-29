using System;
using System.Collections.Generic;
using UIScripts.Screens.Car;

namespace UIScripts
{
    [Serializable]
    public enum ToolTipsScreen
    {
        CurrentChassis,
        NextYearNewChassis,
        NextYearCurrentChassis,
        Cfd
    }
    
    public static class ToolTipsNames
    {
        public static readonly Dictionary<string, HashSet<string>> Categories = new Dictionary<string, HashSet<string>>();

        public static bool AddCategory(string category)
        {
            if (Categories.ContainsKey(category)) return false;

            Categories[category] = new HashSet<string>();
            return true;
        }

        private static readonly Dictionary<Enum, string> _chassisTooltips = new Dictionary<Enum, string>
        {
            {ChassisToolTip.Learning, "learning"},
            {ChassisToolTip.AddBonus, "add-bonus"},
            {ChassisToolTip.DragParameter, "drag-parameter"},
            {ChassisToolTip.ReleaseStaff, "release-staff"},
            {ChassisToolTip.DownForceParameter, "down-force-parameter"},
            {ChassisToolTip.EfficiencyBehindParameter, "efficiency-behind-parameter"},
            {ChassisToolTip.FastCornersParameter, "fast-corners-parameter"},
            {ChassisToolTip.TireWearParameter, "tire-wear-parameter"},
            {ChassisToolTip.SlowCornersParameter, "slow-corners-parameter"},
            {ChassisToolTip.GreenZone, "green-zone"},
            {ChassisToolTip.ReturnStaff, "return-staff"}
        };

        private static readonly Dictionary<Enum, string> _cfdTooltips = new Dictionary<Enum, string>
        {
            {CfdToolTip.AddBonus, "add-bonus"},
            {CfdToolTip.ReleaseStaff, "release-staff"},
            {CfdToolTip.ReturnStaff, "return-staff"},
            {CfdToolTip.CoolingSystem, "cooling-system"},
            {CfdToolTip.FrontWing, "front-wing"},
            {CfdToolTip.RearWing, "rear-wing"},
            {CfdToolTip.SidePods, "side-pods"},
            {CfdToolTip.UnderBody, "under-body"}
        };

        private static Dictionary<ToolTipsScreen, Dictionary<Enum, string>> _screens =
            new Dictionary<ToolTipsScreen, Dictionary<Enum, string>>()
            {
                {ToolTipsScreen.CurrentChassis, _chassisTooltips},
                {ToolTipsScreen.NextYearNewChassis, _chassisTooltips},
                {ToolTipsScreen.NextYearCurrentChassis, _chassisTooltips},
                {ToolTipsScreen.Cfd, _cfdTooltips}
            };
        
        

        public static Dictionary<Enum, string> GetScreenTooltipNames(ToolTipsScreen screenType) => _screens[screenType];
    }
}