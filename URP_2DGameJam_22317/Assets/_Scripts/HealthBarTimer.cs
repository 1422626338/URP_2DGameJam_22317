using UnityEngine;
using UnityEngine.UI;

public class HealthBarTimer : MonoBehaviour
{
    public Slider healthSlider; // 拖入HealthBar的Slider组件
    public float maxTime = 60f; // 总时长
    private float currentTime;

    void Start()
    {
        currentTime = maxTime;
        healthSlider.maxValue = maxTime;
        healthSlider.value = currentTime; // 初始满血
    }

    void Update()
    {
        // 更新时间并限制最小值为0
        currentTime = Mathf.Max(currentTime - Time.deltaTime, 0);
        healthSlider.value = currentTime;

        if (currentTime <= 0)
        {
            // 时间到0时的处理，例如触发游戏结束
            Debug.Log("时间耗尽！");
        }
    }

    //增加血量/时间（可指定恢复量）
    public void AddTime(float amount)
    {
        currentTime = Mathf.Clamp(currentTime + amount, 0, maxTime);
        healthSlider.value = currentTime; // 立即更新血条
    }
}