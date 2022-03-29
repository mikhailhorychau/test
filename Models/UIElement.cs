using System;
using UnityEngine;

namespace UIScripts
{
    [Serializable]
    public abstract class UIElement : MonoBehaviour
    {
        public abstract void UpdateUI();
    }
}