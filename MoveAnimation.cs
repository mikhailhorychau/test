using UnityEngine;

namespace UIScripts
{
    public class MoveAnimation : MonoBehaviour
    {
        public Vector3 moveVector;
        public float moveAnimationTime;

        private Vector3 _startPosition;
        private Vector3 _movePosition;
        private RectTransform _rectTransform;
        private void Start()
        {
            _rectTransform = GetComponent<RectTransform>();
            _startPosition = _rectTransform.anchoredPosition;
            Vector3 movePosVector = _startPosition;
            _movePosition = new Vector3(movePosVector.x + moveVector.x, movePosVector.y - moveVector.y);
        }

        public void MoveToPosition()
        {
            LeanTween.move(_rectTransform, _movePosition, moveAnimationTime);
        }

        public void MoveToStartPosition()
        {
            LeanTween.move(_rectTransform, _startPosition, moveAnimationTime);
        }
    
        public void Show()
        {
            if (!_rectTransform) _rectTransform = GetComponent<RectTransform>();
            LeanTween.moveY(_rectTransform, _rectTransform.anchoredPosition.y - _rectTransform.sizeDelta.y, 1f);
        }
    
        public void Hide()
        {
            if (!_rectTransform) _rectTransform = GetComponent<RectTransform>();
            LeanTween.moveY(_rectTransform, _rectTransform.anchoredPosition.y + _rectTransform.sizeDelta.y, 1f);
        }
    }
}