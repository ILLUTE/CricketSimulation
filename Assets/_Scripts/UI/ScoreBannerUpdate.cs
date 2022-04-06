using System;
using UnityEngine;

public class ScoreBannerUpdate : MonoBehaviour
{
    private CricketScore m_CricketScore;

    public TargetScore m_Target;

    private void Awake()
    {
        GameManager.OnShotPlayed += OnShotPlayed;
        GameManager.OnGameStarted += ResetScore;
    }

    private void Start()
    {
        ResetScore();
    }
    private void ResetScore()
    {
        m_CricketScore = new CricketScore();
        m_CricketScore.m_TargetScore = m_Target;
        GameManager.Instance.ScoreUpdate(m_CricketScore);
    }

    private void OnShotPlayed(int runs)
    {
        m_CricketScore.totalballsBowled++;

        if (runs == -2)
        {
            m_CricketScore.totalWicketsOut++;
        }
        else if (runs > 0)
        {
            m_CricketScore.totalRuns += runs;
        }

        GameManager.Instance.ScoreUpdate(m_CricketScore);

        if (CheckIfOverFinished())
        {
            GameManager.Instance.OverFinished(m_CricketScore.totalballsBowled / 6); // Can use it to tell if an over is done.
        }

        if (CheckGameOver())
        {
            GameManager.Instance.GameOver(GetScenario());
        }
    }

    private bool CheckIfOverFinished()
    {
        if (m_CricketScore.totalballsBowled > 0)
        {
            return m_CricketScore.totalballsBowled % 6 == 0;
        }

        return false;
    }

    private bool CheckGameOver()
    {
        if (m_CricketScore.totalRuns >= m_Target.ChasingTotal || m_CricketScore.totalWicketsOut >= m_Target.maxWickets || m_CricketScore.totalballsBowled >= m_Target.maxBalls)
        {
            return true;
        }
        return false;
    }

    private GameOverScenario GetScenario()
    {
        if (m_CricketScore.totalRuns >= m_Target.ChasingTotal)
        {
            return GameOverScenario.TargetChased;
        }
        else if ((m_CricketScore.totalRuns == m_Target.ChasingTotal - 1) && ((m_CricketScore.totalballsBowled >= m_Target.maxBalls) || m_CricketScore.totalWicketsOut >= m_Target.maxWickets))
        {
            return GameOverScenario.MatchTied;
        }
        else if (m_CricketScore.totalWicketsOut >= m_Target.maxWickets)
        {
            return GameOverScenario.AllOut;
        }
        else if (m_CricketScore.totalballsBowled >= m_Target.maxBalls)
        {
            return GameOverScenario.OversFinished;
        }

        return GameOverScenario.None;
    }
}

[Serializable]
public class CricketScore
{
    public int totalRuns;
    public int totalballsBowled;
    public int totalWicketsOut;

    public TargetScore m_TargetScore;
}

[Serializable]
public class TargetScore
{
    public int maxBalls = 30;
    public int ChasingTotal = 60;
    public int maxWickets = 5;
}

public enum GameOverScenario
{
    None,
    AllOut,
    OversFinished,
    TargetChased,
    MatchTied
}
