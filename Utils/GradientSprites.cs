using System;
using System.Collections.Generic;
using ExtUI;
using UnityEngine;

namespace UIScripts.Utils
{
    public static class GradientSprites
    {
        [Serializable]
        public struct GradientSettings
        {
            public UIColorStyle StartColor;
            public UIColorStyle MiddleColor;
            public UIColorStyle EndColor;
            [NonSerialized] public Vector2 Size;
        }

        private static Dictionary<GradientSettings, Sprite> _textures = new Dictionary<GradientSettings, Sprite>();

        public static Sprite GetGradientSprite(GradientSettings gradientSettings)
        {
            if (!_textures.ContainsKey(gradientSettings) || _textures[gradientSettings] == null)

                _textures[gradientSettings] = CreateGradientTexture(gradientSettings).ToSprite();
            
            return _textures[gradientSettings];
        }

        private static Texture2D CreateGradientTexture(GradientSettings gradientSettings)
        {
            var gradient = new Gradient();
            
            var colorKeys = new[]
            {
                new GradientColorKey(UISettings.Instance.colors.Pick(gradientSettings.StartColor), 0.25f),
                new GradientColorKey(UISettings.Instance.colors.Pick(gradientSettings.MiddleColor), 0.4f),
                new GradientColorKey(UISettings.Instance.colors.Pick(gradientSettings.EndColor), 0.7f), 
            };
            
            var alphaKeys = new[]
            {
                new GradientAlphaKey(255, 0f),
                new GradientAlphaKey(255, 1f),
            };
            gradient.SetKeys(colorKeys, alphaKeys);
            return gradient.ToTexture(Convert.ToInt32(gradientSettings.Size.x), Convert.ToInt32(gradientSettings.Size.y));
        }
    }
}