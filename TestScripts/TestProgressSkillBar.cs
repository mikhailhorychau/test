using System.Collections;
using UIScripts.CommonComponents;
using UIScripts.Utils.Animation;
using UnityEngine;

namespace UIScripts.TestScripts
{
    public class TestProgressSkillBar : MonoBehaviour
    {
        [SerializeField] private SkillProgressPresenter target;

        private void Start()
        {
            var model = new SkillProgressModel(4, 5, true, ProgressType.Decreased);
            target.Initialize(model);

            model.GetProgressAnimation(gameObject, 35, 5);
        }
        
    }
}