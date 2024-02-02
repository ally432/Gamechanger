using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;

public class HerbBook : MonoBehaviour
{
    public RectTransform herbPanel; // 약초 도감 패널
    public GameObject[] herbs; // 각 페이지에 해당하는 약초 이미지들
    public Image[] locks; // 각 약초 이미지에 대응하는 자물쇠 이미지들
    public int date; // 날짜
    public TextMeshProUGUI pageTaxt;

    private int currentPage = 0; // 현재 페이지

    void Start()
    {
        // 모든 자물쇠 이미지를 활성화
        foreach (var lockImage in locks)
        {
            lockImage.gameObject.SetActive(true);
        }
    }

    public void Open()
    {
        herbPanel.DOLocalMoveY(-6, 1f).SetEase(Ease.OutBack);
    }

    public void Close()
    {
        herbPanel.DOLocalMoveY(-78, 1f).SetEase(Ease.InBack);
    }

    private void ShowPage(int page)
    {
        Debug.Log("ShowPage called with: " + page);

        for (int i = 0; i < herbs.Length; i++)
        {
            herbs[i].SetActive(true);
        }
        pageTaxt.text = "Page: " + (page + 1);
    }

    public void UnlockHerbs()
    {
        if (date == 1)
        {
            herbs[0].SetActive(true); // Hone 패널 해금
            locks[0].gameObject.SetActive(false);
            herbs[1].SetActive(true); // Hone 패널 해금
            locks[1].gameObject.SetActive(false);
        }
        else if (date == 2)
        {
            herbs[7].SetActive(true);
            locks[7].gameObject.SetActive(false);
        }
        else if (date == 4)
        {
            herbs[2].SetActive(true);
            locks[2].gameObject.SetActive(false);
        }
        else if (date == 6)
        {
            herbs[3].SetActive(true); // Hthree 패널 해금
            locks[3].gameObject.SetActive(false);
            herbs[8].SetActive(true); // SMtwo 패널 해금
            locks[8].gameObject.SetActive(false);
        }
        else if (date == 8)
        {
            herbs[4].SetActive(true);
            locks[4].gameObject.SetActive(false);
        }
        else if (date == 10)
        {
            herbs[9].SetActive(true);
            locks[9].gameObject.SetActive(false);
        }
        else if (date == 12)
        {
            herbs[5].SetActive(true);
            locks[5].gameObject.SetActive(false);
        }
        else if (date == 14)
        {
            herbs[10].SetActive(true);
            locks[10].gameObject.SetActive(false);
        }
        else if (date == 16)
        {
            herbs[6].SetActive(true);
            locks[6].gameObject.SetActive(false);
        }
    }
    public void GoToPage(int page)
    {
        if (page >= 0 && page < herbs.Length)
        {
            herbs[currentPage].SetActive(false); // 현재 페이지 비활성화
            currentPage = page; // currentPage 업데이트
            herbs[currentPage].SetActive(true); // 새 페이지 활성화
        }
    }

    public void NextPage()
    {
        if (currentPage < herbs.Length - 1)
        {
            herbs[currentPage].SetActive(false); // 현재 페이지 비활성화
            currentPage++;
            herbs[currentPage].SetActive(true); // 새 페이지 활성화
        }
    }

    public void PrevPage()
    {
        if (currentPage > 0)
        {
            herbs[currentPage].SetActive(false); // 현재 페이지 비활성화
            currentPage--;
            herbs[currentPage].SetActive(true); // 새 페이지 활성화
        }
    }
}
