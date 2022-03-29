using TMPro;
using UnityEngine;

namespace UIScripts.CommonComponents
{
    public class SkillProgressParam : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI title;
        [SerializeField] private SkillProgressPresenter presenter;
        [SerializeField] private StyledImage styledImage;

        public string Title
        {
            set => title.text = value;
        }

        public SkillProgressModel ProgressModel
        {
            set => presenter.Initialize(value);
        }

        public StyledImage StyledImage => styledImage;
    }
}