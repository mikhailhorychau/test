using TMPro;
using UnityEngine;
using UnityEngine.Events;

namespace UIScripts
{
    public class StaticPopup : MonoBehaviour
    {
        [SerializeField] private StyledButton accept;
        [SerializeField] private StyledButton cancel;
        [SerializeField] private TextMeshProUGUI title;
        [SerializeField] private TextMeshProUGUI subtitle;
        [SerializeField] private TextMeshProUGUI acceptTitle;
        [SerializeField] private TextMeshProUGUI cancelTitle;

        public string Title
        {
            get => title.text;
            set => title.text = value;
        }

        public string Subtitle
        {
            get => subtitle.text;
            set => subtitle.text = value;
        }

        public string AcceptTitle
        {
            get => acceptTitle.text;
            set => acceptTitle.text = value;
        }

        public string CancelTitle
        {
            get => cancelTitle.text;
            set => cancelTitle.text = value;
        }

        public UnityEvent onAccept;
        private void OnEnable()
        {
            cancel.onClick.AddListener(RemoveListeners);
            accept.onClick.AddListener(onAccept.Invoke);
            accept.onClick.AddListener(RemoveListeners);
        }

        private void OnDisable()
        {
            accept.onClick.RemoveAllListeners();
            cancel.onClick.RemoveAllListeners();
            onAccept.RemoveAllListeners();
        }

        private void RemoveListeners()
        {
            accept.onClick.RemoveAllListeners();
            cancel.onClick.RemoveAllListeners();
            onAccept.RemoveAllListeners();
            gameObject.SetActive(false);
        }        
    }
}