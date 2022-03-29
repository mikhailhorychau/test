using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace UIScripts.CommonComponents.Popup.ToolTip
{
    public class ToolTipsContainer : MonoBehaviour
    {
        [SerializeField] private ToolTipsScreen screen;
        [SerializeField] private List<ToolTipComponent> _components = new List<ToolTipComponent>();

        public ToolTipsScreen Screen => screen;

        public void SetDescription(Enum key, string description)
        {
            var enumKey =
                ToolTipsNames.GetScreenTooltipNames(screen)
                    .Keys
                    .FirstOrDefault(screenKey => screenKey.Equals(key));

            if (enumKey == null) return;
            
            var foundedComponent = _components.Find(component => component.Key == enumKey.ToString());

            if (foundedComponent != null)
            {
                foundedComponent.Description = description;
            }

        }
    }
}