using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class MainGame : MonoBehaviour
{
    // 모든 스크립트에서 참조 가능하게 하기
    public static MainGame instance;

    // 컴포넌트 참조
    public AudioSource BGM;

    // 오브젝트 연결
    public Player player;

    public MainGameData ResultData;

    // 게임 정보
    public int stageNum = 1;

    // 채보 더미 데이터
    public float[][] chart;              // 채보, 행: 채보 노트 인스턴스, 열: {time, x, y}
    public int noteIndex;                // 채보 포인터

    // 시간 관리
    public bool startButton = true;      // true면 게임시작
    public bool isStart = false;         // true면 게임중인 상태
    public bool isGame = false;          // true면 스톱워치 설정
    public bool isEnd = false;           // true면 게임종료
    public float gameTime;               // 스톱워치
    public float musicTime;              // 음원 시간
    public float startTime;              // 게임시작 시간

    // 스코어 개수
    public int score;
    public int combo;
    public int curCombo;
    public int perfect;
    public int good;   
    public int bad;    
    public int miss;
    public int collection;

    // 판정 범위 관리
    public float perfectRange = 0.05f;
    public float goodRange = 0.1f;
    public float badRange = 0.2f;
    public float missRange = 0.5f;

    // 판정 점수 관리
    public int perfectScore = 500;
    public int goodScore = 300;
    public int badScore = 100;
    public int missScore = -500;
    public int comboScore = 10;

    // 판정 UI
    public int uiHideTime = 5;
    public TextMeshProUGUI judgeUI;
    public TextMeshProUGUI comboUI;

    // 게임 UI
    public GameObject gameCanvas;
    public TextMeshProUGUI scoreUI;
    public TextMeshProUGUI countUI;
    public GameObject resultUI;
    public Slider progressUI;

    // 게임 오브젝트
    public GameObject note;


    // Start is called before the first frame update
    void Start()
    {

        // 밟았을 때 이펙트
        // 효과음을 넣었따
        // 이펙트를 넣었다
        // 경로 기능
        // 라이프 기능
        // 수집 기능
        // 싱크 시스템
        // 노래 볼륨 시스템

        // 값 초기화
        MainGame.instance = this;

        // 데이터 정보(캐릭터, 스테이지값) 받아오기
        player = GameObject.Find("Player").GetComponent<Player>();
        BGM = GameObject.Find("BGM").GetComponent<AudioSource>();
        ResultData = GameObject.Find("Data").GetComponent<MainGameData>();

        // 채보 데이터 불러오기(chart 채보 이차원배열 값, 노트 개수)
        noteIndex = 0;
        chart = new float[71][];
        chart[0] = new float[3] { 0.639f, -6, 0 };
        chart[1] = new float[3] { 1.052f, -6, -1 };
        chart[2] = new float[3] { 1.258f, -5, -1 };
        chart[3] = new float[3] { 1.450f, -4, -1 };
        chart[4] = new float[3] { 1.642f, -3, -1 };
        chart[5] = new float[3] { 1.834f, -3, 0 };
        chart[6] = new float[3] { 2.303f, -3, 1 };
        chart[7] = new float[3] { 2.474f, -2, 1 };
        chart[8] = new float[3] { 2.666f, -1, 1 };
        chart[9] = new float[3] { 2.794f, 0, 1 };
        chart[10] = new float[3] { 3.029f, 0, 0 };
        chart[11] = new float[3] { 3.477f, 0, -1 };
        chart[12] = new float[3] { 3.669f, 1, -1 };
        chart[13] = new float[3] { 3.839f, 2, -1 };
        chart[14] = new float[3] { 4.031f, 3, -1 };
        chart[15] = new float[3] { 4.245f, 3, 0 };
        chart[16] = new float[3] { 4.671f, 2, 0 };
        chart[17] = new float[3] { 4.863f, 1, 0 };
        chart[18] = new float[3] { 5.034f, 0, 0 };
        chart[19] = new float[3] { 5.247f, -1, 0 };
        chart[20] = new float[3] { 5.461f, -2, 0 };
        for (int i = 0; i < 50; i++)
        {
            chart[21 + i] = new float[3] { 5.461f + (0.6f * i), 9, 0 - i };
        }


        //chart[0] = new float[3] { 0.584f, -6, 0 };
        //chart[1] = new float[3] { 1.052f, -6, -1 };
        //chart[2] = new float[3] { 1.253f, -5, -1 };
        //chart[3] = new float[3] { 1.458f, -4, -1 };
        //chart[4] = new float[3] { 1.635f, -3, -1 };
        //chart[5] = new float[3] { 1.819f, -3, 0 };
        //chart[6] = new float[3] { 2.269f, -3, 1 };
        //chart[7] = new float[3] { 2.470f, -2, 1 };
        //chart[8] = new float[3] { 2.652f, -1, 1 };
        //chart[9] = new float[3] { 2.835f, 0, 1 };
        //chart[10] = new float[3] { 3.00f, 0, 0 };
        //chart[11] = new float[3] { 3.453f, 0, -1 };
        //chart[12] = new float[3] { 3.671f, 1, -1 };
        //chart[13] = new float[3] { 3.837f, 2, -1 };
        //chart[14] = new float[3] { 4.020f, 3, -1 };
        //chart[15] = new float[3] { 4.203f, 3, 0 };
        //chart[16] = new float[3] { 4.686f, 2, 0 };
        //chart[17] = new float[3] { 4.871f, 1, 0 };
        //chart[18] = new float[3] { 5.053f, 0, 0 };
        //chart[19] = new float[3] { 5.222f, -1, 0 };
        //chart[20] = new float[3] { 5.420f, -2, 0 };

        // 게임 초기화
        isStart = false;
        isGame = false;
        isEnd = false;
        BGM.Stop();
        
        // 시간 초기화
        gameTime = 0;
        musicTime = 0;
        startTime = 0;

        // 게임 점수 초기화
        perfect = 0;
        good = 0;
        bad = 0;
        miss = 0;
        score = 0;
        combo = 0;
        curCombo = 0;
        collection = 0;

        // UI 세팅
        resultUI.SetActive(false);
        gameCanvas.SetActive(false);
    }

    

    // Update is called once per frame
    void Update()
    {
        // startButton == true 되면 게임시작
        if (startButton)
        {
            GameStart();
            startButton = false;
        }

        // 게임 중이면 스톱워치 설정
        if (isGame)
        {
            gameTime += Time.deltaTime;
            musicTime = BGM.time;
        }

        // miss 처리
        // **개선사항
        if (isGame && noteIndex < chart.Length - 1 &&         // 시작했으면서 판정할 노트가 남아있고
            BGM.time > (chart[noteIndex][0] + badRange))  // 현재 시간이 판정시간을 지났으면 (판정시간 + 판정범위 + 생성시간 1초)
        {
            noteIndex++;
            miss++;
            combo = curCombo;
            curCombo = 0;
            score += missScore;
            judgeUI.text = "MISS";
            comboUI.text = "";
            scoreUI.text = "SCORE\n" + score.ToString();
        }

        // 게임 종료
        // 모든 노트 판정하면
        if (noteIndex == chart.Length && !isEnd)
        {
            // 게임 종료
            isEnd = true;
            GameEnd();
        }

        // 음악 진행도
        MusicProgress();
    }

    // 게임 시작
    public void GameStart()
    {
        Start();
        gameCanvas.SetActive(true);
        StartCoroutine(GameStartCo());
    }

    IEnumerator GameStartCo()
    {
        scoreUI.text = "";
        judgeUI.text = "";
        comboUI.text = "";

        // 플레이어 노트 시작 위치
        player.CurPos = new Vector2(
                MainGame.instance.chart[MainGame.instance.noteIndex][1],
                MainGame.instance.chart[MainGame.instance.noteIndex][2]);

        // 첫 노트 보여주기 
        yield return StartCoroutine(ShowNextNoteCo());

        // 카운트
        yield return StartCoroutine(TimeCountCo(judgeUI));

        // 게임시작
        isStart = true;
        isGame = true;
        startTime = Time.time;

        // 1초 후 음악 틀기
        yield return new WaitForSeconds(1);
        BGM.Play();
    }

    // 게임 진행도
    public void MusicProgress()
    {
        progressUI.value = BGM.time / BGM.clip.length;
    }

    // 게임 종료
    public void GameEnd()
    {
        // 게임 종료
        isStart = false;
        isGame = false;
        judgeUI.text = "Game Clear!";
        judgeUI.color = Color.yellow;
        StartCoroutine(GameEndCo());

        // 데이터 저장

        // 스토리 띄우기

        // 결과 화면 띄우기

        // 결과 화면 끄기
    }

    IEnumerator GameEndCo()
    {
        // 5초 후 음악 끄기
        yield return new WaitForSeconds(5);
        judgeUI.text = "";
        comboUI.text = "";
        judgeUI.color = Color.white;
        BGM.Stop();

        string rank = "";
        if(score > 10000)
        {
            rank = "SS";
        }
        else if (score > 5000)
        {
            rank = "S";
        }
        else if (score > 1000)
        {
            rank = "A";
        }
        else if (score > 100)
        {
            rank = "B";
        }
        else
        {
            rank = "F";
        }

        // 결과창 띄우기
        resultUI.SetActive(true);
        TextMeshProUGUI[] results = resultUI.transform.GetChild(1).gameObject.GetComponentsInChildren<TextMeshProUGUI>();
        print(results.Length);
        results[0].text = rank;
        results[1].text = score.ToString();
        results[2].text = combo.ToString();
        results[3].text = perfect.ToString();
        results[4].text = good.ToString();
        results[5].text = bad.ToString();
        results[6].text = miss.ToString();


    }

    // 게임 끝내지 못한 경우
    // 게임 오버
    public void GameOver()
    {
        // 게임 종료
        isStart = false;
        isGame = false;
        StartCoroutine(GameEndCo());

        // 결과 화면 띄우기
    }

    // 판정 함수
    // 이동할 때마다 호출
    public void Judge(float time, float x, float y)
    {
        // 게임 시작 안했으면 종료
        if (!isGame) return;

        // 판정 함수 *****
        // 변수
        // time: player 이동 시간,
        // x: player x좌표,
        // y: player y좌표
        // chart[i][0]: note 판정 시간,
        // chart[i][1]: note x좌표,
        // chart[i][2]: note y좌표,
        // i: chartCount(0 ~ 노트 개수)

        // 판정에서 노트 생성시간 1초 빼주기
        //time -= 1;

        // 판정 이후로 10개만 좌표 맞는 데이터배열 찾기
        // **개선사항: 판정 안된 10개 노트를 점검하는 시스템
        print("move" + time);

        for (int i = noteIndex; i < noteIndex + 10; i++)
        {
            if (i > chart.Length - 1) break; // 마지막 노트보다 크면 break
            if (x == chart[i][1] && y == chart[i][2]) // 좌표 일치하면
            {

                // 시간 판정 맞는지 확인
                if (time < (chart[i][0] + perfectRange) && time > (chart[i][0] - perfectRange))  // PERFECT
                {
                    perfect++;
                    curCombo++;
                    score = score + perfectScore + comboScore * curCombo;
                    noteIndex++;
                    judgeUI.text = "PERFECT!";
                    comboUI.text = curCombo.ToString();
                    scoreUI.text = "SCORE\n" + score.ToString();
                    break;
                }
                else if (time < (chart[i][0] + goodRange) && time > (chart[i][0] - goodRange))   // GOOD
                {
                    good++;
                    curCombo++;
                    score = score + goodScore + comboScore * curCombo;
                    noteIndex++;
                    judgeUI.text = "GOOD";
                    comboUI.text = curCombo.ToString();
                    scoreUI.text = "SCORE\n" + score.ToString();
                    break;
                }
                else if (time < (chart[i][0] + badRange) && time > (chart[i][0] - badRange))    // BAD
                {
                    bad++;
                    combo = curCombo;
                    curCombo = 0;
                    score += badScore;
                    noteIndex++;
                    judgeUI.text = "BAD";
                    comboUI.text = "";
                    scoreUI.text = "SCORE\n" + score.ToString();
                    break;
                }
                else if (time < (chart[i][0] + missRange) && time > (chart[i][0] - missRange))  // MISS
                {
                    miss++;
                    combo = curCombo;
                    curCombo = 0;
                    score += missScore;
                    noteIndex++;
                    judgeUI.text = "MISS";
                    comboUI.text = "";
                    scoreUI.text = "SCORE\n" + score.ToString();
                    break;
                }
                else                                                                             // 좌표는 같은데 시간이 안맞아 판정X인 경우
                {
                    break; 
                }
            }
        }
    }

    public void Stop()
    {
        // 게임 정지
        isGame = false;

        // 게임 시간은 현재 노트의 시간으로 설정(다시 시작 위해)
        gameTime = chart[noteIndex][0];

        // 음악 정지
        BGM.Pause();
        BGM.time = chart[noteIndex][0];

        // **개선 사항: chart 구성을 audiosorce.time으로 설정하여 정확도를 높일 지
    }
    public void Continue()
    {
        // 게임 계속
        // 게임 할 때만 설정 가능
        if (isStart)
        {
            StartCoroutine(ContinueCo());
        }
    }

    IEnumerator ContinueCo()
    {
        yield return StartCoroutine(ShowNextNoteCo());
        yield return StartCoroutine(TimeCountCo());
        isGame = true;
        yield return new WaitForSeconds(1);
        BGM.Play();
    }

    public void TimeCount()
    {
        StartCoroutine(TimeCountCo());
    }
    public void TimeCount(TextMeshProUGUI textUI)
    {
        StartCoroutine(TimeCountCo(textUI));
    }

    IEnumerator TimeCountCo(TextMeshProUGUI textUI)
    {
        textUI.text = "3";
        Debug.Log("COUNT 3");
        yield return new WaitForSeconds(1);
        textUI.text = "2";
        Debug.Log("COUNT 3");
        yield return new WaitForSeconds(1);
        textUI.text = "1";
        Debug.Log("COUNT 3");
        yield return new WaitForSeconds(1);
        textUI.text = "START";
        Debug.Log("START");
        yield return new WaitForSeconds(1);
        textUI.text = "";
    }
    IEnumerator TimeCountCo()
    {
        Debug.Log("COUNT 3");
        yield return new WaitForSeconds(1);
        Debug.Log("COUNT 2");
        yield return new WaitForSeconds(1);
        Debug.Log("COUNT 1");
        yield return new WaitForSeconds(1);
        Debug.Log("START");
        yield return new WaitForSeconds(1);
    }

    public void ShowNextNote()
    {
        StartCoroutine(ShowNextNoteCo());
    }
    IEnumerator ShowNextNoteCo()
    {
        var note1 = Instantiate(note);
        note1.transform.position = new Vector3(chart[noteIndex][1], chart[noteIndex][2], 0);
        Destroy(note1, 3);
        yield return new WaitForSeconds(3);
    }
}
