using UnityEngine;

namespace UIScripts.Initializer
{
    public class WaitUIJobComplete : CustomYieldInstruction
    {
        private UIJob _job;
        
        public WaitUIJobComplete(UIJob job)
        {
            _job = job;
        }

        public override bool keepWaiting => !_job.IsComplete;
    }
}