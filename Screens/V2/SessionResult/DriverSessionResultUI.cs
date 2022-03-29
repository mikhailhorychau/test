using UnityEngine;

namespace UIScripts.Screens.V2.SessionResult
{
    public class DriverSessionResultUI : MonoBehaviour
    {
        [SerializeField] private DriverSessionResultInfoUI info;
        [SerializeField] private DriverSessionResultParamsContainer paramsContainer;

        public DriverSessionResultInfoUI Info => info;
        public DriverSessionResultParamsContainer Params => paramsContainer;
    }
}