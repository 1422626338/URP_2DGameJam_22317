using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.UI;

/// <summary>
///注释
/// <summary>

public class BaseLevel : MonoBehaviour
{
    // 在已有变量后添加
    [Header("时间UI")]
    public Slider slider;  // 将你的前景条Image拖拽到这里


    [SerializeField]
    public float timeCount = 100;
    public Transform P1SpawnPoint;
    public Transform P2SpawnPoint;
    // public Vector2 P1SpawnPoint;
    //public Vector2 P2SpawnPoint;
    public float ligthRadiu;
    public int curLevelPos;
    public bool isTimeOut = false;
    protected bool isFail;
    protected int panelCnt = 0;
    public float curTimeCount;

    [Header("广播")]
    public ObjectEventSO GameOverEvent;
     protected virtual void Update()
    {
        // 在原有代码后添加UI更新
        if (slider != null && timeCount > 0)
        {
            slider.value = curTimeCount / timeCount;
        }


        if (curTimeCount / timeCount > 0.7f)
        {
            LevelManager.Instance.p1.GetComponent<Light2D>().pointLightOuterRadius = ligthRadiu;
            LevelManager.Instance.p2.GetComponent<Light2D>().pointLightOuterRadius = ligthRadiu;
        }
        else if (curTimeCount / timeCount > 0)
        {
            LevelManager.Instance.p1.GetComponent<Light2D>().pointLightOuterRadius = curTimeCount / timeCount * ligthRadiu;
            LevelManager.Instance.p2.GetComponent<Light2D>().pointLightOuterRadius = curTimeCount / timeCount * ligthRadiu;
        }
    }
    public void OnGameWinEvent()
    {
        isTimeOut = true;
    }

}
