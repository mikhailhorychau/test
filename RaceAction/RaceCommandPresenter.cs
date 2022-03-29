using UIScripts.Observable;
using UnityEngine;

namespace UIScripts.RaceAction
{
    public class RaceCommandPresenter : MonoBehaviour
    {
        [RequireInterface(typeof(IRaceCommandView))]
        [SerializeField] private GameObject viewObj;

        private IRaceCommandView view;

        public IRaceCommandView View => view ??= viewObj.GetComponent<IRaceCommandView>(); 
        
        public void Initialize(RaceCommandModel model)
        {
            View.SetState(model.State);
            View.SetDuration(model.Duration);
            
            model.State.AddViewSubscriber(View, View.SetState);
            model.Duration.AddViewSubscriber(View, View.SetDuration);
        }
    }
}