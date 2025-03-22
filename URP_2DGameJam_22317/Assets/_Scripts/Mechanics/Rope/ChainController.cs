using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class ChainController : MonoBehaviour
{
    [Header("锁链对象（Transform）")]
    public Transform chain1;  // 锁链1
    public Transform chain2;  // 锁链2

    [Header("平台检测器")]
    public PlatformDetector detector1; // 挂在锁链1底部平台上的检测器
    public PlatformDetector detector2; // 挂在锁链2底部平台上的检测器

    [Header("运动参数")]
    public float moveSpeed = 2f; // 运动速度

    [Header("位置限制")]
    public float chain1MinY; // 锁链1最低位置（例如：-3）
    public float chain1MaxY; // 锁链1最高位置（例如：0）
    public float chain2MinY; // 锁链2最低位置（例如：0）
    public float chain2MaxY; // 锁链2最高位置（例如：3）

    [Header("位置")]
    public Transform chainAMinY;
    public Transform chainAMaxY;
    public Transform chainBMinY;
    public Transform chainBMaxY;

    void Update()
    {
        // 只有 detector1 检测到碰撞体而 detector2 没有检测到时：
        if (detector1.hasPlayer && !detector2.hasPlayer)
        {
            // 计算本帧移动距离
            float delta = moveSpeed * Time.deltaTime;

            // chain1 下降，但不低于最低位置
            float newChain1Y = Mathf.Max(chain1.position.y - delta, chainAMinY.position.y);
            // chain2 上升，但不高于最高位置
            float newChain2Y = Mathf.Min(chain2.position.y + delta, chainBMaxY.position.y);

            chain1.position = new Vector3(chain1.position.x, newChain1Y, chain1.position.z);
            chain2.position = new Vector3(chain2.position.x, newChain2Y, chain2.position.z);
        }
        // 只有 detector2 检测到碰撞体而 detector1 没有检测到时：
        else if (!detector1.hasPlayer && detector2.hasPlayer)
        {
            float delta = moveSpeed * Time.deltaTime;

            // chain2 下降，但不低于最低位置
            float newChain2Y = Mathf.Max(chain2.position.y - delta, chainBMinY.position.y);
            // chain1 上升，但不高于最高位置
            float newChain1Y = Mathf.Min(chain1.position.y + delta, chainAMaxY.position.y);

            chain2.position = new Vector3(chain2.position.x, newChain2Y, chain2.position.z);
            chain1.position = new Vector3(chain1.position.x, newChain1Y, chain1.position.z);
        }
        // 当两个平台都检测到或都没有检测到时，不改变位置
    }
}