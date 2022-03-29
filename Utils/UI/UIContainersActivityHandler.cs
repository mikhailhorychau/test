using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace UIScripts.Utils.UI
{
    public class UIContainersActivityHandler : UIContainerBehaviour
    {
        [SerializeField] private List<UIContainerBehaviour> containers;

        private void Awake()
        {
            containers.ForEach(child => child.OnContainerContentChanged += ContainerActivityChangedListener);
            gameObject.SetActive(!IsEmpty());
        }

        private void OnDestroy()
        {
            containers.ForEach(child => child.OnContainerContentChanged -= ContainerActivityChangedListener);
        }

        private void ContainerActivityChangedListener()
        {
            gameObject.SetActive(!IsEmpty());
            RaiseContainerChangedEvent();
        }

        public override bool IsEmpty() => containers.All(child => child.IsEmpty());

    }
}