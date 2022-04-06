using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;
using System;

public class VisualUIUpdate : MonoBehaviour
{
    public TextMeshProUGUI m_runsText;

    private RectTransform m_RectTransform;

    private void Awake()
    {
        m_RectTransform = this.GetComponent<RectTransform>();

        GameManager.OnShotPlayed += UpdateText;
    }
    private void Start()
    {
        m_RectTransform.localScale = Vector3.zero;
    }

    private void UpdateText(int runs)
    {
        if(runs == -2)
        {
            m_runsText.text = "Wicket";
        }
        else if(runs == -1)
        {
            m_runsText.text = "Missed";
        }
        else
        {
            m_runsText.text = runs.ToString();
        }

        m_RectTransform.DOScale(1.5f, 1f).OnComplete(() => {

            m_RectTransform.localScale = Vector3.zero;
        });
    }
}
