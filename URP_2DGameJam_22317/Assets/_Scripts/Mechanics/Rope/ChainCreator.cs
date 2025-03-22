using UnityEngine;
using System.Collections.Generic;

public class ChainCreator : MonoBehaviour
{
    [Header("链条设置")]
    public GameObject chainLinkPrefab;      // 链条预制体，要求包含 Rigidbody（可在代码中添加 HingeJoint）
    public int chainLinkCount = 10;           // 链条段数量

    [Header("连接端点")]
    public Transform startPoint;            // 例如：轮滑端
    public Transform endPoint;              // 例如：物理平台端

    [Header("链条参数")]
    public float linkSpacing = 0.5f;          // 各段之间的间隔（可根据实际距离调节）

    [HideInInspector]
    public List<GameObject> createdChainLinks = new List<GameObject>(); // 保存生成的链条段

    void Start()
    {
        CreateChain();
    }

    void CreateChain()
    {
        if (chainLinkPrefab == null || startPoint == null || endPoint == null)
        {
            Debug.LogError("请设置链条预制体和端点！");
            return;
        }

        // 计算起点和终点之间的方向及总距离
        Vector3 direction = (endPoint.position - startPoint.position).normalized;
        float totalDistance = Vector3.Distance(startPoint.position, endPoint.position);
        // 自动计算每段链条间的间隔（也可以直接使用 linkSpacing 参数）
        float spacing = totalDistance / (chainLinkCount + 1);

        // 连接起点（必须有 Rigidbody）
        Rigidbody previousRb = startPoint.GetComponent<Rigidbody>();
        if (previousRb == null)
        {
            Debug.LogError("startPoint 必须挂有 Rigidbody！");
            return;
        }

        // 依次生成链条段并连接
        for (int i = 0; i < chainLinkCount; i++)
        {
            // 每个链条段的位置
            Vector3 pos = startPoint.position + direction * spacing * (i + 1);
            GameObject link = Instantiate(chainLinkPrefab, pos, Quaternion.identity, transform);
            // 确保链条段上有 Rigidbody
            Rigidbody linkRb = link.GetComponent<Rigidbody>();
            if (linkRb == null)
                linkRb = link.AddComponent<Rigidbody>();

            // 添加 HingeJoint 连接到前一段（或起点）
            HingeJoint joint = link.GetComponent<HingeJoint>();
            if (joint == null)
                joint = link.AddComponent<HingeJoint>();
            joint.connectedBody = previousRb;
            // 可调节 joint.anchor、joint.axis 等参数以获得更自然的运动
            joint.anchor = Vector3.zero;

            // 保存生成的链条段
            createdChainLinks.Add(link);
            previousRb = linkRb;
        }

        // 最后将末端与终点连接
        Rigidbody endRb = endPoint.GetComponent<Rigidbody>();
        if (endRb == null)
        {
            Debug.LogError("endPoint 必须挂有 Rigidbody！");
            return;
        }
        HingeJoint endJoint = endPoint.gameObject.AddComponent<HingeJoint>();
        endJoint.connectedBody = previousRb;
        endJoint.anchor = Vector3.zero;
    }
}
