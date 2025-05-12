using UnityEngine;

public class PressurePlateControllercc : MonoBehaviour
{
    public Transform door;
    // 门要移动到的目标位置
    public Transform targetPosition;
    // 移动插值速度
    public float moveSpeed = 5f;
    // 压力板下移距离
    public float plateMoveOffset = 0.1f;

    private Vector3 doorInitialPos;
    private Vector3 plateInitialPos;
    private bool isPressed;

    void Start()
    {
        doorInitialPos = door.position;
        plateInitialPos = transform.position;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player1") || other.CompareTag("Player2") || other.CompareTag("box"))
        {
            isPressed = true;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player1") || other.CompareTag("Player2") || other.CompareTag("box"))
        {
            isPressed = false;
        }
    }

    void Update()
    {
        // 处理门移动
        Vector3 targetDoorPos = isPressed ? targetPosition.position : doorInitialPos;
        door.position = Vector3.Lerp(
            door.position, 
            targetDoorPos, 
            moveSpeed * Time.deltaTime
        );

        // 处理压力板移动
        Vector3 targetPlatePos = isPressed ? 
            plateInitialPos + Vector3.down * plateMoveOffset : 
            plateInitialPos;
        transform.position = Vector3.Lerp(
            transform.position, 
            targetPlatePos, 
            moveSpeed * Time.deltaTime
        );
    }
}