using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DragUI : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public Image targetImage; // Bottle 이미지
    public Image oneImage; // One 이미지
    private Image selfImage; // Cap 이미지

    private void Awake()
    {
        selfImage = GetComponent<Image>(); // Cap 이미지의 Image 컴포넌트를 가져옵니다.
        oneImage.enabled = false; // One 이미지를 보이지 않게 설정합니다.
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
    }

    public void OnDrag(PointerEventData eventData)
    {
        transform.position = eventData.position;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        var topQuarterArea = new Rect(
            targetImage.rectTransform.rect.x,
            targetImage.rectTransform.rect.y + targetImage.rectTransform.rect.height * 0.75f,
            targetImage.rectTransform.rect.width,
            targetImage.rectTransform.rect.height * 0.25f
        );

        if (RectTransformUtility.RectangleContainsScreenPoint(targetImage.rectTransform, eventData.position) &&
            topQuarterArea.Contains(targetImage.rectTransform.InverseTransformPoint(eventData.position)))
        {
            selfImage.enabled = false; // Cap 이미지를 보이지 않게 설정합니다.
            targetImage.enabled = false; // Bottle 이미지를 보이지 않게 설정합니다.

            oneImage.transform.position = targetImage.transform.position; // One 이미지의 위치를 Bottle 이미지의 위치로 설정합니다.
            oneImage.enabled = true; // One 이미지를 보이게 설정합니다.
        }
    }
}
