using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MachineSwitch : MonoBehaviour
{

    public Animator anim;

    private void Start()
    {
        anim = GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player1")&&Input.GetKeyDown(KeyCode.J))
        {

        }
    }
}
