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
    public ObjectEventSO autoLoadLevelEvent;
    private void OnEnable()
    {
        backToMenuButton.onClick.AddListener(OnBackToMenuButtonClicked);
        reChallengeButton.onClick.AddListener(OnReChallengeButtonClicked);
    }

    private void OnReChallengeButtonClicked()
    {
        autoLoadLevelEvent.RaiseEvent(LevelManager.Instance.levelList[LevelManager.Instance.levelPos].curScene, this);
    }

    private void OnBackToMenuButtonClicked()
    {
        backToMenuEvent.RaiseEvent(null, this);
    }
}
