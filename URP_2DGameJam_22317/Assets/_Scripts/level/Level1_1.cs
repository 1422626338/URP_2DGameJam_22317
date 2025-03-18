using System.Collections;
using System.Collections.Generic;
using UnityEngine;
 
 /// <summary>
 ///注释
 /// <summary>

public class Level1_1 : BaseLevel
{

    private void Awake()
    {
        P1SpawnPoint = new Vector2 (1, 0);
        P2SpawnPoint = new Vector2 (2, 0);
    }
    private void OnEnable()
    {
        timeCount = 100;
        isFail = false;
        LevelManager.Instance.SetPlayerPoint(P1SpawnPoint , P2SpawnPoint);
        LevelManager.Instance.p1.SetActive(true);
        LevelManager.Instance.p2.SetActive(true);
    }

    private void Update()
    {
        while (timeCount > 0)
        {
            timeCount -= Time.deltaTime;
        }

        if (timeCount < 0)
        {
            isFail = true;
            //TODO 跳出失败面板
        }
    }
}
