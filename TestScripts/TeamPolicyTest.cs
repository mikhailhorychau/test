using System.Collections.Generic;
using UIScripts.Screens.TeamControl.Policy;
using UnityEngine;

namespace UIScripts.TestScripts
{
    public class TeamPolicyTest : MonoBehaviour
    {
        [SerializeField] private TeamPolicy presenter;

        private void Awake()
        {
            var policies = new Dictionary<int, PolicyInfo>();
            for (int i = 0; i < 5; i++)
            {
                policies.Add(i, new PolicyInfo(i, $"test-description-{i}", $"test-name-{i}"));
            }

            var policy = new PolicyModel(0, policies, default);
            presenter.Initialize(policy);
        }
    }
}