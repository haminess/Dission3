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

    // bgm ����
    public AudioClip[] bgmClip;
    public float[] bgmHookTime; // ������������ bgm �źκк��� ���

    // effect ����
    public AudioClip[] effectClip;

    public Slider bgmslider;    // ����â �� �����̴�
    public Slider effectslider;

    public void Start()
    {
        // ������Ʈ ����
        AudioSource[] audioSources = GetComponentsInChildren<AudioSource>();
        bgm = audioSources[0];
        effect = audioSources[1];


        // ������ �ҷ�����
        DataManager.Instance.LoadSoundData();

        // �ʱ� �� ����
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
