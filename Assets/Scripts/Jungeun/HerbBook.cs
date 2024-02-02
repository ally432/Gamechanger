using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;

public class HerbBook : MonoBehaviour
{
    public RectTransform herbPanel; // ���� ���� �г�
    public GameObject[] herbs; // �� �������� �ش��ϴ� ���� �̹�����
    public Image[] locks; // �� ���� �̹����� �����ϴ� �ڹ��� �̹�����
    public int date; // ��¥
    public TextMeshProUGUI pageTaxt;

    private int currentPage = 0; // ���� ������

    void Start()
    {
        // ��� �ڹ��� �̹����� Ȱ��ȭ
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
            herbs[0].SetActive(true); // Hone �г� �ر�
            locks[0].gameObject.SetActive(false);
            herbs[1].SetActive(true); // Hone �г� �ر�
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
            herbs[3].SetActive(true); // Hthree �г� �ر�
            locks[3].gameObject.SetActive(false);
            herbs[8].SetActive(true); // SMtwo �г� �ر�
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
            herbs[currentPage].SetActive(false); // ���� ������ ��Ȱ��ȭ
            currentPage = page; // currentPage ������Ʈ
            herbs[currentPage].SetActive(true); // �� ������ Ȱ��ȭ
        }
    }

    public void NextPage()
    {
        if (currentPage < herbs.Length - 1)
        {
            herbs[currentPage].SetActive(false); // ���� ������ ��Ȱ��ȭ
            currentPage++;
            herbs[currentPage].SetActive(true); // �� ������ Ȱ��ȭ
        }
    }

    public void PrevPage()
    {
        if (currentPage > 0)
        {
            herbs[currentPage].SetActive(false); // ���� ������ ��Ȱ��ȭ
            currentPage--;
            herbs[currentPage].SetActive(true); // �� ������ Ȱ��ȭ
        }
    }
}
