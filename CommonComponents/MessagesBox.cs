using System;
using System.Collections.Generic;
using TMPro;
using UIScripts.Utils;
using UnityEngine;
using UnityEngine.UI;

namespace UIScripts.CommonComponents
{
    [Serializable]
    public class MessagesBox  : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI text;
        [SerializeField] private Transform container;
        [SerializeField] private ScrollRect scrollRect;

        public void Initialize(List<string> messages)
        {
            messages.ForEach(PrintMessage);
        }

        public void ClearBox() => container.Clear();

        public void PrintMessage(string message)
        {
            var txtMesh = Instantiate(text, container);
            txtMesh.text = message;
            Canvas.ForceUpdateCanvases();
            scrollRect.normalizedPosition = Vector2.zero;
            Canvas.ForceUpdateCanvases();
        }

        public void Clear() => container.Clear();

        public void Test() => PrintMessage(Utils.Utils.RandomString(App.runtime.Rnd.Next(100)));
    }
}