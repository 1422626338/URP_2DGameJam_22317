using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private bool isAttacking;

    private Rigidbody2D erb;
    private Animator anim;

    [Header("怪物移动信息")]
    [SerializeField] private float moveSpeed;

    [Header("检测玩家")]
    [SerializeField] private float playerCheckDistance;
    [SerializeField] private LayerMask whatIsPlayer;
    private RaycastHit2D isPlayerDetected;

    [Header("角色的状态")]
    private int facingDir = 1;
    private bool facingRight = true;

    [Header("检测地面")]
    [SerializeField] LayerMask groundLayer; //地面图层
    [SerializeField] Vector2 boxSize;
    [SerializeField] float castDistance;
    [SerializeField] Transform groundCheckTransform;
    public bool isGrounded;

    [Header("墙面检测")]
    [SerializeField] protected Transform wallCheck;
    [SerializeField] protected float wallCheckDistance;
    protected bool isWallDetected;

    private void Start()
    {
        erb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        if (wallCheck == null)
            wallCheck = transform;
    }

    private void Update()
    {
        CollisionCheck();
        Movement();

        if (isPlayerDetected)
        {
            if (isPlayerDetected.distance > 1)
            {
                //发现玩家可以加速
                erb.velocity = new Vector2(moveSpeed * 1f * facingDir, erb.velocity.y);
                Debug.Log("发现玩家");
                isAttacking = false;
            }

            else
            {
                Debug.Log("Attack!" + isPlayerDetected.collider.gameObject.name);
                isAttacking = true;
            }
        }

        if (!isGrounded || isWallDetected)  //没碰到地面或检测到墙面
            Flip();
    }

    /// <summary>
    /// 检测地面.碰撞检测
    /// </summary>
    private void CollisionCheck()
    {    
        // 地面检测（使用Box射线检测）
        isGrounded = Physics2D.BoxCast(groundCheckTransform.transform.position, boxSize, 0, -groundCheckTransform.transform.up, castDistance, groundLayer);
        isWallDetected = Physics2D.Raycast(wallCheck.position, Vector2.right * facingDir, wallCheckDistance, groundLayer);
        //检测玩家
        isPlayerDetected = Physics2D.Raycast(transform.position, Vector2.right, playerCheckDistance * facingDir, whatIsPlayer);
    }

    /// <summary>
    /// 怪物移动
    /// </summary>
    private void Movement()
    {
        if (!isAttacking)
            erb.velocity = new Vector2(moveSpeed * facingDir, erb.velocity.y);
    }

    /// <summary>
    /// 每执行一次，角色就翻转一次
    /// </summary>
    private void Flip()
    {
        facingDir = facingDir * -1;
        facingRight = !facingRight;
        transform.Rotate(0, 180, 0);
    }
    /// <summary>
    /// 射线地面检测
    /// </summary>
    private void OnDrawGizmos()
    {
        //检测地面
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube(groundCheckTransform.transform.position - groundCheckTransform.transform.up * castDistance, boxSize);
        //检测墙壁
        Gizmos.DrawLine(wallCheck.position, new Vector3(wallCheck.position.x + wallCheckDistance * facingDir, wallCheck.position.y));
        //检测玩家
        Gizmos.color = Color.blue;
        Gizmos.DrawLine(transform.position, new Vector3(transform.position.x + playerCheckDistance * facingDir, transform.position.y));
    }
}
