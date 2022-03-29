using UnityEngine;

namespace UIScripts.Screens.V2.Development.RepairAndUpgrade
{
    public class RepairAndUpgradeUI : MonoBehaviour
    {
        [SerializeField] private RepairAndUpgradeCarComponents firstCar;
        [SerializeField] private RepairAndUpgradeCarComponents secondCar;
        [SerializeField] private RepairAndUpgradeTotalPresenter topBlock;
        [SerializeField] private RepairAndUpgradeTotalPresenter bottomBlock;
        [SerializeField] private StaticPopup popup;
        [SerializeField] private Tab.TabButton tab;

        public RepairAndUpgradeCarComponents FirstCar => firstCar;
        public RepairAndUpgradeCarComponents SecondCar => secondCar;
        public RepairAndUpgradeTotalPresenter TopBlock => topBlock;
        public RepairAndUpgradeTotalPresenter BottomBlock => bottomBlock;
        public StaticPopup Popup => popup;
        public Tab.TabButton Tab => tab;
    }
}