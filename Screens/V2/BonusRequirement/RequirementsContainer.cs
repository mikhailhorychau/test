using System.Collections.Generic;
using UIScripts.Utils;
using UnityEngine;

namespace UIScripts.Screens.V2.BonusRequirement
{
    [AddComponentMenu("UI/Requirements/RequirementsContainer")]
    public class RequirementsContainer : MonoBehaviour, IRequirementsContainer
    {
        [SerializeField] private RequirementView prefab;
        [SerializeField] private RectTransform container;

        private Dictionary<int, RequirementView> _views = new Dictionary<int, RequirementView>();

        public void Initialize(List<RequirementModel> requirements)
        {
            container.Clear();
            _views.Clear();
            requirements.ForEach(requirement =>
            {
                var item = Instantiate(prefab, container);
                _views.Add(requirement.ID, item);
            });
        }

        public IRequirementView GetRequirementView(int id) => _views[id];
        
    }
}