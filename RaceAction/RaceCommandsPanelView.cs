using UIScripts.Screens.SessionPanel;
using UIScripts.Screens.SessionPanel.PlayerDriverBlock;
using UnityEngine;

namespace UIScripts.RaceAction
{
    public class RaceCommandsPanelView : PlayerDriverViewBehaviour
    {
        [SerializeField] private RaceCommandPair overtake;
        [SerializeField] private RaceCommandPair tyres;
        [SerializeField] private RaceCommandPair engine;

        private int _id;
        protected override void SceneInitializedListener()
        {
            var interactor = SessionPanel.Scene.GetInteractor<RaceCommandsInteractor>();
            var commandsBehaviour = interactor.CommandsBehaviour;
            var driverStorage = SessionPanel.Scene.GetStorage<PlayerDriversStorage>();
            _id = driverStorage.GetPropsByDriverNumber(driverNumber).DriverID;
            commandsBehaviour.OnModelInitialize += CommandsInitializeListener;
        }
        
        private void CommandsInitializeListener(int id, RaceCommandsModel models)
        {
            if (id != _id) return;

            overtake.Initialize(models.Overtake);
            tyres.Initialize(models.Tyres);
            engine.Initialize(models.Engine);
        }

        protected override void AddListeners()
        {
            
        }

        protected override void RemoveListeners()
        {
            
        }
    }
}