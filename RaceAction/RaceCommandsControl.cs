using System.Collections.Generic;
using TMPro;
using UIScripts.Screens.Panels.HeaderMenu.Bonuses;
using UIScripts.Screens.SessionPanel;
using UIScripts.Screens.SessionPanel.PlayerDriverBlock;
using UIScripts.Screens.SessionPanel.Windows;
using UnityEngine;

namespace UIScripts.RaceAction
{
    public class RaceCommandsControl : WindowBase, IPlayerDriverNumberContainer
    {
        [SerializeField] private DriverNumber driverNumber;
        [SerializeField] private TextMeshProUGUI title;
        [SerializeField] private TextMeshProUGUI blockerTitle;
        [SerializeField] private GameObject blocker;
        [SerializeField] private BonusesContainer bonuses;
        [SerializeField] private RaceCommandButtonPair overtake;
        [SerializeField] private RaceCommandButtonPair tyres;
        [SerializeField] private RaceCommandButtonPair engine;
        [SerializeField] private StyledIconButton button;

        private int _id;

        private void Awake()
        {
            SessionPanel.OnSceneInitialized += SceneInitializedListener;
            button.onClick.AddListener(ToggleActivity);
        }

        private void OnDestroy()
        {
            SessionPanel.OnSceneInitialized -= SceneInitializedListener;
            button.onClick.RemoveListener(ToggleActivity);
        }

        protected void SceneInitializedListener()
        {
            var interactor = SessionPanel.Scene.GetInteractor<RaceCommandsInteractor>();
            var bonusesBehaviour = interactor.BonusesBehaviour;
            var commandsBehaviour = interactor.CommandsBehaviour;
            var driverStorage = SessionPanel.Scene.GetStorage<PlayerDriversStorage>();
            _id = driverStorage.GetPropsByDriverNumber(driverNumber).DriverID;

            bonusesBehaviour.OnModelsInitialize += BonusesInitializeListener;
            commandsBehaviour.OnModelInitialize += CommandsInitializeListener;
            interactor.Initialize();
            var staticProps = SessionPanel.Scene.GetStorage<SessionStorage>().GetStaticProps();
            title.text = staticProps.RaceOrders;
            blockerTitle.text = staticProps.CarInGarage;
            Close();
        }

        private void BonusesInitializeListener(Dictionary<BonusType, BonusModel> bonusModels)
        {
            foreach (var (type, model) in bonusModels)
            {
                bonuses.GetPresenter(type).Initialize(model);
            }
        }

        private void CommandsInitializeListener(int id, RaceCommandsModel models)
        {
            if (id != _id) return;

            overtake.Initialize(models.Overtake);
            tyres.Initialize(models.Tyres);
            engine.Initialize(models.Engine);
        }
        

        public void SetDriverNumber(DriverNumber newNumber)
        {
            driverNumber = newNumber;
        }

        private void BtnClickListener() => ToggleActivity();
    }
}