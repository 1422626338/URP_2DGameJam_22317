using UnityEngine;

public class PressurePlateController : MonoBehaviour
{
    public Transform door; // 拖入门对象
    //public float doorMoveDistance = 3f; // 门移动距离
    public Transform targetPosition;    //门要移动的位置
    public float moveSpeed = 2f; // 移动速度
    public float plateMoveOffset = 0.1f; // 压力板下移距离

    private Vector3 doorInitialPos; //门的初始位置
    private Vector3 plateInitialPos;    //压力板初始位置
    private bool isPressed;

    void Start()
    {
        doorInitialPos = door.position;
        plateInitialPos = transform.position;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if ((other.CompareTag("Player1") || other.CompareTag("Player2")))
        {
            isPressed = true;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if ((other.CompareTag("Player1") || other.CompareTag("Player2")))
        {
            isPressed = false;
        }
    }

    void Update()
    {
        // 计算目标位置
        Vector3 targetDoorPos = isPressed ? targetPosition.position : doorInitialPos;
        Vector3 targetPlatePos = isPressed ? plateInitialPos + Vector3.down * plateMoveOffset : plateInitialPos;

        // 平滑移动门和压力板
        door.position = Vector3.MoveTowards(door.position, targetDoorPos, moveSpeed * Time.deltaTime);
        transform.position = Vector3.MoveTowards(transform.position, targetPlatePos, moveSpeed * Time.deltaTime);
    }
}