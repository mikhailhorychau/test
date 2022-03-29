using System;
using TMPro;
using UIScripts.Utils.UI;
using UnityEngine;

namespace UIScripts.Observable
{
    public static class ObservableExtension
    {
        public static void SubscribeToTextMeshPro<T>(this ObservableProperty<T> property, TextMeshProUGUI text)
        {
            property.OnValueChange += (value) => TextMeshProAction(text, value);
        }

        private static void TextMeshProAction<T>(TextMeshProUGUI textMesh, T value)
        {
            if (textMesh != null)
                textMesh.text = value.ToString();
            
        }

        public static void SubscribeToString<T>(this ObservableProperty<T> property, Action<string> stringSetter)
        {
            property.OnValueChange += value => stringSetter.Invoke(value.ToString());
        }

        public static void SubscribeToGameObjectActivity(this ObservableBool property, GameObject gameObject)
        {
            property.OnValueChange += gameObject.SetActive;
        }

        public static void AddSubscriber<T>(this ObservableProperty<T> p, Action<T> action)
        {
            p.OnValueChange += action.Invoke;
        }

        public static void AddViewSubscriber<T>(this ObservableProperty<T> p, IDestroyableView view, Action<T> action)
        {
            p.OnValueChange += action.Invoke;
            view.OnViewDestroy += () =>
            {
                p.OnValueChange -= action.Invoke;
            };
        }

        public static void RemoveViewSubscriber<T>(this ObservableProperty<T> p, IDestroyableView view,
            Action<T> action)
        {
            p.OnValueChange -= action.Invoke;
        }
    }
}