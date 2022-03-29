using System;
using TMPro;
using UIScripts.Screens.Panels.HeaderMenu.Bonuses;
using UnityEngine;

namespace UIScripts.Utils
{
    public static class FontUtils
    {
        public static string ToBoldString(this string text) => $"<font=\"Bold\">{text}</font>";
        public static string ToRegularString(this string text) => $"<font=\"Regular\">{text}</font>";
        public static string ToUnderlineString(this string text) => $"<u>{text}</u>";

        public static string SetInPixelSize(this string text, int size) => text.SetSize($"{size}px");

        public static string SetSize(this string text, string size) => $"<size={size}>{text}</size>";

        public static string GetSpace(int count) => $"<space={count}>";

        public static string ToColorString(this string text, UIColorStyle color)
        {
            var hexColor = ColorUtility.ToHtmlStringRGBA(UISettings.Instance.colors.Pick(color));
            return $"<color=#{hexColor}>{text}</color>";
        }

        public static string AddSprite(this string text, int id) => $"{text} {GetSpriteString(id)}";
        public static string GetSpriteString(int id) => $"<sprite index={id}>";

        public static void SetOrDisable(this TextMeshProUGUI textMesh, string text)
        {
            textMesh.gameObject.SetActive(!string.IsNullOrEmpty(text));
            textMesh.text = text;
        }

        public static void SetIfExists(this TextMeshProUGUI textMesh, string text)
        {
            if (textMesh != null)
                textMesh.text = text;
        }

        public static void IfExists(this TextMeshProUGUI textMesh, Action<TextMeshProUGUI> action)
        {
            if (textMesh != null) 
                action.Invoke(textMesh);
        }
    }
}