using System.Collections;
using System.Collections.Generic;
using UnityEngine;
 
 /// <summary>
 ///注释
 /// <summary>

public class ConsoleSquare : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.CompareTag("Player1"))
        {
            LevelManager.Instance.p1.GetComponent<PlayerController>().rb.gravityScale = 0f;

        }
        if (collision.CompareTag("Player2"))
        {
            LevelManager.Instance.p2.GetComponent<PlayerController>().rb.gravityScale = 0f;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player1"))
        {
            LevelManager.Instance.p1.GetComponent<PlayerController>().rb.gravityScale = 4f;
        }

        if (collision.CompareTag("Player2"))
        {
            LevelManager.Instance.p2.GetComponent<PlayerController>().rb.gravityScale = 4f;
        }
    }
}
