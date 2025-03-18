using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform player1;
    public Transform player2;
    public float minSize = 5f;  //��С������ͼ��Ұ
    public float maxSize = 10f; //���������ͼ��Ұ
    public float padding = 2f;  //�߾�

    private Camera cam; 

    void Start()
    {
        cam = GetComponent<Camera>();
    }

    void LateUpdate()
    {
        Vector3 midpoint = (player1.position + player2.position) / 2;   //����ɫ�������λ��
        float distance = Vector3.Distance(player1.position, player2.position);  //����ɫλ�ü�ľ���

        //HACK:������������
        cam.transform.position = Vector3.Lerp(cam.transform.position, 
            new Vector3(midpoint.x,midpoint.y,cam.transform.position.z), 
            Time.deltaTime * 5f);   //����ϵ������ƽ���ٶ�

        //cam.transform.position = new Vector3(
        //    midpoint.x,
        //    midpoint.y,
        //    cam.transform.position.z
        //);  //�����ʼ��������ɫ������λ�ã�z�᲻�䣬�����������ĵ����

        cam.orthographicSize = Mathf.Clamp(distance / 2 + padding, minSize, maxSize);   //��������Сֵ�����ֵ����ķ�Χ�ڡ�
    }
}
