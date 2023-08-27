using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class SoundManager : MonoBehaviour
{
    private SoundData sounddata => DataManager.Instance.sounddata;

    public AudioSource BGM;
    public AudioSource Effect;

    public Slider bgmslider;
    public Slider effectslider;

    public void Start()
    {
        // ������ �ҷ�����
        DataManager.Instance.LoadSoundData();

        Debug.Log(sounddata.bgm);   // ����� ��ݰ� (Ȯ�ο�)

        // �ʱ� �� ����
        bgmslider.value = sounddata.bgm;
        effectslider.value = sounddata.effect;


        //if (gameObject.name == "bgm sound")
        //{

        //    Debug.Log(sounddata.bgm);
        //}
        //else if (gameObject.name == "effect sound")
        //{
        //    GetComponent<Slider>().value = sounddata.effect;
        //    Debug.Log(sounddata.effect);
        //}
    }

    public void SetMusicVol(float bgm)
    {
        BGM.volume = bgm;
        sounddata.bgm = bgm;
    }

    public void SetEffectVol(float effect)
    {
        Effect.volume = effect;
        sounddata.effect = effect;
    }
}
