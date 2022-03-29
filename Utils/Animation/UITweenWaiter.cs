using System;
using System.Collections.Generic;
using System.Linq;

namespace UIScripts.Utils.Animation
{
    public class UITweenWaiter
    {
        public event Action OnComplete;

        private int _totalCount;
        private int _completeCount;

        public UITweenWaiter(List<ITween> tweens)
        {
            _totalCount = tweens.Count;
            _completeCount = tweens.FindAll(tween => tween.Completed).Count();
            
            var notCompleted = !CheckCompletion();

            if (notCompleted)
            {
                tweens.Where(tween => !tween.Completed).ToList().ForEach(tween => tween.OnComplete += AddCompletedTween);
            }
        }

        private bool CheckCompletion()
        {
            var isComplete = _completeCount == _totalCount;
            if (isComplete)
                OnComplete?.Invoke();

            return isComplete;
        }

        private void AddCompletedTween()
        {
            _completeCount++;
            CheckCompletion();
        }
    }
}