using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
///注释
/// <summary>

public class BaseLevel : MonoBehaviour
{
    [SerializeField]
    public float timeCount = 100;
    public Vector2 P1SpawnPoint;
    public Vector2 P2SpawnPoint;

    protected bool isFail;

}
