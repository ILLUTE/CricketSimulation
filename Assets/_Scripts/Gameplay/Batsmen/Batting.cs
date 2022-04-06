using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Batting : MonoBehaviour
{
    public Transform[] offSideTransform;
    public Transform[] onSideTransform;

    private Runs runsToScore;

    public Transform batTransform;

    public float[] probabilty = new float[] { 1.0f, 0.9f, 0.85f, 0.6f, 0.35f, 0.2f };

    private void Awake()
    {
        GameManager.OnDeliveryStarted += ShowBatting;
        GameManager.OnDeliveryCompleted += PlayAShot;
    }

    public void SetRuns(Runs run)
    {
        float x = UnityEngine.Random.Range(0.0f, 1.0f);

        if (x <= probabilty[(int)run])
        {
            runsToScore = run;
        }
        else
        {
            runsToScore = x > 0.8f ? Runs.Wicket : Runs.None; // 20% Chance of a wicket if shot not registered.
        }

        GameManager.Instance.ShotSelected();
    }

   

   

    private void PlayAShot(Ball ball)
    {
        Runs m_TempSpot = runsToScore;

        if ((int)m_TempSpot < 0)
        {
            m_TempSpot = Runs.None;
        }

        Transform shotPosition = (int)ball.ballTransform.position.x switch
        {
            1 => onSideTransform[(int)m_TempSpot],
            -1 => offSideTransform[(int)m_TempSpot],
            _ => ball.transform,
        };

        ball.SetShotTrajectory(shotPosition.position, m_TempSpot, OnShotStartedRoutine, OnShortEndedRoutine);
    }

    private void OnShotStartedRoutine(Ball ball)
    {
        GameManager.Instance.ShotStarted(ball);
    }

    private void OnShortEndedRoutine(Runs runs, Ball ball)
    {
        GameManager.Instance.ShotPlayed(GameManager.Instance.GetRunsAsInt(runsToScore));
        ResetBat();
        Destroy(ball.gameObject);
    }

    private void ShowBatting(Ball ball)
    {
        batTransform.DOMoveX(ball.DestinationPos.x, 0.1f);
    }

    private void ResetBat(Action callback = null)
    {
        batTransform.DOMoveX(0, 0.2f).OnComplete(()=>
        {
            callback?.Invoke();
        });
    }
}

public enum ShotSide
{
    None,
    Missed,
    Straight,
    Offside,
    Onside
}

public enum Runs
{
    Wicket = -1,
    None = 0,
    Zero = 1,
    One = 2,
    Two = 3,
    Four = 4,
    Six = 5
}
