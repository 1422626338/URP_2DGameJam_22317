using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformDetector : MonoBehaviour
{
    // ��¼��ǰƽ̨�Ƿ�����ײ�壨��ң�����
    public bool hasPlayer = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // �����ײ���ǩΪ "Player"������Ϊ��⵽
        if (collision.CompareTag("Player1") || collision.CompareTag("Player2"))
        {
            hasPlayer = true;
            //TODO:���ĸ����壬��ֹ���ֶ���
            collision.transform.parent = this.transform;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        // ����뿪�󣬽������Ϊ false
        if (collision.CompareTag("Player1") || collision.CompareTag("Player2"))
        {
            hasPlayer = false;
            //TODO:�˳�������
            if(collision.transform.parent != null)
            {
                collision.transform.parent = null;
            }
        }
    }
}

