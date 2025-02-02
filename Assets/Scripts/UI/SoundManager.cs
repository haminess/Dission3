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

    // bgm ����
    public int bgmId = 0;
    public AudioClip[] bgmClip;
    public float[] bgmHookTime; // ������������ bgm �źκк��� ���
    public float[] bgmBpm;
    public float[] bgmTempo;
    public float[] bgmStartTime;

    // metronome
    public Metronome metro;
    public Connector connector;

    // effect ����
    public AudioClip[] effectClip;

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

    private void Update()
    {

    }

    public void SetBgm(int _num)
    {
        bgmId = _num;
        bgm.clip = bgmClip[_num];
    }
    public void SetBgm(string _name)
    {
        for(int i = 0; i < bgmClip.Length; i++)
        {
            if(bgmClip[i].name == _name)
            {
                bgmId = i;
                bgm.clip = bgmClip[i];
                return;
            }
        }
        print("SoundManager : �ش� ����� Ŭ���� �����ϴ�.");
    }
    public void SetBgm(AudioClip _clip)
    {
        for (int i = 0; i < bgmClip.Length; i++)
        {
            if (bgmClip[i].name == _clip.name)
            {
                bgmId = i;
                return;
            }
        }
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
                effect.clip = effectClip[i];
                return;
            }
        }
        print("SoundManager : �ش� ����� Ŭ���� �����ϴ�.");
    }
    public void SetEffect(AudioClip _clip)
    {
        effect.clip = _clip;
    }

    public void PlayEffect()
    {
        effect.Play();
    }
    public void PlayEffect(int _num)
    {
        SetEffect(_num);
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

    public void ConnectSoundManager()
    {
        // ���� ������ҽ� ����
        //bgm.Stop();
        //effect.Stop();

        // ����
        connector.FindManager();
        bgm = connector.soundMan.bgm;
        effect = connector.soundMan.effect;

        // ���� �� ������ҽ� ����
        //bgm.Stop();
        //effect.Stop();

    }

    public float GetBeatTime(int _bgm)
    {
        float sec = (60f / bgmBpm[_bgm]) * (tempo1 / tempo2);
        
        return sec;
    }

    public void PlayMetronome()
    {
        if (SceneManager.GetActiveScene().name != "MainGame")
        {
            return;
        }
        else
        {

        }
        print(bgmId + " " + bgmStartTime.Length);
        if(!bgm.isPlaying || bgm.time < bgmStartTime[bgmId] || metro.isMetroPlaying)
        {
            return;
        }
        metro.musicBPM = bgmBpm[bgmId];
        metro.MakeBeat();
        metro.PlayBeat();
        metro.isMetroPlaying = true;
    }

    public void StopBGM()
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
}
