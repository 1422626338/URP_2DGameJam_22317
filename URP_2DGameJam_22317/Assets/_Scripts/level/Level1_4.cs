using System.Collections;
using System.Collections.Generic;
using UnityEngine;
 
 /// <summary>
 ///注释
 /// <summary>

public class Level1_4 : BaseLevel
{
    private void Awake()
    {

    }
    private void OnEnable()
    {
        curTimeCount = timeCount;
        panelCnt = 0;
        isFail = false;
        LevelManager.Instance.SetPlayerPoint(P1SpawnPoint.position, P2SpawnPoint.position);
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

        }

        if (isFail && panelCnt == 0)
        {
            panelCnt = 1;
            Debug.Log("游戏失败");
            GameOverEvent.RaiseEvent(null, this);
        }
    }
}
