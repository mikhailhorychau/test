using System;
using System.Linq;
using UnityEngine;

namespace UIScripts.SpriteLoader
{
    [Serializable]
    public class UISpritesContainer<T> where T : Enum
    {
        [SerializeField] private EnumSpritePair<T>[] pairs = 
            Enum
                .GetValues(typeof(T))
                .Cast<T>()
                .ToArray()
                .Select(en => new EnumSpritePair<T>(en, null))
                .ToArray();
    }

    [Serializable]
    public class EnumSpritePair<T> : StringSprite where T : Enum
    {
        public T type;

        public EnumSpritePair(T type, Sprite sprite) : base(Enum.GetName(typeof(T), type), sprite)
        {
            this.type = type;
        }
    }

    public class StringSprite
    {
        public string key;
        public Sprite sprite;

        public StringSprite(string key, Sprite sprite)
        {
            this.key = key;
            this.sprite = sprite;
        }
    }
}