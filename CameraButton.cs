using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace UIScripts
{
    public class CameraButton : MonoBehaviour
    {
        [SerializeField] private StyledIconButton button;
        [SerializeField] private float blinkingTime;

        private bool _isBlinking;
        private float _lastBlinkingTime;
        
        public UnityEvent OnClick => button.onClick;

        private StyledIconButton Button => button ??= GetComponent<StyledIconButton>();

        public void StartBlinkAnimation()
        {
            if (_isBlinking) return;
            
            Button.SetDisabled(true);
        
            _isBlinking = true;
            _lastBlinkingTime = Time.time;
        }

        public void StopBlinkAnimation()
        {
            if (!_isBlinking) return;
            
            Button.SwitchStyle(Button.StateStyles.common);
            Button.SetDisabled(false);
            
            _isBlinking = false;
        }
    
        private void Update()
        {
            if (_isBlinking && Time.time - _lastBlinkingTime >= blinkingTime)
            {
                _lastBlinkingTime = Time.time;
                
                Button.SwitchStyle(Button.CurrentStyle == Button.StateStyles.pressed 
                    ? Button.StateStyles.disabled 
                    : Button.StateStyles.pressed);
            }
        }
    }
}