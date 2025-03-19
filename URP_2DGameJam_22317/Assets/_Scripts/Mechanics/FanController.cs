using UnityEngine;

public class FanController : MonoBehaviour
{
    public float maxUpwardForce = 30f; // �����������

    private Rigidbody2D playerRb;
    private BoxCollider2D fanArea;

    void Start()
    {
        fanArea = GetComponent<BoxCollider2D>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerRb = other.GetComponent<Rigidbody2D>();
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerRb = null;
        }
    }

    void FixedUpdate()
    {
        if (playerRb != null)
        {
            Bounds bounds = fanArea.bounds;
            float yMin = bounds.min.y;
            float yMax = bounds.max.y;
            float playerY = playerRb.position.y;

            // ������������������ڵĴ�ֱλ�ñ���
            float t = Mathf.Clamp01((playerY - yMin) / (yMax - yMin));

            // ��̬�����������ײ���󣬶������㣩
            float upwardForce = maxUpwardForce * (1 - t);

            // �������� + ��������
            float gravityForce = -playerRb.mass * Physics2D.gravity.y * playerRb.gravityScale;
            playerRb.AddForce(new Vector2(0, gravityForce + upwardForce));
        }
    }
}