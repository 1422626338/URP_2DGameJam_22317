using UnityEngine;

public class RopeSegment : MonoBehaviour
{
    public GameObject connectedAbove, connectedBelow;

    void Start()
    {
        // ȷ���Ϸ�������������Ұ���HingeJoint2D
        if (GetComponent<HingeJoint2D>().connectedBody != null)
        {
            connectedAbove = GetComponent<HingeJoint2D>().connectedBody.gameObject;
            RopeSegment aboveSegment = connectedAbove.GetComponent<RopeSegment>();

            if (aboveSegment != null)
            {
                // ����˫������
                aboveSegment.connectedBelow = gameObject;

                // ��̬����ê��λ��
                SpriteRenderer aboveSprite = connectedAbove.GetComponent<SpriteRenderer>();
                if (aboveSprite != null)
                {
                    float spriteHeight = aboveSprite.bounds.size.y;
                    // ��ê���������Ϸ�����ĵײ�����
                    GetComponent<HingeJoint2D>().connectedAnchor = new Vector2(0, -spriteHeight * 0.5f);
                }
            }
            else
            {
                // ����Ƕ����Ĺҹ���ê����ΪĬ��
                GetComponent<HingeJoint2D>().connectedAnchor = Vector2.zero;
            }
        }
    }
}