using UnityEngine;

/// <summary>
/// TODO:添加美术资源后，出现上方锁链不移动，需要修改其图片为循环
/// </summary>

public class ChainController : MonoBehaviour
{
    [Header("锁链对象（Transform）")]
    public Transform chain1;  // 锁链1
    public Transform chain2;  // 锁链2

    [Header("平台检测器")]
    public PlatformDetector detector1; // 挂在链条1平台上的检测器
    public PlatformDetector detector2; // 挂在链条2平台上的检测器

    [Header("运动参数")]
    public float moveSpeed = 2f; // 运动速度

    [Header("边界参考点")]
    public Transform chain1UpperBoundary; // 链条1允许的上边界
    public Transform chain1LowerBoundary; // 链条1允许的下边界
    public Transform chain2UpperBoundary; // 链条2允许的上边界
    public Transform chain2LowerBoundary; // 链条2允许的下边界

    // 自动计算的平衡目标（不需要在 Inspector 中设置）
    private float chain1BalancedY;
    private float chain2BalancedY;

    void Start()
    {
        // 自动计算平衡目标位置
        chain1BalancedY = (chain1UpperBoundary.position.y + chain1LowerBoundary.position.y) / 2f;
        chain2BalancedY = (chain2UpperBoundary.position.y + chain2LowerBoundary.position.y) / 2f;
    }

    void Update()
    {
        float delta = moveSpeed * Time.deltaTime;

        // 情况1：只有 detector1 检测到角色（让 chain1 向下、chain2 向上）
        if (detector1.hasPlayer && !detector2.hasPlayer)
        {
            // 检查运动前是否未超出边界
            if (chain1.position.y > chain1LowerBoundary.position.y && chain2.position.y < chain2UpperBoundary.position.y)
            {
                float newChain1Y = Mathf.Max(chain1.position.y - delta, chain1LowerBoundary.position.y);
                float newChain2Y = Mathf.Min(chain2.position.y + delta, chain2UpperBoundary.position.y);
                chain1.position = new Vector3(chain1.position.x, newChain1Y, chain1.position.z);
                chain2.position = new Vector3(chain2.position.x, newChain2Y, chain2.position.z);
            }
        }
        // 情况2：只有 detector2 检测到角色（让 chain2 向下、chain1 向上）
        else if (!detector1.hasPlayer && detector2.hasPlayer)
        {
            if (chain2.position.y > chain2LowerBoundary.position.y && chain1.position.y < chain1UpperBoundary.position.y)
            {
                float newChain2Y = Mathf.Max(chain2.position.y - delta, chain2LowerBoundary.position.y);
                float newChain1Y = Mathf.Min(chain1.position.y + delta, chain1UpperBoundary.position.y);
                chain2.position = new Vector3(chain2.position.x, newChain2Y, chain2.position.z);
                chain1.position = new Vector3(chain1.position.x, newChain1Y, chain1.position.z);
            }
        }
        // 情况3：两个平台都有角色时，向平衡目标位置运动
        else if (detector1.hasPlayer && detector2.hasPlayer)
        {
            // 调整 chain1 朝向 chain1BalancedY 运动
            if (Mathf.Abs(chain1.position.y - chain1BalancedY) > 0.01f)
            {
                if (chain1.position.y > chain1BalancedY)
                    chain1.position = new Vector3(chain1.position.x, Mathf.Max(chain1.position.y - delta, chain1BalancedY), chain1.position.z);
                else
                    chain1.position = new Vector3(chain1.position.x, Mathf.Min(chain1.position.y + delta, chain1BalancedY), chain1.position.z);
            }
            // 调整 chain2 朝向 chain2BalancedY 运动
            if (Mathf.Abs(chain2.position.y - chain2BalancedY) > 0.01f)
            {
                if (chain2.position.y > chain2BalancedY)
                    chain2.position = new Vector3(chain2.position.x, Mathf.Max(chain2.position.y - delta, chain2BalancedY), chain2.position.z);
                else
                    chain2.position = new Vector3(chain2.position.x, Mathf.Min(chain2.position.y + delta, chain2BalancedY), chain2.position.z);
            }
        }
        // 情况4：两个平台都没有角色时，保持当前状态
    }
}
