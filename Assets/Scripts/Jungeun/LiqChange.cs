using UnityEngine;
using UnityEngine.UI;

public class ImageChanger : MonoBehaviour
{
    public Image oldImageComponent;
    public Image newImageComponent;
    public Button doorButton;  // Door ��ư ����
    public Button openDoorButton;

    void Start()
    {
        newImageComponent.enabled = false;
        openDoorButton.gameObject.SetActive(false);
    }

    public void ChangeImage()
    {
        oldImageComponent.enabled = false;
        newImageComponent.enabled = true;
        doorButton.gameObject.SetActive(false);  // Door ��ư�� ��Ȱ��ȭ
        openDoorButton.gameObject.SetActive(true);  // Open Door ��ư�� Ȱ��ȭ
    }
    public void BackImage()
    {
        newImageComponent.enabled = false;
        oldImageComponent.enabled = true;
        doorButton.gameObject.SetActive(true);  // Door ��ư�� Ȱ��ȭ
        openDoorButton.gameObject.SetActive(false);  // Open Door ��ư�� ��Ȱ��ȭ
    }
}
