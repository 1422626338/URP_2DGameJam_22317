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
            //HACK:更改父物体，防止出现抖动
            collision.transform.position = this.transform.position;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        // 玩家离开后，将标记置为 false
        if (collision.CompareTag("Player1") || collision.CompareTag("Player2"))
        {
            hasPlayer = false;
            //TODO:退出父物体
            if (collision.transform.parent != null)
            {
                collision.transform.parent = null;  //这里报错不太敏感，先不做处理
            }
        }
    }
    //HACK:
    #region 如果角色站在锁链平台停止运行游戏，发生错误时，使用
    //private IEnumerator DetachPlayerAfterDelay(Transform playerTransform)
    //{
    //    yield return null;  // 等待一帧
    //    if (playerTransform != null)
    //    {
    //        playerTransform.parent = null;
    //    }
    //}

    //private void OnTriggerExit2D(Collider2D collision)
    //{
    //    if (collision.CompareTag("Player1") || collision.CompareTag("Player2"))
    //    {
    //        hasPlayer = false;
    //        StartCoroutine(DetachPlayerAfterDelay(collision.transform));
    //    }
    //}
    #endregion
}

