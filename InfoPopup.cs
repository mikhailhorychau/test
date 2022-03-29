using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace UIScripts
{
    public class InfoPopup : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI title;
        [SerializeField] private StyledButton acceptButton;
        [SerializeField] private Toggle toggle;
        [SerializeField] private TextMeshProUGUI dontShowAgainTitle;

        public string AcceptTitle
        {
            get => acceptButton.Text;
            set => acceptButton.Text = value;
        }

        public string Title
        {
            get => title.text;
            set => title.text = value;
        }

        public string DontShowAgainTitle
        {
            get => dontShowAgainTitle.text;
            set => dontShowAgainTitle.text = value;
        }

        public UnityEvent<bool> onToggleValueChange;
        private void Awake()
        {
            acceptButton.onClick.AddListener(() => gameObject.SetActive(false));
            toggle.onValueChanged.AddListener(onToggleValueChange.Invoke);
        }
    }
}