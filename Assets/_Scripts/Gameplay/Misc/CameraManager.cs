using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using System;

public class CameraManager : MonoBehaviour
{
    public CinemachineVirtualCamera PitchCamera;
    public CinemachineVirtualCamera ShotTracker;

    private void Awake()
    {
        GameManager.OnDeliveryStarted += OnDeliveryStarted;
        GameManager.OnShotPlayed += OnShotPlayed;
    }

    private void OnShotPlayed(int obj)
    {
        // Switch Camera
        PitchCamera.Priority = 1;
        ShotTracker.Priority = 0;
        ShotTracker.m_Follow = null;
    }

    private void OnDeliveryStarted(Ball ball)
    {
        ShotTracker.m_Follow = ball.ballTransform;
        ShotTracker.m_LookAt = ball.ballTransform;
        PitchCamera.Priority = 0;
        ShotTracker.Priority = 1;
        // Switch Camera
    }
}
