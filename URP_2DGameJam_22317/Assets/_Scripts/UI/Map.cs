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

    public AssetReference level1_1;
    public AssetReference level1_2;
    public AssetReference level1_3;

    public Button level1_1Button;
    public Button level1_2Button;
    public Button level1_3Button;

    private void OnEnable()
    {
        level1_1Button.onClick.AddListener(() => OnloadLevelEventButtonClicked(level1_1));
        level1_2Button.onClick.AddListener(() => OnloadLevelEventButtonClicked(level1_2));
        level1_3Button.onClick.AddListener(() => OnloadLevelEventButtonClicked(level1_3));
    }

    private void OnloadLevelEventButtonClicked(AssetReference levelScene)
    {
        Debug.Log("test1");
        loadLevelEvent.RaiseEvent(levelScene, this);
    }
}
