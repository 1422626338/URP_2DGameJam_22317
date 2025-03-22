using UnityEngine;

public class Rope : MonoBehaviour
{
    public Rigidbody2D hook;          // 挂钩的刚体
    public GameObject[] prefabRopeSegs; // 绳子段预制体数组
    public int numLinks = 5;           // 绳子段数量

    void Start()
    {
        GenerateRope();
    }

    void GenerateRope()
    {
        Rigidbody2D prevBod = hook;

        for (int i = 0; i < numLinks; i++)
        {
            // 随机选择绳段预制体
            int index = Random.Range(0, prefabRopeSegs.Length);
            GameObject newSeg = Instantiate(prefabRopeSegs[index]);

            // 设置父物体和名称（便于调试）
            newSeg.transform.parent = transform;
            newSeg.name = "RopeSegment_" + i;

            // 动态获取绳段高度
            SpriteRenderer segSprite = newSeg.GetComponent<SpriteRenderer>();
            float segmentHeight = segSprite.bounds.size.y;

            // 计算位置：前一个刚体位置 + 向下偏移（锚点在下半部）
            Vector3 spawnPosition = prevBod.transform.position + Vector3.down * segmentHeight * 0.5f;
            newSeg.transform.position = spawnPosition;

            // 配置物理关节
            HingeJoint2D hj = newSeg.GetComponent<HingeJoint2D>();
            hj.connectedBody = prevBod;

            // 更新为当前刚体供下一段使用
            prevBod = newSeg.GetComponent<Rigidbody2D>();
        }
    }
}