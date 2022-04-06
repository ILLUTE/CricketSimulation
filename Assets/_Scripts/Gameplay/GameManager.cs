using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;

    public static GameManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<GameManager>();
            }

            return instance;
        }
    }

    #region Events
    public static event Action OnDeliveryTypeSelected;
    public static event Action<Ball> OnDeliveryStarted;
    public static event Action<Ball> OnDeliveryCompleted;
    public static event Action OnShotSelected;
    public static event Action<Ball> OnShotStarted;
    public static event Action<int> OnShotPlayed;
    public static event Action<GameOverScenario> OnGameOver;
    public static event Action OnGameStarted;
    public static event Action<CricketScore> OnScoreUpdate;
    public static event Action<int> OnOverFinished;
    #endregion

    public void OnDeliveryTypeSelectedMethod()
    {
        OnDeliveryTypeSelected?.Invoke();
    }

    public void OnDeliveryStartedMethod(Ball ball)
    {
        OnDeliveryStarted?.Invoke(ball);
    }

    public void OnDeliveryCompletedMethod(Ball ball)
    {
        OnDeliveryCompleted?.Invoke(ball);
    }

    public void OnShotSelectedMethod()
    {
        OnShotSelected?.Invoke();
    }

    public void OnShotStartedMethod(Ball ball)
    {
        OnShotStarted?.Invoke(ball);
    }

    public void OnShotPlayedMethod(int runs)
    {
        OnShotPlayed?.Invoke(runs);
    }

    public void OnGameOverMethod(GameOverScenario scenario)
    {
        OnGameOver?.Invoke(scenario);
    }

    public void OnGameStartedMethod()
    {
        OnGameStarted?.Invoke();
    }

    public void OnScoreUpdateMethod(CricketScore score)
    {
        OnScoreUpdate?.Invoke(score);
    }

    public void OnOverFinishedMethod(int overNum)
    {
        OnOverFinished?.Invoke(overNum);
    }

    public void ResetData()
    {
        OnGameStartedMethod();
    }
}
