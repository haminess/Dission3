using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SoundManager : MonoBehaviour
{
    private SoundData sounddata => DataManager.Instance.sounddata;

    public AudioSource bgm;
    public AudioSource effect;

    // bgm 관리
    public AudioClip[] bgmClip;
    public float[] bgmHookTime; // 스테이지에서 bgm 훅부분부터 출력

    // effect 관리
    public AudioClip[] effectClip;

    public Slider bgmslider;    // 설정창 값 슬라이더
    public Slider effectslider;

    public void Start()
    {
        // 컴포넌트 참조
        AudioSource[] audioSources = GetComponentsInChildren<AudioSource>();
        bgm = audioSources[0];
        effect = audioSources[1];


        // 데이터 불러오기
        DataManager.Instance.LoadSoundData();

        // 로컬 음량 세팅
        bgm.volume = sounddata.bgm;
        effect.volume = sounddata.effect;

        // 설정창
        GameObject settingui = GameObject.Find("SettingUI");
        Slider[] slider = settingui.GetComponentsInChildren<Slider>();
        bgmslider = slider[0];
        effectslider = slider[1];
        bgmslider.value = bgm.volume;
        effectslider.value = effect.volume;
    }
    public void SetBgm(int _num)
    {
        bgm.clip = bgmClip[_num];
    }
    public void SetBgm(string _name)
    {
        for(int i = 0; i < bgmClip.Length; i++)
        {
            if(bgmClip[i].name == _name)
            {
                bgm.clip = bgmClip[i];
            }
        }
    }
    public void SetBgm(AudioClip _clip)
    {
        bgm.clip = _clip;
    }

    public void SetEffect(int _num)
    {
        effect.clip = effectClip[_num];
    }
    public void SetEffect(string _name)
    {
        for (int i = 0; i < effectClip.Length; i++)
        {
            if (effectClip[i].name == _name)
            {
                effect.clip = bgmClip[i];
            }
        }
    }
    public void SetEffect(AudioClip _clip)
    {
        effect.clip = _clip;
    }

    public void PlayEffect()
    {
        if(GameObject.Find("SoundManager"))
        {
            SoundManager soundMan = GameObject.Find("SoundManager").GetComponent<SoundManager>();
            soundMan.effect.Play();
            return;
        }

        effect.Play();
    }
    public void PlayEffect(int _num)
    {
        if (GameObject.Find("SoundManager"))
        {
            SoundManager soundMan = GameObject.Find("SoundManager").GetComponent<SoundManager>();
            soundMan.SetEffect(_num);
            soundMan.effect.Play();
            return;
        }

        SetEffect(_num);
        effect.Play();
    }
    public void PlayEffect(AudioClip _clip)
    {
        if (GameObject.Find("SoundManager"))
        {
            SoundManager soundMan = GameObject.Find("SoundManager").GetComponent<SoundManager>();
            soundMan.SetEffect(_clip);
            soundMan.effect.Play();
            return;
        }

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

        bgmslider.value = set;
        bgm.volume = set;
    }
    public void AddEffectVol(float _plus)
    {
        float set = this.effect.volume + _plus;
        if (set > 1)
            set = 1;
        else if (set < 0)
            set = 0;

        effectslider.value = set;
        effect.volume = set;
    }
    public void SubMusicVol(float _minus)
    {
        float set = this.bgm.volume - _minus;
        if (set > 1)
            set = 1;
        else if (set < 0)
            set = 0;

        bgmslider.value = set;
        bgm.volume = set;
    }
    public void SubEffectVol(float _minus)
    {
        float set = this.effect.volume - _minus;
        if (set > 1)
            set = 1;
        else if (set < 0)
            set = 0;

        effectslider.value = set;
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

    public void ConnectSoundManager()
    {
        if(GameObject.Find("SoundManager"))
        {
            SoundManager sm = GameObject.Find("SoundManager").GetComponent<SoundManager>();
            bgm = sm.bgm;
            effect = sm.effect;
        }
    }
}
