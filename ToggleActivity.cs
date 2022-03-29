using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace UIScripts
{
    public class ToggleActivity : MonoBehaviour
    {
        [SerializeField] private bool hideOnFalse = true;
        [SerializeField] private List<GameObject> objects;

        public void SetActivity(Toggle toggle)
        {
            if (hideOnFalse)
                objects.ForEach(obj => obj.SetActive(toggle.isOn));
            else 
                objects.ForEach(obj => obj.SetActive(!toggle.isOn));
        }

        private void Start()
        {
            var toggle = GetComponent<Toggle>();
            if (!toggle) return;
        
            SetActivity(toggle);
        }
    }
}