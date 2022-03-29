using System.Collections.Generic;
using UIScripts.Utils.UI;
using UnityEngine.Events;

namespace UIScripts
{
    public interface IDropdown<T> : IInteractable where T : IProps
    {
        public int CurrentValue { get; set; }
        public void Initialize(List<T> items);
        
        public bool IsDisabled { get; set; }
        
        public UnityEvent<int> OnValueChange { get; }
    }
}