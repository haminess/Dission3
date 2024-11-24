using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class SoundManager : MonoBehaviour
{
    private SoundData sounddata => DataManager.Instance.sounddata;

    public AudioSource bgm;
    public AudioSource effect;
    
    public Slider bgmslider;    // ����â �� �����̴�
    public Slider effectslider;
    public float tempo1 = 4;
    public float tempo2 = 4;
    public float tempo = 1;

    public void Start()
    {
        // ������ �ҷ�����
        DataManager.Instance.LoadSoundData();

        // ���� ���� ����
        bgm.volume = sounddata.bgm;
        effect.volume = sounddata.effect;

        // ����â
        if (bgmslider)
        {
            bgmslider.value = bgm.volume;
            effectslider.value = effect.volume;
        }
    }


    public void SetBGM(string _name)
    {
        // ���Ͽ��� clip ã�� ����
        AudioClip clip = null;

        if(clip != null)
            bgm.clip = clip;
        else
            Debug.LogWarning($"SoundManager : {_name} ����� Ŭ���� �����ϴ�.");
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
        // ���Ͽ��� clip ã�� ����
        AudioClip clip = null;

        if (clip != null)
            effect.clip = clip;
        else
            Debug.LogWarning($"SoundManager : {_name} ����� Ŭ���� �����ϴ�.");
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

    // �����̴�
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


}
