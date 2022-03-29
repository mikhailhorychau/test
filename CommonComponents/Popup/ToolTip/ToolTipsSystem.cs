using System;
using System.Collections.Generic;
using UnityEngine;

namespace UIScripts.CommonComponents.Popup.ToolTip
{
    public class ToolTipsSystem : MonoBehaviour
    {
        [SerializeField] private List<ToolTipsContainer> _containers;
        
        public void SetDescription(ToolTipsScreen screen, Enum key, string description)
        {
            _containers
                .FindAll(container => container.Screen == screen)
                .ForEach(container =>
                {
                    container.SetDescription(key, description);
                });
        }
    }
}