using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoresUI : MonoBehaviour
{
    public Text ScoresText;
    public IntEventChannelSO ScoresChangedEvent;

    void Start()
    {
        ScoresChangedEvent.OnEventRaised += OnScoresChanged;
    }

    private void OnDestroy()
    {
        ScoresChangedEvent.OnEventRaised -= OnScoresChanged;
    }

    private void OnScoresChanged(int value)
    {
        ScoresText.text = value.ToString();
    }
}
