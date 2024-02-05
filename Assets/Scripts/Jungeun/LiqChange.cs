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
        Debug.Log("ChangeImage �Լ��� ȣ��Ǿ����ϴ�.");
        newImageComponent.enabled = true;
        openDoorButton.gameObject.SetActive(true);  // Open Door ��ư�� Ȱ��ȭ
        oldImageComponent.enabled = false;
        doorButton.gameObject.SetActive(false);  // Door ��ư�� ��Ȱ��ȭ
    }
    public void BackImage()
    {
        oldImageComponent.enabled = true;
        doorButton.gameObject.SetActive(true);  // Door ��ư�� Ȱ��ȭ
        newImageComponent.enabled = false;
        openDoorButton.gameObject.SetActive(false);  // Open Door ��ư�� ��Ȱ��ȭ
    }
}
