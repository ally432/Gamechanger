using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class HerbBook : MonoBehaviour
{
    public RectTransform herbpanel;
    public void Open()
    {
        herbpanel.DOLocalMoveY(-6, 1f).SetEase(Ease.OutBack);
    }
    public void Close()
    {
        herbpanel.DOLocalMoveY(-78, 1f).SetEase(Ease.InBack);
    }
}
