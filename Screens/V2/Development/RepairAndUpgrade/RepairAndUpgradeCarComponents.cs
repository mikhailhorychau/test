using System;
using System.Collections.Generic;
using Models.Car;
using Office.Development;
using TMPro;
using UIScripts.Screens.Development.RepairAndUpgrade;
using UIScripts.Screens.Development.RepairAndUpgrade.BottomBlock;
using UIScripts.Screens.Development.RepairAndUpgrade.CarBlock;
using UnityEngine;

namespace UIScripts.Screens.V2.Development.RepairAndUpgrade
{
    public class RepairAndUpgradeCarComponents : MonoBehaviour
    {
        public event Action OnBuildChanged;
        public event Action OnRepairChanged;
        public event Action OnUpgradeChanged;

        [SerializeField] private TextMeshProUGUI carName;
        [SerializeField] private RepairAndUpgradeChassisItem nextYearChassis;
        [SerializeField] private RepairAndUpgradeItem chassis;
        [SerializeField] private RepairAndUpgradeItem frontWing;
        [SerializeField] private RepairAndUpgradeItem rearWing;
        [SerializeField] private RepairAndUpgradeItem underBody;
        [SerializeField] private RepairAndUpgradeItem sidePods;
        [SerializeField] private RepairAndUpgradeItem coolingSystem;
        [SerializeField] private RepairAndUpgradeItem brakes;
        [SerializeField] private RepairAndUpgradeItem gearbox;
        [SerializeField] private RepairAndUpgradeItem suspension;
        [SerializeField] private RepairAndUpgradeItem hydraulics;
        [SerializeField] private RepairAndUpgradeItem engine;

        private Dictionary<AeroComponents, RepairAndUpgradeItem> _aeroComponents;
        private Dictionary<ComponentType, RepairAndUpgradeItem> _components;

        public RepairAndUpgradeChassisItem NextYearChassis => nextYearChassis;
        public readonly Dictionary<int, RepairAndUpgradeItem> items = new Dictionary<int, RepairAndUpgradeItem>();

        public void Initialize(RepairAndUpgradeCarProps data, RepairAndUpgradeStaticProps staticProps)
        {
            carName.text = data.CarName;
            InitializeAeroComponents(data.TopBlockProps, staticProps);
            InitializeComponents(data.BottomBlockProps, staticProps);
            
            nextYearChassis.Initialize(data.TopBlockProps.NextYearChassisProps, staticProps);
            nextYearChassis.onBuildChange.AddListener(BuildChangeListener);
        }

        public void InitializeAeroComponents(RepairAndUpgradeTopBlockProps data, RepairAndUpgradeStaticProps staticProps)
        {
            InitializeItem(chassis, data.ChassisProps, staticProps);
            InitializeItem(frontWing, data.FrontWingProps, staticProps);
            InitializeItem(rearWing, data.RearWingProps, staticProps);
            InitializeItem(underBody, data.UnderBodyProps, staticProps);
            InitializeItem(sidePods, data.SidePodsProps, staticProps);
            InitializeItem(coolingSystem, data.CoolingSystemProps, staticProps);
        }

        private void InitializeComponents(RepairAndUpgradeBottomBlockProps data,
            RepairAndUpgradeStaticProps staticProps)
        {
            InitializeItem(brakes, data.Brakes, staticProps);
            InitializeItem(gearbox, data.Gearbox, staticProps);
            InitializeItem(suspension, data.Suspension, staticProps);
            InitializeItem(hydraulics, data.Hydraulics, staticProps);
            InitializeItem(engine, data.Engine, staticProps);
        }

        private void InitializeItem(RepairAndUpgradeItem item, RepairAndUpgradeItemProps props, RepairAndUpgradeStaticProps staticProps)
        {
            item.Initialize(props, staticProps);
            items[props.ID] = item;
            item.onRepairChange.AddListener(RepairChangeListener);
            item.onUpgradeChange.AddListener(UpgradeChangeListener);
        }

        private void RepairChangeListener(bool value)
        {
            OnRepairChanged?.Invoke();
        }

        private void UpgradeChangeListener(bool value)
        {
            OnUpgradeChanged?.Invoke();
        }

        private void BuildChangeListener(bool value)
        {
            OnBuildChanged?.Invoke();
        }
    }
    
}