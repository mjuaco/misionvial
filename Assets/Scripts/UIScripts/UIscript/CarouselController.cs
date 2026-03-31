using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public sealed class CarouselController : MonoBehaviour
{
    [Header("Configuración")]
    public ScrollRect scrollRect;
    public RectTransform contentPanel;
    public float lerpSpeed = 10f;

    private int totalElements;
    private float stepSize; 
    private float targetNormalizedPosition;

    void Start()
    {
        totalElements = contentPanel.childCount;
        stepSize = 1f / (totalElements - 4);
        targetNormalizedPosition = scrollRect.horizontalNormalizedPosition;
    }

    public void MoveRight()
    {
        targetNormalizedPosition = Mathf.Clamp(targetNormalizedPosition + stepSize, 0f, 1f);
        StopAllCoroutines();
        StartCoroutine(AnimateScroll());
    }

    public void MoveLeft()
    {
        targetNormalizedPosition = Mathf.Clamp(targetNormalizedPosition - stepSize, 0f, 1f);
        StopAllCoroutines();
        StartCoroutine(AnimateScroll());
    }

    IEnumerator AnimateScroll()
    {
        while (Mathf.Abs(scrollRect.horizontalNormalizedPosition - targetNormalizedPosition) > 0.001f)
        {
            scrollRect.horizontalNormalizedPosition = Mathf.Lerp(
                scrollRect.horizontalNormalizedPosition,
                targetNormalizedPosition,
                Time.deltaTime * lerpSpeed
            );
            yield return null;
        }
        scrollRect.horizontalNormalizedPosition = targetNormalizedPosition;
    }
}

