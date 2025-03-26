using System.Collections;
using System.Collections.Generic;
using UnityEngine;
 
 /// <summary>
 ///注释
 /// <summary>

public class PlayerAnimation : MonoBehaviour
{
    private Rigidbody2D rb;
    private Animator animator;
    private PlayerController playerController;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        playerController = GetComponent<PlayerController>();
    }

    private void Update()
    {
        SetAnimation();
    }

    private void SetAnimation()
    {
        animator.SetBool("IsGround", playerController.isGrounded);
        animator.SetFloat("VelocityX" , Mathf.Abs(rb.velocity.x));
        animator.SetFloat("VelocityY" , rb.velocity.y);
    }


}
