using UIScripts.Abstract;
using UIScripts.Utils.UI;
using UnityEngine;
using UnityEngine.Events;

namespace UIScripts
{
    public interface IButton : IInteractable, IClickable
    {
        public string Text { get; set; }
        public GameObject gameObject { get; }
    }
}