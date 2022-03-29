using TMPro;
using UnityEngine;

namespace UIScripts.TitleValue
{
    public class TitleSkillBarValue : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI title;
        [SerializeField] private SkillBar skillBar;

        public string Title
        {
            get => title.text;
            set => title.text = value;
        }

        public int Value
        {
            get => skillBar.Value;
            set => skillBar.Value = value;
        }
    }
}