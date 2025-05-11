using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ButtonInteraction : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler
{
    public Image buttonImage; // Image����   
    public float hoverScale = 1.2f;

    private Vector3 originalScale; // �洢ԭʼ����

    void Start()
    {
        originalScale = buttonImage.transform.localScale; // ��¼ԭʼ����
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        buttonImage.transform.localScale = originalScale * hoverScale; // ����ͼƬ
        if (AudioController.Instance != null)
        {
            AudioController.Instance.PlaySFX(AudioController.Instance.Hover);
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        buttonImage.transform.localScale = originalScale; // �ָ�ԭʼ����
        
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (AudioController.Instance != null)
        {
            AudioController.Instance.PlaySFX(AudioController.Instance.Confirm);
        }
    }
}