using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Batting : MonoBehaviour
{
    private static Batting instance;

    public static Batting Instance
    {
        get
        {
            if(instance == null)
            {
                instance = FindObjectOfType<Batting>();
            }

            return instance;
        }
    }
    public Transform[] offSideTransform;
    public Transform[] onSideTransform;

    private Runs runsToScore;

    public Transform batTransform;

    private float[] probabilty = new float[] { 1.0f, 0.9f, 0.85f, 0.6f, 0.35f, 0.2f };

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
            runsToScore = x > 0.5f ? Runs.Wicket : Runs.None;
        }

        GameManager.Instance.OnShotSelectedMethod();
    }

    public float GetProbability(Runs m_Runs)
    {
        if (m_Runs.Equals(Runs.Wicket) || m_Runs.Equals(Runs.None))
        {
            return 0;
        }

        return probabilty[(int)m_Runs];
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
        GameManager.Instance.OnShotStartedMethod(ball);
    }

    private void OnShortEndedRoutine(Runs runs, Ball ball)
    {
        GameManager.Instance.OnShotPlayedMethod(GetRunsAsInt(runsToScore));
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
