using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoresCounter : MonoBehaviour
{
    public VoidEventChannelSO GameStartedEvent;
    public VoidEventChannelSO CrystalCollectedEvent;
    public IntEventChannelSO ScoreChangedEvent;
    private int Score;

    void Start()
    {
        GameStartedEvent.OnEventRaised += OnGameStarted;
        CrystalCollectedEvent.OnEventRaised += OnCrystalCollected;
    }

    private void OnDestroy()
    {
        GameStartedEvent.OnEventRaised -= OnGameStarted;
        CrystalCollectedEvent.OnEventRaised -= OnCrystalCollected;
    }

    private void OnGameStarted()
    {
        Score = 0;
        ScoreChangedEvent.RaiseEvent(Score);
    }

    private void OnCrystalCollected()
    {
        Score++;
        ScoreChangedEvent.RaiseEvent(Score);
    }
}
