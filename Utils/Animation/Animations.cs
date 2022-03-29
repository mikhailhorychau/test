using System.Collections.Generic;
using UIScripts.CommonComponents;
using UnityEngine;

namespace UIScripts.Utils.Animation
{
    public static class Animations
    {
        private static List<int> _animations = new List<int>();
        
        public static LTDescr PlayAddProgress(this SkillProgressModel model, int addedProgress, float time)
        {
            var maxLvl = App.runtime.MaxLevel;
            var totalProgress = model.Level * maxLvl + model.Progress;
            var newProgress = totalProgress + addedProgress;

            var anim = LeanTween
                .value(totalProgress, newProgress, time)
                .setOnStart(() => model.ProgressType.Value = addedProgress > 0 ? ProgressType.Increased : ProgressType.Decreased)
                .setOnUpdate((value) =>
                {
                    var rounded = Mathf.RoundToInt(value);
                    var lvl = rounded / maxLvl;
                    var progress = rounded % maxLvl;

                    model.Level.Value = lvl;
                    model.Progress.Value = progress;
                });
            
            _animations.Add(anim.uniqueId);
            return anim;
        }

        public static ITween GetProgressAnimation(this SkillProgressModel model, GameObject gameObject, int addedProgress, float time)
        {
            var maxLvl = App.runtime.MaxLevel;
            var totalProgress = model.Level * maxLvl + model.Progress;
            var newProgress = totalProgress + addedProgress;

            model.ProgressType.Value = addedProgress > 0 ? ProgressType.Increased : ProgressType.Decreased;
            var anim = new UITweenFloat(gameObject, totalProgress, newProgress, time);
            anim.OnValueChanged += (value) =>
            {
                var rounded = Mathf.RoundToInt(value);
                var lvl = rounded / maxLvl;
                var progress = rounded % maxLvl;

                model.Level.Value = lvl;
                model.Progress.Value = progress;
            };

            // _animations.Add(anim.uniqueId);
            // UITweenRunner.RunTween(anim);
            return anim;
        }

        private static void ClearAnimationsIfExists(int id)
        {
            if (_animations.Contains(id))
            {
                LeanTween.cancel(id);
            }
        }
    }
}