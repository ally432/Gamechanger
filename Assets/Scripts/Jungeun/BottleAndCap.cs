using UnityEngine;
using UnityEngine.EventSystems;

public class DragUI : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    private RectTransform originalParent;
    public RectTransform targetImage; // 이것은 Cap 이미지를 놓을 대상 이미지(Bottle 이미지)입니다. Unity 에디터에서 설정하세요.

    public void OnBeginDrag(PointerEventData eventData)
    {
        originalParent = transform as RectTransform;
    }

    public void OnDrag(PointerEventData eventData)
    {
        transform.position = eventData.position;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        // Calculate the top quarter area of the target image.
        var topQuarterArea = new Rect(
            targetImage.rect.x,
            targetImage.rect.y + targetImage.rect.height * 0.75f,
            targetImage.rect.width,
            targetImage.rect.height * 0.25f
        );

        // Check if the mouse release position is within the top quarter area of the target image.
        if (RectTransformUtility.RectangleContainsScreenPoint(targetImage, eventData.position) &&
            topQuarterArea.Contains(targetImage.InverseTransformPoint(eventData.position)))
        {
            transform.SetParent(targetImage); // Set the target image as the new parent.
        }
        else
        {
            // If the mouse release position is not within the top quarter area of the target image, revert to the original parent.
            transform.SetParent(originalParent);
        }
    }

}
