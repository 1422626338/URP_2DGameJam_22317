using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
///注释
/// <summary>

public class UnlockPanel : MonoBehaviour
{
    public Button closeButton;
    public GameObject closePanel;

    private void OnEnable()
    {
        closeButton.onClick.AddListener(OnCloseButtonClicked);
    }

    private void OnCloseButtonClicked()
    {
        closePanel.SetActive(false);
    }
}
