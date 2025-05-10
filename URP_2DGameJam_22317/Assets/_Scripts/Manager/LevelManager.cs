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
    public AssetReference level1_1;
    public AssetReference level1_2;
    public AssetReference level1_3;
    public AssetReference level1_4;
    public AssetReference level1_5;
    public AssetReference menu;


    public GameObject p1;
    public GameObject p2;
    public int levelPos = 0;
    public List<level> levelList = new List<level>();

    protected override void Awake()
    {
        base.Awake();
        levelList.Add(new level
        {
            curScene = level1_1,
            nextScene = level1_2,
            levelState = LevelState.unlock
        }) ; 
        levelList.Add(new level
        {
            curScene = level1_2,
            nextScene = level1_3,
            levelState = LevelState.locked
        }) ;
        levelList.Add(new level
        {
            curScene = level1_3,
            nextScene = level1_4,
            levelState = LevelState.locked
        }) ;
        levelList.Add(new level
        {
            curScene = level1_4,
            nextScene = level1_5,
            levelState = LevelState.locked
        }) ;
        levelList.Add(new level
        {
            curScene = level1_5,
            nextScene = menu,
            levelState = LevelState.locked
        }) ;

    }
    public void SetPlayerPoint(Vector2 p1Point , Vector2 p2Point)
    {
        p1.transform.position = p1Point;
        p2.transform.position = p2Point;
    }

    public void UpdateLevelState(int i)
    {
        int n = levelList.Count;
        levelList[i].levelState = LevelState.complete;
        if (i < n - 1)
        {
            levelList[i + 1].levelState = LevelState.unlock;
        }
    }
}
