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
    public Button reChallengeButton;
    public Button nextLevelButton;

    public ObjectEventSO backToMenuEvent;
    public ObjectEventSO autoLoadLevelEvent;

    private void OnEnable()
    {
        backToMenuButton.onClick.AddListener(OnBackToMenuButtonClicked);
        nextLevelButton.onClick.AddListener(OnNextLevelButtonClicked);
        reChallengeButton.onClick.AddListener(OnReChallengeButtonClicked);
    }

    private void OnReChallengeButtonClicked()
    {
        autoLoadLevelEvent.RaiseEvent(LevelManager.Instance.levelList[LevelManager.Instance.levelPos].curScene, this);
    }

    private void OnNextLevelButtonClicked()
    {
        LevelManager.Instance.UpdateLevelState(LevelManager.Instance.levelPos);
        autoLoadLevelEvent.RaiseEvent(LevelManager.Instance.levelList[LevelManager.Instance.levelPos].nextScene, this);
    }

    private void OnBackToMenuButtonClicked()
    {
        backToMenuEvent.RaiseEvent(null , this);
        self.SetActive(false);
    }
}
