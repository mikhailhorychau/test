using UIScripts.Screens.Panels.HeaderMenu.Bonuses;
using UnityEngine;

namespace UIScripts.SpriteLoader
{
    [CreateAssetMenu(fileName = "UISprites", menuName = "UI/Data/Sprites", order = 0)]
    public class UISpritesData : ScriptableObject
    {
        [SerializeField] private UISpritesContainer<BonusType> sprites;
    }
}