using System.Collections.Generic;
using UnityEngine;

namespace UIScripts
{
    public class ContentHider :  UIElement
    {
        [SerializeField] private Transform contentContainer;
        [SerializeField] private List<string> hidingObjectNames;

        private List<GameObject> _hidingObjects = new List<GameObject>();
        private bool _activity = true;
        private void Awake()
        {
            if (!contentContainer) return;


            foreach (Transform el in contentContainer)
            {
                hidingObjectNames.ForEach((objString) =>
                {
                    var hidingObj = el.Find(objString);
                    if (!hidingObj) return;
                    _hidingObjects.Add(hidingObj.gameObject);
                });
            }
        }

        public override void UpdateUI() => SetActivity(_activity);
    
        public void SetActivity(bool activity) 
        {
            _hidingObjects.Clear();
            _activity = activity;
            foreach (Transform el in contentContainer)
            {
                hidingObjectNames.ForEach((objString) =>
                {
                    var hidingObj = el.Find(objString);
                    if (!hidingObj) return;
                    _hidingObjects.Add(hidingObj.gameObject);
                });
            }
            _hidingObjects.ForEach(obj => obj.SetActive(activity));
        }
        public void SetActivity(StyledCheckboxSlider slider) => SetActivity(slider.selected);
    }
}