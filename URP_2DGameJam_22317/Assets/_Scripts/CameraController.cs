using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform player1;
    public Transform player2;
    public float minSize = 5f;  //最小正交视图视野
    public float maxSize = 10f; //最大正交视图视野
    public float padding = 2f;  //边距

    private Camera cam; 

    void Start()
    {
        cam = GetComponent<Camera>();
    }

    void LateUpdate()
    {
        Vector3 midpoint = (player1.position + player2.position) / 2;   //两角色间的中心位置
        float distance = Vector3.Distance(player1.position, player2.position);  //两角色位置间的距离

        //HACK:解决摄像机抖动
        cam.transform.position = Vector3.Lerp(cam.transform.position, 
            new Vector3(midpoint.x,midpoint.y,cam.transform.position.z), 
            Time.deltaTime * 5f);   //调整系数控制平滑速度

        //cam.transform.position = new Vector3(
        //    midpoint.x,
        //    midpoint.y,
        //    cam.transform.position.z
        //);  //摄像机始终在两角色的中心位置，z轴不变，跟随两个中心点玩家

        cam.orthographicSize = Mathf.Clamp(distance / 2 + padding, minSize, maxSize);   //限制在最小值和最大值定义的范围内。
    }
}
