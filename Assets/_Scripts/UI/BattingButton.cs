using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BattingButton : MonoBehaviour
{
    public Runs m_Runs;

    [SerializeField]
    private TextMeshProUGUI battingRuns;

    [SerializeField]
    private TextMeshProUGUI battingProbability;

    private BattingUI m_battingUI;

    public void OnSetButton(BattingUI battingUI)
    {
        m_battingUI = battingUI;
        battingRuns.text = Batting.Instance.GetRunsAsInt(m_Runs).ToString() ;
        battingProbability.text = string.Format("{0}%", Batting.Instance.GetProbability(m_Runs) * 100);
    }

    public void OnSelected()
    {
        if (m_battingUI == null)
        {
            return;
        }
        m_battingUI.OnRunsSelected(m_Runs);
    }
}
