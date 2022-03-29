using System;
using System.Collections.Generic;
using System.Linq;
using UIScripts.Utils;
using UnityEngine;

namespace UIScripts
{
    public class ZebraList : MonoBehaviour
    {
        [SerializeField] private List<StyledImage> itemsList;

        [SerializeField] private Sprite firstSprite;
        
        [Space] [SerializeField] private UIColorStyle firstColor = UIColorStyle.Black;
        [SerializeField] private float firstColorAlpha = 0.7f;
    
        [Space] [SerializeField] private UIColorStyle secondColor = UIColorStyle.MainBackground;
        [SerializeField] private float secondColorAlpha = 0.9f;

        [SerializeField] private bool initializeOnAwake = false;
        [SerializeField] private bool autoInitializeOnChildCountChanged = false;

        private bool _initializedFromCode = false;

        public UIColorStyle FirstColor => firstColor;
        public UIColorStyle SecondColor => secondColor;
        
        public List<StyledImage> ItemsList
        {
            get => itemsList;
            set
            {
                _initializedFromCode = true;
                itemsList = value;
                Initialize();
            }
        }

        private void Awake()
        {
            if (ItemsList.Count != 0 && ItemsList != null && !_initializedFromCode && initializeOnAwake)
            {
                Initialize();
            }
        }

        private void Initialize()
        {
            var index = 0;
            if (itemsList == null || itemsList.Count == 0) return; 
            itemsList.ForEach(item =>
            {
                if (!item) return;
                
                index++;
                var color = index % 2 != 0 ? firstColor : secondColor;
                var alpha = index % 2 != 0 ? firstColorAlpha : secondColorAlpha;
                item.SwapColor(color, alpha);
                if (firstSprite != null && index == 1)
                    item.image.overrideSprite = firstSprite;
            });
        }

        private void OnTransformChildrenChanged()
        {
            if (!autoInitializeOnChildCountChanged) return;
            itemsList = transform.GetTopLevelChildComponents<StyledImage>();
            Initialize();
        }

#if UNITY_EDITOR
        private void OnValidate()
        {
            if (ItemsList != null && ItemsList.Count != 0 )
            {
                Initialize();
            }
        }
#endif
    }
}