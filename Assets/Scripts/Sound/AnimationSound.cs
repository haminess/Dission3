using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationSound : MonoBehaviour
{
    [SerializeField] private bool playOnStart = false;
    [SerializeField] private AudioClip[] clips;

    private AudioSource audioSource;
    private Animator animator;

    [SerializeField] private float timer;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        audioSource = gameObject.AddComponent<AudioSource>();
        //audioSource.volume = FindObjectOfType<SoundManager>().effect.volume;
    }

    public void PlaySound()
    {
        if (clips.Length == 0) return;

        if(clips[0])
        {
            audioSource.clip = clips[0];
            audioSource.Play();
        }
    }

    public void PlaySoundByName(string _clipName)
    {
        if (clips.Length == 0) return;

        foreach (var clip in clips)
        {
            if (clip.name == _clipName)
            {
                audioSource.clip = clip;
                audioSource.Play();
            }

        }
    }

    public void PlaySoundByIndex(int _Idx)
    {
        if (0 <= _Idx && _Idx < clips.Length)
        {
            audioSource.clip = clips[_Idx];
            audioSource.Play();
        }
    }


    private void CheckAndPlaySoundOnFirstFrame()
    {
        AnimatorStateInfo info = animator.GetCurrentAnimatorStateInfo(0);

        timer += Time.deltaTime;
        if (timer > info.length)
        {
            timer = 0;
            PlaySound();
        }
    }
}
