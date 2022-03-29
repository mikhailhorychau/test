using UnityEngine;

namespace UIScripts.Screens.V2.SessionResult
{
    public class SessionResultUI : MonoBehaviour
    {
        [SerializeField] private DriverSessionResultUI first;
        [SerializeField] private DriverSessionResultUI second;
        [SerializeField] private StyledButton btn;

        public DriverSessionResultUI First => first;
        public DriverSessionResultUI Second => second;
        public StyledButton Btn => btn;
    }
}