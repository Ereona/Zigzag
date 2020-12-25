using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransformFollowEnable : MonoBehaviour
{
    public VoidEventChannelSO GameStartedEvent;
    public VoidEventChannelSO GameOverEvent;
    public TransformFollow Follow;

    void Start()
    {
        GameStartedEvent.OnEventRaised += OnGameStarted;
        GameOverEvent.OnEventRaised += OnGameOver;
    }

    private void OnGameStarted()
    {
        Follow.enabled = true;
    }

    private void OnGameOver()
    {
        Follow.enabled = false;
    }
}
