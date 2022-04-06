using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreUIUpdate : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI m_TotalScore;
    [SerializeField]
    private TextMeshProUGUI m_Overs;

    private void Awake()
    {
        GameManager.OnScoreUpdate += OnScoreUpdate;
    }

    private void OnScoreUpdate(CricketScore score)
    {
        m_TotalScore.text = string.Format("{0}/{1}", score.totalRuns, score.totalWicketsOut);

        int balls = score.m_TargetScore.maxBalls - score.totalballsBowled;
        int scoreLeft = score.m_TargetScore.ChasingTotal - score.totalRuns;
        m_Overs.text = string.Format("Need {2} runs {0}.{1} overs", balls / 6, balls % 6, scoreLeft);
    }
}
