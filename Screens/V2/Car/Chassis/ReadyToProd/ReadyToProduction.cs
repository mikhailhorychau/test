using System;
using UnityEngine;

namespace UIScripts.Screens.V2.Car.Chassis.ReadyToProd
{
    public class ReadyToProduction : MonoBehaviour
    {
        [RequireInterface(typeof(IReadyToProductionView))] 
        [SerializeField] private GameObject readyToProdView;

        private ReadyToProductionPresenter _readyToProdPresenter;
        private IReadyToProductionView _view;

        public event Action OnStartDevelop
        {
            add => ReadyToProdView.OnStartDevelop += value;
            remove => ReadyToProdView.OnStartDevelop -= value;
        }

        public InfoPopup Popup => ReadyToProdView.Popup;
        public StaticPopup ConfirmationPopup => ReadyToProdView.ConfirmationPopup;
        
        private IReadyToProductionView ReadyToProdView => _view ??= readyToProdView.GetComponent<IReadyToProductionView>();

        public void Initialize(ReadyToProductionModel model)
        {
            _readyToProdPresenter = new ReadyToProductionPresenter(model, ReadyToProdView);
        }

        private void OnDestroy()
        {
            _readyToProdPresenter = null;
        }
    }
}