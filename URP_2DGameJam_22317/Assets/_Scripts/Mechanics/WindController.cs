using UnityEngine;

public class WindController : MonoBehaviour
{
    [Header("风力设置")]
    public float windForce = 15f;          // 风力强度
    public Vector2 windDirection = Vector2.up; // 风向
    public float airResistance = 0.5f;     // 空中阻力系数

    [Header("特效")]
    public ParticleSystem windParticles;  // 粒子特效

    private Rigidbody2D playerRb;
    private bool isPlayerInWindZone;

    void Start()
    {
        windDirection = windDirection.normalized;
        if (windParticles) windParticles.Stop();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerRb = other.GetComponent<Rigidbody2D>();
            isPlayerInWindZone = true;

            // 启动特效
            if (windParticles) windParticles.Play();

            // 减小空中控制力
            if (playerRb) playerRb.drag = airResistance;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInWindZone = false;

            // 停止特效
            if (windParticles) windParticles.Stop();

            // 恢复默认空气阻力
            if (playerRb) playerRb.drag = 0f;
        }
    }

    void FixedUpdate()
    {
        if (isPlayerInWindZone && playerRb)
        {
            // 应用持续风力（使用质量计算更物理准确）
            Vector2 force = windDirection * windForce * playerRb.mass;
            playerRb.AddForce(force, ForceMode2D.Force);

            // 限制最大上升速度
            playerRb.velocity = Vector2.ClampMagnitude(
                playerRb.velocity,
                windForce * 1.5f
            );
        }
    }
}