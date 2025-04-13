using UnityEngine;

public class EnergyBeam2D : MonoBehaviour
{
    public float fadeInSpeed = 3f;
    public float fadeOutSpeed = 5f;
    public KeyCode player1Key = KeyCode.E;
    public KeyCode player2Key = KeyCode.Alpha2;
    public LayerMask enemyLayer; // 在Inspector中设置敌人所在的Layer

    private LineRenderer lineRenderer;
    private EdgeCollider2D beamCollider; // 新增2D碰撞体
    private Color originalColor;
    private float alpha = 0f;

    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.positionCount = 2;

        // 添加并配置2D碰撞体
        beamCollider = gameObject.AddComponent<EdgeCollider2D>();
        beamCollider.isTrigger = true;
        UpdateColliderPoints(); // 初始更新碰撞体位置

        originalColor = lineRenderer.startColor;
        UpdateLineVisibility(0f);
        beamCollider.enabled = false; // 初始禁用碰撞体
    }

    void Update()
    {
        bool bothKeysPressed = Input.GetKey(player1Key) && Input.GetKey(player2Key);

        if (bothKeysPressed)
        {
            // 渐显并激活碰撞体
            alpha = Mathf.Min(alpha + fadeInSpeed * Time.deltaTime, 1f);
            UpdateLineVisibility(alpha);
            UpdateLinePositions();
            beamCollider.enabled = true; // 激活碰撞检测
        }
        else
        {
            // 渐隐并禁用碰撞体
            alpha = Mathf.Max(alpha - fadeOutSpeed * Time.deltaTime, 0f);
            UpdateLineVisibility(alpha);
            if (alpha <= 0f) beamCollider.enabled = false;
        }
    }

    void UpdateLinePositions()
    {
        GameObject p1 = GameObject.FindWithTag("Player1");
        GameObject p2 = GameObject.FindWithTag("Player2");
        if (p1 && p2)
        {
            // 更新线渲染器
            lineRenderer.SetPosition(0, p1.transform.position);
            lineRenderer.SetPosition(1, p2.transform.position);

            // 同步更新碰撞体位置
            UpdateColliderPoints();
        }
    }

    void UpdateColliderPoints()
    {
        // 将LineRenderer的位置转换为2D碰撞点
        Vector2[] points = new Vector2[2];
        points[0] = lineRenderer.GetPosition(0);
        points[1] = lineRenderer.GetPosition(1);
        beamCollider.points = points;
    }

    void UpdateLineVisibility(float alphaValue)
    {
        if (alphaValue > 0f)
        {
            lineRenderer.enabled = true;
            Color newColor = new Color(originalColor.r, originalColor.g, originalColor.b, alphaValue);
            lineRenderer.startColor = newColor;
            lineRenderer.endColor = newColor;
        }
        else
        {
            lineRenderer.enabled = false;
        }
    }

    // 当敌人碰到能量线时触发
    void OnTriggerEnter2D(Collider2D other)
    {
        if (enemyLayer == (enemyLayer | (1 << other.gameObject.layer)))
        {
            if (other.CompareTag("Enemy"))
            {
                Destroy(other.gameObject);
                // TODO：播放消灭特效或声音
            }
        }
    }
}