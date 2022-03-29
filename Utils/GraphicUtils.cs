using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace UIScripts.Utils
{
    public static class GraphicUtils
    {
        public static List<Graphic> GetGraphicElements(this GameObject gameObject)
        {
            var _elements = new List<Graphic> {gameObject.GetComponent<Graphic>()};

            gameObject.GetComponentsInChildren<Graphic>()
                .ToList()
                .ForEach(_elements.Add);

            return _elements;
        }

        public static void SetAlpha(this Graphic graphic, float alpha)
        {
            if (graphic == null || !graphic.gameObject.activeSelf) return;
            var color = graphic.color;
            color.a = alpha;
            graphic.color = color;
        }

        public static void SetAlpha(this Image image, float alpha) => SetAlpha((Graphic) image, alpha);

        public static HexColor ToHexColor(this Color color) => new HexColor($"#{ColorUtility.ToHtmlStringRGB(color)}", color.a);
    }
}