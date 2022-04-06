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

    [SerializeField]
    private Batting batting;
    [SerializeField]
    private BowlingDelivery bowling;

    public void DeliveryTypeSelected()
    {
        OnDeliveryTypeSelected?.Invoke();
    }

    public void DeliveryStarted(Ball ball)
    {
        OnDeliveryStarted?.Invoke(ball);
    }

    public void DeliveryCompleted(Ball ball)
    {
        OnDeliveryCompleted?.Invoke(ball);
    }

    public void ShotSelected()
    {
        OnShotSelected?.Invoke();
    }

    public void ShotStarted(Ball ball)
    {
        OnShotStarted?.Invoke(ball);
    }

    public void ShotPlayed(int runs)
    {
        OnShotPlayed?.Invoke(runs);
    }

    public void GameOver(GameOverScenario scenario)
    {
        OnGameOver?.Invoke(scenario);
    }

    public void GameStarted()
    {
        OnGameStarted?.Invoke();
    }

    public void ScoreUpdate(CricketScore score)
    {
        OnScoreUpdate?.Invoke(score);
    }

    public void OverFinished(int overNum)
    {
        OnOverFinished?.Invoke(overNum);
    }

    public void ResetData()
    {
        GameStarted();
    }

    public int GetRunsAsInt(Runs m_Runs)
    {
        return m_Runs switch
        {
            Runs.Wicket => -2,
            Runs.None => -1,
            Runs.Zero => 0,
            Runs.One => 1,
            Runs.Two => 2,
            Runs.Four => 4,
            Runs.Six => 6,
            _ => -1,
        };
    }

    public float GetProbability(Runs m_Runs)
    {
        if (m_Runs.Equals(Runs.Wicket) || m_Runs.Equals(Runs.None))
        {
            return 0;
        }

        return batting.probabilty[(int)m_Runs];
    }

    public void SetBattingRuns(Runs runs)
    {
        batting.SetRuns(runs);
    }


    // Bowling

    public void SetBowlingSpeed(BowlSpeed speed)
    {
        bowling.SetBowlSpeed(speed);
    }

    public void SetBowlingLength(Transform temp)
    {
        bowling.SetBowlingLength(temp.position);
    }

    public void InitiateBowling()
    {
        bowling.InitiateBowling();
    }

    public bool IsBowlingSpeedSelected()
    {
        return bowling.ballSpeed != BowlSpeed.None;
    }
}
