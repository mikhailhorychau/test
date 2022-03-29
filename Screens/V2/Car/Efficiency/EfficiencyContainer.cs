using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

namespace UIScripts.Screens.V2.Car.Efficiency
{
    public class EfficiencyContainer : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI efficiencyTitle;
        [SerializeField] private EfficiencyItem drag;
        [SerializeField] private EfficiencyItem fastCorners;
        [SerializeField] private EfficiencyItem slowCorners;
        [SerializeField] private EfficiencyItem downforce;
        [SerializeField] private EfficiencyItem efficiencyBehind;
        [SerializeField] private EfficiencyItem tireWear;

        private Dictionary<EfficiencyItemType, EfficiencyItem> _items;
        private EfficiencyPresenter _presenter = new EfficiencyPresenter();
        
        public string EfficiencyTitle
        {
            set => efficiencyTitle.text = value;
        }

        public void Initialize(Dictionary<EfficiencyItemType, EfficiencyModel> data) =>
            data.ToList().ForEach(pair => _presenter.Initialize(GetEfficiencyItemByType(pair.Key), pair.Value));

        public EfficiencyItem GetEfficiencyItemByType(EfficiencyItemType type)
        {
            if (_items == null || _items.Count == 0) 
                InitializeItems();

            return _items[type];
        }

        private void InitializeItems()
        {
            _items = new Dictionary<EfficiencyItemType, EfficiencyItem>
            {
                {EfficiencyItemType.Drag, drag},
                {EfficiencyItemType.FastCorners, fastCorners},
                {EfficiencyItemType.SlowCorners, slowCorners},
                {EfficiencyItemType.Downforce, downforce},
                {EfficiencyItemType.EfficiencyBehind, efficiencyBehind},
                {EfficiencyItemType.TireWear, tireWear}
            };
        }
    }
}