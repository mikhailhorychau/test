using System.Collections.Generic;
using UnityEngine;

namespace UIScripts.Tutorial
{
    public class TutorialTest : MonoBehaviour
    {
        [SerializeField] private TutorialWindow tutorial;
        [SerializeField] private List<RectTransform> tutorialItems;

        private void Start()
        {
            var items = new List<TutorialItem>();
            tutorialItems.ForEach(item =>
            {
                var tutorialItem = new TutorialItem()
                {
                    Description = Utils.Utils.RandomString(100),
                    Rect = item
                };
                
                items.Add(tutorialItem);
            });
            
            tutorial.StartTutorial(items);
        }
    }
}