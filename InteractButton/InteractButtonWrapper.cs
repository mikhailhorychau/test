using System;
using UnityEngine;

namespace UIScripts.InteractButton
{
    public class InteractButtonWrapper : MonoBehaviour, IInteractButton
    {
        [SerializeField] private StyledIconButton btn;
        [SerializeField] private float blinkDelay = 0.5f;
        
        private bool _disabled;
        private bool _blinking;

        private float _lastBlinkTime;

        public void SetDisabled(bool disabled) => btn.SetDisabled(disabled);

        public void SetBlinking(bool blinking)
        {
            if (_blinking == blinking) return;

            btn.Interactable = !blinking;

            if (!blinking)
                _lastBlinkTime = Time.time;
            
            if (blinking)
                btn.SwitchStyle(btn.StateStyles.common);

            _blinking = blinking;
        }

        public void AddListener(Action action)
        {
            btn.onClick.AddListener(action.Invoke);
        }

        public void SetSprite(Sprite sprite) => btn.SwitchIconSprite(sprite);

        private void Update()
        {
            if (_blinking && Time.time - _lastBlinkTime >= blinkDelay)
            {
                _lastBlinkTime = Time.time;
                btn.SwitchStyle(btn.CurrentStyle == btn.StateStyles.pressed 
                    ? btn.StateStyles.common 
                    : btn.StateStyles.pressed);
            }
        }
    }
    
}