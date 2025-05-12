using UnityEngine;
using UnityEngine.Rendering.Universal;

public class BreathingLight : MonoBehaviour
{
    [Header("呼吸参数")]
    public float minIntensity = 0.8f;  // 最小亮度
    public float maxIntensity = 1.5f;    // 最大亮度
    public float speed = 1.5f;          // 呼吸速度
    public Color minColor = Color.yellow;
    public Color maxColor = Color.red;

    [Header("尺寸参数")]
    public float minRadius = 1f;      // 最小半径
    public float maxRadius = 3f;      // 最大半径
    public bool enableSizeChange = true; // 是否启用尺寸变化

    private Light2D targetLight;
    private float currentTime;

    void Start()
    {
        targetLight = GetComponent<Light2D>();
        currentTime = Random.Range(0f, 2f * Mathf.PI);

        // 自动检测光源类型
        if (targetLight.lightType != Light2D.LightType.Point)
        {
            //仅限点光源
            enableSizeChange = false;
        }
    }

    void Update()
    {
        float lerpValue = (Mathf.Sin(currentTime * speed) + 1f) / 2f;

        // 亮度控制
        targetLight.intensity = Mathf.Lerp(minIntensity, maxIntensity, lerpValue);

        // 颜色控制
        targetLight.color = Color.Lerp(minColor, maxColor, lerpValue);

        // 半径控制（仅限点光源）
        if (enableSizeChange && targetLight.lightType == Light2D.LightType.Point)
        {
            targetLight.pointLightOuterRadius = Mathf.Lerp(minRadius, maxRadius, lerpValue);
        }

        currentTime += Time.deltaTime;
    }
}