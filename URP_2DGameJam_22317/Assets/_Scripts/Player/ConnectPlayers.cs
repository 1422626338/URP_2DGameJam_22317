using UnityEngine;

public class ConnectPlayers : MonoBehaviour
{
    public Transform player1;
    public Transform player2;
    public float maxDistance = 10f; // 最大距离限制
    public float fadeOutSpeed = 2f; // 渐隐速度
    public float fadeInSpeed = 2f; // 新增渐显速度
    public bool shouldShowLine = true; // 控制连线显示的布尔值
    public LayerMask layerMask; // 碰到这个图层的碰撞体，就消失

    private LineRenderer lineRenderer;
    private Color originalColor;
    private float alpha = 1f;

    void Start()
    {
        // 通过标签查找Player1并获取其Transform组件
        player1 = GameObject.FindWithTag("Player1").transform;
        // 通过标签查找Player2并获取其Transform组件
        player2 = GameObject.FindWithTag("Player2").transform;

        // 获取LineRenderer组件
        lineRenderer = GetComponent<LineRenderer>();
        // 设置LineRenderer的顶点数量为2
        lineRenderer.positionCount = 2;

        // 记录原始颜色
        originalColor = lineRenderer.startColor;
    }

    void Update()
    {
        if (player1 != null && player2 != null)
        {
            if (!shouldShowLine)
            {
                // 布尔值为 false，渐隐连线
                alpha = Mathf.Max(alpha - fadeOutSpeed * Time.deltaTime, 0f);
                UpdateLineColor();
                if (alpha <= 0f)
                {
                    lineRenderer.enabled = false;
                    //TODO:触发死亡机制
                }
            }
            else
            {
                // 计算两个玩家之间的距离
                float distance = Vector3.Distance(player1.position, player2.position);
                bool isObstructed = Physics2D.Linecast(player1.position, player2.position, layerMask);

                if (distance <= maxDistance && !isObstructed)
                {
                    // 距离在限制范围内且无障碍物，显示连线
                    lineRenderer.enabled = true;
                    //alpha = 1f; // 重置透明度
                    // 渐显逻辑：透明度逐渐增加
                    alpha = Mathf.Min(alpha + fadeInSpeed * Time.deltaTime, 1f);
                    UpdateLineColor();
                    // 设置LineRenderer的起始点为Player1的位置
                    lineRenderer.SetPosition(0, player1.position);
                    // 设置LineRenderer的结束点为Player2的位置
                    lineRenderer.SetPosition(1, player2.position);
                }
                else
                {
                    // 距离超过限制或有障碍物，渐隐连线
                    alpha = Mathf.Max(alpha - fadeOutSpeed * Time.deltaTime, 0f);
                    UpdateLineColor();
                    if (alpha <= 0f)
                    {
                        lineRenderer.enabled = false;
                        //TODO:触发死亡机制
                    }
                }
            }
        }
    }

    void UpdateLineColor()
    {
        Color newColor = new Color(originalColor.r, originalColor.g, originalColor.b, alpha);
        lineRenderer.startColor = newColor;
        lineRenderer.endColor = newColor;
    }
}