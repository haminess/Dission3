using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Metronome : MonoBehaviour
{

    AudioSource metro;

    public float stdBPM = 60f;
    public float musicBPM = 60f;
    public float tempo1 = 4;
    public float tempo2 = 4;

    public float sec;

    public bool startMetronome = false;
    public bool isMetroPlaying = false;

    public GameObject ticPrefab;
    public GameObject canvas;
    public int tic = 0;

    // Start is called before the first frame update
    void Start()
    {
        metro = GetComponent<AudioSource>();
        tic = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (startMetronome)
        {
            isMetroPlaying = true;
            StartCoroutine(Play());
            startMetronome = false;
        }
    }

    IEnumerator Play()
    {
        sec = (stdBPM / musicBPM) * (tempo1 / tempo2);
        if (isMetroPlaying)
        {
            metro.Play();
            ShowMetronome();
            yield return new WaitForSeconds(sec);
            StartCoroutine(Play());
        }
        else
        {
            yield return null;
        }
    }

    void ShowMetronome()
    {
        GameObject tempTic = Instantiate(ticPrefab, canvas.transform);
        tempTic.transform.SetParent(canvas.transform, false);
        Destroy(tempTic, sec * (tempo1 - tic) - (sec / 2));

        if ((int)tempo1 % 2 == 1)
        {
            // Ȧ ����
            tempTic.transform.localPosition = new Vector2( ((int)tempo1 / 2) * -60 + (tic * 60), 0); // ������ġ + 60 ����
        }
        else
        {
            // ¦ ����
            tempTic.transform.localPosition = new Vector2(((int)tempo1 / 2) * -60 + (tic * 60) + 30, 0); // ������ġ + 60 ���� + 30
        }

        tic++;
        if(tic >= tempo1)
        {
            tic = 0;
        }
    }
}