using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DragUI : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public Image targetImage; // Bottle �̹���
    public Image oneImage; // One �̹���
    private Image selfImage; // Cap �̹���

    private void Awake()
    {
        selfImage = GetComponent<Image>(); // Cap �̹����� Image ������Ʈ�� �����ɴϴ�.
        oneImage.enabled = false; // One �̹����� ������ �ʰ� �����մϴ�.
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
            selfImage.enabled = false; // Cap �̹����� ������ �ʰ� �����մϴ�.
            targetImage.enabled = false; // Bottle �̹����� ������ �ʰ� �����մϴ�.

            oneImage.transform.position = targetImage.transform.position; // One �̹����� ��ġ�� Bottle �̹����� ��ġ�� �����մϴ�.
            oneImage.enabled = true; // One �̹����� ���̰� �����մϴ�.
        }
    }
}
