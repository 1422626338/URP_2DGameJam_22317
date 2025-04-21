using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
///注释
/// <summary>

public class GameOverPanel : MonoBehaviour
{
    [SerializeField]
    private Button reChallengeButton , backToMenuButton;

    [Header("广播")]
    public ObjectEventSO backToMenuEvent;
    private void OnEnable()
    {
        backToMenuButton.onClick.AddListener(OnBackToMenuButtonClicked);
    }

    private void OnBackToMenuButtonClicked()
    {
        backToMenuEvent.RaiseEvent(null, this);
    }
}
