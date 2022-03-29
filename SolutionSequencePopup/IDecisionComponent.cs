using System;

namespace UIScripts.SolutionSequencePopup
{
    public interface IDecisionComponent
    {
        public event Action OnMakeDecision;
    }
}