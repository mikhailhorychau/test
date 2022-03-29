using System.Collections.Generic;
using UIScripts.Utils;
using UnityEngine;

namespace UIScripts.Screens.V2.BonusRequirement
{
    public class EmptySpriteRequirementsContainer : MonoBehaviour, IRequirementsContainer
    {
        [SerializeField] private RequirementView commonPrefab;
        [SerializeField] private RequirementView emptyPrefab;
        [SerializeField] private RectTransform container;

        private Dictionary<int, IRequirementView> _views = new Dictionary<int, IRequirementView>();
        
        public void Initialize(List<RequirementModel> requirements)
        {
            _views.Clear();
            container.Clear();
            requirements.ForEach(requirement =>
            {
                var prefab = GetPrefabBySpriteState(requirement.Icon);
                var item = Instantiate(prefab, container);
                _views.Add(requirement.ID, item);
            });
        }

        public IRequirementView GetRequirementView(int id)
        {
            return _views[id];
        }

        private RequirementView GetPrefabBySpriteState(Sprite sprite) => sprite ? commonPrefab : emptyPrefab;
    }
}