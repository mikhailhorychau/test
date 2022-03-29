using System.Collections.Generic;
using Helpers;
using UIScripts.CommonComponents;
using UIScripts.Utils;
using UIScripts.Utils.Animation;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UI;

namespace UIScripts.Screens.V2.SessionResult
{
    public class SessionResultParamData
    {
        public string ParamName { get; set; }
        public SkillProgressModel ProgressModel { get; set; }
        public int ChangesPoints { get; set; }
    }
    
    public class DriverSessionResultParamsContainer : ObjectContainer<SkillProgressParam>
    {
        private const UIColorStyle FIRST_COLOR = UIColorStyle.Background1;
        private const UIColorStyle SECOND_COLOR = UIColorStyle.Background2;
        private const float ANIMATION_TIME = 0.5f;
        
        [SerializeField] private ZebraList zebra;
        [SerializeField] private ScrollRect scrollRect;

        private List<int> _animations = new List<int>();

        private void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                _animations.ForEach(UITweenRunner.FinishTween);
                _animations.Clear();
            }
        }

        public UITweenSeq GetAppearanceAnimation(List<SessionResultParamData> skills)
        {
            var seq = new UITweenSeq(gameObject);

            for (var i = 0; i < skills.Count; i++)
            {
                var skill = skills[i];
                var progress = skill.ProgressModel;
                var paramName = skill.ParamName;
                var changes = skill.ChangesPoints;

                var index = i;
                
                seq.Append(() =>
                {
                    var item = Initialize(index);
                    item.ProgressModel = progress;
                    item.Title = paramName;
                    item.transform.SetAsLastSibling();
                    var color = index % 2 == 0 ? FIRST_COLOR : SECOND_COLOR;
                    item.StyledImage.SwapColor(color);

                });
                seq.Append(ScrollBtm);
                // var animationTime = POINTS_PER_SECOND.PointsToTime(math.abs(changes));
                var tween = progress.GetProgressAnimation(gameObject, changes, ANIMATION_TIME);
                seq.Append(tween);
            }

            return seq;
        }   
        
        private void ScrollBtm() => scrollRect.normalizedPosition = Vector2.zero;
    }
}