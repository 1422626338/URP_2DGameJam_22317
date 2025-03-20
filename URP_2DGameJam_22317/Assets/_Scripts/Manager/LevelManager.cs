using System.Collections;
using System.Collections.Generic;
using UnityEngine;
 
 /// <summary>
 ///注释
 /// <summary>

public class LevelManager : SingletonMono<LevelManager>
{
    public GameObject p1;
    public GameObject p2;
    
    public void SetPlayerPoint(Vector2 p1Point , Vector2 p2Point)
    {
        p1.transform.position = p1Point;
        p2.transform.position = p2Point;
    }
}
