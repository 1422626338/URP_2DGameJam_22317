using UnityEngine;
using UnityEngine.Rendering.Universal;

public class BreathingLight : MonoBehaviour
{
    [Header("��������")]
    public float minIntensity = 0.8f;  // ��С����
    public float maxIntensity = 1.5f;    // �������
    public float speed = 1.5f;          // �����ٶ�
    public Color minColor = Color.yellow;
    public Color maxColor = Color.red;

    [Header("�ߴ����")]
    public float minRadius = 1f;      // ��С�뾶
    public float maxRadius = 3f;      // ���뾶
    public bool enableSizeChange = true; // �Ƿ����óߴ�仯

    private Light2D targetLight;
    private float currentTime;

    void Start()
    {
        targetLight = GetComponent<Light2D>();
        currentTime = Random.Range(0f, 2f * Mathf.PI);

        // �Զ�����Դ����
        if (targetLight.lightType != Light2D.LightType.Point)
        {
            //���޵��Դ
            enableSizeChange = false;
        }
    }

    void Update()
    {
        float lerpValue = (Mathf.Sin(currentTime * speed) + 1f) / 2f;

        // ���ȿ���
        targetLight.intensity = Mathf.Lerp(minIntensity, maxIntensity, lerpValue);

        // ��ɫ����
        targetLight.color = Color.Lerp(minColor, maxColor, lerpValue);

        // �뾶���ƣ����޵��Դ��
        if (enableSizeChange && targetLight.lightType == Light2D.LightType.Point)
        {
            targetLight.pointLightOuterRadius = Mathf.Lerp(minRadius, maxRadius, lerpValue);
        }

        currentTime += Time.deltaTime;
    }
}