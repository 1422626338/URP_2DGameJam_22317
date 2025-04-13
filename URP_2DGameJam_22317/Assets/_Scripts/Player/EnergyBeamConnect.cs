using UnityEngine;

public class EnergyBeam2D : MonoBehaviour
{
    public float fadeInSpeed = 3f;
    public float fadeOutSpeed = 5f;
    public KeyCode player1Key = KeyCode.E;
    public KeyCode player2Key = KeyCode.Alpha2;
    public LayerMask enemyLayer; // ��Inspector�����õ������ڵ�Layer

    private LineRenderer lineRenderer;
    private EdgeCollider2D beamCollider; // ����2D��ײ��
    private Color originalColor;
    private float alpha = 0f;

    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.positionCount = 2;

        // ��Ӳ�����2D��ײ��
        beamCollider = gameObject.AddComponent<EdgeCollider2D>();
        beamCollider.isTrigger = true;
        UpdateColliderPoints(); // ��ʼ������ײ��λ��

        originalColor = lineRenderer.startColor;
        UpdateLineVisibility(0f);
        beamCollider.enabled = false; // ��ʼ������ײ��
    }

    void Update()
    {
        bool bothKeysPressed = Input.GetKey(player1Key) && Input.GetKey(player2Key);

        if (bothKeysPressed)
        {
            // ���Բ�������ײ��
            alpha = Mathf.Min(alpha + fadeInSpeed * Time.deltaTime, 1f);
            UpdateLineVisibility(alpha);
            UpdateLinePositions();
            beamCollider.enabled = true; // ������ײ���
        }
        else
        {
            // ������������ײ��
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
            // ��������Ⱦ��
            lineRenderer.SetPosition(0, p1.transform.position);
            lineRenderer.SetPosition(1, p2.transform.position);

            // ͬ��������ײ��λ��
            UpdateColliderPoints();
        }
    }

    void UpdateColliderPoints()
    {
        // ��LineRenderer��λ��ת��Ϊ2D��ײ��
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

    // ����������������ʱ����
    void OnTriggerEnter2D(Collider2D other)
    {
        if (enemyLayer == (enemyLayer | (1 << other.gameObject.layer)))
        {
            if (other.CompareTag("Enemy"))
            {
                Destroy(other.gameObject);
                // TODO������������Ч������
            }
        }
    }
}