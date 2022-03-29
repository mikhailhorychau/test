using System;
using TMPro;
using UIScripts.Observable;
using UIScripts.Utils.UI;
using UnityEngine;

namespace UIScripts.Screens.V2.Car.Efficiency
{
    public interface IEfficiencyView : IDestroyableView
    {
        public string Name { set; }
        public string Value { set; }
    }
    
    public class EfficiencyItem : MonoBehaviour, IEfficiencyView
    {
        public event Action OnViewDestroy;
        
        [SerializeField] private TextMeshProUGUI nameView;
        [SerializeField] private TextMeshProUGUI valueView;

        public string Name
        {
            set => nameView.text = value;
        }

        public string Value
        {
            set => valueView.text = value;
        }
        

        public void Initialize(EfficiencyModel model)
        {
            Name = model.Name;
            Value = model.Value;
        }

        private void OnDestroy()
        {
            OnViewDestroy?.Invoke();
        }
    }

    public class EfficiencyModel 
    {
        public string Name { get; set; }
        public ObservableString Value { get; } = new ObservableString();

        public EfficiencyModel()
        {
            
        }

        public EfficiencyModel(string name, string value)
        {
            Name = name;
            Value.Value = value;
        }

    }
}