using UnityEngine;
using UnityEngine.UI;

public class HealthBarTimer : MonoBehaviour
{
    public Slider healthSlider; // ����HealthBar��Slider���
    public float maxTime = 60f; // ��ʱ��
    private float currentTime;

    void Start()
    {
        currentTime = maxTime;
        healthSlider.maxValue = maxTime;
        healthSlider.value = currentTime; // ��ʼ��Ѫ
    }

    void Update()
    {
        // ����ʱ�䲢������СֵΪ0
        currentTime = Mathf.Max(currentTime - Time.deltaTime, 0);
        healthSlider.value = currentTime;

        if (currentTime <= 0)
        {
            // ʱ�䵽0ʱ�Ĵ������紥����Ϸ����
            Debug.Log("ʱ��ľ���");
        }
    }

    //����Ѫ��/ʱ�䣨��ָ���ָ�����
    public void AddTime(float amount)
    {
        currentTime = Mathf.Clamp(currentTime + amount, 0, maxTime);
        healthSlider.value = currentTime; // ��������Ѫ��
    }
}