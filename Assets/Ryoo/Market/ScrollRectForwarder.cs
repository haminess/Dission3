using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ScrollRectForwarder : MonoBehaviour, IScrollHandler
{
    private ScrollRect parentScrollRect;

    void Start()
    {
        // 부모 ScrollRect 찾기
        parentScrollRect = GetComponentInParent<ScrollRect>();
    }

    public void OnScroll(PointerEventData eventData)
    {
        if (parentScrollRect != null)
        {
            // 스크롤 이벤트를 부모 ScrollRect로 전달
            parentScrollRect.OnScroll(eventData);
        }
    }
}