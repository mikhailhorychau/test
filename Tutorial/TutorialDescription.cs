using TMPro;
using UnityEngine;

namespace UIScripts.Tutorial
{
    public class TutorialDescription : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI description;
        [SerializeField] private TextMeshProUGUI stage;

        public void SetText(string text) => description.text = text;
        public void SetStage(string text) => stage.text = text;
    }
}