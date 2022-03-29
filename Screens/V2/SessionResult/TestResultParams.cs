using System.Collections.Generic;
using UIScripts.CommonComponents;
using UIScripts.Utils;
using UIScripts.Utils.Animation;
using UnityEngine;

namespace UIScripts.Screens.V2.SessionResult
{
    public class TestResultParams : MonoBehaviour
    {
        [SerializeField] private DriverSessionResultParamsContainer firstContainer;
        [SerializeField] private DriverSessionResultParamsContainer secondContainer;
        [SerializeField] private GameObject btn;

        private List<ITween> list = new List<ITween>();
        
        private void Start()
        {
            list = new List<ITween>();
            var first = firstContainer.GetAppearanceAnimation(Generated());
            var second = secondContainer.GetAppearanceAnimation(Generated());
            
            list.Add(first);
            list.Add(second);
            
            list.ForEach(tween => UITweenRunner.RunTween(tween));

            var awaiter = new UITweenWaiter(list);
            awaiter.OnComplete += () => print("completed");
            awaiter.OnComplete += () => btn.SetActive(true);

            var root = firstContainer.gameObject.GetRootCanvas();
            var blocker = Blocker.CreateWithHighOrder(root);

            blocker.OnClick += () =>
            {
                first.Completed = true;
                second.Completed = true;
            };

            awaiter.OnComplete += () => Destroy(blocker.gameObject);
        }

        private void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                list.ForEach(tween => tween.Completed = true);
            }
        }

        private List<SessionResultParamData> Generated()
        {
            var list = new List<SessionResultParamData>();
            for (var i = 0; i < 10; i++)
            {
                list.Add(new SessionResultParamData()
                {
                    ParamName = "name",
                    ProgressModel = new SkillProgressModel(Random.Range(1, 8), Random.Range(0, 8), true, ProgressType.Increased),
                    ChangesPoints = Random.Range(-10, 10)
                });
            }

            return list;
        }
    }
}