using UnityEngine;
using DG.Tweening;
 
 /// <summary>
 ///注释
 /// <summary>

public class DrawBarController : MonoBehaviour
{
    public Sprite closeSprite;
    public Sprite openSprite;
    public Transform targetPos; //终止位置
    //public Vector3 initDoorPos; //机关原来的位置
    public Transform initDoorPos;
    public GameObject door;     //机关
    public float duration; //移动时间

    private int state = -1;//状态
    private bool isPlayer1;
    private bool isPlayer2;
    private SpriteRenderer spriteRenderer;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = closeSprite;    //默认初始为关闭图片
    }
    private void Update()
    {
        if(isPlayer1)
        {
            if(Input.GetKeyDown(KeyCode.E)) 
            {
                state *= -1;
                OpenLever();
            }
        }

        if (isPlayer2)
        {
            if (Input.GetKeyDown(KeyCode.Keypad1))
            {
                state *= -1;
                OpenLever();
            }
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
    
    private void OpenLever()
    {
        switch(state)
        {
            case 1:
                door.transform.DOMove(targetPos.position , duration).SetEase(Ease.InOutQuad);
                spriteRenderer.sprite = openSprite; //开启图片
                break;
            case -1:
                door.transform.DOMove(initDoorPos.position, duration).SetEase(Ease.InOutQuad);
                spriteRenderer.sprite = closeSprite;    //关闭图片
                break;
        }
    }
}
