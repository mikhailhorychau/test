using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UIScripts.Utils.Animation
{
    public class UITweenTest : MonoBehaviour
    {
        [SerializeField] private float from;
        [SerializeField] private float to;
        [SerializeField] private float duration;
        
        [SerializeField] private TextMeshProUGUI text;
        [SerializeField] private Button start;
        [SerializeField] private Button pause;
        [SerializeField] private Button resume;
        [SerializeField] private Button stop;
        [SerializeField] private Button finish;

        private UITweenSeq tweenSeq;
        private int id;
        
        private void Awake()
        {
            // void Action(float v) => text.text = v.ToString();
            // var id = 0;
            //
            // start.onClick.AddListener(() => id = UITweenRunner.RunFloatTween(gameObject, from, to, duration, Action));
            // pause.onClick.AddListener(() => UITweenRunner.PauseTween(id));
            // resume.onClick.AddListener(() => UITweenRunner.ResumeTween(id));
            // stop.onClick.AddListener(() => UITweenRunner.CancelTween(id));
            // finish.onClick.AddListener(() => UITweenRunner.FinishTween(id));

            tweenSeq = new UITweenSeq(gameObject);
            var first = new UITweenFloat(gameObject, 2f);
            first.OnComplete += () => print("first");
            var second = new UITweenFloat(gameObject, from, to, 10);
            second.OnValueChanged += v => text.text = v.ToString();
            tweenSeq.Append(first);
            tweenSeq.Append(second);
            tweenSeq.OnComplete += () => print("Completed");
            
            start.onClick.AddListener(Test);
            pause.onClick.AddListener(() => UITweenRunner.PauseTween(id));
            resume.onClick.AddListener(() => UITweenRunner.ResumeTween(id));
            stop.onClick.AddListener(() => UITweenRunner.CancelTween(id));
            finish.onClick.AddListener(() => UITweenRunner.FinishTween(id));
        }

        private void Test()
        {
            id = UITweenRunner.RunTween(tweenSeq);
        }
    }
}