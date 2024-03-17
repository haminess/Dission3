using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class LNoteManager : MonoBehaviour
{
    Dictionary<int, string[]> explain = new Dictionary<int, string[]>();
    public TextMeshProUGUI tJudge;
    public TextMeshProUGUI tJudgeValue;
    public TextMeshProUGUI tExplain;

    public GameObject guidePrefab;
    public GameObject canvas;

    public TutorialMove player;
    public AudioSource bgm;
    public Metronome metronome;
    public GameObject note;
    public GameObject preNote;
    public Connector connector;

    float[][] chart = new float[100][];
    public float time;
    public int noteIndex;
    public int curNote;

    // 판정범위
    public float perfectRange = 0.05f;
    public float goodRange = 0.1f;
    public float badRange = 0.2f;
    public float missRange = 0.5f;
    public float synkRange = 0f;
    public float judgeRange = 0f;

    // tutorial 조건 달성
    public int isNext;

    public enum Step
    {
        Wait,
        Move,
        Note1,
        Note2,
        Note3,
        Note4,
        Synk,
        None
    }

    public Step step = Step.Wait;

    // Start is called before the first frame update
    void Start()
    {
        ResetTotal();
        ResetChart();
        chart[0] = new float[3] { 1f, 1, 0 };
        chart[1] = new float[3] { 2f, 2, 0 };
        chart[2] = new float[3] { 3f, 3, 0 };
        chart[3] = new float[3] { 4f, 4, 0 };
        chart[4] = new float[3] { 5f, 5, 0 };
        chart[5] = new float[3] { -1f, 6, 0 };
        chart[6] = new float[3] { -1f, 7, 0 };
        chart[7] = new float[3] { -1f, 8, 0 };
        chart[8] = new float[3] { 9f, 9, 0 };
        chart[9] = new float[3] { 10f, 10, 0 };
        chart[10] = new float[3] { 11f, 11, 0 };
        chart[11] = new float[3] { 12f, 12, 0 };
        chart[12] = new float[3] { 13f, 13, 0 };
        chart[13] = new float[3] { 14f, 14, 0 };
        chart[14] = new float[3] { 15f, 15, 0 };
    }

    // Update is called once per frame
    void Update()
    {
        switch (step)
        {
            case Step.Wait:
                break;

            case Step.Note4:
                RunTime();
                PlayerReposition();
                ShowNote(1);
                if (Input.anyKeyDown)
                {
                    Judge(time, player.CurPos.x, player.CurPos.y);
                }

                break;

            case Step.Synk:
                synkRange = connector.maingamedata.synk;
                judgeRange = connector.maingamedata.judge;
                RunTime();
                PlayerReposition();
                ShowNote(1 - synkRange, transform);
        
                if (Input.anyKeyDown)
                {
                    connector.UpdateData();
                    Judge(time + judgeRange);
                }
                break;
        }
    }


    public void SetStep(int _step)
    {
        if(step == (Step)_step)
        {
            step = Step.Wait;
        }
        else
        {
            step = (Step)_step;
        }
    }
    public void SetStep(Step _step)
    {
        step = _step;
    }

    IEnumerator ShowGuide(int _id)
    {
        GameObject guide = Instantiate(guidePrefab, canvas.transform);
        guide.GetComponent<Guide>().explain = explain[_id];
        while (true)
        {
            if(guide == null)
            {
                break;
            }
            yield return null;
        }
    }

    // 튜토리얼 흐름
    IEnumerator StepNote4()
    {
        yield return StartCoroutine(ShowGuide(4));
        tExplain.text = "박자에 맞춰 이동해보자";

        // 플레이
        // 채보4
        {
            // 초기화
            ResetChart();

            // 생성
            chart[0] = new float[3] { 1.0f, 1, 0 };
            chart[1] = new float[3] { 1.5f, 1, 1 };
            chart[2] = new float[3] { 2.0f, 2, 1 };
            chart[3] = new float[3] { 2.5f, 2, 0 };
            chart[4] = new float[3] { 3.0f, 2, -1 };
            chart[5] = new float[3] { 3.5f, 3, -1 };
            chart[6] = new float[3] { 4.0f, 3, 0 };
            chart[7] = new float[3] { 4.5f, 3, 1 };
            chart[8] = new float[3] { 5.0f, 4, 1 };
            chart[9] = new float[3] { 5.5f, 4, 0 };
            chart[10] = new float[3] { 6.0f, 4, -1 };
            chart[11] = new float[3] { 6.5f, 5, -1 };
            chart[12] = new float[3] { 7.0f, 5, 0 };
            chart[13] = new float[3] { 7.5f, 5, 1 };
            chart[14] = new float[3] { 8.0f, 6, 1 };
            chart[15] = new float[3] { 8.5f, 6, 0 };
            chart[16] = new float[3] { 9.0f, 6, -1 };
            chart[17] = new float[3] { 9.5f, 7, -1 };
            chart[18] = new float[3] { 10.0f, 7, 0 };
            chart[19] = new float[3] { 10.5f, 8, 0 };
        }

        bgm.Stop();
        player.enabled = true;
        PlayerReposition();
        yield return StartCoroutine(ShowNextNoteCo());
        yield return StartCoroutine(Count());

        // 시작
        step = Step.Note4;
    }
    IEnumerator FinishTutorial()
    {
        step = Step.Wait;
        yield return StartCoroutine(ShowGuide(5));
        step = Step.Wait;
        player.enabled = true;
        yield return new WaitForSeconds(5);
        SceneManager.LoadScene("TitleScene");
    }
    IEnumerator Count()
    {
        tJudge.text = "3";
        yield return new WaitForSeconds(1);
        tJudge.text = "2";
        yield return new WaitForSeconds(1);
        tJudge.text = "1";
        yield return new WaitForSeconds(1);
        tJudge.text = "Go!";
        yield return new WaitForSeconds(1);
        tJudge.text = "";
    }

    public void ShowNote(float _time, Transform _parent)
    {
        // 튜토리얼 채보는 1초 이상으로만 구성할 것.
        if (noteIndex < chart.Length - 1 && time > chart[noteIndex][0] - 1)
        {
            if (chart[noteIndex][0] == 0) return;
            if (step == Step.Note1) PlayerReposition();
            // 노트 생성
            GameObject note1 = Instantiate(note, _parent);
            note1.transform.localPosition = Vector3.zero;
            noteIndex++;

            // 박자 소리 출력
            Invoke("PlayBeat", _time);
        }
    }
    public void ShowNote(float _time, Vector3 _pos)
    {
        // 튜토리얼 채보는 1초 이상으로만 구성할 것.
        if (noteIndex < chart.Length - 1 && time > chart[noteIndex][0] - 1)
        {
            if (chart[noteIndex][0] == 0) return;
            if (step == Step.Note1) PlayerReposition();
            // 노트 생성
            GameObject note1 = Instantiate(note);
            note1.transform.position = _pos;
            noteIndex++;

            // 박자 소리 출력
            Invoke("PlayBeat", _time);  
        }
    }
    public void ShowNote(float _time)
    {
        ShowNote(_time, new Vector3(chart[noteIndex][1], chart[noteIndex][2], 0));
    }

    public void RunTime()
    {
        // 튜토리얼 시간
        time += Time.deltaTime;

        // miss 처리
        if (chart[curNote][0] != 0 &&
            time > (chart[curNote][0] + badRange + judgeRange))
        {
            curNote++;

            // miss 처리
            tJudge.text = "MISS";
            isNext = 0;
        }
    }

    public void PlayBeat()
    {
        metronome.metAudio.Play();
    }

    public void Judge(float _time, float _x, float _y)
    {
        if (step == Step.Move || step == Step.Wait) return; // 상태 확인
        if (chart[curNote][0] == 0) return; // 채보 끝이면 리턴
        if( _time < 0)
        {
            // 롱노트
            curNote++;
            return;
        }
        if (player.CurPos.x == chart[curNote][1] && player.CurPos.y == chart[curNote][2]) // 좌표 일치 확인
        {
            Judge(_time);
        }
        else
        {
            print("좌표 불일치");
        }
    }
    public void Judge(float _time)
    {
        if (tJudgeValue) tJudgeValue.text = ((time - (chart[curNote][0] + judgeRange)) * 1000).ToString("0") + "ms";
        // 판정시간 일치 확인
        if (time < (chart[curNote][0] + perfectRange + judgeRange) && time > (chart[curNote][0] - perfectRange + judgeRange))  // PERFECT
        {
            curNote++;
            isNext++;

            // judge
            tJudge.text = "PERFECT!";
        }
        else if (time < (chart[curNote][0] + goodRange + judgeRange) && time > (chart[curNote][0] - goodRange + judgeRange))   // GOOD
        {
            curNote++;
            isNext++;

            // judge
            tJudge.text = "GOOD";
        }
        else if (time < (chart[curNote][0] + badRange + judgeRange) && time > (chart[curNote][0] - badRange + judgeRange))    // BAD
        {
            curNote++;

            // judge
            tJudge.text = "BAD";
        }
        else if (time < (chart[curNote][0] + missRange + judgeRange) && time > (chart[curNote][0] - missRange + judgeRange))  // MISS
        {
            curNote++;
            isNext = 0;

            // judge
            tJudge.text = "MISS";
        }
        else
        {
            // 좌표는 일치하나 시간 범위에 맞지 않음
            print("좌표만 일치");
        }
    }

    public void PlayerReposition()
    {
        // 시작 위치 조정

        // 채보 중간이면 탈출
        if (chart[curNote][0] != 0 && curNote != 0) return;
        // 채보 끝났으면 초기화
        else if ( noteIndex != 0 && curNote == noteIndex && time > 3)
        {
            noteIndex = 0;
            curNote = 0;
            time = 0;
        }
        if (step == Step.Synk)
        {
            return;
        }

        Vector3 firstNote = new Vector3(
                chart[0][1],
                chart[0][2], 0);

        LayerMask mask = LayerMask.GetMask("Wall") | LayerMask.GetMask("Object");

        if (Physics2D.Raycast(firstNote, Vector3.up, 1, mask) == false)
        {
            firstNote += Vector3.left;
        }
        else if (Physics2D.Raycast(firstNote, Vector3.down, 1, mask) == false)
        {
            firstNote += Vector3.up;
        }
        else if (Physics2D.Raycast(firstNote, Vector3.left, 1, mask) == false)
        {
            firstNote += Vector3.right;
        }
        else if (Physics2D.Raycast(firstNote, Vector3.right, 1, mask) == false)
        {
            firstNote += Vector3.down;
        }

        player.CurPos = firstNote;
    }

    IEnumerator ShowNextNoteCo()
    {
        var note1 = Instantiate(preNote);
        note1.transform.position = new Vector3(chart[0][1], chart[0][2], 0);
        Destroy(note1, 3);
        yield return new WaitForSeconds(3);
    }

    public void ResetChart()
    {
        for (int i = 0; i < chart.Length; i++)
        {
            chart[i] = new float[3] { 0, 0, 0 };
        }
        noteIndex = 0;
        curNote = 0;
        time = 0;
    }
    public void ResetTotal()
    {
        time = 0;
        noteIndex = 0;
        curNote = 0;
        isNext = 0;
        //if(player != null) player.enabled = false;
        //CreateExplain();
    }
}
