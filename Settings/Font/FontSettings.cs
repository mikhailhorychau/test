using System;
using TMPro;

namespace UIScripts
{
    [Serializable]
    public struct FontSettings
    {
        public TMP_FontAsset regular;
        public TMP_FontAsset bold;

        public TMP_FontAsset Pick(UIFontStyle fontStyle)
        {
            switch (fontStyle)
            {
                case UIFontStyle.Regular : return regular;
                case UIFontStyle.Bold : return bold;
                default : return regular;
            }
        }
    }
}