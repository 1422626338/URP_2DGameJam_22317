using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioController : MonoBehaviour
{
    //����ģʽ
    public static AudioController Instance;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }


    [SerializeField] AudioSource BGMAudio;
    [SerializeField] AudioSource SFXAudio;

    public AudioClip bgm;
    public AudioClip death;
    public AudioClip decline;
    public AudioClip pickUpKey;
    public AudioClip useIt;
    public AudioClip panelUp;
    public AudioClip panelDown;
    public AudioClip pulldown;  //���صĿ����͹ر�
    public AudioClip heal;
    public AudioClip jump;

    [Header("UI")]
    public AudioClip Hover;
    public AudioClip Confirm;

    private void Start()
    {
        BGMAudio.clip = bgm;
        BGMAudio.Play();
    }

    public void PlaySFX(AudioClip clip) //�������Ľű��е���
    {
        SFXAudio.PlayOneShot(clip);
    }
}
