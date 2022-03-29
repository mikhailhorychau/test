using UnityEngine;
using UnityEngine.UI;

namespace UIScripts.CommonComponents.SkillBarWithProgress
{
    public class ProgressSkillBar : MonoBehaviour
    {
        [SerializeField] private SkillBar skillBar;
        [SerializeField] private Slider slider;
        [SerializeField] private StyledImage sliderFill;

        public void SetLevel(int lvl) => skillBar.Value = lvl;
        public void SetProgress(int progress) => slider.value = progress;
        public void SetProgressColor(UIColorStyle color) => sliderFill.SwapColor(color);
    }
    
}