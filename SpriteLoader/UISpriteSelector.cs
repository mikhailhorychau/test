using System;
using UnityEngine;

namespace UIScripts.SpriteLoader
{
    public class UISpriteSelector : IUISpriteSelector
    {
        private readonly UISprites _sprites = new UISprites();

        public Sprite GetSprite<T>(T type) where T : Enum => _sprites.Get(type);
    }

    public interface IUISpriteSelector
    {
        public Sprite GetSprite<T>(T type) where T : Enum;
    }
}