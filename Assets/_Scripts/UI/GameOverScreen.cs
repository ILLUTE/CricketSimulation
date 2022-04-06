using System;
using System.Text;
using TMPro;
using UnityEngine;
using DG.Tweening;
using UnityEngine.SceneManagement;

public class GameOverScreen : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI m_GameOverTxt;
    [SerializeField]
    private CanvasGroup m_CanvasGroup;

    private void Awake()
    {
        GameManager.OnGameStarted += OnGameStarted;
        GameManager.OnGameOver += OnGameOver;
    }

    private void OnGameStarted()
    {
        m_CanvasGroup.interactable = false;
        m_CanvasGroup.blocksRaycasts = false;
        m_CanvasGroup.DOFade(0, 0.5f);
    }

    private void OnGameOver(GameOverScenario scenario)
    {
        m_CanvasGroup.interactable = true;
        m_CanvasGroup.blocksRaycasts = true;
        m_GameOverTxt.text = EnumCapitalCaseToString(scenario.ToString());
        m_CanvasGroup.DOFade(1, 0.5f);
    }

    public void ResetBtn()
    {
        GameManager.Instance.ResetData();
    }
    private string EnumCapitalCaseToString(string text)
    {
        if (string.IsNullOrWhiteSpace(text))
            return "";
        StringBuilder newText = new StringBuilder(text.Length * 2);
        newText.Append(text[0]);
        for (int i = 1; i < text.Length; i++)
        {
            if (char.IsUpper(text[i]) && text[i - 1] != ' ')
                newText.Append(' ');
            newText.Append(text[i]);
        }
        return newText.ToString();
    }
}
