using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Energy : MonoBehaviour
{
    public bool canRevive;
    public float reviveTimeCount;
    public float healTime;
    private bool isPlayer1;
    private bool isUsed = false;
    private float curReviveTimeCount;

    private SpriteRenderer spriteRenderer;

    private void Awake()
    {
        curReviveTimeCount = reviveTimeCount;
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        if (isPlayer1 && !isUsed)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                var level = GetComponentInParent<BaseLevel>();
                // 添加数值上限限制
                level.curTimeCount = Mathf.Min(level.curTimeCount + healTime, level.timeCount);

                if (AudioController.Instance != null)
                {
                    AudioController.Instance.PlaySFX(AudioController.Instance.heal);
                }
                isUsed = true;
                spriteRenderer.enabled = false;
            }
        }

        if (canRevive && isUsed)
        {
            curReviveTimeCount -= Time.deltaTime;
            if (curReviveTimeCount < 0)
            {
                isUsed = false;
                spriteRenderer.enabled = true;
                curReviveTimeCount = reviveTimeCount;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player1"))
        {
            isPlayer1 = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player1"))
        {
            isPlayer1 = false;
        }
    }
}