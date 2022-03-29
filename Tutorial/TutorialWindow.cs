using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace UIScripts.Tutorial
{
    public class TutorialWindow : MonoBehaviour, IPointerClickHandler
    {
        public event Action OnTutorialComplete;
        
        [SerializeField] private TutorialDescription description;
        [SerializeField] private TutorialSelection selection;

        private List<TutorialItem> items;
        private int _currentStage = 0;

        public int StageIndex => _currentStage - 1;
        
                
        public void OnPointerClick(PointerEventData eventData)
        {
            BlockerClickListener();
        }

        public void StartTutorial(List<TutorialItem> tutorialItems)
        {
            gameObject.SetActive(true);
            _currentStage = 0;
            items = tutorialItems;
            selection.gameObject.SetActive(true);
            
            UISettings.Instance.GameCursor.SetCursorType(CursorType.Mouse);
            NextStage();
        }

        private void BlockerClickListener()
        {
            NextStage();
        }

        private void NextStage()
        {
            _currentStage++;
            if (_currentStage > items.Count)
            {
                gameObject.SetActive(false);
                selection.gameObject.SetActive(false);
                selection.transform.SetParent(transform);
                UISettings.Instance.GameCursor.SetCursorType(CursorType.Common);
                OnTutorialComplete?.Invoke();
                return;
            }

            var current = items[StageIndex];
            selection.transform.SetParent(current.Rect);

            description.SetText(current.Description);
            description.SetStage(GetStageText());
        }

        private string GetStageText() => $"{_currentStage}/{items.Count}";

    }

    [Serializable]
    public class TutorialItem
    {
        public RectTransform Rect { get; set; }
        public string Description { get; set; }
    }
}