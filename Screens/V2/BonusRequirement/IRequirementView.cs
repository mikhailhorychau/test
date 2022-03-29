using UIScripts.Utils.UI;
using UnityEngine;

namespace UIScripts.Screens.V2.BonusRequirement
{
    public interface IRequirementView : IDestroyableView
    {
        public Sprite Icon { set; }
        public string Value { set; }

        public bool CanBeUsed { set; }

        public void SetValue(string value);
        public void SetCanBeUsed(bool canBeUsed);
    }
}