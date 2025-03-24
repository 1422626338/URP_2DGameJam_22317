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
    [SerializeField] float jumpForce = 14f;
    [SerializeField] LayerMask groundLayer; //地面图层

    [Header("血量控制")]
    public HealthBarTimer healthBarTimer; // 拖入挂载HealthBarTimer的对象

    // 组件引用
    public Rigidbody2D rb;
    private bool isGrounded;
    private float horizontalInput;
    

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        playerState = PlayerState.normal;
    }

    void Update()
    {
        // 地面检测（使用圆形射线检测）
        isGrounded = Physics2D.CircleCast(transform.position, 0.5f, Vector2.down, 0.6f, groundLayer);

        switch (playerState)
        {
            case PlayerState.normal:
                // 移动处理
                MoveCharacter();
                // 输入处理
                HandleInput();
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
                }
                break;

            case PlayerType.Player2:
                horizontalInput = Input.GetAxisRaw("Horizontal_P2");
                if (Input.GetKeyDown(KeyCode.UpArrow) && isGrounded)
                {
                    rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
                }
                break;
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
        
    }

}
