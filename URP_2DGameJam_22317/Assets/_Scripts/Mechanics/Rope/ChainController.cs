using UnityEngine;

/// <summary>
/// TODO:���������Դ�󣬳����Ϸ��������ƶ�����Ҫ�޸���ͼƬΪѭ��
/// </summary>

public class ChainController : MonoBehaviour
{
    [Header("��������Transform��")]
    public Transform chain1;  // ����1
    public Transform chain2;  // ����2

    [Header("ƽ̨�����")]
    public PlatformDetector detector1; // ��������1ƽ̨�ϵļ����
    public PlatformDetector detector2; // ��������2ƽ̨�ϵļ����

    [Header("�˶�����")]
    public float moveSpeed = 2f; // �˶��ٶ�

    [Header("�߽�ο���")]
    public Transform chain1UpperBoundary; // ����1������ϱ߽�
    public Transform chain1LowerBoundary; // ����1������±߽�
    public Transform chain2UpperBoundary; // ����2������ϱ߽�
    public Transform chain2LowerBoundary; // ����2������±߽�

    // �Զ������ƽ��Ŀ�꣨����Ҫ�� Inspector �����ã�
    private float chain1BalancedY;
    private float chain2BalancedY;

    void Start()
    {
        // �Զ�����ƽ��Ŀ��λ��
        chain1BalancedY = (chain1UpperBoundary.position.y + chain1LowerBoundary.position.y) / 2f;
        chain2BalancedY = (chain2UpperBoundary.position.y + chain2LowerBoundary.position.y) / 2f;
    }

    void Update()
    {
        float delta = moveSpeed * Time.deltaTime;

        // ���1��ֻ�� detector1 ��⵽��ɫ���� chain1 ���¡�chain2 ���ϣ�
        if (detector1.hasPlayer && !detector2.hasPlayer)
        {
            // ����˶�ǰ�Ƿ�δ�����߽�
            if (chain1.position.y > chain1LowerBoundary.position.y && chain2.position.y < chain2UpperBoundary.position.y)
            {
                float newChain1Y = Mathf.Max(chain1.position.y - delta, chain1LowerBoundary.position.y);
                float newChain2Y = Mathf.Min(chain2.position.y + delta, chain2UpperBoundary.position.y);
                chain1.position = new Vector3(chain1.position.x, newChain1Y, chain1.position.z);
                chain2.position = new Vector3(chain2.position.x, newChain2Y, chain2.position.z);
            }
        }
        // ���2��ֻ�� detector2 ��⵽��ɫ���� chain2 ���¡�chain1 ���ϣ�
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
        // ���3������ƽ̨���н�ɫʱ����ƽ��Ŀ��λ���˶�
        else if (detector1.hasPlayer && detector2.hasPlayer)
        {
            // ���� chain1 ���� chain1BalancedY �˶�
            if (Mathf.Abs(chain1.position.y - chain1BalancedY) > 0.01f)
            {
                if (chain1.position.y > chain1BalancedY)
                    chain1.position = new Vector3(chain1.position.x, Mathf.Max(chain1.position.y - delta, chain1BalancedY), chain1.position.z);
                else
                    chain1.position = new Vector3(chain1.position.x, Mathf.Min(chain1.position.y + delta, chain1BalancedY), chain1.position.z);
            }
            // ���� chain2 ���� chain2BalancedY �˶�
            if (Mathf.Abs(chain2.position.y - chain2BalancedY) > 0.01f)
            {
                if (chain2.position.y > chain2BalancedY)
                    chain2.position = new Vector3(chain2.position.x, Mathf.Max(chain2.position.y - delta, chain2BalancedY), chain2.position.z);
                else
                    chain2.position = new Vector3(chain2.position.x, Mathf.Min(chain2.position.y + delta, chain2BalancedY), chain2.position.z);
            }
        }
        // ���4������ƽ̨��û�н�ɫʱ�����ֵ�ǰ״̬
    }
}
