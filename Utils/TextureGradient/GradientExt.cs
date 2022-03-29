using UnityEngine;

namespace ExtUI
{
    public static class GradientExt
    {
        public static Texture2D ToTexture(this Gradient grad, int width = 32, int height = 1)
        {
            var gradTex = new Texture2D(width, height, TextureFormat.ARGB32, false)
            {
                filterMode = FilterMode.Bilinear
            };

            var inv = 1f / (width - 1);
            for (var y = 0; y < height; y++)
            {
                for (var x = 0; x < width; x++)
                {
                    var t = x * inv;
                    var col = grad.Evaluate(t);
                    gradTex.SetPixel(x, y, col);
                }
            }

            gradTex.Apply();
            return gradTex;
        }
    }
}