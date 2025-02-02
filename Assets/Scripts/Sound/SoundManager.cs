using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
using Ookii.Dialogs;
using static ButtonSound;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TextBox;
using UnityEngine.Timeline;

public class SoundManager : MonoBehaviour
{

    private static SoundManager instance;
    public static SoundManager Instance
    {
        get
        {
            if (instance == null)
            {
                // 1. 먼저 씬에서 찾아보기
                instance = FindObjectOfType<SoundManager>();

                // 2. 씬에도 없다면 생성
                if (instance == null)
                {
                    GameObject container = new GameObject("SoundManager");
                    instance = container.AddComponent<SoundManager>();
                    instance.bgm = instance.AddComponent<AudioSource>();
                    instance.effect = instance.AddComponent<AudioSource>();
                }

                DontDestroyOnLoad(instance.gameObject);
            }
            return instance;
        }
    }


    private SoundData sounddata => DataManager.Instance.sounddata;

    [HideInInspector] public AudioSource bgm;
    [HideInInspector] public AudioSource effect;

    [Header("Button Sound")]
    [SerializeField] private AudioClip hoverSound;
    [SerializeField] private AudioClip clickSound;
    [SerializeField] private AudioClip cancelHoverSound;
    [SerializeField] private AudioClip cancelClickSound;
    [SerializeField] private AudioClip confirmHoverSound;
    [SerializeField] private AudioClip confirmClickSound;

    public void Start()
    {
        // 오디오 소스 생성
        GameObject bgmObj = new GameObject("BGM");
        GameObject effObj = new GameObject("Effect");
        bgm = bgmObj.AddComponent<AudioSource>();
        effect = effObj.AddComponent<AudioSource>();
        bgmObj.transform.SetParent(transform);
        effObj.transform.SetParent(transform);

        // 데이터 불러오기
        DataManager.Instance.LoadSoundData();

        // 로컬 음량 세팅
        bgm.volume = sounddata.bgm;
        effect.volume = sounddata.effect;



        // 매니저에서 디폴트 효과음 일괄 적용
        ButtonSound[] buttons = GameObject.FindObjectsOfType<ButtonSound>();

        foreach(ButtonSound bs in buttons)
        {
            AudioSource AS = bs.GetComponent<AudioSource>();
            AS.volume = effect.volume;

        switch (bs.ButtonType)
            {
                case ButtonSoundType.Default:
                    bs.HoverSound = hoverSound;
                    bs.ClickSound = clickSound;
                    break;
                case ButtonSoundType.Confirm:
                    bs.HoverSound = confirmHoverSound;
                    bs.ClickSound = confirmClickSound;
                    break;
                case ButtonSoundType.Cancel:
                    bs.HoverSound = cancelHoverSound;
                    bs.ClickSound = cancelClickSound;
                    break;
                case ButtonSoundType.Custom:
                    if(bs.HoverSound == null) bs.HoverSound = hoverSound;
                    if(bs.ClickSound == null) bs.ClickSound = clickSound;
                    break;
            }
        }
    }


    public void SetBGM(string _name)
    {
        // 파일에서 clip 찾아 실행
        AudioClip clip = null;

        if(clip != null)
            bgm.clip = clip;
        else
            Debug.LogWarning($"SoundManager : {_name} 오디오 클립이 없습니다.");
    }
    public void SetBGM(AudioClip _clip)
    {
        bgm.clip = _clip;
    }

    public void PlayBGM()
    {
        if (bgm.isPlaying)
        {
            return;
        }
        else
        {
            bgm.Play();
        }
    }
    public void SwitchBGM()
    {
        if (bgm.isPlaying)
        {
            bgm.Stop();
        }
        else
        {
            bgm.Play();
        }
    }

    public void PauseBGM()
    {
        bgm.Pause();
    }

    public void StopBGM()
    {
        bgm.Stop();
    }

    public void SetEffect(string _name)
    {
        // 파일에서 clip 찾아 실행
        AudioClip clip = null;

        if (clip != null)
            effect.clip = clip;
        else
            Debug.LogWarning($"SoundManager : {_name} 오디오 클립이 없습니다.");
    }
    public void SetEffect(AudioClip _clip)
    {
        effect.clip = _clip;
    }

    public void PlayEffect()
    {
        effect.Play();
    }

    public void PlayEffect(AudioClip _clip)
    {
        SetEffect(_clip);
        effect.Play();
    }

    public void SetMusicVol(float bgm)
    {
        this.bgm.volume = bgm;
        sounddata.bgm = bgm;
    }

    public void SetEffectVol(float effect)
    {
        this.effect.volume = effect;
        sounddata.effect = effect;
    }

    // 슬라이더
    public void AddMusicVol(float _plus)
    {
        float set = this.bgm.volume + _plus;
        if (set > 1)
            set = 1;
        else if (set < 0)
            set = 0;

        bgm.volume = set;
    }
    public void AddEffectVol(float _plus)
    {
        float set = this.effect.volume + _plus;
        if (set > 1)
            set = 1;
        else if (set < 0)
            set = 0;

        effect.volume = set;
    }
    public void SubMusicVol(float _minus)
    {
        float set = this.bgm.volume - _minus;
        if (set > 1)
            set = 1;
        else if (set < 0)
            set = 0;

        bgm.volume = set;
    }
    public void SubEffectVol(float _minus)
    {
        float set = this.effect.volume - _minus;
        if (set > 1)
            set = 1;
        else if (set < 0)
            set = 0;

        effect.volume = set;
    }

    public void ShowValue(TextMeshProUGUI _text)
    {
        if(_text.name == "bgmvolume")
        {
            _text.text = (bgm.volume * 100).ToString("000");
        }
        if (_text.name == "effectvolume")
        {
            _text.text = (effect.volume * 100).ToString("000");
        }
    }


}
