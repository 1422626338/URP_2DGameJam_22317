using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.UI;


/// <summary>
///注释
/// <summary>

public class Map : MonoBehaviour
{
    public ObjectEventSO loadLevelEvent;
    public ObjectEventSO backToMenuEvent;

    public AssetReference level1_1;
    public AssetReference level1_2;
    public AssetReference level1_3;
    public AssetReference level1_4;
    public AssetReference level1_5;

    public Button backToMenuButton;
    public Button level1_1Button;
    public Button level1_2Button;
    public Button level1_3Button;
    public Button level1_4Button;
    public Button level1_5Button;

    private void OnEnable()
    {
        level1_1Button.onClick.AddListener(() => OnloadLevelEventButtonClicked(LevelManager.Instance.levelList[0]));
        level1_2Button.onClick.AddListener(() => OnloadLevelEventButtonClicked(LevelManager.Instance.levelList[1]));
        level1_3Button.onClick.AddListener(() => OnloadLevelEventButtonClicked(LevelManager.Instance.levelList[2]));
        level1_4Button.onClick.AddListener(() => OnloadLevelEventButtonClicked(LevelManager.Instance.levelList[3]));
        level1_5Button.onClick.AddListener(() => OnloadLevelEventButtonClicked(LevelManager.Instance.levelList[4]));

        backToMenuButton.onClick.AddListener(OnBackToMenuButtonClicked);
    }

    private void OnBackToMenuButtonClicked()
    {
        backToMenuEvent.RaiseEvent(null, this);
    }

    private void OnloadLevelEventButtonClicked(level levelScene)
    {
        Debug.Log("test1");
        loadLevelEvent.RaiseEvent(levelScene, this);
    }
}
