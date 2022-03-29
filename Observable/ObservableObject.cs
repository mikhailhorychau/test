using System.Collections;
using TMPro;
using UnityEngine;

namespace UIScripts.Observable
{
    public class ObservableObject : MonoBehaviour
    {
        private ObservableString text = new ObservableString();
        [SerializeField] private TextMeshProUGUI title;
        
        public void Start()
        {
            StartCoroutine(Test());
        }

        public IEnumerator Test()
        {
            text.Value = "first text";
            yield return new WaitForSeconds(1f);
            text.Value = "second text";
            yield return new WaitForSeconds(1f);
        }

        private void OnEnable()
        {
            text.OnValueChange += TextChangeListener;
        }

        private void OnDisable()
        {
            text.OnValueChange -= TextChangeListener;
        }

        private void TextChangeListener(string value)
        {
            title.text = value;
        }
    }
}