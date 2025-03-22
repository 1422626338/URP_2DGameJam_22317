using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformDetector : MonoBehaviour
{
    // 记录当前平台是否有碰撞体（玩家）进入
    public bool hasPlayer = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // 如果碰撞体标签为 "Player"，则标记为检测到
        if (collision.CompareTag("Player1") || collision.CompareTag("Player2"))
        {
            hasPlayer = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        // 玩家离开后，将标记置为 false
        if (collision.CompareTag("Player1") || collision.CompareTag("Player2"))
        {
            hasPlayer = false;
        }
    }
}

