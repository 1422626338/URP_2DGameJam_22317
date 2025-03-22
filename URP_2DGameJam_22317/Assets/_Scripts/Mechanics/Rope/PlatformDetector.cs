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
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        // ����뿪�󣬽������Ϊ false
        if (collision.CompareTag("Player1") || collision.CompareTag("Player2"))
        {
            hasPlayer = false;
        }
    }
}

