using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;

/// <summary>
///注释
/// <summary>

[System.Serializable]
public class level
{
    public AssetReference curScene;
    public AssetReference nextScene;
    public LevelState levelState;
}
public class LevelManager : SingletonMono<LevelManager>
{
    public GameObject p1;
    public GameObject p2;

    public List<level> levelList = new List<level>();
    
    public void SetPlayerPoint(Vector2 p1Point , Vector2 p2Point)
    {
        p1.transform.position = p1Point;
        p2.transform.position = p2Point;
    }

    public void UpdateLevelState(int i)
    {
        int n = levelList.Count;
        levelList[i].levelState = LevelState.complete;
        if(i < n) levelList[i + 1].levelState = LevelState.unlock;
    }
}
