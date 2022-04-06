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

    public BowlSpeed bowlSpeed { get; private set; } = BowlSpeed.None;

    private static BowlingDelivery instance;

    public static BowlingDelivery Instance
    {
        get
        {
            if(instance == null)
            {
                instance = FindObjectOfType<BowlingDelivery>();
            }

            return instance;
        }
    }

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
        bowlSpeed = BowlSpeed.None;
        simulating = false;
    }

    public void SetBowlingLength(Vector3 pos)
    {
        if (!bowlSpeed.Equals(BowlSpeed.None))
        {
            deliverLength = new Vector3(pos.x, 0.25f, pos.z);

            InitiateBowling();
        }
    }

    public void InitiateBowling()
    {
        if (!simulating && !bowlSpeed.Equals(BowlSpeed.None))
        {
            GameManager.Instance.OnDeliveryTypeSelectedMethod();
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
        bowlSpeed = speed;
    }

    private IEnumerator Simulation(Ball ball)
    {
        yield return null;

        ball.SetPosition(deliverLength, bowlSpeed, () =>
        {
            GameManager.Instance.OnDeliveryStartedMethod(ball);

            ball.SetPosition(new Vector3(ball.ballTransform.position.x, 0.7f, 2), bowlSpeed, () =>
            {
                GameManager.Instance.OnDeliveryCompletedMethod(ball);
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
