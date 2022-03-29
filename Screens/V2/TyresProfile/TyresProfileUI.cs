using System;
using TMPro;
using UIScripts.Screens.V2.BonusRequirement;
using UIScripts.Screens.V2.Contracts;
using UnityEngine;
using UnityEngine.UI;

namespace UIScripts.Screens.V2.TyresProfile
{
    public class TyresProfileUI : MonoBehaviour
    {
        [SerializeField] private Image logo;
        [SerializeField] private TextMeshProUGUI title;
        [SerializeField] private TyresParams tyresParams;
        [SerializeField] private ContractsContainer contracts;
        [SerializeField] private SingleRequirementButtonPresenter reqButton;

        public Sprite Logo
        {
            set => logo.overrideSprite = value;
        }

        public string Title
        {
            set => title.text = value;
        }

        public TyresParams TyresParams => tyresParams;
        public ContractsContainer Contracts => contracts;
        public SingleRequirementButtonPresenter ReqButton => reqButton;

        private void Awake()
        {
            ReqButton.gameObject.SetActive(false);
        }
    }

    [Serializable]
    public class TyresParams
    {
        [SerializeField] private TyresParam hard;
        [SerializeField] private TyresParam intermediate;
        [SerializeField] private TyresParam soft;
        [SerializeField] private TyresParam wet;

        public TyresParam Hard => hard;
        public TyresParam Intermediate => intermediate;
        public TyresParam Soft => soft;
        public TyresParam Wet => wet;
    }
}