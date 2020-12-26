using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallController : MonoBehaviour
{
    public BallObject Ball;
    public VoidEventChannelSO StartGameEvent;
    public VoidEventChannelSO TapEvent;
    public VoidEventChannelSO GameOverEvent;
    public BallSettingsSO Settings;

    private Direction direction;
    private bool started;

    private void Start()
    {
        StartGameEvent.OnEventRaised += OnGameStarted;
        TapEvent.OnEventRaised += OnTap;
        GameOverEvent.OnEventRaised += OnGameOver;
        Ball.transform.position = new Vector3(0, Settings.BallSize / 2, 0);
        Ball.transform.localScale = Vector3.one * Settings.BallSize;
    }

    private void OnDestroy()
    {
        StartGameEvent.OnEventRaised -= OnGameStarted;
        TapEvent.OnEventRaised -= OnTap;
        GameOverEvent.OnEventRaised -= OnGameOver;
    }

    private void OnGameStarted()
    {
        direction = Direction.Forward;
        Ball.index = -1;
        started = true;
        Ball.Rb.isKinematic = false;
        Ball.transform.position = new Vector3(0, Settings.BallSize / 2, 0);
        Ball.transform.localScale = Vector3.one * Settings.BallSize;
        Ball.Rb.velocity = GetMovingDirection();
    }

    private void OnTap()
    {
        if (started)
        {
            direction = direction == Direction.Forward ? Direction.Right : Direction.Forward;
            Ball.Rb.velocity = GetMovingDirection();
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
        Ball.Rb.AddForce(GetMovingDirection(), ForceMode.Acceleration);
        float magnitude = Mathf.Max(Ball.Rb.velocity.x, Ball.Rb.velocity.z);
        if (magnitude > Settings.BallVelocity)
        {
            Ball.Rb.velocity = Ball.Rb.velocity / magnitude * Settings.BallVelocity;
        }
    }

    private Vector3 GetMovingDirection()
    {
        switch (direction)
        {
            case Direction.Forward:
                return new Vector3(0, 0, 1) * Settings.BallVelocity;
            case Direction.Right:
             return new Vector3(1, 0, 0) * Settings.BallVelocity;
        }
        throw new System.NotImplementedException("Unknown direction");
    }

    private void OnGameOver()
    {
        started = false;
    }
}
