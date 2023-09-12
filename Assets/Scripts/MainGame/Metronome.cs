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
    public float startSec = 0;

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
        sec = (stdBPM / musicBPM) * (tempo1 / tempo2);
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

        //// 메트로놈 기능
        //else if (!isMetroPlaying && startSec < MainGame.instance.bgm.time)
        //{
        //    isMetroPlaying = true;

        //    // 애니메이션 리듬 맞추기
        //    Animator animator = MainGame.instance.player.GetComponentInChildren<Animator>();
        //    animator.Rebind();
        //    animator.speed = 1.0f / sec;

        //    StartCoroutine(Play());


        //    startMetronome = false;
        //}
    }

    IEnumerator Play()
    {
        sec = (stdBPM / musicBPM) * (tempo1 / tempo2);

        // 
        // 마디 개수
        // 마디 초

        // 메트로놈
        if (isMetroPlaying)
        {
            metro.Play();
            MakeTic();
            yield return new WaitForSeconds(sec);
            StartCoroutine(Play());
        }
        else
        {
            yield return null;
        }
    }

    void MakeTic()
    {
        // 박자 생성
        GameObject tempTic = Instantiate(ticPrefab, canvas.transform);
        tempTic.transform.SetParent(canvas.transform, false);
        Destroy(tempTic, sec * (tempo1 - tic) - (sec / 2));

        if ((int)tempo1 % 2 == 1)
        {
            // 홀 박자
            tempTic.transform.localPosition = new Vector2( ((int)tempo1 / 2) * -60 + (tic * 60), 0); // 시작위치 + 60 간격
        }
        else
        {
            // 짝 박자
            tempTic.transform.localPosition = new Vector2(((int)tempo1 / 2) * -60 + (tic * 60) + 30, 0); // 시작위치 + 60 간격 + 30
        }

        // 박자 카운트
        tic++;

        // 박자 다 나오면 리셋
        if(tic >= tempo1)
        {
            tic = 0;
        }
    }
}
