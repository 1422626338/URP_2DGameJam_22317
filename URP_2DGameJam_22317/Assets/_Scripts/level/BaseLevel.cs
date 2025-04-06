using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

/// <summary>
///注释
/// <summary>

public class BaseLevel : MonoBehaviour
{
    [SerializeField]
    public float timeCount = 100;
    public Transform P1SpawnPoint;
    public Transform P2SpawnPoint;
    // public Vector2 P1SpawnPoint;
    //public Vector2 P2SpawnPoint;
    public float ligthRadiu;

    protected bool isFail;
    public float curTimeCount;

     protected virtual void Update()
    {
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

}
