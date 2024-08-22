using UnityEngine;
using System.Collections;

public class RhythmAnalyzer : MonoBehaviour
{
    public AudioSource musicSource;
    public AudioSource metronomeSource;
    public AudioClip metronomeSound;
    public float threshold = 0.1f;
    public float[] samplesLeft = new float[512];
    public float[] samplesRight = new float[512];

    private float currentBeatTime = 0f;
    private float lastBeatTime = 0f;

    void Start()
    {
        if (musicSource == null)
        {
            musicSource = GetComponent<AudioSource>();
        }

        if (metronomeSource == null)
        {
            // ��Ʈ�γ� �Ҹ��� ���� ���ο� AudioSource�� �����մϴ�
            metronomeSource = gameObject.AddComponent<AudioSource>();
        }

        // ��Ʈ�γ� �Ҹ� ����
        if (metronomeSound != null)
        {
            metronomeSource.clip = metronomeSound;
        }
        else
        {
            Debug.LogWarning("��Ʈ�γ� �Ҹ��� �������� �ʾҽ��ϴ�!");
        }
    }

    void Update()
    {
        AnalyzeSpectrum();
    }

    void AnalyzeSpectrum()
    {
        musicSource.GetSpectrumData(samplesLeft, 0, FFTWindow.BlackmanHarris);
        musicSource.GetSpectrumData(samplesRight, 1, FFTWindow.BlackmanHarris);

        float sum = 0f;
        for (int i = 0; i < samplesLeft.Length; i++)
        {
            sum += samplesLeft[i] + samplesRight[i];
        }

        if (sum > threshold && Time.time > lastBeatTime + 0.5f)
        {
            OnBeat();
            lastBeatTime = Time.time;
        }

        currentBeatTime = Time.time - lastBeatTime;
    }

    void OnBeat()
    {
        Debug.Log("Beat detected!");
        PlayMetronomeSound();
    }

    void PlayMetronomeSound()
    {
        if (metronomeSource != null && metronomeSound != null)
        {
            metronomeSource.PlayOneShot(metronomeSound);
        }
    }

    public float GetCurrentBeatTime()
    {
        return currentBeatTime;
    }
}