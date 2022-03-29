using System;
using UnityEngine;

namespace UIScripts
{
    [Serializable]
    public class HexColor
    {
        public string color;
        public float alpha;

        public HexColor(string color, float alpha)
        {
            this.color = color;
            this.alpha = alpha;
        }

        public HexColor()
        {
            color = "000000";
            alpha = 1;
        }

        public Color RGBA()
        {
            ColorUtility.TryParseHtmlString(this.color, out var rgbaColor);
            // Debug.Log(this.color + " => " + rgbaColor);
            rgbaColor.a = alpha;
            return rgbaColor;
        }
    }
}