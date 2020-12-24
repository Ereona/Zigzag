using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartGameUI : MonoBehaviour
{
    public Button StartGameButton;
    public VoidEventChannelSO StartGameEvent;
    public VoidEventChannelSO GameOverEvent;
    public Text DefeatText;

    private void Start()
    {
        DefeatText.gameObject.SetActive(false);
        StartGameButton.onClick.AddListener(StartGame);
        GameOverEvent.OnEventRaised += OnGameOver;
    }

    private void OnDestroy()
    {
        GameOverEvent.OnEventRaised -= OnGameOver;
    }

    private void StartGame()
    {
        StartGameEvent.RaiseEvent();
        StartGameButton.gameObject.SetActive(false);
    }

    private void OnGameOver()
    {
        DefeatText.gameObject.SetActive(true);
        StartGameButton.gameObject.SetActive(true);
    }
}
