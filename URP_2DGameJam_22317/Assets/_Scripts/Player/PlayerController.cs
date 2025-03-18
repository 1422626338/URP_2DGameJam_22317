using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // �������ö��
    public enum PlayerType { Player1, Player2 }
    public PlayerType playerType;

    // �ƶ�����
    [SerializeField] float moveSpeed = 5f;
    [SerializeField] float jumpForce = 14f;
    [SerializeField] LayerMask groundLayer; //����ͼ��

    // �������
    private Rigidbody2D rb;
    private bool isGrounded;
    private float horizontalInput;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        // �����⣨ʹ��Բ�����߼�⣩
        isGrounded = Physics2D.CircleCast(transform.position, 0.5f, Vector2.down, 0.6f, groundLayer);

        // ���봦��
        HandleInput();

        // �ƶ�����
        MoveCharacter();
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
        // ƽ���ƶ�
        //Vector2 targetVelocity = new Vector2(horizontalInput * moveSpeed, rb.velocity.y);
        //rb.velocity = Vector2.Lerp(rb.velocity, targetVelocity, Time.fixedDeltaTime * 10);  //ʹ��Lerp�����Ը���
        rb.velocity = new Vector2(horizontalInput * moveSpeed, rb.velocity.y);
    }

    private void OnDrawGizmos()
    {
        
    }
}
