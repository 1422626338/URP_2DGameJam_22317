using UnityEngine;
/// <summary>
/// ���������ѹ������ʱ��ֻ��Ҫ������ӵı�ǩTag
/// </summary>
public class PressurePlateController : MonoBehaviour
{
    public Transform door; // �����Ŷ���

    public Transform targetPosition;    //��Ҫ�ƶ���λ��
    public float moveSpeed = 2f; // �ƶ��ٶ�
    public float plateMoveOffset = 0.1f; // ѹ�������ƾ���

    private Vector3 doorInitialPos; //�ŵĳ�ʼλ��
    private Vector3 plateInitialPos;    //ѹ�����ʼλ��
    private bool isPressed;

    void Start()
    {
        doorInitialPos = door.position;
        plateInitialPos = transform.position;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player1") || other.CompareTag("Player2"))
        {
            isPressed = true;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player1") || other.CompareTag("Player2"))
        {
            isPressed = false;
        }
    }

    void Update()
    {
        // ����Ŀ��λ��
        Vector3 targetDoorPos = isPressed ? targetPosition.position : doorInitialPos;
        Vector3 targetPlatePos = isPressed ? plateInitialPos + Vector3.down * plateMoveOffset : plateInitialPos;

        // ƽ���ƶ��ź�ѹ����
        door.position = Vector3.MoveTowards(door.position, targetDoorPos, moveSpeed * Time.deltaTime);              //���ع�����Ŀ�괦
        transform.position = Vector3.MoveTowards(transform.position, targetPlatePos, moveSpeed * Time.deltaTime);   //ѹ���屾������
    }
}