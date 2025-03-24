using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

/// <summary>
///注释
/// <summary>

public class Level1_1 : BaseLevel
{

    private void Awake()
    {
        
    }
    private void OnEnable()
    {
        curTimeCount = timeCount;
        isFail = false;
        LevelManager.Instance.SetPlayerPoint(P1SpawnPoint , P2SpawnPoint);
        LevelManager.Instance.p1.SetActive(true);
        LevelManager.Instance.p2.SetActive(true);
    }

     protected override void Update()
    {
        base.Update();
        if (curTimeCount > 0)
        {
            curTimeCount -= Time.deltaTime;
        }

        if (curTimeCount < 0)
        {
            isFail = true;
            Debug.Log("游戏失败");
            //TODO 跳出失败面板
        }
    }
   
}
