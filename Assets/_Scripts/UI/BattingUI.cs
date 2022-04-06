using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class BattingUI : MonoBehaviour
{
    [SerializeField]
    private CanvasGroup battingCanvas;

    [SerializeField]
    private BattingButton[] battingButtons;

    private void Awake()
    {
        GameManager.OnDeliveryTypeSelected += OnBallSelected;
        GameManager.OnShotSelected += OnShotSelected;
    }

    private void Start()
    {
        for (int i = 0; i < battingButtons.Length; i++)
        {
            battingButtons[i].OnSetButton(this);
        }
    }

    private void OnShotSelected()
    {
        SetFade(0, 0.1f);
    }

    private void OnBallSelected()
    {
        SetFade(1, 0.1f);
    }

    private void SetFade(float alpha, float time,Action callback = null)
    {
        battingCanvas.DOFade(alpha, time).OnComplete(() =>
        {
            callback?.Invoke();
        });
    }

    public void OnRunsSelected(Runs m_Runs)
    {
        GameManager.Instance.SetBattingRuns(m_Runs);
    }
}
