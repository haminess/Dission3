using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

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

        // 초기 값 세팅
        if(GameObject.Find("Option"))
        {
            GameObject settingui = GameObject.Find("SettingUI");
            Slider[] slider = settingui.GetComponentsInChildren<Slider>();
            bgmslider = slider[0];
            effectslider = slider[1];

            bgmslider.value = sounddata.bgm;
            effectslider.value = sounddata.effect;
        }
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
}
