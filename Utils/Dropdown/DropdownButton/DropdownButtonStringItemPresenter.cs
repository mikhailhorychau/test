using System;
using TMPro;
using UnityEngine;

namespace UIScripts
{
    public class DropdownButtonStringItemPresenter : MonoBehaviour, IDropdownItemPresenter<StringProps>
    {
        [SerializeField] private TextMeshProUGUI title;
        
        public void Initialize(StringProps props)
        {
            title.text = props.value;
        }
    }
}