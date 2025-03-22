using UnityEngine;

public class RopeSegment : MonoBehaviour
{
    public GameObject connectedAbove, connectedBelow;

    void Start()
    {
        // 确保上方连接物体存在且包含HingeJoint2D
        if (GetComponent<HingeJoint2D>().connectedBody != null)
        {
            connectedAbove = GetComponent<HingeJoint2D>().connectedBody.gameObject;
            RopeSegment aboveSegment = connectedAbove.GetComponent<RopeSegment>();

            if (aboveSegment != null)
            {
                // 建立双向连接
                aboveSegment.connectedBelow = gameObject;

                // 动态计算锚点位置
                SpriteRenderer aboveSprite = connectedAbove.GetComponent<SpriteRenderer>();
                if (aboveSprite != null)
                {
                    float spriteHeight = aboveSprite.bounds.size.y;
                    // 将锚点设置在上方物体的底部中心
                    GetComponent<HingeJoint2D>().connectedAnchor = new Vector2(0, -spriteHeight * 0.5f);
                }
            }
            else
            {
                // 如果是顶部的挂钩，锚点设为默认
                GetComponent<HingeJoint2D>().connectedAnchor = Vector2.zero;
            }
        }
    }
}