using UnityEngine;

public class WindController : MonoBehaviour
{
    [Header("��������")]
    public float windForce = 15f;          // ����ǿ��
    public Vector2 windDirection = Vector2.up; // ����
    public float airResistance = 0.5f;     // ��������ϵ��

    [Header("��Ч")]
    public ParticleSystem windParticles;  // ������Ч

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

            // ������Ч
            if (windParticles) windParticles.Play();

            // ��С���п�����
            if (playerRb) playerRb.drag = airResistance;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInWindZone = false;

            // ֹͣ��Ч
            if (windParticles) windParticles.Stop();

            // �ָ�Ĭ�Ͽ�������
            if (playerRb) playerRb.drag = 0f;
        }
    }

    void FixedUpdate()
    {
        if (isPlayerInWindZone && playerRb)
        {
            // Ӧ�ó���������ʹ���������������׼ȷ��
            Vector2 force = windDirection * windForce * playerRb.mass;
            playerRb.AddForce(force, ForceMode2D.Force);

            // ������������ٶ�
            playerRb.velocity = Vector2.ClampMagnitude(
                playerRb.velocity,
                windForce * 1.5f
            );
        }
    }
}