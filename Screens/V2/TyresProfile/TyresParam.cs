using TMPro;
using UIScripts.CommonComponents;
using UnityEngine;

namespace UIScripts.Screens.V2.TyresProfile
{
    public class TyresParam : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI title;
        [SerializeField] private SkillProgressParam quality;
        [SerializeField] private SkillProgressParam wearResistance;

        public void Initialize(TyresParamData data)
        {
            title.text = data.Name;
            quality.Title = data.QualityTitle;
            wearResistance.Title = data.WearResistanceTitle;
            quality.ProgressModel = data.Quality;
            wearResistance.ProgressModel = data.WearResistance;
        }
    }

    public struct TyresParamData
    {
        public string Name { get; set; }
        public string QualityTitle { get; set; }
        public string WearResistanceTitle { get; set; }
        public SkillProgressModel Quality { get; set; }
        public SkillProgressModel WearResistance { get; set; }
    }
}