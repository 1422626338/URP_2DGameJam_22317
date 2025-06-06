using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
///注释
/// <summary>

public class GamingPanel : MonoBehaviour
{
    public Button backToMenuButton;
    public Button continueButton;
    public Button reChallengeButton;
    public Button settingButton;

    public GameObject settingPanel;
    [Header("广播")]
    public ObjectEventSO backToMenuEvent;
    public ObjectEventSO autoLoadLevelEvent;
    public ObjectEventSO isTimeOutEvent;

    private void OnEnable()
    {
        backToMenuButton.onClick.AddListener(OnBackToMenuButtonClicked);
        continueButton.onClick.AddListener(OnContinueButtonClicked);
        reChallengeButton.onClick.AddListener(OnReChallengeButtonClicked);
        settingButton.onClick.AddListener(OnSettingButtonClicked);
        settingPanel.SetActive(false);
    }

    private void OnSettingButtonClicked()
    {
        settingPanel.SetActive(true);
        isTimeOutEvent.RaiseEvent(true, this);
        LevelManager.Instance.p1.SetActive(false);
        LevelManager.Instance.p2.SetActive(false);
    }

    private void OnReChallengeButtonClicked()
    {
        settingPanel.SetActive(false);
        isTimeOutEvent.RaiseEvent(false, this);
        autoLoadLevelEvent.RaiseEvent(LevelManager.Instance.levelList[LevelManager.Instance.levelPos].curScene, this);
    }

    private void OnContinueButtonClicked()
    {
        settingPanel.SetActive(false);
        LevelManager.Instance.p1.SetActive(true);
        LevelManager.Instance.p2.SetActive(true);
        isTimeOutEvent.RaiseEvent(false, this);

    }

    private void OnBackToMenuButtonClicked()
    {
        backToMenuEvent.RaiseEvent(null, this);
    }
}
