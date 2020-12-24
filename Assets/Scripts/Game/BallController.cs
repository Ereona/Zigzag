using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallController : MonoBehaviour
{
    public Rigidbody Ball;
    public VoidEventChannelSO StartGameEvent;
    public VoidEventChannelSO TapEvent;
    public VoidEventChannelSO GameOverEvent;
    public BallSettingsSO Settings;

    private bool forward;
    private bool started;

    private void Start()
    {
        StartGameEvent.OnEventRaised += OnGameStarted;
        TapEvent.OnEventRaised += OnTap;
        GameOverEvent.OnEventRaised += OnGameOver;
    }

    private void OnDestroy()
    {
        StartGameEvent.OnEventRaised -= OnGameStarted;
        TapEvent.OnEventRaised -= OnTap;
        GameOverEvent.OnEventRaised -= OnGameOver;
    }

    private void OnGameStarted()
    {
        forward = true;
        started = true;
        Ball.isKinematic = false;
        Ball.transform.position = new Vector3(0, Settings.BallSize / 2, 0);
        Ball.transform.localScale = Vector3.one * Settings.BallSize;
        Ball.velocity = GetMovingDirection();
    }

    private void OnTap()
    {
        if (started)
        {
            forward = !forward;
            Ball.velocity = GetMovingDirection();
        }
    }

    private void FixedUpdate()
    {
        if (started)
        {
           RefreshVelocity();
        }
    }

    private void RefreshVelocity()
    {
        Ball.AddForce(GetMovingDirection(), ForceMode.Acceleration);
        float magnitude = Mathf.Max(Ball.velocity.x, Ball.velocity.z);
        if (magnitude > Settings.BallVelocity)
        {
            Ball.velocity = Ball.velocity / magnitude;
        }
    }

    private Vector3 GetMovingDirection()
    {
        Vector3 direction;
        if (forward)
        {
            direction = new Vector3(0, 0, 1);
        }
        else
        {
            direction = new Vector3(1, 0, 0);
        }
        return direction;
    }

    private void OnGameOver()
    {
        Ball.isKinematic = true;
        started = false;
    }
}
