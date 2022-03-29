using System;
using UIScripts.Observable;
using UIScripts.Utils.UI;
using UnityEngine;

namespace UIScripts.Screens.V2.DurationButton
{
    public class InProgressHandler : MonoBehaviour, IDestroyableView
    {
        public event Action<bool> InProgressChanged; 
        public event Action OnViewDestroy;
        
        [SerializeField] private GameObject commonObj;
        [SerializeField] private GameObject inProgressObj;

        public void Initialize(ObservableBool inProgress)
        {
            SetInProgress(inProgress);
            
            inProgress.AddViewSubscriber(this, SetInProgress);
        }

        protected virtual void SetInProgress(bool inProgress)
        {
            if (commonObj != null)
                commonObj.SetActive(!inProgress);
            if (inProgressObj != null)
                inProgressObj.SetActive(inProgress);
            
            InProgressChanged?.Invoke(inProgress);
        }

        private void OnDestroy()
        {
            OnViewDestroy?.Invoke();
        }
    }
}