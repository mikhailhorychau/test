using System.Collections;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(CanvasGroup))]
public class CanvasInitializer : MonoBehaviour
{
    [SerializeField] private CanvasGroup canvasGroup;

    public UnityEvent onShowAnimationEnd;
    public UnityEvent onHideAnimationEnd;

    private CanvasGroup CanvasGroup
    {
        get
        {
            if (canvasGroup == null)
                canvasGroup = GetComponent<CanvasGroup>();

            if (canvasGroup == null)
                canvasGroup = gameObject.AddComponent<CanvasGroup>();

            return canvasGroup;
        }
    }

    public void Show(float appearanceDuration = 1f)
    {
        gameObject.SetActive(true);
        CanvasGroup.alpha = 0f;
        LeanTween.alphaCanvas(CanvasGroup, 1f, appearanceDuration).setOnComplete(onShowAnimationEnd.Invoke);
    }

    public void Hide(float hideDuration = 1f)
    {
        LeanTween.alphaCanvas(CanvasGroup, 0f, hideDuration).setOnComplete(onHideAnimationEnd.Invoke);
    }
    
    public IEnumerator CallEndEvent(UnityEvent ev, float waitingTime)
    {
        yield return new WaitForSeconds(waitingTime);
        ev.Invoke();
    }
}
