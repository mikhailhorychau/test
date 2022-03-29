using System;
using UIScripts.Observable;
using UIScripts.Utils.UI;
using UnityEngine;

namespace UIScripts.Screens.V2.BonusRequirement
{
    public class RequirementModel : IUIData
    {
        public event Action<string> OnValueChange;

        private string _value;
        
        public int ID { get; set; }
        public Sprite Icon { get; set; }
        public string Value
        {
            get => _value;
            set
            {
                if (_value == value) return;

                _value = value;
                OnValueChange?.Invoke(_value);
            }
        }
        public ObservableBool CanBeUsed { get; } = new ObservableBool();
    }
}