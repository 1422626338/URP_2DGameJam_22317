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
            //HACK:���ĸ����壬��ֹ���ֶ���
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
            if (collision.transform.parent != null)
            {
                collision.transform.parent = null;  //���ﱨ��̫���У��Ȳ�������
            }
        }
    }
    //HACK:
    #region �����ɫվ������ƽֹ̨ͣ������Ϸ����������ʱ��ʹ��
    //private IEnumerator DetachPlayerAfterDelay(Transform playerTransform)
    //{
    //    yield return null;  // �ȴ�һ֡
    //    if (playerTransform != null)
    //    {
    //        playerTransform.parent = null;
    //    }
    //}

    //private void OnTriggerExit2D(Collider2D collision)
    //{
    //    if (collision.CompareTag("Player1") || collision.CompareTag("Player2"))
    //    {
    //        hasPlayer = false;
    //        StartCoroutine(DetachPlayerAfterDelay(collision.transform));
    //    }
    //}
    #endregion
}

