using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class ChainController : MonoBehaviour
{
    [Header("��������Transform��")]
    public Transform chain1;  // ����1
    public Transform chain2;  // ����2

    [Header("ƽ̨�����")]
    public PlatformDetector detector1; // ��������1�ײ�ƽ̨�ϵļ����
    public PlatformDetector detector2; // ��������2�ײ�ƽ̨�ϵļ����

    [Header("�˶�����")]
    public float moveSpeed = 2f; // �˶��ٶ�

    [Header("λ������")]
    public float chain1MinY; // ����1���λ�ã����磺-3��
    public float chain1MaxY; // ����1���λ�ã����磺0��
    public float chain2MinY; // ����2���λ�ã����磺0��
    public float chain2MaxY; // ����2���λ�ã����磺3��

    [Header("λ��")]
    public Transform chainAMinY;
    public Transform chainAMaxY;
    public Transform chainBMinY;
    public Transform chainBMaxY;

    void Update()
    {
        // ֻ�� detector1 ��⵽��ײ��� detector2 û�м�⵽ʱ��
        if (detector1.hasPlayer && !detector2.hasPlayer)
        {
            // ���㱾֡�ƶ�����
            float delta = moveSpeed * Time.deltaTime;

            // chain1 �½��������������λ��
            float newChain1Y = Mathf.Max(chain1.position.y - delta, chainAMinY.position.y);
            // chain2 �����������������λ��
            float newChain2Y = Mathf.Min(chain2.position.y + delta, chainBMaxY.position.y);

            chain1.position = new Vector3(chain1.position.x, newChain1Y, chain1.position.z);
            chain2.position = new Vector3(chain2.position.x, newChain2Y, chain2.position.z);
        }
        // ֻ�� detector2 ��⵽��ײ��� detector1 û�м�⵽ʱ��
        else if (!detector1.hasPlayer && detector2.hasPlayer)
        {
            float delta = moveSpeed * Time.deltaTime;

            // chain2 �½��������������λ��
            float newChain2Y = Mathf.Max(chain2.position.y - delta, chainBMinY.position.y);
            // chain1 �����������������λ��
            float newChain1Y = Mathf.Min(chain1.position.y + delta, chainAMaxY.position.y);

            chain2.position = new Vector3(chain2.position.x, newChain2Y, chain2.position.z);
            chain1.position = new Vector3(chain1.position.x, newChain1Y, chain1.position.z);
        }
        // ������ƽ̨����⵽��û�м�⵽ʱ�����ı�λ��
    }
}