using System.Collections;
using System.Collections.Generic;
using UnityEngine;
 
 /// <summary>
 ///注释
 /// <summary>

public class door : MonoBehaviour
{
    private bool isPlayer1;
    private bool isPlayer2;
    private int  canClearance = 0;
    private bool canOpenDoor;

    public ObjectEventSO GameWinEvent;

    private void Update()
    {
        if(isPlayer1 && canOpenDoor)
        {
            if(Input.GetKeyDown(KeyCode.E))
            {
                GameManager.Instance.p1.SetActive(false);
                canClearance++;
            }
        }
        
        if(isPlayer2 && canOpenDoor) 
        {
            if (Input.GetKeyDown(KeyCode.Keypad1))
            {
                GameManager.Instance.p2.SetActive(false);
                canClearance++;
            }
        }

        if(canClearance >= 2)
        {
            canClearance = 0;
            GameWinEvent.RaiseEvent(null, this);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player1"))
        {
            isPlayer1 = true;
        }
        if(collision.CompareTag("Player2"))
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

    public void OnGetKeyEvent()
    {
        canOpenDoor = true;
    }
}
