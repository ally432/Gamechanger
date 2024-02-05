using UnityEngine;
using UnityEngine.UI;

public class ImageChanger : MonoBehaviour
{
    public GameObject open, close, openbtn, closebtn;  // 닫힌 이미지, 열린 이미지

    void Start()
    {
        open.SetActive(false);  // 열린 이미지 안 보이게
        closebtn.SetActive(false);  // 닫는 버튼 안 보이게
    }

    public void ChangeImage()   // 열림 버튼 눌렀을 때
    {
        openbtn.SetActive(true);
        open.SetActive(true);
        closebtn.SetActive(true);
        close.SetActive(false);
    }
    public void BackImage() // 닫힘 버튼 눌렀을 때
    {
        close.SetActive(true);
        open.SetActive(false);
        closebtn.SetActive(false);
    }
}
