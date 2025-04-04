using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
///注释
/// <summary>

public class GoToMapButton : MonoBehaviour
{
    private Button goToMapButton;

    private void Awake()
    {
        goToMapButton = GetComponent<Button>();

        goToMapButton.onClick.AddListener(LoadMap);
    }

    private void LoadMap()
    {
        SceneLoadManager.Instance.LoadMap();
    }
}
