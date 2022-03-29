using System;
using Unity.Mathematics;

namespace UIScripts.CommonComponents.PlusMinusInput
{
    public class PlusMinusInputValueContainer 
    {
        public event Action<PlusMinusInputEventData> OnValueChanged;

        private PlusMinusInputConfig _config;

        public int Value => _config.value;
        
        public PlusMinusInputValueContainer(PlusMinusInputConfig config)
        {
            _config = config;
        }
        
        public void SetConfig(PlusMinusInputConfig config)
        {
            _config = config;
        }

        public void SetValue(object sender, int newValue)
        {
            if (_config.value == newValue) return;

            var prevValue = _config.value;
            _config.value = math.clamp(newValue, _config.min, _config.max);
            
            if (prevValue == _config.value) return;
            
            var eventData = new PlusMinusInputEventData(sender, prevValue, _config.value);
            OnValueChanged?.Invoke(eventData);
        }

        public void Increase(object sender = null) => SetValue(sender ?? this, _config.value + 1);
        public void Decrease(object sender = null) => SetValue(sender ?? this, _config.value - 1);
    }

    public class PlusMinusInputEventData
    {
        public object Sender { get; }
        public int PrevValue { get; }
        public int CurrentValue { get; }

        public PlusMinusInputEventData(object sender, int prevValue, int currentValue)
        {
            Sender = sender;
            PrevValue = prevValue;
            CurrentValue = currentValue;
        }
    }
}