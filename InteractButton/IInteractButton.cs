using System;
using UnityEngine;

namespace UIScripts.InteractButton
{
    public interface IInteractButton
    {
        public void SetDisabled(bool disabled);
        public void SetBlinking(bool blinking);
        public void SetSprite(Sprite sprite);

        public void AddListener(Action action);
    }
}