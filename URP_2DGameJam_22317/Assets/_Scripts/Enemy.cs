using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private bool isAttacking;

    private Rigidbody2D erb;
    private Animator anim;

    [Header("�����ƶ���Ϣ")]
    [SerializeField] private float moveSpeed;

    [Header("������")]
    [SerializeField] private float playerCheckDistance;
    [SerializeField] private LayerMask whatIsPlayer;
    private RaycastHit2D isPlayerDetected;

    [Header("��ɫ��״̬")]
    private int facingDir = 1;
    private bool facingRight = true;

    [Header("������")]
    [SerializeField] LayerMask groundLayer; //����ͼ��
    [SerializeField] Vector2 boxSize;
    [SerializeField] float castDistance;
    [SerializeField] Transform groundCheckTransform;
    public bool isGrounded;

    [Header("ǽ����")]
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
                //������ҿ��Լ���
                erb.velocity = new Vector2(moveSpeed * 1f * facingDir, erb.velocity.y);
                Debug.Log("�������");
                isAttacking = false;
            }

            else
            {
                Debug.Log("Attack!" + isPlayerDetected.collider.gameObject.name);
                isAttacking = true;
            }
        }

        if (!isGrounded || isWallDetected)  //û����������⵽ǽ��
            Flip();
    }

    /// <summary>
    /// ������.��ײ���
    /// </summary>
    private void CollisionCheck()
    {    
        // �����⣨ʹ��Box���߼�⣩
        isGrounded = Physics2D.BoxCast(groundCheckTransform.transform.position, boxSize, 0, -groundCheckTransform.transform.up, castDistance, groundLayer);
        isWallDetected = Physics2D.Raycast(wallCheck.position, Vector2.right * facingDir, wallCheckDistance, groundLayer);
        //������
        isPlayerDetected = Physics2D.Raycast(transform.position, Vector2.right, playerCheckDistance * facingDir, whatIsPlayer);
    }

    /// <summary>
    /// �����ƶ�
    /// </summary>
    private void Movement()
    {
        if (!isAttacking)
            erb.velocity = new Vector2(moveSpeed * facingDir, erb.velocity.y);
    }

    /// <summary>
    /// ÿִ��һ�Σ���ɫ�ͷ�תһ��
    /// </summary>
    private void Flip()
    {
        facingDir = facingDir * -1;
        facingRight = !facingRight;
        transform.Rotate(0, 180, 0);
    }
    /// <summary>
    /// ���ߵ�����
    /// </summary>
    private void OnDrawGizmos()
    {
        //������
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube(groundCheckTransform.transform.position - groundCheckTransform.transform.up * castDistance, boxSize);
        //���ǽ��
        Gizmos.DrawLine(wallCheck.position, new Vector3(wallCheck.position.x + wallCheckDistance * facingDir, wallCheck.position.y));
        //������
        Gizmos.color = Color.blue;
        Gizmos.DrawLine(transform.position, new Vector3(transform.position.x + playerCheckDistance * facingDir, transform.position.y));
    }
}
