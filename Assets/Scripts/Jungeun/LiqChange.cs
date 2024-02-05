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
        Debug.Log("ChangeImage 함수가 호출되었습니다.");
        newImageComponent.enabled = true;
        openDoorButton.gameObject.SetActive(true);  // Open Door 버튼을 활성화
        oldImageComponent.enabled = false;
        doorButton.gameObject.SetActive(false);  // Door 버튼을 비활성화
    }
    public void BackImage()
    {
        oldImageComponent.enabled = true;
        doorButton.gameObject.SetActive(true);  // Door 버튼을 활성화
        newImageComponent.enabled = false;
        openDoorButton.gameObject.SetActive(false);  // Open Door 버튼을 비활성화
    }
}
