using UnityEngine;
using System.Collections.Generic;

public class ChainCreator : MonoBehaviour
{
    [Header("��������")]
    public GameObject chainLinkPrefab;      // ����Ԥ���壬Ҫ����� Rigidbody�����ڴ�������� HingeJoint��
    public int chainLinkCount = 10;           // ����������

    [Header("���Ӷ˵�")]
    public Transform startPoint;            // ���磺�ֻ���
    public Transform endPoint;              // ���磺����ƽ̨��

    [Header("��������")]
    public float linkSpacing = 0.5f;          // ����֮��ļ�����ɸ���ʵ�ʾ�����ڣ�

    [HideInInspector]
    public List<GameObject> createdChainLinks = new List<GameObject>(); // �������ɵ�������

    void Start()
    {
        CreateChain();
    }

    void CreateChain()
    {
        if (chainLinkPrefab == null || startPoint == null || endPoint == null)
        {
            Debug.LogError("����������Ԥ����Ͷ˵㣡");
            return;
        }

        // ���������յ�֮��ķ����ܾ���
        Vector3 direction = (endPoint.position - startPoint.position).normalized;
        float totalDistance = Vector3.Distance(startPoint.position, endPoint.position);
        // �Զ�����ÿ��������ļ����Ҳ����ֱ��ʹ�� linkSpacing ������
        float spacing = totalDistance / (chainLinkCount + 1);

        // ������㣨������ Rigidbody��
        Rigidbody previousRb = startPoint.GetComponent<Rigidbody>();
        if (previousRb == null)
        {
            Debug.LogError("startPoint ������� Rigidbody��");
            return;
        }

        // �������������β�����
        for (int i = 0; i < chainLinkCount; i++)
        {
            // ÿ�������ε�λ��
            Vector3 pos = startPoint.position + direction * spacing * (i + 1);
            GameObject link = Instantiate(chainLinkPrefab, pos, Quaternion.identity, transform);
            // ȷ������������ Rigidbody
            Rigidbody linkRb = link.GetComponent<Rigidbody>();
            if (linkRb == null)
                linkRb = link.AddComponent<Rigidbody>();

            // ��� HingeJoint ���ӵ�ǰһ�Σ�����㣩
            HingeJoint joint = link.GetComponent<HingeJoint>();
            if (joint == null)
                joint = link.AddComponent<HingeJoint>();
            joint.connectedBody = previousRb;
            // �ɵ��� joint.anchor��joint.axis �Ȳ����Ի�ø���Ȼ���˶�
            joint.anchor = Vector3.zero;

            // �������ɵ�������
            createdChainLinks.Add(link);
            previousRb = linkRb;
        }

        // ���ĩ�����յ�����
        Rigidbody endRb = endPoint.GetComponent<Rigidbody>();
        if (endRb == null)
        {
            Debug.LogError("endPoint ������� Rigidbody��");
            return;
        }
        HingeJoint endJoint = endPoint.gameObject.AddComponent<HingeJoint>();
        endJoint.connectedBody = previousRb;
        endJoint.anchor = Vector3.zero;
    }
}
