using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace UIScripts.Screens.V2.Car.CFD.Components
{
    public class CarComponentsContainer : MonoBehaviour
    {
        [SerializeField] private CarComponent frontWing;
        [SerializeField] private CarComponent rearWing;
        [SerializeField] private CarComponent sidePods;
        [SerializeField] private CarComponent coolingSystem;
        [SerializeField] private CarComponent underBody;
        [SerializeField] private StaticPopup popup;

        private CarComponentsPresenter _presenter;
        private Dictionary<CarComponentType, ICarComponentView> _views = 
            new Dictionary<CarComponentType, ICarComponentView>();

        public event Action<int> OnUpgrade
        {
            add => _presenter.OnUpgradeAccept += value.Invoke;
            remove => _presenter.OnUpgradeAccept -= value.Invoke;
        }

        public void Initialize(Dictionary<CarComponentType, CarComponentModel> data)
        {
            _presenter = new CarComponentsPresenter(popup);
            data.ToList().ForEach(pair => InitComponent(pair.Key, pair.Value));
        }

        private void InitComponent(CarComponentType type, CarComponentModel model) =>
            _presenter.Initialize(GetComponentView(type), model);

        private ICarComponentView GetComponentView(CarComponentType type)
        {
            if (_views == null || _views.Count == 0)
            {
                InitializeDictionary();
            }

            return _views[type];
        }

        private void InitializeDictionary()
        {
            _views.Add(CarComponentType.FrontWing, frontWing);
            _views.Add(CarComponentType.RearWing, rearWing);
            _views.Add(CarComponentType.SidePods, sidePods);
            _views.Add(CarComponentType.CoolingSystem, coolingSystem);
            _views.Add(CarComponentType.UnderBody, underBody);
        }
    }
}