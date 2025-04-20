using System.Collections;
using System.Collections.Generic;
using UnityEngine;
 
 /// <summary>
 ///注释
 /// <summary>

public class key : MonoBehaviour
{
    private SpriteRenderer sprite;
    private bool isPlayer2;

    public ObjectEventSO getKeyEvent;
    private void Awake()
    {
        sprite = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        if(isPlayer2)
        {
            if (Input.GetKeyDown(KeyCode.Keypad1))
            {
                sprite.enabled = false;
                getKeyEvent.RaiseEvent(null, this);
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player2"))
        {
            isPlayer2 = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.CompareTag("Player2"))
        {
            isPlayer2 = false;
        }
    }
}
