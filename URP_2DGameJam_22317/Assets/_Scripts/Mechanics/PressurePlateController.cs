using UnityEngine;
using DG.Tweening;
/// <summary>
/// 添加箱子在压力板上时，只需要检测箱子的标签Tag
/// </summary>
public class PressurePlateController : MonoBehaviour
{
    public Transform door; // 拖入门对象

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
        // 计算目标位置
        Vector3 targetDoorPos = isPressed ? targetPosition.position : doorInitialPos;
        Vector3 targetPlatePos = isPressed ? plateInitialPos + Vector3.down * plateMoveOffset : plateInitialPos;
     

        // 平滑移动门和压力板
        door.DOMove(targetDoorPos, 0.2f).SetEase(Ease.InOutQuad); ;            //机关管移至目标处
        transform.position = Vector3.MoveTowards(transform.position, targetPlatePos, moveSpeed * Time.deltaTime);   //压力板本身下移
    }
}