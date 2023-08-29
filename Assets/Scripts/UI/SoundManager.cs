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
            Transform settingui = GameObject.Find("SettingUI").transform.GetChild(0);
            settingui.gameObject.SetActive(true);
            Slider[] slider = settingui.GetComponentsInChildren<Slider>();
            bgmslider = slider[0];
            effectslider = slider[1];

            bgmslider.value = sounddata.bgm;
            effectslider.value = sounddata.effect;
            settingui.gameObject.SetActive(false);
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
