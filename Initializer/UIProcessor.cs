using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UIScripts.Initializer
{
    public class UIProcessor : MonoSingleton<UIProcessor>
    {
        private Queue<UIJob> _jobs = new Queue<UIJob>();
        
        private bool _inProcess;

        public void AddJob(UIJob job)
        {
            _jobs.Enqueue(job);
            if (!_inProcess)
                StartCoroutine(Process());
        }

        public void AddSequence(UIJobSequence seq)
        {
            foreach (var seqJob in seq._jobs)
            {
                AddJob(seqJob);
            }
        }

        private IEnumerator Process()
        {
            _inProcess = true;

            yield return null;
            while (_jobs.Count > 0)
            {
                yield return _jobs.Dequeue().Execute();
            }

            _inProcess = false;
        }
    }
}