using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System;

public class BowlingDelivery : MonoBehaviour
{
    public Transform bowlingArea;

    public Transform battingArea;

    [SerializeField]
    private Ball ball;

    private Vector3 deliverLength;

    private bool simulating;

    public BowlSpeed ballSpeed { get; private set; } = BowlSpeed.None;

    private void Awake()
    {
        GameManager.OnDeliveryCompleted += OnDeliverFinished;
        GameManager.OnShotSelected += StartSimulation;
    }

    private void StartSimulation()
    {
        StartBowling();
    }

    private void OnDeliverFinished(Ball obj)
    {
        ballSpeed = BowlSpeed.None;
        simulating = false;
    }

    public void SetBowlingLength(Vector3 pos)
    {
        if (!ballSpeed.Equals(BowlSpeed.None))
        {
            deliverLength = new Vector3(pos.x, 0.25f, pos.z);

            InitiateBowling();
        }
    }

    public void InitiateBowling()
    {
        if (!simulating && !ballSpeed.Equals(BowlSpeed.None))
        {
            GameManager.Instance.DeliveryTypeSelected();
        }
    }

    private void StartBowling()
    {
        Ball temp = Instantiate(ball);

        temp.ballTransform.position = bowlingArea.position;

        simulating = true;

        StartCoroutine(Simulation(temp));
    }

    public void SetBowlSpeed(BowlSpeed speed)
    {
        ballSpeed = speed;
    }

    private IEnumerator Simulation(Ball ball)
    {
        yield return null;

        ball.SetPosition(deliverLength, ballSpeed, () =>
        {
            GameManager.Instance.DeliveryStarted(ball);

            ball.SetPosition(new Vector3(ball.ballTransform.position.x, 0.7f, 2), ballSpeed, () =>
            {
                GameManager.Instance.DeliveryCompleted(ball);
            });
        });
    }

}

public enum BowlSpeed
{
    None,
    Spin,
    Fast
}
