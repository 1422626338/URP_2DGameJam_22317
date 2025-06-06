using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class PlayerController : MonoBehaviour
{
    // 玩家类型枚举
    public enum PlayerType { Player1, Player2 }
    public PlayerType playerType;
    public PlayerState playerState;

    // 移动参数
    [SerializeField] float moveSpeed = 5f;
    [SerializeField] float jumpForce = 10f;

    [Header("检测地面")]
    [SerializeField] LayerMask groundLayer; //地面图层
    [SerializeField] Vector2 boxSize;
    [SerializeField] float castDistance;
    public bool isGrounded;

    [Header("血量控制")]
    public HealthBarTimer healthBarTimer; // 拖入挂载HealthBarTimer的对象

    [Header("角色朝向")]
    [SerializeField] bool isFacingRight = true; // 默认面向右边

    // 组件引用
    public Rigidbody2D rb;
    private float horizontalInput;   

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        playerState = PlayerState.normal;
    }

    void Update()
    {
        // 地面检测（使用Box射线检测）
        isGrounded = Physics2D.BoxCast(transform.position,boxSize,0,-transform.up,castDistance,groundLayer);

        switch (playerState)
        {
            case PlayerState.normal:
                // 移动处理
                MoveCharacter();
                // 输入处理
                HandleInput();
                //处理角色翻转
                HandleFlip();
                break;
            case PlayerState.console:
                
            break;
        }
            
    }
    void FixedUpdate()
    {
        
       
    }

    void HandleInput()
    {
        switch (playerType)
        {
            case PlayerType.Player1:
                horizontalInput = Input.GetAxisRaw("Horizontal_P1");
                if (Input.GetKeyDown(KeyCode.W) && isGrounded)
                {
                    rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
                    //TODO：跳跃音效
                    if (AudioController.Instance != null)
                    {
                        AudioController.Instance.PlaySFX(AudioController.Instance.jump);
                    }
                }
                break;

            case PlayerType.Player2:
                horizontalInput = Input.GetAxisRaw("Horizontal_P2");
                if (Input.GetKeyDown(KeyCode.UpArrow) && isGrounded)
                {
                    rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
                    //TODO：跳跃音效
                    if (AudioController.Instance != null)
                    {
                        AudioController.Instance.PlaySFX(AudioController.Instance.jump);
                    }
                }
                break;
        }
    }
    /// <summary>
    /// 角色翻转
    /// </summary>
    void HandleFlip()
    {
        if (horizontalInput != 0)
        {
            bool shouldFaceRight = horizontalInput > 0;

            if (shouldFaceRight != isFacingRight)
            {
                // 执行翻转
                Vector3 newScale = transform.localScale;
                newScale.x *= -1;
                transform.localScale = newScale;

                isFacingRight = !isFacingRight;
            }
        }
    }

    void MoveCharacter()
    {
        // 平滑移动
        //Vector2 targetVelocity = new Vector2(horizontalInput * moveSpeed, rb.velocity.y);
        //rb.velocity = Vector2.Lerp(rb.velocity, targetVelocity, Time.fixedDeltaTime * 10);  //使用Lerp，可以更改
        rb.velocity = new Vector2(horizontalInput * moveSpeed, rb.velocity.y);
    }


    private void OnTriggerStay2D(Collider2D collision)
    {
        
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
       
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube(transform.position - transform.up * castDistance,boxSize);
    }

}
