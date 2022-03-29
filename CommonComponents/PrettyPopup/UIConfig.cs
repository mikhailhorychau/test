using UnityEngine;

namespace UIScripts.CommonComponents.PrettyPopup
{
    public struct UIConfig
    {
        public bool withBorder;
        public UIBorderConfig borderConfig;
        public Vector2 size;
    }

    public static class Test
    {
        public static void Main()
        {
            var config =
                new UIConfig()
                    .Border(2, Color.blue)
                    .Size(100, 20);
        }
    }

    public struct UIBorderConfig
    {
        public float width;
        public Color color;

        public UIBorderConfig(float width, Color color)
        {
            this.width = width;
            this.color = color;
        }
    }

    public static class UIConfigExt
    {
        public static UIConfig Border(this UIConfig config, float width = 1, Color color = default)
        {
            config.withBorder = true;
            config.borderConfig = new UIBorderConfig(width, color);

            return config;
        }

        public static UIConfig Size(this UIConfig config, float width, float height)
        {
            config.size = new Vector2(width, height);

            return config;
        }
    }
}