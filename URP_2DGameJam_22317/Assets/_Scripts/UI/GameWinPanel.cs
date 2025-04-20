using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
///注释
/// <summary>

public class GameWinPanel : MonoBehaviour
{
    public GameObject self;

    public Button backToMenuButton;
    public Button ReChallengeButton;
    public Button nextLevelButton;

    public ObjectEventSO backToMenuEvent;

    private void OnEnable()
    {
        backToMenuButton.onClick.AddListener(OnBackToMenuButtonClicked);
    }

    private void OnBackToMenuButtonClicked()
    {
        backToMenuEvent.RaiseEvent(null , this);
        self.SetActive(false);
    }
}
