using System;
using UnityEngine;

namespace UIScripts
{
    [Serializable]
    public class ButtonStyle
    {
        public Sprite backgroundSprite;
        public UIColorStyle backgroundColor;
        public Sprite borderSprite;
        public UIColorStyle borderColor;
        public TextStyle textStyle;
    }
}