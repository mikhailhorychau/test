using System.Collections;
using UIScripts.Utils;
using UnityEngine;

namespace UIScripts.CommonComponents.Popup
{
    public class BetterPopupTooltip : MonoBehaviour
    {
        [SerializeField] private BetterPopupComponent popupController;
        [SerializeField] private DynamicText textPrefab;
        public BetterPopupComponent PopupController => popupController;

        private string _message;

        private void OnEnable()
        {
            popupController.onPointerEnter.AddListener(PointerEnterListener);
        }

        private void OnDisable()
        {
            popupController.onPointerEnter.RemoveListener(PointerEnterListener);
        }

        private void PointerEnterListener()
        {
            StartCoroutine(UpdateMessage());
        }

        private IEnumerator UpdateMessage()
        {
            var dynamicText = Instantiate(textPrefab);
            popupController.Popup.Clear();
            yield return dynamicText.UpdateText(_message);
            yield return popupController.Popup.AddItemWithFitter(dynamicText.RectTransform);
        }

        public void Initialize(string message)
        {
            _message = message;
        }

        private IEnumerator AddItem(DynamicText dynamicText, string message)
        {
            yield return dynamicText.UpdateText(message);
            yield return popupController.Popup.AddItemWithFitter(dynamicText.RectTransform);
        }
    }
}