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
    public ObjectEventSO StaffEvent;

    public Button goToMapButton;
    public Button newGameButton;
    public Button staffButton;
    private void OnEnable()
    {
        goToMapButton.onClick.AddListener(OnGoToMapButtonClieked);
        newGameButton.onClick.AddListener(OnNewGameButtonClieked);
        staffButton.onClick.AddListener(OnStaffButtonClickeded);
    }

    private void OnStaffButtonClickeded()
    {
        StaffEvent.RaiseEvent(null , this);
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
