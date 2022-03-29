using System;
using System.Collections.Generic;
using System.Linq;
using Models.Notifications;
using UIScripts.Screens.Panels.HeaderMenu.Bonuses;
using UIScripts.Screens.V2.Development.Factory.ProductionRequest;
using UnityEngine;

namespace UIScripts.SpriteLoader
{
    public class UISprites
    {
        private const string IMAGES_PATH = "UI/Images";

        private static Dictionary<Type, Dictionary<Enum, Sprite>> _map =
            new Dictionary<Type, Dictionary<Enum, Sprite>>();

        public UISprites()
        {
            RegistryTypesOfEnum<BonusType>();
            RegistryTypesOfEnum<RequestType>();
            RegistryTypesOfEnum<NotificationType>();
        }

        public Sprite Get<T>(T type) where T : Enum
        {
            var sprite = _map[typeof(T)][type];

            if (sprite == null)
            {
                var path = GetSpritePathFromEnum(type);
                sprite = LoadSprite(path);
                _map[typeof(T)][type] = sprite;
            }

            return sprite;
        }

        private void RegistryTypesOfEnum<T>() where T : Enum
        {
            _map[typeof(T)] = new Dictionary<Enum, Sprite>();

            var values = Enum.GetValues(typeof(T)).Cast<T>();

            foreach (var value in values)
            {
                Registry(value);
            }
        }

        private void Registry<T>(T type) where T : Enum
        {
            var path = GetSpritePathFromEnum(type);
            var sprite = LoadSprite(path);
            _map[typeof(T)].Add(type, sprite);
        }

        private string GetSpritePathFromEnum<T>(T type) where T : Enum
        {
            var enumType = typeof(T);
            return $"{IMAGES_PATH}/{enumType.Name}/{Enum.GetName(enumType, type)}";
        }

        private Sprite LoadSprite(string path) => Resources.Load<Sprite>(path);
    }
}