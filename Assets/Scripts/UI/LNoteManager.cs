using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class LNoteManager : MonoBehaviour
{
    // 싱글톤
    public static LNoteManager lInstance;

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
    public GameObject routeNote;
    public GameObject lRouteNote;

    // 상태 변수
    float[][] chart = new float[100][];
    public float time;
    public int scidx;   // show chart idx
    public int jcidx;   // judge chart idx

    // 판정범위
    public float perfectRange = 0.05f;
    public float goodRange = 0.1f;
    public float badRange = 0.2f;
    public float missRange = 0.5f;
    public float synkRange = 0f;
    public float judgeRange = 0f;

    // 설정 상수
    const int ctime = 1;
    const int cx = 1;
    const int cy = 1;
    const int cllen = 3;
    const int cldir = 4;

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

        //// 생성
        //chart[0] = new float[5] { 1, 1, 0, 0, 0 };
        //chart[1] = new float[5] { 2, 2, 0, 0, 0 };
        //chart[2] = new float[5] { 3, 3, 0, 0, 0 };
        //chart[3] = new float[5] { 4, 4, 0, 0, 0 };
        //chart[4] = new float[5] { 5, 5, 0, 4, 3 };
        //chart[5] = new float[5] { 9, 9, 0, 0, 0 };
        //chart[6] = new float[5] { 10, 10, 0, 0, 0 };
        //chart[7] = new float[5] { 11, 11, 0, 0, 0 };
        //chart[8] = new float[5] { 12, 12, 0, 0, 0 };
        //chart[9] = new float[5] { 13, 13, 0, 5, 3 };

        chart[0] = new float[5] { 1, 1, 0, 5, 3 };
        chart[1] = new float[5] { 3, 6, 0, 4, 3 };
        chart[2] = new float[5] { 5, 10, 0, 3, 3 };
        chart[3] = new float[5] { 7, 13, 0, 2, 3 };
        chart[4] = new float[5] { 9, 15, 0, 1, 3 };
        chart[5] = new float[5] { 10, 16, 0, 1, 3 };
        chart[6] = new float[5] { 11, 17, 0, 2, 3 };
        chart[7] = new float[5] { 13, 19, 0, 3, 3 };
        chart[8] = new float[5] { 15, 22, 0, 4, 3 };
        chart[9] = new float[5] { 17, 26, 0, 5, 3 };
    }

    // Update is called once per frame
    void Update()
    {
        switch (step)
        {
            case Step.Wait:
                break;

            case Step.Note4:
                PlayerReposition();
                ShowNote();
                RunTime();
                if (Input.anyKeyDown)
                {
                    Judge(time, player.CurPos.x, player.CurPos.y);
                }

                break;

            case Step.Synk:
                //synkRange = connector.maingamedata.synk;
                //judgeRange = connector.maingamedata.judge;
                RunTime();
                PlayerReposition();
                ShowNote(1 - synkRange, transform);
        
                if (Input.anyKeyDown)
                {
                    //connector.UpdateData();
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
        // 플레이
        // 채보4
        {
            // 초기화
            ResetChart();

            // 생성
            chart[0] = new float[5] { 1, 1, 0, 0, 0 };
            chart[1] = new float[5] { 2, 2, 0, 0, 0 };
            chart[2] = new float[5] { 3, 3, 0, 0, 0 };
            chart[3] = new float[5] { 4, 4, 0, 0, 0 };
            chart[4] = new float[5] { 5, 5, 0, 4, 3 };
            chart[5] = new float[5] { 9, 9, 0, 0, 0 };
            chart[6] = new float[5] { 10, 10, 0, 0, 0 };
            chart[7] = new float[5] { 11, 11, 0, 0, 0 };
            chart[8] = new float[5] { 12, 12, 0, 0, 0 };
            chart[9] = new float[5] { 13, 13, 0, 5, 3 };
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
        if (scidx < chart.Length - 1 && time > chart[scidx][ctime] - 1)
        {
            if (chart[scidx][ctime] == 0) return;
            if (step == Step.Note1) PlayerReposition();
            // 노트 생성
            GameObject note1 = Instantiate(note, _parent);
            note1.transform.localPosition = Vector3.zero;
            scidx++;

            // 박자 소리 출력
            Invoke("PlayBeat", _time);
        }
    }
    public void ShowNote(float _time, Vector3 _pos)
    {
        // 튜토리얼 채보는 1초 이상으로만 구성할 것.
        if (scidx < chart.Length - 1 && time > chart[scidx][0] - 1)
        {
            if (chart[scidx][0] == 0) return;
            if (step == Step.Note1) PlayerReposition();
            // 노트 생성
            GameObject note1 = Instantiate(note);
            note1.transform.position = _pos;
            scidx++;

            // 박자 소리 출력
            Invoke("PlayBeat", _time);  
        }
    }
    public void ShowNote(float _time)
    {
        ShowNote(_time, new Vector3(chart[scidx][1], chart[scidx][2], 0));
    }

    IEnumerator ShowNextNoteCo()
    {
        var note1 = Instantiate(preNote);
        note1.transform.position = new Vector3(chart[0][1], chart[0][2], 0);
        Destroy(note1, 3);
        yield return new WaitForSeconds(3);
    }
    void MakeNote()
    {
        var newnote = Instantiate(note);
        newnote.name = "note" + scidx.ToString();
        newnote.transform.position = new Vector3(chart[scidx][1], chart[scidx][2], 0);

        // long note
        if (chart[scidx][3] > 1)
        {
            newnote.transform.Rotate(Vector3.forward * 90 * chart[scidx][4]);

            LNote longnote = newnote.GetComponent<LNote>();

            longnote.ltype = true;
            longnote.length = (int)chart[scidx][3];
            longnote.s_time = chart[scidx][0] - time;
            longnote.e_time = chart[scidx + 1][0] - 1 - time;
            Vector3 head = Vector3.up;
            switch (chart[scidx][4])
            {
                case 0:
                    head = Vector3.up;
                    break;  
                case 1:
                    head = Vector3.left;
                    break;
                case 2:
                    head = Vector3.down;
                    break;
                case 3:
                    head = Vector3.right;
                    break;
            }
            longnote.head = head;
        }

        scidx++;
    }
    GameObject MakeRoute(int _index)
    {
        // 채보노트 끝나면 리턴
        if (_index >= chart.Length || chart[_index][0] == 0) return null;

        GameObject route = null;

        // 롱노트
        if (chart[_index][3] > 1)
        {
            route = Instantiate(lRouteNote);

            // 길이 조정
            route.GetComponent<SpriteRenderer>().size = new Vector2(1, chart[_index][3]); 

            // 각도 조정
            Vector3 rot = Vector3.forward * 90 * chart[_index][4];
            route.transform.Rotate(rot);

            // 위치 조정
            Vector2 pos = new Vector2(chart[_index][1], chart[_index][2]);
            switch (chart[_index][4])
            {
                case 0:
                    pos += Vector2.down * 0.5f;
                    break;
                case 1:
                    pos += Vector2.right * 0.5f;
                    break;
                case 2:
                    pos += Vector2.up * 0.5f;
                    break;
                case 3:
                    pos += Vector2.left * 0.5f;
                    break;
            }
            route.transform.position = pos;

            // 삭제될 시간 = 판정시간 - 현재시간 (판정될때 사라짐)
            if(chart[_index + 1][0] != 0)
                Destroy(route, chart[_index + 1][0] - 1 - time);
            else
                Destroy(route, chart[_index][0] - time);
        }
        // 숏노트
        else
        {
            // route 생성
            route = Instantiate(routeNote);

            // route 위치 지정
            route.transform.position = new Vector2(chart[_index][1], chart[_index][2]);

            // 삭제될 시간 = 판정시간 - 현재시간 (판정될때 사라짐)
            Destroy(route, chart[_index][0] - time);
        }

        return route;

    }
    // 채보 노트 생성
    void ShowNote()
    {
        // 노트 끝나면 리턴
        //if (noteIndex > chart.Length - 1) return;
        if (scidx > 0 && chart[scidx][0] == 0) return;

        // 처음 경로 4칸 띄우기
        if (time == 0 && !GameObject.Find("route"))
        {
            GameObject rt = MakeRoute(0);
            if (rt) rt.name = "route";
            MakeRoute(1).name = "route" + 1;
            MakeRoute(2).name = "route" + 2;
            MakeRoute(3).name = "route" + 3;
        }

        // 노트가 1초 내인 경우만 따로 처리
        if (chart[scidx][0] - 1 - synkRange < 0)
        {
            if (time > chart[scidx][0] - 1)
            {
                MakeNote();
            }
            return;
        }

        // 현재 시간이 시작시간 이후로 데이터 시간이 지나면 생성
        else if (time > chart[scidx][0] - 1 - synkRange)       
        {
            // 4칸 앞 경로 띄우기
            if (scidx < chart.Length - 4)
            {
                GameObject rt = MakeRoute(scidx + 4);
                if (rt) rt.name = "route" + (scidx + 4);
            }

            // 실제 노트 뿌리기
            MakeNote();
        }
    }

    public void RunTime()
    {
        // 튜토리얼 시간
        time += Time.deltaTime;

        // miss 처리
        if (chart[jcidx][0] != 0 &&
            time > (chart[jcidx][0] + badRange + judgeRange))
        {
            jcidx++;

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
        if (chart[jcidx][0] == 0) return; // 채보 끝이면 리턴
        if( _time < 0)
        {
            // 롱노트
            jcidx++;
            return;
        }
        if (player.CurPos.x == chart[jcidx][1] && player.CurPos.y == chart[jcidx][2]) // 좌표 일치 확인
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
        if (tJudgeValue) tJudgeValue.text = ((time - (chart[jcidx][0] + judgeRange)) * 1000).ToString("0") + "ms";
        // 판정시간 일치 확인
        if (time < (chart[jcidx][0] + perfectRange + judgeRange) && time > (chart[jcidx][0] - perfectRange + judgeRange))  // PERFECT
        {
            jcidx++;
            isNext++;

            // judge
            tJudge.text = "PERFECT!";
        }
        else if (time < (chart[jcidx][0] + goodRange + judgeRange) && time > (chart[jcidx][0] - goodRange + judgeRange))   // GOOD
        {
            jcidx++;
            isNext++;

            // judge
            tJudge.text = "GOOD";
        }
        else if (time < (chart[jcidx][0] + badRange + judgeRange) && time > (chart[jcidx][0] - badRange + judgeRange))    // BAD
        {
            jcidx++;

            // judge
            tJudge.text = "BAD";
        }
        else if (time < (chart[jcidx][0] + missRange + judgeRange) && time > (chart[jcidx][0] - missRange + judgeRange))  // MISS
        {
            jcidx++;
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
        if (chart[jcidx][0] != 0 && jcidx != 0) return;

        // 채보 끝났으면 초기화
        else if (scidx != 0 && time > 3)
        {
            Destroy(GameObject.Find("route"));
            scidx = 0;
            jcidx = 0;
            time = 0;
        }
        if (step == Step.Synk)
        {
            return;
        }

        // 맨 처음이면 시작위치 이동
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


    public void ResetChart()
    {
        for (int i = 0; i < chart.Length; i++)
        {
            chart[i] = new float[5] { 0, 0, 0, 0, 0 };
        }
        scidx = 0;
        jcidx = 0;
        time = 0;
    }
    public void ResetTotal()
    {
        time = 0;
        scidx = 0;
        jcidx = 0;
        isNext = 0;
        //if(player != null) player.enabled = false;
        //CreateExplain();
    }
}
