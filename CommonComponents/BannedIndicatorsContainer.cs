using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UI;

namespace UIScripts.CommonComponents
{
    public class BannedIndicatorsContainer : MonoBehaviour
    {
        [SerializeField] private Sprite filled;
        [SerializeField] private Sprite empty;
        [SerializeField] private Image[] images = new Image[10];
        [SerializeField] private int bannedLevel;
        [SerializeField] private int currentLevel;

        private readonly int _max = 10;

        public int Banned
        {
            get => bannedLevel;
            set => UpdateValue(ref bannedLevel, value);
        }

        public int Current
        {
            get => currentLevel;
            set => UpdateValue(ref currentLevel, value);
        }

        private void UpdateValue(ref int value, int newValue)
        {
            var normalized = math.clamp(newValue, 0, _max);
            if (normalized == value) return;

            value = normalized;
            UpdateImages();
        }
        
        private void UpdateImages()
        {
            if (bannedLevel == 0)
            {
                foreach (var image in images)
                {
                    image.enabled = false;
                }

                return;
            }
            
            for (var i = 0; i < images.Length; i++)
            {
                var image = images[i];

                if (image == null)
                    return;
                
                var level = i + 1;
                var isBanned = level >= bannedLevel;

                image.enabled = isBanned;

                if (isBanned)
                {
                    var isCollision = currentLevel >= level;
                    var sprite = isCollision ? filled : empty;

                    image.overrideSprite = sprite;
                }
            }
        }

#if UNITY_EDITOR
        private void OnValidate()
        {
            UpdateImages();
        }
#endif
    }
}