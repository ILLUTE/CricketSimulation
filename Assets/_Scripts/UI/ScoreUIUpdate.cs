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

        int balls = score.totalballsBowled;

        m_Overs.text = string.Format("Overs {0}.{1}", balls / 6, balls % 6);
    }
}
