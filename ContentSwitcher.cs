using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace UIScripts
{
    public class ContentSwitcher : MonoBehaviour
    {
        [SerializeField] private List<string> firstElementsNames;
        [SerializeField] private List<string> secondElementsNames;
        [SerializeField] private Transform container;

        private List<GameObject> _firstElements = new List<GameObject>();
        private List<GameObject> _secondElements = new List<GameObject>();

        public void Start()
        {
            if (!container) return;

            foreach (Transform tr in container)
            {
                firstElementsNames.ForEach(objName =>
                {
                    var obj = tr.Find(objName);
                    if (obj)
                        _firstElements.Add(obj.gameObject);
                });
                secondElementsNames.ForEach(objName =>
                {
                    var obj = tr.Find(objName);
                    if (obj)
                        _secondElements.Add(obj.gameObject);
                });
            }
        
        }

        public void SwitchElements(Toggle toggle) => SwitchElements(toggle.isOn);
        public void SwitchElements(StyledCheckboxSlider toggle) => SwitchElements(toggle.selected);
    
    
        public void SwitchElements(bool switched)
        {
            _firstElements.Clear();
            _secondElements.Clear();
            foreach (Transform tr in container)
            {
                firstElementsNames.ForEach(objName =>
                {
                    var obj = tr.Find(objName);
                    if (obj)
                        _firstElements.Add(obj.gameObject);
                });
                secondElementsNames.ForEach(objName =>
                {
                    var obj = tr.Find(objName);
                    if (obj)
                        _secondElements.Add(obj.gameObject);
                });
            }
            _firstElements.ForEach(el => el.SetActive(switched));
            _secondElements.ForEach(el => el.SetActive(!switched));
        }
    }
}