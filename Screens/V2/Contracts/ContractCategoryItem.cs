using System;
using ExtUI;
using TMPro;
using UnityEngine;

namespace UIScripts.Screens.V2.Contracts
{
    public class ContractCategoryItem : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI title;
        [SerializeField] private TextMeshProExt textExt;
        [SerializeField] private StyledImage background;
        
        public event Action<TMP_LinkInfo> OnLinkClick
        {
            add => textExt.onMouseClick.AddListener(value.Invoke);
            remove => textExt.onMouseClick.RemoveListener(value.Invoke);
        }

        public void SetTitle(string text) => title.text = text;
        public void SetColor(UIColorStyle color) => background.SwapColor(color);
    }
}