using UIScripts.Screens.Development.Garage;
using UIScripts.Screens.V2.Development.Factory;
using UIScripts.Screens.V2.Development.RepairAndUpgrade;
using UnityEngine;

namespace UIScripts.Screens.V2.Development
{
    public class DevelopmentUI : MonoBehaviour
    {
        [SerializeField] private RepairAndUpgradeUI repairAndUpgrade;
        [SerializeField] private GarageClass garage;
        [SerializeField] private FactoriesContainer factories;
        [SerializeField] private StaticPopup confirmationPopup;

        public RepairAndUpgradeUI RepairAndUpgrade => repairAndUpgrade;
        public GarageClass Garage => garage;
        public FactoriesContainer Factories => factories;
        public StaticPopup ConfirmationPopup => confirmationPopup;
    }
}