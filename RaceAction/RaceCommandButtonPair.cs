using TMPro;
using UnityEngine;

namespace UIScripts.RaceAction
{
    public class RaceCommandButtonPair : MonoBehaviour
    {
        [SerializeField] private RaceCommandButtonPresenter first;
        [SerializeField] private RaceCommandButtonPresenter second;
        [SerializeField] private TextMeshProUGUI title;
        
        public void Initialize(RaceCommandPairModel model)
        {
            title.text = model.Title;
            first.Initialize(model.First);
            second.Initialize(model.Second);
        }
    }
}