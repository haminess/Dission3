using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public AudioSource BGM;
    public AudioSource Effect;

    public float bgm;
    public float effect;

    public void SetMusicVol(float bgm)
    {
        this.bgm = bgm;
        BGM.volume = this.bgm;
    }

    public void SetEffectVol(float effect)
    {
        this.effect = effect;
        Effect.volume = this.effect;
    }
}
