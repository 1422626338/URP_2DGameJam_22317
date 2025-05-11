using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioController : MonoBehaviour
{
    //单例模式
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
    public AudioClip pulldown;  //机关的开启和关闭
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

    public void PlaySFX(AudioClip clip) //在其他的脚本中调用
    {
        SFXAudio.PlayOneShot(clip);
    }
}
