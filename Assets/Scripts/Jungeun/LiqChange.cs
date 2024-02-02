using UnityEngine;
using UnityEngine.UI;

public class ImageChanger : MonoBehaviour
{
    public Image oldImageComponent;
    public Image newImageComponent;
    public Button doorButton;  // Door 버튼 참조
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
        doorButton.gameObject.SetActive(false);  // Door 버튼을 비활성화
        openDoorButton.gameObject.SetActive(true);  // Open Door 버튼을 활성화
    }
    public void BackImage()
    {
        newImageComponent.enabled = false;
        oldImageComponent.enabled = true;
        doorButton.gameObject.SetActive(true);  // Door 버튼을 활성화
        openDoorButton.gameObject.SetActive(false);  // Open Door 버튼을 비활성화
    }
}
