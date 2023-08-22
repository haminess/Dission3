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

    public void Start()
    {
        DataManager.Instance.LoadSoundData();

        GameObject.Find("bgm sound").GetComponent<Slider>().value = sounddata.bgm;
        Debug.Log(sounddata.bgm);

        GameObject.Find("effect sound").GetComponent<Slider>().value = sounddata.effect;


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
