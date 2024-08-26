using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class MelodyExtractor : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioSource metronomeSource;
    public AudioClip metronomeSound;
    public int spectrumSize = 1024;
    public float[] spectrum;
    public float[] lastSpectrum;
    public float[] spectralFlux;
    public float sensitivity = 1.5f;
    public float minTempo = 80f;
    public float maxTempo = 160f;

    private List<float> onsetTimes = new List<float>();
    private float[] tempoScores;
    private float estimatedTempo;

    void Start()
    {
        spectrum = new float[spectrumSize];
        lastSpectrum = new float[spectrumSize];
        spectralFlux = new float[spectrumSize / 2];
        tempoScores = new float[Mathf.CeilToInt(maxTempo - minTempo) + 1];

        if (metronomeSource == null)
        {
            metronomeSource = gameObject.AddComponent<AudioSource>();
        }

        if (metronomeSound != null)
        {
            metronomeSource.clip = metronomeSound;
        }
        else
        {
            Debug.LogWarning("메트로놈 소리가 설정되지 않았습니다!");
        }
    }

    void Update()
    {
        AnalyzeAudio();
    }

    void AnalyzeAudio()
    {
        audioSource.GetSpectrumData(spectrum, 0, FFTWindow.BlackmanHarris);

        CalculateSpectralFlux();
        DetectOnsets();
        EstimateTempo();

        System.Array.Copy(spectrum, lastSpectrum, spectrum.Length);
    }

    void CalculateSpectralFlux()
    {
        for (int i = 0; i < spectralFlux.Length; i++)
        {
            float diff = spectrum[i] - lastSpectrum[i];
            spectralFlux[i] = diff > 0 ? diff : 0;
        }
    }

    void DetectOnsets()
    {
        float sum = 0;
        float mean = 0;
        float stdDev = 0;

        // Calculate mean
        for (int i = 0; i < spectralFlux.Length; i++)
        {
            sum += spectralFlux[i];
        }
        mean = sum / spectralFlux.Length;

        // Calculate standard deviation
        sum = 0;
        for (int i = 0; i < spectralFlux.Length; i++)
        {
            sum += (spectralFlux[i] - mean) * (spectralFlux[i] - mean);
        }
        stdDev = Mathf.Sqrt(sum / spectralFlux.Length);

        // Detect onset
        float threshold = mean + stdDev * sensitivity;
        if (spectralFlux[0] > threshold && Time.time - (onsetTimes.Count > 0 ? onsetTimes[onsetTimes.Count - 1] : 0) > 0.1f)
        {
            onsetTimes.Add(Time.time);
            PlayMetronomeSound();
        }
    }

    void EstimateTempo()
    {
        if (onsetTimes.Count < 4) return;

        for (int i = 0; i < tempoScores.Length; i++)
        {
            tempoScores[i] = 0;
        }

        for (int i = 1; i < onsetTimes.Count; i++)
        {
            float intervalSeconds = onsetTimes[i] - onsetTimes[i - 1];
            float bpm = 60f / intervalSeconds;

            if (bpm >= minTempo && bpm <= maxTempo)
            {
                int index = Mathf.RoundToInt(bpm - minTempo);
                tempoScores[index]++;
            }
        }

        int maxIndex = System.Array.IndexOf(tempoScores, tempoScores.Max());
        estimatedTempo = minTempo + maxIndex;

        Debug.Log($"Estimated Tempo: {estimatedTempo} BPM");
    }

    void PlayMetronomeSound()
    {
        if (metronomeSource != null && metronomeSound != null)
        {
            metronomeSource.PlayOneShot(metronomeSound);
        }
    }
}