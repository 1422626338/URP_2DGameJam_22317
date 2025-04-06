using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
///注释
/// <summary>

public class Menu : MonoBehaviour
{
    public ObjectEventSO NewGameEvent;
    public ObjectEventSO LoadMapEvent;

    public Button goToMapButton;
    public Button newGameButton;
    private void OnEnable()
    {
        goToMapButton.onClick.AddListener(OnGoToMapButtonClieked);
        newGameButton.onClick.AddListener(OnNewGameButtonClieked);
    }

    private void OnNewGameButtonClieked()
    {
        NewGameEvent.RaiseEvent(null, this);
    }

    private void OnGoToMapButtonClieked()
    {
        LoadMapEvent.RaiseEvent(null, this);
    }
}
