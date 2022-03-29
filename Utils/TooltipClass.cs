using System;
using System.Collections.Generic;
using System.Linq;
using UIScripts.CommonComponents.Popup;
using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;

namespace UIScripts.Utils
{
    public class TooltipClass : MonoBehaviour
    {
        [SerializeField] private BetterPopup popup;
        
        [SerializeField] private int checksPerSecond = 10;

        private float _lastCheckTime;
        private float _delayBetweenCheck;
        private bool _isPopupActive;
        private Vector3 _prevMousePosition;

        private Dictionary<string, string> _toolTips = new Dictionary<string, string>();

        public void AddTooltip(string path, string text) => _toolTips.Add(path, text);
        
        private void Start()
        {
            _lastCheckTime = Time.time;
            _delayBetweenCheck = 1 / (float) checksPerSecond;
        }
        
        private void Update()
        {
            if (Time.time - _lastCheckTime <= _delayBetweenCheck) 
                return;
            
            _lastCheckTime = Time.time;

            if (Input.mousePosition == _prevMousePosition) 
                return;
            
            _prevMousePosition = Input.mousePosition;
            
            // var raycastResults = RaycastMouse();
            // raycastResults
            //     .Select(result => result.gameObject)
            //     .ToList()
            //     // .ForEach(go => print($"{go.name}: {go.transform.GetTransPath()}"));
        }
        
        public List<RaycastResult> RaycastMouse(){
         
            PointerEventData pointerData = new PointerEventData (EventSystem.current)
            {
                pointerId = -1,
            };
         
            pointerData.position = Input.mousePosition;
 
            List<RaycastResult> results = new List<RaycastResult>();
            EventSystem.current.RaycastAll(pointerData, results);

            return results;
        }
    }
}