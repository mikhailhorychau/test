using UIScripts.Screens.SessionPanel;
using UIScripts.Screens.V2.BonusRequirement;
using UnityEngine;

namespace UIScripts.RaceAction
{
    public class RaceCommandButtonPresenter : MonoBehaviour
    {
        [SerializeField] private RaceCommandPresenter commandPresenter;
        [SerializeField] private RequirementsButtonPresenter btnPresenter;
        [SerializeField] private GameObject requirementsObj;

        private RaceCommandsBehaviour _behaviour = new RaceCommandsBehaviour();
        private RaceCommandButtonModel _model = new RaceCommandButtonModel();
        
        private void Awake()
        {
            btnPresenter.Button.OnButtonClick += BtnClickListener;
        }

        private void OnDestroy()
        {
            btnPresenter.Button.OnButtonClick -= BtnClickListener;
        }

        public void Initialize(RaceCommandButtonModel model)
        {
            if (_model != null)
            {
                _model.CommandModel.State.OnValueChange -= StateChangeListener;
            }
            
            btnPresenter.Initialize(model.ButtonData);
            commandPresenter.Initialize(model.CommandModel);

            model.CommandModel.State.OnValueChange += StateChangeListener;
            _model = model;
            _behaviour = SessionPanel.Scene.GetInteractor<RaceCommandsInteractor>().CommandsBehaviour;

        }

        private void StateChangeListener(RaceCommandState state)
        {
            requirementsObj.SetActive(state == RaceCommandState.Common);
        }

        private void BtnClickListener()
        {
            var id = _model.CommandModel.ID;
            _behaviour.UseCommand(id);
        }
    }
}