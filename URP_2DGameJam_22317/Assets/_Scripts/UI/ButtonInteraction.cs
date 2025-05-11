using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ButtonInteraction : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler
{
    public Image buttonImage; // Image类型   
    public float hoverScale = 1.2f;

    private Vector3 originalScale; // 存储原始缩放

    void Start()
    {
        originalScale = buttonImage.transform.localScale; // 记录原始缩放
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        buttonImage.transform.localScale = originalScale * hoverScale; // 缩放图片
        if (AudioController.Instance != null)
        {
            AudioController.Instance.PlaySFX(AudioController.Instance.Hover);
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        buttonImage.transform.localScale = originalScale; // 恢复原始缩放
        
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (AudioController.Instance != null)
        {
            AudioController.Instance.PlaySFX(AudioController.Instance.Confirm);
        }
    }
}