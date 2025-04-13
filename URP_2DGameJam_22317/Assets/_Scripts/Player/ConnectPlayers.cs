using UnityEngine;

public class ConnectPlayers : MonoBehaviour
{
    public Transform player1;
    public Transform player2;
    public float maxDistance = 10f; // ����������
    public float fadeOutSpeed = 2f; // �����ٶ�
    public float fadeInSpeed = 2f; // ���������ٶ�
    public bool shouldShowLine = true; // ����������ʾ�Ĳ���ֵ
    public LayerMask layerMask; // �������ͼ�����ײ�壬����ʧ

    private LineRenderer lineRenderer;
    private Color originalColor;
    private float alpha = 1f;

    void Start()
    {
        // ͨ����ǩ����Player1����ȡ��Transform���
        player1 = GameObject.FindWithTag("Player1").transform;
        // ͨ����ǩ����Player2����ȡ��Transform���
        player2 = GameObject.FindWithTag("Player2").transform;

        // ��ȡLineRenderer���
        lineRenderer = GetComponent<LineRenderer>();
        // ����LineRenderer�Ķ�������Ϊ2
        lineRenderer.positionCount = 2;

        // ��¼ԭʼ��ɫ
        originalColor = lineRenderer.startColor;
    }

    void Update()
    {
        if (player1 != null && player2 != null)
        {
            if (!shouldShowLine)
            {
                // ����ֵΪ false����������
                alpha = Mathf.Max(alpha - fadeOutSpeed * Time.deltaTime, 0f);
                UpdateLineColor();
                if (alpha <= 0f)
                {
                    lineRenderer.enabled = false;
                    //TODO:������������
                }
            }
            else
            {
                // �����������֮��ľ���
                float distance = Vector3.Distance(player1.position, player2.position);
                bool isObstructed = Physics2D.Linecast(player1.position, player2.position, layerMask);

                if (distance <= maxDistance && !isObstructed)
                {
                    // ���������Ʒ�Χ�������ϰ����ʾ����
                    lineRenderer.enabled = true;
                    //alpha = 1f; // ����͸����
                    // �����߼���͸����������
                    alpha = Mathf.Min(alpha + fadeInSpeed * Time.deltaTime, 1f);
                    UpdateLineColor();
                    // ����LineRenderer����ʼ��ΪPlayer1��λ��
                    lineRenderer.SetPosition(0, player1.position);
                    // ����LineRenderer�Ľ�����ΪPlayer2��λ��
                    lineRenderer.SetPosition(1, player2.position);
                }
                else
                {
                    // ���볬�����ƻ����ϰ����������
                    alpha = Mathf.Max(alpha - fadeOutSpeed * Time.deltaTime, 0f);
                    UpdateLineColor();
                    if (alpha <= 0f)
                    {
                        lineRenderer.enabled = false;
                        //TODO:������������
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