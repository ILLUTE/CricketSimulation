using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System;

public class BowlingUI : MonoBehaviour
{
    public CanvasGroup canvasGroup;

    private void Awake()
    {
        GameManager.OnGameStarted += OnGameStarted;
        GameManager.OnDeliveryTypeSelected += OnBallSelected;
        GameManager.OnShotPlayed += OnShotPlayed;
    }

    private void OnGameStarted()
    {
        SetFade(1, 0.1f);
    }

    private void OnShotPlayed(int obj)
    {
        SetFade(1, 0.1f);
    }

    private void OnBallSelected()
    {
        SetFade(0, 0.1f);
    }

    public void SetFade(float alpha, float time, Action callback = null)
    {
        canvasGroup.DOFade(alpha, time).OnComplete(() =>
        {
            callback?.Invoke();
        });
    }
}
