using UnityEngine;
using UnityEngine.EventSystems;

public class DragSell: MonoBehaviour, IBeginDragHandler, IDragHandler
{
    private Vector2 originalPosition;

    public void OnBeginDrag(PointerEventData eventData)
    {
        originalPosition = transform.position;
    }

    public void OnDrag(PointerEventData eventData)
    {
        transform.position = eventData.position;
    }
}
