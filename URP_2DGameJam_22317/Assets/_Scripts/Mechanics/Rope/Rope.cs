using UnityEngine;

public class Rope : MonoBehaviour
{
    public Rigidbody2D hook;          // �ҹ��ĸ���
    public GameObject[] prefabRopeSegs; // ���Ӷ�Ԥ��������
    public int numLinks = 5;           // ���Ӷ�����

    void Start()
    {
        GenerateRope();
    }

    void GenerateRope()
    {
        Rigidbody2D prevBod = hook;

        for (int i = 0; i < numLinks; i++)
        {
            // ���ѡ������Ԥ����
            int index = Random.Range(0, prefabRopeSegs.Length);
            GameObject newSeg = Instantiate(prefabRopeSegs[index]);

            // ���ø���������ƣ����ڵ��ԣ�
            newSeg.transform.parent = transform;
            newSeg.name = "RopeSegment_" + i;

            // ��̬��ȡ���θ߶�
            SpriteRenderer segSprite = newSeg.GetComponent<SpriteRenderer>();
            float segmentHeight = segSprite.bounds.size.y;

            // ����λ�ã�ǰһ������λ�� + ����ƫ�ƣ�ê�����°벿��
            Vector3 spawnPosition = prevBod.transform.position + Vector3.down * segmentHeight * 0.5f;
            newSeg.transform.position = spawnPosition;

            // ��������ؽ�
            HingeJoint2D hj = newSeg.GetComponent<HingeJoint2D>();
            hj.connectedBody = prevBod;

            // ����Ϊ��ǰ���幩��һ��ʹ��
            prevBod = newSeg.GetComponent<Rigidbody2D>();
        }
    }
}