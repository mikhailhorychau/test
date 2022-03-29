using UnityEngine;
using UnityEngine.UI;

namespace UIScripts
{
    public class ButtonTitle : MonoBehaviour
    {
        private void Start()
        {
            LayoutRebuilder.ForceRebuildLayoutImmediate(GetComponent<RectTransform>());
            gameObject.SetActive(false);
        }
    }
}