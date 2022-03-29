using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace UIScripts.Initializer
{
    public class UIJobSequence : UIJob
    {
        public event Action<float> OnProgressChanged; 
        public IEnumerable<UIJob> _jobs;

        private float _progress;
        private int _count;
        public float Progress
        {
            get => _progress;
            set
            {
                if (_progress == value) return;

                _progress = value;
                OnProgressChanged?.Invoke(_progress);
            }
        }
        
        public UIJobSequence(List<UIJob> jobs) : base(() => {})
        {
            _jobs = jobs;
            _count = jobs.Count;
            
            for (var i = 0; i < jobs.Count; i++)
            {
                var index = i + 1;
                jobs[i].OnComplete += () => JobCompleteListener(index);
            }
        }

        public override IEnumerator Execute()
        {
            foreach (var uiJob in _jobs)
            {
                yield return uiJob.Execute();
            }
        }

        private void JobCompleteListener(int id)
        {
            Progress = id / (float) _count;
        }
    }
}