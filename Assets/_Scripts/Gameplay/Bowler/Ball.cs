using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System;

public class Ball : MonoBehaviour
{
    public Transform ballTransform { get; private set; }

    private bool IsSpin;

    public Vector3 DestinationPos { get; private set; }

    public float BallSpeed
    {
        get
        {
            return IsSpin ? 1.0f : 0.45f;
        }
    }

    private void Awake()
    {
        ballTransform = this.GetComponent<Transform>();
    }

    public void SetPosition(Vector3 pos, BowlSpeed speed, Action callback, Ease ease = Ease.Linear)
    {
        IsSpin = speed.Equals(BowlSpeed.Spin);

        DestinationPos = pos;

        ballTransform.DOMove(pos, BallSpeed).SetEase(ease).OnComplete(() =>
        {
            callback?.Invoke();
        });
    }

    public void SetShotTrajectory(Vector3 pos, Runs t_Runs, Action<Ball> startCallback, Action<Runs,Ball> endCallback)
    {
        DestinationPos = pos;

        float distance = (ballTransform.position - DestinationPos).magnitude;
        float timeTaken = distance / 10f;

        if (t_Runs.Equals(Runs.Six))
        {
            ballTransform.DOJump(DestinationPos, 15, 1, timeTaken).OnStart(() =>
            {
                startCallback?.Invoke(this);
            }).OnComplete(() =>
            {
                endCallback?.Invoke(t_Runs, this);
            });
        }
        else
        {
            ballTransform.DOMove(pos, timeTaken).OnStart(() =>
            {
                startCallback?.Invoke(this);

            }).OnComplete(() =>
            {
                endCallback?.Invoke(t_Runs, this);
            });
        }
    }
}
