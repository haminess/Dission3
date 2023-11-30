using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Unity.VisualScripting;
using UnityEngine.Rendering.Universal;

public class MainGame : MonoBehaviour
{
    // 메인게임 싱글톤
    public static MainGame instance;

    // 컴포넌트
    [Header("Component")]
    public AudioSource bgm;
    public AudioSource effect;
    public SoundManager soundMan;
    StoryManager storyManager;
    ChangeScene sceneManager;

    // 게임 오브젝트
    [Header("GameObject")]
    public Player player;
    public GameObject note;
    public GameObject judgeEffect;
    public Animation comboeff;
    public Animation judgeeff;
    public GameObject[] stageObject;

    // 로컬 데이터
    DataManager dataMan;

    // 채보 관련 데이터
    [Header("Chart")]
    public float[][] chart;            
    public int noteIndex;

    // 메인 상태 데이터
    [Header("Game State")]
    public bool startButton = false;
    public int stageNum = 1;
    public bool stageMode = false;
    public bool isStart = false;
    public bool isGame = false;
    public bool isEnd = false;
    public float gameTime;
    public float musicTime;
    public float startTime;

    // 메인 유저점수 데이터
    [Header("Score")]
    public int score;
    public int combo;
    public int curCombo;
    public int perfect;
    public int good;
    public int bad;
    public int miss;
    public int collection;
    public Color[] color;
    public List<string[]> collections = new List<string[]>();

    // life
    public int life;
    public int maxLife = 10;

    // 메인 판정범위 관리
    [Header("Range")]
    public float perfectRange = 0.05f;
    public float goodRange = 0.1f;
    public float badRange = 0.2f;
    public float missRange = 0.5f;
    public float notesynkRange = 0f;
    public float judgeRange = 0f;

    // 메인 점수범위 관리
    public int perfectScore = 500;
    public int goodScore = 300;
    public int badScore = 100;
    public int missScore = 0;
    public int comboScore = 10;


    // 유저 UI
    [Header("UI")]
    public int uiHideTime = 5;
    public TextMeshProUGUI judgeUI;
    public TextMeshProUGUI comboUI;
    public TextMeshProUGUI combotext;

    // 게임 UI
    public GameObject gameCanvas;
    public GameObject screenCanvas;
    public TextMeshProUGUI scoreUI;
    public TextMeshProUGUI countUI;
    public Slider progressUI;
    public Slider lifeUI;

    [Header("Prefab")]
    // 가이드 오브젝트
    public GameObject guidePrefab;

    // Start is called before the first frame update
    public void Start()
    {
        // 메인게임 스크립트 싱글톤
        MainGame.instance = this;

        // 컴포넌트 연결
        sceneManager = GameObject.Find("SceneManager").GetComponent<ChangeScene>();
        storyManager = GetComponent<StoryManager>();

        // 게임 데이터 초기화
        ResetMain();

        // 로컬데이터 불러오기
        GetMainData();

        SetStage();

        // 스테이지(실행) 모드이면 바로 시작
        if (stageMode)
        {
            StageStart();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (startButton)
        {
            if(stageMode)
            {
                StageStart();
            }
            else
            {
                GameStart();
            }
            startButton = false;
        }


        if (!isGame)
            return;

        // 게임 시간 동작
        if (isGame)
        {
            gameTime += Time.deltaTime;
            musicTime = bgm.time;
        }

        // miss 처리
        if (isGame && noteIndex < chart.Length - 1 &&                   
            bgm.time > (chart[noteIndex][0] + badRange + judgeRange))    
        {
            noteIndex++;

            // miss 처리
            miss++;
            judgeUI.color = color[3];
            judgeUI.text = "MISS";

            // combo 처리
            combo = curCombo;
            curCombo = 0;
            comboUI.text = "";
            combotext.text = "";

            // score 처리
            score += missScore;
            scoreUI.text = "SCORE\n" + score.ToString();

            // life 감소
            life--;
            lifeUI.value = life;

            // 이펙트 처리
            judgeeff.Play();
        }

        // 게임 종료
        if ((noteIndex > chart.Length - 1 && !isEnd) /*|| (life <= 0 && !isEnd)*/)  // 편의 위해 생명 시스템 off
        {
            if (stageMode)
            {
                StageEnd();
            }
            else
            {
                GameEnd();
            }
            isEnd = true;
        }


        // 배경음악 진행바
        MusicProgress();
    }

    // 메인게임 시작
    public void GameStart()
    {
        StartCoroutine(GameStartCo());
    }

    IEnumerator GameStartCo()
    {
        // 게임 데이터 초기화
        ResetMain();

        // 로컬데이터 불러오기
        GetMainData();

        SetStage();

        gameCanvas.SetActive(true);
        scoreUI.text = "";
        judgeUI.text = "";
        comboUI.text = "";
        combotext.text = "";

        // 占시뤄옙占싱억옙 占쏙옙트 占쏙옙占쏙옙占쏙옙 占쏙옙치
        PlayerReposition();

        // 첫 占쏙옙트 占쏙옙占쏙옙占쌍깍옙 
        yield return StartCoroutine(ShowNextNoteCo());

        // 카占쏙옙트
        yield return StartCoroutine(TimeCountCo(judgeUI));

        // 占쏙옙占쌈쏙옙占쏙옙
        isStart = true;        // 占쏙옙占쏙옙 占쌩댐옙占쏙옙
        isGame = true;         // 占쏙옙占쏙옙 占쏙옙占쏙옙占쏙옙

        // 占쏙옙占쏙옙 占십깍옙화
        bgm.Stop();                  
        bgm.time = 0;                

        // 1占쏙옙 占쏙옙 占쏙옙占쏙옙 틀占쏙옙
        yield return new WaitForSeconds(1);
        bgm.Play();
        yield return new WaitForSeconds(3);
        Settable(true);        // 占쏙옙占쏙옙창 占쏙옙諛∽옙占?
    }


    public void StageStart()
    {
        ResetMain();
        gameCanvas.SetActive(true);
        StartCoroutine(StageStartCo());
    }

    public IEnumerator StageStartCo()
    {
        // 스토리 노래 틀어주기
        float curVolume = bgm.volume;
        //bgm.time = soundMan.bgmHookTime[stageNum - 1];
        bgm.volume = 0;
        bgm.Play();
        storyManager.sID = (stageNum - 1) * 3;
        while (true)
        {
            bgm.volume += 0.001f;
            yield return new WaitForSeconds(0.01f);
            if (bgm.volume > curVolume)
            {
                break;
            }
        }

        // 스토리 출력
        yield return StartCoroutine(storyManager.ShowStoryCo());

        // 리듬게임 시작 전 플레이어 위치 변경
        PlayerReposition();

        // 스토리 노래 페이드아웃
        while (true)
        {
            bgm.volume -= 0.001f;
            yield return new WaitForSeconds(0.01f);
            if(bgm.volume < 0.01f)
            {
                break;
            }
        }

        // 게임 시작
        yield return StartCoroutine(GameStartCo());
    }

    // 占쏙옙占쏙옙 占쏙옙占썅도
    public void MusicProgress()
    {
        // 占쏙옙占쏙옙 占쏙옙占쏙옙 占쏙옙占썅도
        // progressUI.value = BGM.time / BGM.clip.length;

        // 채占쏙옙 占쏙옙占쏙옙 占쏙옙占썅도
        if(bgm.time > 0)
            progressUI.value = bgm.time / chart[chart.Length - 1][0];
    }

    // 占쏙옙占쏙옙 占쏙옙占쏙옙
    public void GameEnd()
    {
        StartCoroutine(GameEndCo());
    }

    IEnumerator GameEndCo()
    {
        // 占쏙옙占쏙옙 占쏙옙占쏙옙
        isStart = false;
        isGame = false;

        Settable(true);     // 占쏙옙占쏙옙창 占쏙옙占쏙옙

        // score ui 占쏙옙占쏙옙
        judgeUI.text = "Game Clear!";
        judgeUI.color = Color.yellow;

        // 占쏙옙占쏙옙占쏙옙 占쏙옙占쏙옙
        GameObject.Find("ResultData").GetComponent<ResultManager>().SendResult();

        // 5占쏙옙 占쏙옙 占쏙옙占쏙옙 占쏙옙占?
        yield return new WaitForSeconds(5);

        // ui 占십깍옙화
        judgeUI.text = "";
        comboUI.text = "";
        combotext.text = "";
        judgeUI.color = Color.white;

        // 占쏙옙占쏙옙 占십깍옙화
        bgm.Stop();
    }

    // life 0돼서 게임 오버됐을 때
    public void GameOver()
    {
        StartCoroutine(GameOverCo());
    }
    IEnumerator GameOverCo()
    {
        yield return StartCoroutine(GameEndCo());

        yield return new WaitForSeconds(1);
        sceneManager.ToScoreScene();
    }

    public void StageEnd()
    {
        StartCoroutine(StageEndCo());
    }
    IEnumerator StageEndCo()
    {
        // 占쏙옙占쏙옙 占쏙옙占쏙옙
        yield return StartCoroutine(GameEndCo());

        // 占쏙옙占쏙옙 占쏙옙占썰리 占쏙옙占?
        yield return new WaitForSeconds(1);
        storyManager.sID = (stageNum - 1) * 3;
        if(miss < 10)
        {
            storyManager.sID += 1;
            storyManager.sId += 1;
        }
        else
        {
            storyManager.sID += 2;
            storyManager.sId += 2;
        }
        yield return StartCoroutine(storyManager.ShowStoryCo());

        print(collections.Count + "개 수집");
        for(int i = 0; i < collections.Count; i++)
        {
            yield return StartCoroutine(ShowGuide(collections[i]));
        }

        // 占쏙옙占?화占쏙옙 占쏙옙환
        yield return new WaitForSeconds(1);
        sceneManager.ToScoreScene();
    }

    IEnumerator ShowGuide(string[] _content)
    {
        print("가이드 생성");
        GameObject guide = Instantiate(guidePrefab, screenCanvas.transform);
        guide.GetComponent<Guide>().explain = _content;
        while (true)
        {
            if (guide == null)
            {
                break;
            }
            yield return null;
        }
    }

    public void Judge(float time, float x, float y)
    {
        // 판정 함수
        if (!isGame) return;

        // 현재 ~ 10개 노트 판정 검사
        for (int i = noteIndex; i < noteIndex + 10; i++)
        {
            if (i > chart.Length - 1) break; // 채보 끝이면 리턴
            if (x == chart[i][1] && y == chart[i][2]) // 좌표 일치 확인
            {

                // 판정시간 일치 확인
                if (time < (chart[i][0] + perfectRange + judgeRange) && time > (chart[i][0] - perfectRange + judgeRange))  // PERFECT
                {
                    noteIndex++;

                    // judge
                    perfect++;
                    judgeUI.text = "PERFECT!";
                    judgeUI.color = color[0];
                    judgeeff.Play();

                    // combo
                    curCombo++;
                    comboUI.text = curCombo.ToString();
                    combotext.text = "Combo";
                    comboeff.Play();

                    // score
                    score = score + perfectScore + comboScore * curCombo;
                    scoreUI.text = "SCORE\n" + score.ToString();

                    // effect
                    GameObject effect = Instantiate(judgeEffect);
                    effect.transform.localPosition = new Vector3(chart[i][1], chart[i][2]);
                    Destroy(effect, 0.5f);

                    break;
                }
                else if (time < (chart[i][0] + goodRange + judgeRange) && time > (chart[i][0] - goodRange + judgeRange))   // GOOD
                {
                    noteIndex++;

                    // judge
                    good++;
                    judgeUI.text = "GOOD";
                    judgeUI.color = color[1];
                    judgeeff.Play();

                    // combo
                    curCombo++;
                    comboUI.text = curCombo.ToString();
                    combotext.text = "Combo";
                    comboeff.Play();

                    // score
                    score = score + goodScore + comboScore * curCombo;
                    scoreUI.text = "SCORE\n" + score.ToString();

                    // effect
                    GameObject effect = Instantiate(judgeEffect);
                    effect.transform.localPosition = new Vector3(chart[i][1], chart[i][2]);
                    Destroy(effect, 0.5f);

                    break;
                }
                else if (time < (chart[i][0] + badRange + judgeRange) && time > (chart[i][0] - badRange + judgeRange))    // BAD
                {
                    noteIndex++;

                    // judge
                    bad++;
                    judgeUI.text = "BAD";
                    judgeUI.color = color[2];
                    judgeeff.Play();

                    // combo
                    combo = curCombo;
                    curCombo = 0;
                    comboUI.text = "";
                    combotext.text = "";

                    // score
                    score += badScore;
                    scoreUI.text = "SCORE\n" + score.ToString();

                    break;
                }
                else if (time < (chart[i][0] + missRange + judgeRange) && time > (chart[i][0] - missRange + judgeRange))  // MISS
                {
                    noteIndex++;

                    // miss 처리
                    miss++;
                    judgeUI.text = "MISS";
                    judgeUI.color = color[3];

                    // combo 처리
                    combo = curCombo;
                    curCombo = 0;
                    comboUI.text = "";
                    combotext.text = "";

                    // score 처리
                    score += missScore;
                    scoreUI.text = "SCORE\n" + score.ToString();

                    // life 감소
                    life--;
                    lifeUI.value = life;

                    // 이펙트
                    judgeeff.Play();
                    break;
                }
                else
                {
                    // 좌표는 일치하나 시간 범위에 맞지 않음
                    break; 
                }
            }
        }
    }

    public void Stop()
    {
        if (!isGame) return;

        // 게임 중단
        isGame = false;

        // 게임 시간 다음 판정 노트 시간으로 변경
        gameTime = chart[noteIndex][0];

        // 배경음악 중단
        bgm.Pause();
        
        // 게임 시간에 맞게 bgm 시간도 변경
        if (chart[noteIndex][0] > 1)
            bgm.time = chart[noteIndex][0] - 1;
    }
    public void Continue()
    {
        // 계속하기
        if (isStart)
        {
            StartCoroutine(ContinueCo());
        }
    }

    IEnumerator ContinueCo()
    {
        // 시작 전 첫 노트 앞으로 위치 이동
        PlayerReposition();

        // 시작 전 첫 노트 위치 보여주기
        yield return StartCoroutine(ShowNextNoteCo());

        // 시작 카운트
        yield return StartCoroutine(TimeCountCo());

        // 게임 시작
        isGame = true;

        // 노트 뜨는 시간 1초 필요하기 때문에
        // 1초 후에 음악 재생
        yield return new WaitForSeconds(1);
        bgm.Play();
    }
    public void TimeCount(TextMeshProUGUI textUI)
    {
        StartCoroutine(TimeCountCo(textUI));
    }
    public void TimeCount()
    {
        StartCoroutine(TimeCountCo());
    }

    IEnumerator TimeCountCo(TextMeshProUGUI textUI)
    {
        textUI.text = "3";
        yield return new WaitForSeconds(1);
        textUI.text = "2";
        yield return new WaitForSeconds(1);
        textUI.text = "1";
        yield return new WaitForSeconds(1);
        textUI.text = "START";
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

    public void PlayerReposition()
    {
        // 시작 위치 조정
        Vector2 firstNote = new Vector2(
                chart[noteIndex][1],
                chart[noteIndex][2]);
        player.CurPos = firstNote;

        LayerMask mask = LayerMask.GetMask("Wall") | LayerMask.GetMask("Object");

        if (Physics2D.Raycast(firstNote, Vector3.up, 1, mask) == false)
        {
            player.CurPos += Vector3.up;
        }
        else if (Physics2D.Raycast(firstNote, Vector3.down, 1, mask) == false)
        {
            player.CurPos += Vector3.down;
        }
        else if (Physics2D.Raycast(firstNote, Vector3.left, 1, mask) == false)
        {
            player.CurPos += Vector3.left;
        }
        else if (Physics2D.Raycast(firstNote, Vector3.right, 1, mask) == false)
        {
            player.CurPos += Vector3.right;
        }
    }

    public void Settable(bool _isCan)
    {
        gameCanvas.transform.GetChild(0).gameObject.SetActive(_isCan);
        player.GetComponent<Player>().Settable = _isCan;
    }

    public void ResetMain()
    {

        // 게임 초기화
        noteIndex = 0;
        isStart = false;
        isGame = false;
        isEnd = false;
        bgm.Stop();
        Settable(false);     // 占쏙옙占쏙옙창 占쏙옙占?
        GetComponent<NoteGenerator>().noteIndex = 0;
        collections.Clear();

        // 게임 시간
        gameTime = 0;
        musicTime = 0;
        startTime = 0;

        // 게임 점수
        perfect = 0;
        good = 0;
        bad = 0;
        miss = 0;
        score = 0;

        // life
        life = maxLife;
        lifeUI.value = life;

        // combo
        combo = 0;
        curCombo = 0;
        collection = 0;

        // 게임 캔버스
        gameCanvas.SetActive(false);
    }

    public void SetStage()
    {
        // 스테이지 데이터 세팅
        stageNum = 1;

        if (GameObject.Find("Data"))
        {
            DataManager dm = GameObject.Find("Data").GetComponent<DataManager>();
            stageNum = dm.stageNum;
        }

        // 스테이지 음악 세팅
        bgm.clip = soundMan.bgmClip[stageNum - 1];

        // 스테이지 오브젝트 활성화
        for(int i = 0; i < stageObject.Length; i++)
        {
            stageObject[i].SetActive(false);
        }
        stageObject[stageNum - 1].SetActive(true);


        // 채보 정보 가져오기
        // chart = 
    }

    public void GetMainData()
    {

        // 채보 불러오기

        // 임시 채보, 추후 삭제
        if(stageNum == 1)
        {
            chart = new float[236][];
            chart[0] = new float[3] { 1, 7, -28 };
            chart[1] = new float[3] { 1.770f, 8, -28 };
            chart[2] = new float[3] { 2.474f, 9, -27 };
            chart[3] = new float[3] { 3.029f, 10, -27 };
            chart[4] = new float[3] { 3.690f, 11, -27 };
            chart[5] = new float[3] { 5.696f, 12, -27 };
            chart[6] = new float[3] { 6.464f, 12, -27 };
            chart[7] = new float[3] { 6.997f, 13, -28 };
            chart[8] = new float[3] { 7.744f, 14, -28 };
            chart[9] = new float[3] { 8.085f, 14, -27 };
            chart[10] = new float[3] { 8.237f, 15, -27 };
            chart[11] = new float[3] { 8.5333f, 16, -27 };
            chart[12] = new float[3] { 8.704f, 17, -27 };
            chart[13] = new float[3] { 9.066f, 17, -28 };
            chart[14] = new float[3] { 9.237f, 18, -28 };
            chart[15] = new float[3] { 9.685f, 19, -28 };
            chart[16] = new float[3] { 10.474f, 19, -27 };
            chart[17] = new float[3] { 11.008f, 20, -27 };
            chart[18] = new float[3] { 11.690f, 21, -27 };
            chart[19] = new float[3] { 12.053f, 22, -27 };
            chart[20] = new float[3] { 12.202f, 23, -27 };
            chart[21] = new float[3] { 12.586f, 24, -27 };
            chart[22] = new float[3] { 12.736f, 25, -27 };
            chart[23] = new float[3] { 13.034f, 26, -27 };
            chart[24] = new float[3] { 13.226f, 27, -27 };
            chart[25] = new float[3] { 13.653f, 27, -28 };
            chart[26] = new float[3] { 14.506f, 28, -28 };
            chart[27] = new float[3] { 15.04f, 29, -28 };
            chart[28] = new float[3] { 15.701f, 30, -28 };
            chart[29] = new float[3] { 16.064f, 31, -28 };
            chart[30] = new float[3] { 16.149f, 31, -27 };
            chart[31] = new float[3] { 16.512f, 32, -27 };
            chart[32] = new float[3] { 16.704f, 33, -27 };
            chart[33] = new float[3] { 17.066f, 34, -27 };
            chart[34] = new float[3] { 17.237f, 35, -27 };
            chart[35] = new float[3] { 17.688f, 36, -27 };
            chart[36] = new float[3] { 18.474f, 37, -27 };
            chart[37] = new float[3] { 19.008f, 38, -27 };
            chart[38] = new float[3] { 19.712f, 39, -27 };
            chart[39] = new float[3] { 20.202f, 40, -27 };
            chart[40] = new float[3] { 20.672f, 41, -27 };
            chart[41] = new float[3] { 21.184f, 42, -27 };
            chart[42] = new float[3] { 21.717f, 42, -26 };
            chart[43] = new float[3] { 22.528f, 43, -26 };
            chart[44] = new float[3] { 23.04f, 44, -26 };
            chart[45] = new float[3] { 23.68f, 45, -26 };
            chart[46] = new float[3] { 24.192f, 45, -27 };
            chart[47] = new float[3] { 24.405f, 46, -27 };
            chart[48] = new float[3] { 24.704f, 47, -27 };
            chart[49] = new float[3] { 24.874f, 48, -27 };
            chart[50] = new float[3] { 25.173f, 49, -27 };
            chart[51] = new float[3] { 25.344f, 49, -28 };
            chart[52] = new float[3] { 25.834f, 50, -28 };
            chart[53] = new float[3] { 26.517f, 51, -28 };
            chart[54] = new float[3] { 27.050f, 51, -27 };
            chart[55] = new float[3] { 27.712f, 52, -27 };
            chart[56] = new float[3] { 28.074f, 53, -27 };
            chart[57] = new float[3] { 28.245f, 54, -27 };
            chart[58] = new float[3] { 28.586f, 55, -27 };
            chart[59] = new float[3] { 28.778f, 56, -27 };
            chart[60] = new float[3] { 29.098f, 57, -27 };
            chart[61] = new float[3] { 29.269f, 58, -27 };
            chart[62] = new float[3] { 29.738f, 58, -28 };
            chart[63] = new float[3] { 30.464f, 59, -28 };
            chart[64] = new float[3] { 30.976f, 60, -28 };
            chart[65] = new float[3] { 31.637f, 61, -28 };
            chart[66] = new float[3] { 32.064f, 62, -28 };
            chart[67] = new float[3] { 32.213f, 63, -28 };
            chart[68] = new float[3] { 32.576f, 63, -27 };
            chart[69] = new float[3] { 32.746f, 63, -26 };
            chart[70] = new float[3] { 33.088f, 63, -25 };
            chart[71] = new float[3] { 33.258f, 63, -24 };
            chart[72] = new float[3] { 33.685f, 63, -23 };
            chart[73] = new float[3] { 34.474f, 63, -22 };
            chart[74] = new float[3] { 34.965f, 63, -21 };
            chart[75] = new float[3] { 35.712f, 63, -20 };
            chart[76] = new float[3] { 36.16f, 62, -20 };
            chart[77] = new float[3] { 36.693f, 61, -20 };
            chart[78] = new float[3] { 37.205f, 60, -20 };
            chart[79] = new float[3] { 37.696f, 59, -20 };
            chart[80] = new float[3] { 38.528f, 58, -20 };
            chart[81] = new float[3] { 38.997f, 57, -20 };
            chart[82] = new float[3] { 39.68f, 56, -20 };
            chart[83] = new float[3] { 40.874f, 55, -20 };
            chart[84] = new float[3] { 41.109f, 55, -21 };
            chart[85] = new float[3] { 41.28f, 54, -21 };
            chart[86] = new float[3] { 41.813f, 53, -21 };
            chart[87] = new float[3] { 42.304f, 52, -21 };
            chart[88] = new float[3] { 42.752f, 51, -21 };
            chart[89] = new float[3] { 43.242f, 50, -21 };
            chart[90] = new float[3] { 44.8f, 49, -21 };
            chart[91] = new float[3] { 45.12f, 48, -21 };
            chart[92] = new float[3] { 45.312f, 47, -21 };
            chart[93] = new float[3] { 45.76f, 47, -20 };
            chart[94] = new float[3] { 46.272f, 46, -20 };
            chart[95] = new float[3] { 46.72f, 46, -19 };
            chart[96] = new float[3] { 47.232f, 45, -19 };
            chart[97] = new float[3] { 48.298f, 44, -19 };
            chart[98] = new float[3] { 49.344f, 43, -19 };
            chart[99] = new float[3] { 49.493f, 42, -19 };
            chart[100] = new float[3] { 49.664f, 41, -19 };
            chart[101] = new float[3] { 49.792f, 40, -19 };
            chart[102] = new float[3] { 50.090f, 39, -19 };
            chart[103] = new float[3] { 50.56f, 38, -19 };
            chart[104] = new float[3] { 50.709f, 37, -19 };
            chart[105] = new float[3] { 51.264f, 36, -19 };
            chart[106] = new float[3] { 51.434f, 35, -19 };
            chart[107] = new float[3] { 51.584f, 34, -19 };
            chart[108] = new float[3] { 51.733f, 34, -20 };
            chart[109] = new float[3] { 52.117f, 33, -20 };
            chart[110] = new float[3] { 52.565f, 32, -20 };
            chart[111] = new float[3] { 52.714f, 31, -20 };
            chart[112] = new float[3] { 53.077f, 30, -20 };
            chart[113] = new float[3] { 53.226f, 29, -20 };
            chart[114] = new float[3] { 54.677f, 28, -20 };
            chart[115] = new float[3] { 54.784f, 27, -20 };
            chart[116] = new float[3] { 55.104f, 26, -20 };
            chart[117] = new float[3] { 55.488f, 25, -20 };
            chart[118] = new float[3] { 56f, 24, -20 };
            chart[119] = new float[3] { 56.746f, 23, -20 };
            chart[120] = new float[3] { 57.066f, 22, -20 };
            chart[121] = new float[3] { 57.237f, 21, -20 };
            chart[122] = new float[3] { 57.685f, 21, -21 };
            chart[123] = new float[3] { 58.218f, 20, -21 };
            chart[124] = new float[3] { 58.666f, 19, -21 };
            chart[125] = new float[3] { 59.221f, 18, -21 };
            chart[126] = new float[3] { 60.757f, 17, -21 };
            chart[127] = new float[3] { 61.098f, 16, -21 };
            chart[128] = new float[3] { 61.248f, 16, -20 };
            chart[129] = new float[3] { 61.717f, 15, -20 };
            chart[130] = new float[3] { 62.186f, 14, -20 };
            chart[131] = new float[3] { 62.677f, 13, -20 };
            chart[132] = new float[3] { 63.210f, 12, -20 };
            chart[133] = new float[3] { 64.277f, 11, -20 };
            chart[134] = new float[3] { 65.322f, 10, -20 };
            chart[135] = new float[3] { 65.472f, 10, -21 };
            chart[136] = new float[3] { 65.621f, 9, -21 };
            chart[137] = new float[3] { 65.770f, 8, -21 };
            chart[138] = new float[3] { 66.112f, 7, -21 };
            chart[139] = new float[3] { 66.517f, 7, -20 };
            chart[140] = new float[3] { 66.688f, 6, -20 };
            chart[141] = new float[3] { 67.264f, 5, -20 };
            chart[142] = new float[3] { 67.413f, 4, -20 };
            chart[143] = new float[3] { 67.584f, 3, -20 };
            chart[144] = new float[3] { 67.754f, 2, -20 };
            chart[145] = new float[3] { 68.096f, 1, -20 };
            chart[146] = new float[3] { 68.565f, 1, -19 };
            chart[147] = new float[3] { 68.736f, 1, -18 };
            chart[148] = new float[3] { 69.077f, 2, -18 };
            chart[149] = new float[3] { 69.248f, 3, -18 };
            chart[150] = new float[3] { 70.72f, 4, -18 };
            chart[151] = new float[3] { 70.805f, 4, -19 };
            chart[152] = new float[3] { 71.125f, 4, -20 };
            chart[153] = new float[3] { 71.594f, 5, -20 };
            chart[154] = new float[3] { 72.064f, 6, -20 };
            chart[155] = new float[3] { 72.448f, 7, -20 };
            chart[156] = new float[3] { 72.874f, 8, -20 };
            chart[157] = new float[3] { 73.258f, 9, -20 };
            chart[158] = new float[3] { 73.429f, 9, -19 };
            chart[159] = new float[3] { 73.578f, 9, -18 };
            chart[160] = new float[3] { 73.749f, 9, -17 };
            chart[161] = new float[3] { 74.005f, 9, -16 };
            chart[162] = new float[3] { 74.133f, 9, -15 };
            chart[163] = new float[3] { 74.304f, 9, -14 };
            chart[164] = new float[3] { 74.624f, 9, -13 };
            chart[165] = new float[3] { 74.773f, 9, -12 };
            chart[166] = new float[3] { 75.093f, 8, -12 };
            chart[167] = new float[3] { 75.264f, 8, -11 };
            chart[168] = new float[3] { 75.733f, 8, -10 };
            chart[169] = new float[3] { 76.544f, 8, -9 };
            chart[170] = new float[3] { 77.013f, 7, -9 };
            chart[171] = new float[3] { 77.76f, 7, -8 };
            chart[172] = new float[3] { 78.058f, 6, -8 };
            chart[173] = new float[3] { 78.250f, 5, -8 };
            chart[174] = new float[3] { 78.570f, 4, -8 };
            chart[175] = new float[3] { 78.762f, 3, -8 };
            chart[176] = new float[3] { 79.104f, 2, -8 };
            chart[177] = new float[3] { 79.253f, 1, -8 };
            chart[178] = new float[3] { 79.744f, 1, -7 };
            chart[179] = new float[3] { 80.576f, 1, -6 };
            chart[180] = new float[3] { 81.024f, 1, -5 };
            chart[181] = new float[3] { 81.706f, 1, -4 };
            chart[182] = new float[3] { 82.069f, 1, -3 };
            chart[183] = new float[3] { 82.24f, 1, -2 };
            chart[184] = new float[3] { 82.624f, 1, -1 };
            chart[185] = new float[3] { 82.794f, 1, 0 };
            chart[186] = new float[3] { 83.114f, 1, 1 };
            chart[187] = new float[3] { 83.306f, 2, 1 };
            chart[188] = new float[3] { 83.733f, 2, 0 };
            chart[189] = new float[3] { 84.522f, 3, 0 };
            chart[190] = new float[3] { 85.013f, 4, 0 };
            chart[191] = new float[3] { 85.717f, 5, 0 };
            chart[192] = new float[3] { 86.186f, 6, 0 };
            chart[193] = new float[3] { 86.698f, 7, 0 };
            chart[194] = new float[3] { 87.210f, 7, 1 };
            chart[195] = new float[3] { 87.701f, 8, 1 };
            chart[196] = new float[3] { 88.512f, 9, 1 };
            chart[197] = new float[3] { 88.981f, 9, 2 };
            chart[198] = new float[3] { 89.706f, 9, 3 };
            chart[199] = new float[3] { 90.048f, 10, 3 };
            chart[200] = new float[3] { 90.24f, 10, 4 };
            chart[201] = new float[3] { 90.581f, 10, 5 };
            chart[202] = new float[3] { 90.773f, 10, 6 };
            chart[203] = new float[3] { 91.136f, 10, 7 };
            chart[204] = new float[3] { 91.306f, 10, 8 };
            chart[205] = new float[3] { 91.733f, 9, 8 };
            chart[206] = new float[3] { 92.522f, 9, 9 };
            chart[207] = new float[3] { 92.970f, 10, 9 };
            chart[208] = new float[3] { 93.696f, 10, 10 };
            chart[209] = new float[3] { 94.037f, 10, 11 };
            chart[210] = new float[3] { 94.208f, 9, 11 };
            chart[211] = new float[3] { 94.570f, 9, 12 };
            chart[212] = new float[3] { 94.72f, 9, 13 };
            chart[213] = new float[3] { 95.061f, 8, 13 };
            chart[214] = new float[3] { 95.253f, 8, 14 };
            chart[215] = new float[3] { 95.701f, 8, 15 };
            chart[216] = new float[3] { 96.512f, 7, 15 };
            chart[217] = new float[3] { 97.002f, 6, 15 };
            chart[218] = new float[3] { 97.664f, 5, 15 };
            chart[219] = new float[3] { 98.048f, 4, 15 };
            chart[220] = new float[3] { 98.218f, 3, 15 };
            chart[221] = new float[3] { 98.581f, 2, 15 };
            chart[222] = new float[3] { 98.794f, 2, 14 };
            chart[223] = new float[3] { 99.093f, 2, 13 };
            chart[224] = new float[3] { 99.264f, 1, 13 };
            chart[225] = new float[3] { 99.690f, 0, 13 };
            chart[226] = new float[3] { 100.48f, -1, 13 };
            chart[227] = new float[3] { 100.992f, -2, 13 };
            chart[228] = new float[3] { 101.696f, -3, 13 };
            chart[229] = new float[3] { 102.186f, -3, 14 };
            chart[230] = new float[3] { 102.677f, -3, 15 };
            chart[231] = new float[3] { 103.189f, -3, 16 };
            chart[232] = new float[3] { 103.701f, -3, 17 };
            chart[233] = new float[3] { 104.512f, -3, 18 };
            chart[234] = new float[3] { 104.96f, -3, 19 };
            chart[235] = new float[3] { 105.642f, -3, 20 };
        }
        else if(stageNum == 2)
        {
            chart = new float[137][];
            chart[0] = new float[3] { 0.4693333f, 43, -45 };
            chart[1] = new float[3] { 1.258667f, 44, -45 };
            chart[2] = new float[3] { 1.984f, 45, -45 };
            chart[3] = new float[3] { 2.218667f, 46, -45 };
            chart[4] = new float[3] { 2.773333f, 47, -45 };
            chart[5] = new float[3] { 3.584f, 48, -45 };
            chart[6] = new float[3] { 4.330667f, 49, -45 };
            chart[7] = new float[3] { 5.098667f, 50, -45 };
            chart[8] = new float[3] { 5.354667f, 51, -45 };
            chart[9] = new float[3] { 5.845333f, 52, -45 };
            chart[10] = new float[3] { 6.613333f, 53, -45 };
            chart[11] = new float[3] { 7.381333f, 54, -45 };
            chart[12] = new float[3] { 8.170667f, 55, -45 };
            chart[13] = new float[3] { 8.426666f, 55, -44 };
            chart[14] = new float[3] { 8.938666f, 55, -43 };
            chart[15] = new float[3] { 9.706667f, 56, -43 };
            chart[16] = new float[3] { 10.47467f, 57, -43 };
            chart[17] = new float[3] { 11.22133f, 58, -43 };
            chart[18] = new float[3] { 11.49867f, 59, -43 };
            chart[19] = new float[3] { 12.032f, 60, -43 };
            chart[20] = new float[3] { 12.8f, 61, -43 };
            chart[21] = new float[3] { 13.58933f, 62, -43 };
            chart[22] = new float[3] { 14.29333f, 63, -43 };
            chart[23] = new float[3] { 14.57067f, 64, -43 };
            chart[24] = new float[3] { 15.08267f, 65, -43 };
            chart[25] = new float[3] { 15.85067f, 66, -43 };
            chart[26] = new float[3] { 16.64f, 67, -43 };
            chart[27] = new float[3] { 17.38667f, 68, -43 };
            chart[28] = new float[3] { 17.64267f, 69, -43 };
            chart[29] = new float[3] { 18.15467f, 69, -42 };
            chart[30] = new float[3] { 18.944f, 69, -41 };
            chart[31] = new float[3] { 19.73333f, 69, -40 };
            chart[32] = new float[3] { 20.48f, 69, -39 };
            chart[33] = new float[3] { 20.736f, 69, -38 };
            chart[34] = new float[3] { 21.26933f, 69, -37 };
            chart[35] = new float[3] { 22.03733f, 69, -36 };
            chart[36] = new float[3] { 22.80533f, 69, -35 };
            chart[37] = new float[3] { 24.704f, 69, -34 };
            chart[38] = new float[3] { 25.856f, 69, -33 };
            chart[39] = new float[3] { 27.02933f, 69, -32 };
            chart[40] = new float[3] { 27.17867f, 69, -31 };
            chart[41] = new float[3] { 27.392f, 69, -30 };
            chart[42] = new float[3] { 27.776f, 69, -29 };
            chart[43] = new float[3] { 28.90667f, 68, -29 };
            chart[44] = new float[3] { 30.08f, 68, -28 };
            chart[45] = new float[3] { 30.25067f, 68, -27 };
            chart[46] = new float[3] { 30.464f, 68, -26 };
            chart[47] = new float[3] { 30.80533f, 68, -25 };
            chart[48] = new float[3] { 32f, 68, -24 };
            chart[49] = new float[3] { 33.13066f, 68, -23 };
            chart[50] = new float[3] { 33.344f, 68, -22 };
            chart[51] = new float[3] { 33.51467f, 68, -21 };
            chart[52] = new float[3] { 33.89867f, 67, -21 };
            chart[53] = new float[3] { 35.072f, 66, -21 };
            chart[54] = new float[3] { 36.224f, 65, -21 };
            chart[55] = new float[3] { 36.416f, 64, -21 };
            chart[56] = new float[3] { 36.58667f, 64, -22 };
            chart[57] = new float[3] { 36.97066f, 64, -23 };
            chart[58] = new float[3] { 38.144f, 64, -24 };
            chart[59] = new float[3] { 39.31733f, 64, -25 };
            chart[60] = new float[3] { 39.50933f, 64, -26 };
            chart[61] = new float[3] { 39.70133f, 64, -27 };
            chart[62] = new float[3] { 40.064f, 64, -28 };
            chart[63] = new float[3] { 41.23734f, 63, -28 };
            chart[64] = new float[3] { 42.368f, 62, -28 };
            chart[65] = new float[3] { 42.58133f, 61, -28 };
            chart[66] = new float[3] { 42.73067f, 60, -28 };
            chart[67] = new float[3] { 43.136f, 59, -28 };
            chart[68] = new float[3] { 44.30933f, 58, -28 };
            chart[69] = new float[3] { 45.48267f, 57, -28 };
            chart[70] = new float[3] { 45.65333f, 56, -28 };
            chart[71] = new float[3] { 45.86666f, 55, -28 };
            chart[72] = new float[3] { 46.208f, 54, -28 };
            chart[73] = new float[3] { 47.38133f, 54, -29 };
            chart[74] = new float[3] { 48.64f, 54, -30 };
            chart[75] = new float[3] { 48.72533f, 54, -31 };
            chart[76] = new float[3] { 48.896f, 54, -32 };
            chart[77] = new float[3] { 49.30133f, 54, -33 };
            chart[78] = new float[3] { 50.45333f, 54, -34 };
            chart[79] = new float[3] { 51.60534f, 54, -35 };
            chart[80] = new float[3] { 51.84f, 54, -36 };
            chart[81] = new float[3] { 51.968f, 53, -36 };
            chart[82] = new float[3] { 52.37333f, 52, -36 };
            chart[83] = new float[3] { 53.54667f, 51, -36 };
            chart[84] = new float[3] { 54.67733f, 50, -36 };
            chart[85] = new float[3] { 54.86934f, 49, -36 };
            chart[86] = new float[3] { 55.06133f, 48, -36 };
            chart[87] = new float[3] { 55.46667f, 47, -36 };
            chart[88] = new float[3] { 56.59733f, 46, -36 };
            chart[89] = new float[3] { 57.74933f, 45, -36 };
            chart[90] = new float[3] { 57.96267f, 44, -36 };
            chart[91] = new float[3] { 58.15467f, 43, -36 };
            chart[92] = new float[3] { 58.53867f, 42, -36 };
            chart[93] = new float[3] { 59.69067f, 41, -36 };
            chart[94] = new float[3] { 60.82133f, 40, -36 };
            chart[95] = new float[3] { 61.03467f, 39, -36 };
            chart[96] = new float[3] { 61.22667f, 38, -36 };
            chart[97] = new float[3] { 61.58933f, 37, -36 };
            chart[98] = new float[3] { 62.82667f, 36, -36 };
            chart[99] = new float[3] { 63.95733f, 35, -36 };
            chart[100] = new float[3] { 64.17067f, 34, -36 };
            chart[101] = new float[3] { 64.34133f, 33, -36 };
            chart[102] = new float[3] { 64.704f, 32, -36 };
            chart[103] = new float[3] { 65.81333f, 32, -35 };
            chart[104] = new float[3] { 66.98666f, 32, -34 };
            chart[105] = new float[3] { 67.24267f, 31, -34 };
            chart[106] = new float[3] { 67.392f, 30, -34 };
            chart[107] = new float[3] { 67.776f, 29, -34 };
            chart[108] = new float[3] { 68.90667f, 28, -34 };
            chart[109] = new float[3] { 70.10133f, 27, -34 };
            chart[110] = new float[3] { 70.272f, 26, -34 };
            chart[111] = new float[3] { 70.464f, 25, -34 };
            chart[112] = new float[3] { 70.86933f, 24, -34 };
            chart[113] = new float[3] { 72.04266f, 24, -33 };
            chart[114] = new float[3] { 73.152f, 24, -32 };
            chart[115] = new float[3] { 73.36533f, 24, -31 };
            chart[116] = new float[3] { 73.51466f, 24, -30 };
            chart[117] = new float[3] { 73.89867f, 24, -29 };
            chart[118] = new float[3] { 75.05067f, 24, -28 };
            chart[119] = new float[3] { 76.24533f, 24, -27 };
            chart[120] = new float[3] { 76.43733f, 25, -27 };
            chart[121] = new float[3] { 76.62933f, 25, -26 };
            chart[122] = new float[3] { 76.992f, 25, -25 };
            chart[123] = new float[3] { 78.144f, 25, -24 };
            chart[124] = new float[3] { 79.27467f, 25, -23 };
            chart[125] = new float[3] { 79.488f, 25, -22 };
            chart[126] = new float[3] { 79.68f, 25, -21 };
            chart[127] = new float[3] { 80.08533f, 25, -20 };
            chart[128] = new float[3] { 81.23734f, 24, -20 };
            chart[129] = new float[3] { 82.34666f, 23, -20 };
            chart[130] = new float[3] { 82.60267f, 22, -20 };
            chart[131] = new float[3] { 82.752f, 21, -20 };
            chart[132] = new float[3] { 83.15733f, 20, -20 };
            chart[133] = new float[3] { 84.33067f, 20, -19 };
            chart[134] = new float[3] { 85.44f, 20, -18 };
            chart[135] = new float[3] { 85.67467f, 20, -17 };
            chart[136] = new float[3] { 85.824f, 20, -16 };
        }
        else if(stageNum == 3)
        {
            chart = new float[207][];
            chart[0] = new float[3] { 1.258667f, 13f, -53 };
            chart[1] = new float[3] { 1.557333f, 14f, -53 };
            chart[2] = new float[3] { 1.877333f, 15f, -53 };
            chart[3] = new float[3] { 2.218667f, 16f, -53 };
            chart[4] = new float[3] { 2.517333f, 17f, -53 };
            chart[5] = new float[3] { 3.093333f, 18f, -53 };
            chart[6] = new float[3] { 3.690667f, 18f, -54 };
            chart[7] = new float[3] { 4.288f, 18f, -55 };
            chart[8] = new float[3] { 4.885334f, 18f, -56 };
            chart[9] = new float[3] { 6.101333f, 19f, -56 };
            chart[10] = new float[3] { 6.677333f, 20f, -56 };
            chart[11] = new float[3] { 6.976f, 21f, -56 };
            chart[12] = new float[3] { 7.232f, 22f, -56 };
            chart[13] = new float[3] { 7.850667f, 23f, -56 };
            chart[14] = new float[3] { 8.448f, 24f, -56 };
            chart[15] = new float[3] { 9.045333f, 25f, -56 };
            chart[16] = new float[3] { 9.344f, 26f, -56 };
            chart[17] = new float[3] { 9.685333f, 27f, -56 };
            chart[18] = new float[3] { 10.92267f, 28f, -56 };
            chart[19] = new float[3] { 12.88533f, 29f, -56 };
            chart[20] = new float[3] { 13.26933f, 29f, -55 };
            chart[21] = new float[3] { 13.632f, 29f, -54 };
            chart[22] = new float[3] { 13.84533f, 30f, -54 };
            chart[23] = new float[3] { 14.03733f, 31f, -54 };
            chart[24] = new float[3] { 14.44267f, 32f, -54 };
            chart[25] = new float[3] { 14.80533f, 33f, -54 };
            chart[26] = new float[3] { 15.01867f, 34f, -54 };
            chart[27] = new float[3] { 15.21067f, 35f, -54 };
            chart[28] = new float[3] { 16f, 36f, -54 };
            chart[29] = new float[3] { 16.42667f, 36f, -53 };
            chart[30] = new float[3] { 16.64f, 37f, -53 };
            chart[31] = new float[3] { 16.832f, 38f, -53 };
            chart[32] = new float[3] { 17.64267f, 39f, -53 };
            chart[33] = new float[3] { 18.02667f, 40f, -53 };
            chart[34] = new float[3] { 18.24f, 41f, -53 };
            chart[35] = new float[3] { 18.432f, 42f, -53 };
            chart[36] = new float[3] { 19.2f, 43f, -53 };
            chart[37] = new float[3] { 20.864f, 44f, -53 };
            chart[38] = new float[3] { 21.22667f, 45f, -53 };
            chart[39] = new float[3] { 21.41867f, 46f, -53 };
            chart[40] = new float[3] { 21.61067f, 47f, -53 };
            chart[41] = new float[3] { 22.42133f, 47f, -54 };
            chart[42] = new float[3] { 22.82667f, 47f, -55 };
            chart[43] = new float[3] { 23.04f, 48f, -55 };
            chart[44] = new float[3] { 23.232f, 49f, -55 };
            chart[45] = new float[3] { 24f, 50f, -55 };
            chart[46] = new float[3] { 24.40533f, 51f, -55 };
            chart[47] = new float[3] { 24.59733f, 52f, -55 };
            chart[48] = new float[3] { 24.832f, 52f, -56 };
            chart[49] = new float[3] { 25.57867f, 52f, -57 };
            chart[50] = new float[3] { 27.22133f, 52f, -58 };
            chart[51] = new float[3] { 28.05333f, 51f, -58 };
            chart[52] = new float[3] { 28.43733f, 50f, -58 };
            chart[53] = new float[3] { 28.84267f, 49f, -58 };
            chart[54] = new float[3] { 29.41867f, 48f, -58 };
            chart[55] = new float[3] { 29.61067f, 47f, -58 };
            chart[56] = new float[3] { 30.42133f, 46f, -58 };
            chart[57] = new float[3] { 30.82667f, 45f, -58 };
            chart[58] = new float[3] { 31.21067f, 44f, -58 };
            chart[59] = new float[3] { 31.616f, 43f, -58 };
            chart[60] = new float[3] { 32f, 43f, -59 };
            chart[61] = new float[3] { 32.832f, 43f, -60 };
            chart[62] = new float[3] { 33.62133f, 43f, -61 };
            chart[63] = new float[3] { 34.41067f, 43f, -62 };
            chart[64] = new float[3] { 34.79467f, 43f, -63 };
            chart[65] = new float[3] { 35.2f, 43f, -64 };
            chart[66] = new float[3] { 35.79733f, 43f, -65 };
            chart[67] = new float[3] { 36.032f, 44f, -65 };
            chart[68] = new float[3] { 36.82133f, 45f, -65 };
            chart[69] = new float[3] { 37.20533f, 46f, -65 };
            chart[70] = new float[3] { 37.61067f, 47f, -65 };
            chart[71] = new float[3] { 37.99467f, 48f, -65 };
            chart[72] = new float[3] { 38.4f, 49f, -65 };
            chart[73] = new float[3] { 40.02133f, 50f, -65 };
            chart[74] = new float[3] { 41.6f, 51f, -65 };
            chart[75] = new float[3] { 43.2f, 52f, -65 };
            chart[76] = new float[3] { 43.60534f, 52f, -64 };
            chart[77] = new float[3] { 44.032f, 52f, -63 };
            chart[78] = new float[3] { 44.224f, 52f, -62 };
            chart[79] = new float[3] { 44.416f, 53f, -62 };
            chart[80] = new float[3] { 44.82133f, 54f, -62 };
            chart[81] = new float[3] { 45.22667f, 55f, -62 };
            chart[82] = new float[3] { 45.41867f, 56f, -62 };
            chart[83] = new float[3] { 45.61067f, 57f, -62 };
            chart[84] = new float[3] { 46.42133f, 57f, -63 };
            chart[85] = new float[3] { 46.784f, 57f, -64 };
            chart[86] = new float[3] { 47.01867f, 57f, -65 };
            chart[87] = new float[3] { 47.232f, 57f, -66 };
            chart[88] = new float[3] { 48.02133f, 57f, -67 };
            chart[89] = new float[3] { 48.42667f, 57f, -68 };
            chart[90] = new float[3] { 48.61867f, 57f, -69 };
            chart[91] = new float[3] { 48.832f, 56f, -69 };
            chart[92] = new float[3] { 49.64267f, 55f, -69 };
            chart[93] = new float[3] { 51.22133f, 54f, -69 };
            chart[94] = new float[3] { 51.62667f, 53f, -69 };
            chart[95] = new float[3] { 51.81867f, 52f, -69 };
            chart[96] = new float[3] { 52.032f, 51f, -69 };
            chart[97] = new float[3] { 52.82133f, 50f, -69 };
            chart[98] = new float[3] { 53.20533f, 49f, -69 };
            chart[99] = new float[3] { 53.41867f, 48f, -69 };
            chart[100] = new float[3] { 53.61067f, 47f, -69 };
            chart[101] = new float[3] { 54.42133f, 46f, -69 };
            chart[102] = new float[3] { 54.82667f, 45f, -69 };
            chart[103] = new float[3] { 55.01867f, 45f, -70 };
            chart[104] = new float[3] { 55.232f, 44f, -70 };
            chart[105] = new float[3] { 56.04267f, 43f, -70 };
            chart[106] = new float[3] { 57.664f, 42f, -70 };
            chart[107] = new float[3] { 59.22133f, 41f, -70 };
            chart[108] = new float[3] { 59.584f, 40f, -70 };
            chart[109] = new float[3] { 60.8f, 39f, -70 };
            chart[110] = new float[3] { 62.4f, 39f, -69 };
            chart[111] = new float[3] { 62.784f, 39f, -68 };
            chart[112] = new float[3] { 64f, 39f, -67 };
            chart[113] = new float[3] { 65.6f, 39f, -66 };
            chart[114] = new float[3] { 66.00533f, 39f, -65 };
            chart[115] = new float[3] { 67.24267f, 39f, -64 };
            chart[116] = new float[3] { 68.8f, 39f, -63 };
            chart[117] = new float[3] { 69.184f, 39f, -62 };
            chart[118] = new float[3] { 70.4f, 38f, -62 };
            chart[119] = new float[3] { 71.95734f, 37f, -62 };
            chart[120] = new float[3] { 73.62133f, 36f, -62 };
            chart[121] = new float[3] { 74.02666f, 35f, -62 };
            chart[122] = new float[3] { 74.41067f, 34f, -62 };
            chart[123] = new float[3] { 74.624f, 33f, -62 };
            chart[124] = new float[3] { 74.816f, 32f, -62 };
            chart[125] = new float[3] { 75.22134f, 31f, -62 };
            chart[126] = new float[3] { 75.60533f, 30f, -62 };
            chart[127] = new float[3] { 75.81866f, 30f, -61 };
            chart[128] = new float[3] { 76.01067f, 30f, -60 };
            chart[129] = new float[3] { 76.82133f, 30f, -59 };
            chart[130] = new float[3] { 77.22667f, 29f, -59 };
            chart[131] = new float[3] { 77.41866f, 28f, -59 };
            chart[132] = new float[3] { 77.61066f, 27f, -59 };
            chart[133] = new float[3] { 78.4f, 26f, -59 };
            chart[134] = new float[3] { 78.80534f, 25f, -59 };
            chart[135] = new float[3] { 79.01867f, 24f, -59 };
            chart[136] = new float[3] { 79.232f, 23f, -59 };
            chart[137] = new float[3] { 80.02133f, 22f, -59 };
            chart[138] = new float[3] { 81.64267f, 21f, -59 };
            chart[139] = new float[3] { 82.02666f, 20f, -59 };
            chart[140] = new float[3] { 82.24f, 19f, -59 };
            chart[141] = new float[3] { 82.41067f, 18f, -59 };
            chart[142] = new float[3] { 83.22134f, 17f, -59 };
            chart[143] = new float[3] { 83.62666f, 16f, -59 };
            chart[144] = new float[3] { 83.81866f, 15f, -59 };
            chart[145] = new float[3] { 84.032f, 14f, -59 };
            chart[146] = new float[3] { 84.864f, 13f, -59 };
            chart[147] = new float[3] { 85.20533f, 13f, -60 };
            chart[148] = new float[3] { 85.41866f, 13f, -61 };
            chart[149] = new float[3] { 85.61066f, 13f, -62 };
            chart[150] = new float[3] { 86.42133f, 14f, -62 };
            chart[151] = new float[3] { 88.04266f, 15f, -62 };
            chart[152] = new float[3] { 88.832f, 16f, -62 };
            chart[153] = new float[3] { 89.216f, 17f, -62 };
            chart[154] = new float[3] { 89.62133f, 18f, -62 };
            chart[155] = new float[3] { 90.19733f, 19f, -62 };
            chart[156] = new float[3] { 90.432f, 20f, -62 };
            chart[157] = new float[3] { 91.22134f, 21f, -62 };
            chart[158] = new float[3] { 91.62666f, 22f, -62 };
            chart[159] = new float[3] { 91.98933f, 23f, -62 };
            chart[160] = new float[3] { 92.416f, 24f, -62 };
            chart[161] = new float[3] { 92.8f, 24f, -63 };
            chart[162] = new float[3] { 93.61066f, 24f, -64 };
            chart[163] = new float[3] { 94.42133f, 24f, -65 };
            chart[164] = new float[3] { 95.232f, 23f, -65 };
            chart[165] = new float[3] { 95.59467f, 22f, -65 };
            chart[166] = new float[3] { 96f, 21f, -65 };
            chart[167] = new float[3] { 96.59734f, 20f, -65 };
            chart[168] = new float[3] { 96.81067f, 19f, -65 };
            chart[169] = new float[3] { 97.6f, 18f, -65 };
            chart[170] = new float[3] { 98.02666f, 17f, -65 };
            chart[171] = new float[3] { 98.41067f, 16f, -65 };
            chart[172] = new float[3] { 98.816f, 15f, -65 };
            chart[173] = new float[3] { 99.24267f, 15f, -66 };
            chart[174] = new float[3] { 100.7787f, 15f, -67 };
            chart[175] = new float[3] { 102.4f, 15f, -68 };
            chart[176] = new float[3] { 104.0213f, 16f, -68 };
            chart[177] = new float[3] { 104.4267f, 17f, -68 };
            chart[178] = new float[3] { 104.832f, 18f, -68 };
            chart[179] = new float[3] { 105.024f, 19f, -68 };
            chart[180] = new float[3] { 105.216f, 20f, -68 };
            chart[181] = new float[3] { 105.6213f, 21f, -68 };
            chart[182] = new float[3] { 106.0267f, 22f, -68 };
            chart[183] = new float[3] { 106.2187f, 23f, -68 };
            chart[184] = new float[3] { 106.4107f, 24f, -68 };
            chart[185] = new float[3] { 107.2f, 25f, -68 };
            chart[186] = new float[3] { 107.6267f, 26f, -68 };
            chart[187] = new float[3] { 107.84f, 27f, -68 };
            chart[188] = new float[3] { 108.0107f, 28f, -68 };
            chart[189] = new float[3] { 108.8f, 29f, -68 };
            chart[190] = new float[3] { 109.2053f, 29f, -67 };
            chart[191] = new float[3] { 109.4187f, 29f, -66 };
            chart[192] = new float[3] { 109.632f, 29f, -65 };
            chart[193] = new float[3] { 110.4427f, 30f, -65 };
            chart[194] = new float[3] { 112.0213f, 31f, -65 };
            chart[195] = new float[3] { 112.4267f, 32f, -65 };
            chart[196] = new float[3] { 112.6187f, 33f, -65 };
            chart[197] = new float[3] { 112.832f, 34f, -65 };
            chart[198] = new float[3] { 113.6427f, 35f, -65 };
            chart[199] = new float[3] { 114.0053f, 35f, -66 };
            chart[200] = new float[3] { 114.24f, 35f, -67 };
            chart[201] = new float[3] { 114.4107f, 35f, -68 };
            chart[202] = new float[3] { 115.2213f, 35f, -69 };
            chart[203] = new float[3] { 115.6053f, 35f, -70 };
            chart[204] = new float[3] { 115.84f, 35f, -71 };
            chart[205] = new float[3] { 116.032f, 34f, -71 };
            chart[206] = new float[3] { 116.8213f, 33f, -71 };
        }
        //else if (stageNum == 4)
        //{

        //}
        //else if (stageNum == 5)
        //{

        //}

        // 커넥터 연결하기
        Connector connector = GetComponent<Connector>();
        connector.UpdateData();
        bgm.volume = connector.sounddata.bgm;
        effect.volume = connector.sounddata.effect;
        notesynkRange = connector.maingamedata.synk;
        judgeRange = connector.maingamedata.judge;
        
        print("커넥터 연결 완료");
        judgeRange = connector.maingamedata.judge;


        // 데이터 연결
        if (!connector.dataMan) return;
        dataMan = connector.dataMan;
        stageNum = dataMan.stageNum;
    }

}
