using System;
using UnityEngine;

namespace UIScripts
{
    [Serializable]
    public class TyresSettings
    {
        public Sprite soft;
        public Sprite medium;
        public Sprite hard;
        public Sprite intermediate;
        public Sprite wet;

        public Sprite Pick(TyresType type)
        {
            switch (type)
            {
                case TyresType.Soft : return soft;
                case TyresType.Medium : return medium;
                case TyresType.Hard : return hard;
                case TyresType.Intermediate : return intermediate;
                case TyresType.Wet : return wet;
                default : return soft;
            }
        }
    }
}