using UnityEngine;

namespace UIScripts.RaceAction
{
    public class RaceCommandPair : MonoBehaviour
    {
        [SerializeField] private RaceCommandPresenter first;
        [SerializeField] private RaceCommandPresenter second;
        
        public void Initialize(RaceCommandPairModel model)
        {
            first.Initialize(model.First.CommandModel);
            second.Initialize(model.Second.CommandModel);
        }
    }
}