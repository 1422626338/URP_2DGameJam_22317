using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
 
 /// <summary>
 ///注释
 /// <summary>

public class ConsoleController : MonoBehaviour
{
    public GameObject targetObj;
    public float moveSpeed = 5.0f;

    private int state = -1;//状态
    private bool isPlayer1;
    private bool isPlayer2;
    private Rigidbody2D rb;
    private float moveX = 0f;
    private float moveY = 0f;

    private void Awake()
    {
        rb = targetObj.GetComponent<Rigidbody2D>();
    }
    private void Update()
    {


        if (isPlayer1)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                state *= -1;
                OnConsole(LevelManager.Instance.p1);
            }
        }

        if (isPlayer2)
        {
            if (Input.GetKeyDown(KeyCode.Keypad1))
            {
                state *= -1;
                OnConsole(LevelManager.Instance.p2);
            }
        }

        if(state == 1)
        {
            if (LevelManager.Instance.p1.GetComponent<PlayerController>().playerState == PlayerState.console)
            {
                moveX = moveY = 0f;

                if (Input.GetKey(KeyCode.W)) 
                {
                    moveY = 1f;
                }
                if (Input.GetKey(KeyCode.S)) 
                {
                    moveY = -1f;
                }
                if (Input.GetKey(KeyCode.A)) 
                {
                    moveX = -1f;
                }
                if (Input.GetKey(KeyCode.D)) 
                {
                    moveX = 1f;
                }
                Vector2 moveMent = new Vector2(moveX, moveY).normalized;
                if (moveMent.magnitude > 0)
                {
                    rb.velocity = moveSpeed * moveMent;
                }
                else
                {
                    rb.velocity = Vector2.zero;
                }
            }

            if (LevelManager.Instance.p2.GetComponent<PlayerController>().playerState == PlayerState.console)
            {

                moveX = moveY = 0f;
                if (Input.GetKey(KeyCode.UpArrow))
                {
                    moveY = 1f;
                }
                if (Input.GetKey(KeyCode.DownArrow))
                {
                    moveY = -1f;
                }
                if (Input.GetKey(KeyCode.LeftArrow))
                {
                    moveX = -1f;
                }
                if (Input.GetKey(KeyCode.RightArrow))
                {
                    moveX = 1f;
                }
                Vector2 moveMent = new Vector2(moveX, moveY).normalized;
                if(moveMent.magnitude > 0)
                {
                    rb.velocity = moveSpeed * moveMent;
                }
                else
                {
                    rb.velocity = Vector2.zero;
                }
            }
            Debug.Log(rb.velocity);
        }
    }

    private void OnConsole(GameObject player)
    {
        PlayerController playerController = player.GetComponent<PlayerController>();
        switch (state)
        {
            case -1:
                playerController.playerState = PlayerState.normal;
                targetObj.GetComponent<Rigidbody2D>().constraints |= RigidbodyConstraints2D.FreezePositionX;
                targetObj.GetComponent<Rigidbody2D>().constraints |= RigidbodyConstraints2D.FreezePositionY;
                
                break;
            case 1:
                playerController.playerState = PlayerState.console;
                targetObj.GetComponent<Rigidbody2D>().constraints &= ~RigidbodyConstraints2D.FreezePositionX;
                targetObj.GetComponent<Rigidbody2D>().constraints &= ~RigidbodyConstraints2D.FreezePositionY;
                break;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        if(collision.CompareTag("Player1"))
        {
            isPlayer1 = true;
            
        }
        if (collision.CompareTag("Player2"))
        {
            isPlayer2 = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player1"))
        {
            isPlayer1 = false;
        }

        if (collision.CompareTag("Player2"))
        {
            isPlayer2 = false;
        }
    }
}
