using System;
using TMPro;
using UIScripts.Abstract;
using UnityEngine;

namespace UIScripts.CommonComponents.PlusMinusInput
{
    public class PlusMinusInput : MonoBehaviour
    {
        public Action<int> OnValueChanged;
        
        [SerializeField] private TextMeshProUGUI valueView;
        
        [RequireInterface(typeof(IClickable))] 
        [SerializeField] private GameObject plus;
        
        [RequireInterface(typeof(IClickable))] 
        [SerializeField] private GameObject minus;

        [SerializeField] private PlusMinusInputConfig config;

        private PlusMinusInputValueContainer _container;

        private IClickable _plus;
        private IClickable _minus;

        public PlusMinusInputConfig Config => config;
        public int Value => _container.Value;
        
        private void Awake()
        {
            _plus = plus.GetComponent<IClickable>();
            _minus = minus.GetComponent<IClickable>();
            
            Initialize(config);
            
            _plus.OnClick.AddListener(PlusBtnClickListener);
            _minus.OnClick.AddListener(MinusBtnClickListener);
            _container.OnValueChanged += ValueChangeListener;
        }

        public void Initialize(PlusMinusInputConfig initConfig)
        {
            config = initConfig;
            if (_container == null)
            {
                _container = new PlusMinusInputValueContainer(config);
            }
            else
            {
                _container.SetConfig(config);
            }

            valueView.text = config.value + config.postfix;
        }

        public void SetValue(int value, object sender = null) => _container?.SetValue(sender, value);

        private void OnDestroy()
        {
            _plus.OnClick.RemoveListener(PlusBtnClickListener);
            _minus.OnClick.RemoveListener(MinusBtnClickListener);
            _container.OnValueChanged -= ValueChangeListener;
        }

        private void PlusBtnClickListener() => _container.Increase(this);
        private void MinusBtnClickListener() => _container.Decrease(this);

        private void ValueChangeListener(PlusMinusInputEventData eventData)
        {
            valueView.text = eventData.CurrentValue + config.postfix;
            if (eventData.Sender == this)
            {
                OnValueChanged?.Invoke(eventData.CurrentValue);
            }
        }
    }
}