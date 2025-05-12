using UnityEngine;

public class PressurePlateController : MonoBehaviour
{
    public static bool AnyPlatePressed;  // 全局共享状态

    public Transform door;              // 所有压力板拖入同一个门对象
    public Transform targetPosition;    // 门的目标位置
    public float moveSpeed = 2f;        // 移动速度
    public float plateMoveOffset = 0.1f; // 压力板下移距离

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
            AnyPlatePressed = true; // 更新全局状态
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player1") || other.CompareTag("Player2") || other.CompareTag("box"))
        {
            isPressed = false;
            // 检查其他压力板状态
            AnyPlatePressed = CheckOtherPlates();
        }
    }

    // 检测是否有其他压力板被按下
    bool CheckOtherPlates()
    {
        PressurePlateController[] plates = FindObjectsOfType<PressurePlateController>();
        foreach (var plate in plates)
        {
            if (plate.isPressed) return true;
        }
        return false;
    }

    void Update()
    {
        // 门的移动逻辑（用 Lerp 实现平滑）
        Vector3 targetDoorPos = AnyPlatePressed ? targetPosition.position : doorInitialPos;
        door.position = Vector3.Lerp(door.position, targetDoorPos, moveSpeed * Time.deltaTime);

        // 压力板的移动逻辑
        Vector3 targetPlatePos = isPressed ?
            plateInitialPos + Vector3.down * plateMoveOffset :
            plateInitialPos;
        transform.position = Vector3.MoveTowards(
            transform.position,
            targetPlatePos,
            moveSpeed * Time.deltaTime
        );
    }
}