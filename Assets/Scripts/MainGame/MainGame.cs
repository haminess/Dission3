using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Unity.VisualScripting;

public class MainGame : MonoBehaviour
{
    // ��� ��ũ��Ʈ���� ���� �����ϰ� �ϱ�
    public static MainGame instance;

    // ������Ʈ ����
    public AudioSource BGM;
    public StoryManager storyManager;
    public ChangeScene sceneManager;

    // ������Ʈ ����
    public Player player;

    // ������ �ҷ�����
    public MainGameData DataObject;

    // ä�� ���� ������
    public float[][] chart;              // ä��, ��: ä�� ��Ʈ �ν��Ͻ�, ��: {time, x, y}
    public int noteIndex;                // ä�� ������

    // �ð� ����
    public bool startButton = false;      // true�� ���ӽ���
    public int stageNum = 1;
    public bool stageMode = false;      // true�� ���ӽ���
    public bool isStart = false;         // true�� �������� ����
    public bool isGame = false;          // true�� �����ġ ����
    public bool isEnd = false;           // true�� ��������
    public float gameTime;               // �����ġ
    public float musicTime;              // ���� �ð�
    public float startTime;              // ���ӽ��� �ð�

    // ���ھ� ����
    public int score;
    public int combo;
    public int curCombo;
    public int perfect;
    public int good;   
    public int bad;    
    public int miss;
    public int collection;

    // ���� ���� ����
    public float perfectRange = 0.05f;
    public float goodRange = 0.1f;
    public float badRange = 0.2f;
    public float missRange = 0.5f;
    public float userRange = 0f;
    public float userRangePlus = 0.1f;

    // ���� ���� ����
    public int perfectScore = 500;
    public int goodScore = 300;
    public int badScore = 100;
    public int missScore = -500;
    public int comboScore = 10;

    // ���� UI
    public int uiHideTime = 5;
    public TextMeshProUGUI judgeUI;
    public TextMeshProUGUI comboUI;

    // ���� UI
    public GameObject gameCanvas;
    public TextMeshProUGUI scoreUI;
    public TextMeshProUGUI countUI;
    public Slider progressUI;

    // ���� ������Ʈ
    public GameObject note;


    // Start is called before the first frame update
    public void Start()
    {

        // ����� �� ����Ʈ
        // ȿ������ �־���
        // ����Ʈ�� �־���
        // ��� ���
        // ������ ���
        // ���� ���
        // ��ũ �ý���
        // �뷡 ���� �ý���

        // �� �ʱ�ȭ
        MainGame.instance = this;

        // ������ ����(ĳ����, ����������) �޾ƿ���
        player = GameObject.Find("Player").GetComponent<Player>();
        BGM = GameObject.Find("BGM").GetComponent<AudioSource>();
        sceneManager = GameObject.Find("SceneManager").GetComponent<ChangeScene>();
        storyManager = GetComponent<StoryManager>();

        var data = GameObject.Find("Data");
        if (data)
        {
            //DataObject = GameObject.Find("Data").GetComponent<MainGameData>();
            //stageNum = DataObject.STageNum;
        }

        // ä�� ������ �ҷ�����(chart ä�� �������迭 ��, ��Ʈ ����)
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

        // ���� �ʱ�ȭ
        isStart = false;
        isGame = false;
        isEnd = false;
        BGM.Stop();
        Settable(false);     // ����â ���

        // �ð� �ʱ�ȭ
        gameTime = 0;
        musicTime = 0;
        startTime = 0;

        // ���� ���� �ʱ�ȭ
        perfect = 0;
        good = 0;
        bad = 0;
        miss = 0;
        score = 0;
        combo = 0;
        curCombo = 0;
        collection = 0;

        // UI ����
        gameCanvas.SetActive(false);
    }

    

    // Update is called once per frame
    void Update()
    {
        // startButton == true �Ǹ� ���ӽ���
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


        // ���� ���̸� �����ġ ����
        if (isGame)
        {
            gameTime += Time.deltaTime;
            musicTime = BGM.time;
        }

        // miss ó��
        // **��������
        if (isGame && noteIndex < chart.Length - 1 &&                   // ���������鼭 ������ ��Ʈ�� �����ְ�
            BGM.time > (chart[noteIndex][0] + badRange + userRange))    // ���� �ð��� �����ð��� �������� (�����ð� + �������� + �����ð� 1��)
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

        // ���� ����
        // ��� ��Ʈ �����ϸ�
        if (noteIndex > chart.Length - 1 && !isEnd)
        {
            // ���� ����
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

        // ���� ���൵
        MusicProgress();
    }

    // ���� ����
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

        // �÷��̾� ��Ʈ ������ ��ġ
        PlayerReposition();

        // ù ��Ʈ �����ֱ� 
        yield return StartCoroutine(ShowNextNoteCo());

        // ī��Ʈ
        yield return StartCoroutine(TimeCountCo(judgeUI));

        // ���ӽ���
        isStart = true;        // ���� �ߴ���
        isGame = true;         // ���� ������
        startTime = Time.time;

        // ���� �ʱ�ȭ
        BGM.Stop();                  
        BGM.time = 0;                

        // 1�� �� ���� Ʋ��
        yield return new WaitForSeconds(1);
        BGM.Play();
        yield return new WaitForSeconds(3);
        Settable(true);        // ����â ��밡��
    }


    // �������� ����
    public void StageStart()
    {
        Start();
        gameCanvas.SetActive(true);
        StartCoroutine(StageStartCo());
    }

    public IEnumerator StageStartCo()
    {
        yield return StartCoroutine(storyManager.ShowStoryCo());
        GameStart();
    }

    // ���� ���൵
    public void MusicProgress()
    {
        // ���� ���� ���൵
        //progressUI.value = BGM.time / BGM.clip.length;

        // ä�� ���� ���൵
        progressUI.value = BGM.time / chart[chart.Length - 1][0];
    }

    // ���� ����
    public void GameEnd()
    {
        StartCoroutine(GameEndCo());


        // ���丮 ����

        // ��� ȭ�� ����

        // ��� ȭ�� ����
    }

    IEnumerator GameEndCo()
    {
        // ���� ����
        isStart = false;
        isGame = false;

        Settable(true);     // ����â ����

        // score ui ����
        judgeUI.text = "Game Clear!";
        judgeUI.color = Color.yellow;

        // ������ ����
        GameObject.Find("ResultData").GetComponent<ResultManager>().SendResult();

        // 5�� �� ���� ����
        yield return new WaitForSeconds(5);

        // ui �ʱ�ȭ
        judgeUI.text = "";
        comboUI.text = "";
        judgeUI.color = Color.white;

        // ���� �ʱ�ȭ
        BGM.Stop();
    }

    // ���� ������ ���� ���
    // ���� ����
    public void GameOver()
    {
        StartCoroutine(GameEndCo());

    }
    IEnumerator GameOverCo()
    {
        // ���� ����
        yield return StartCoroutine(GameEndCo());

        // ��� ȭ�� ��ȯ
        yield return new WaitForSeconds(1);
        sceneManager.ToScoreScene();
    }

    public void StageEnd()
    {
        StartCoroutine(StageEndCo());

    }
    IEnumerator StageEndCo()
    {
        // ���� ����
        yield return StartCoroutine(GameEndCo());

        // ���� ���丮 ���
        yield return new WaitForSeconds(1);
        yield return StartCoroutine(storyManager.ShowStoryCo());

        // ��� ȭ�� ��ȯ
        yield return new WaitForSeconds(1);
        sceneManager.ToScoreScene();
    }

    // ���� �Լ�
    // �̵��� ������ ȣ��
    public void Judge(float time, float x, float y)
    {
        // ���� ���� �������� ����
        if (!isGame) return;

        // ���� �Լ� *****
        // ����
        // time: player �̵� �ð�,
        // x: player x��ǥ,
        // y: player y��ǥ
        // chart[i][0]: note ���� �ð�,
        // chart[i][1]: note x��ǥ,
        // chart[i][2]: note y��ǥ,
        // i: chartCount(0 ~ ��Ʈ ����)

        // �������� ��Ʈ �����ð� 1�� ���ֱ�
        //time -= 1;

        // ���� ���ķ� 10���� ��ǥ �´� �����͹迭 ã��
        // **��������: ���� �ȵ� 10�� ��Ʈ�� �����ϴ� �ý���

        for (int i = noteIndex; i < noteIndex + 10; i++)
        {
            if (i > chart.Length - 1) break; // ������ ��Ʈ���� ũ�� break
            if (x == chart[i][1] && y == chart[i][2]) // ��ǥ ��ġ�ϸ�
            {

                // �ð� ���� �´��� Ȯ��
                if (time < (chart[i][0] + perfectRange + userRange) && time > (chart[i][0] - perfectRange + userRange))  // PERFECT
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
                else if (time < (chart[i][0] + goodRange + userRange) && time > (chart[i][0] - goodRange + userRange))   // GOOD
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
                else if (time < (chart[i][0] + badRange + userRange) && time > (chart[i][0] - badRange + userRange))    // BAD
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
                else if (time < (chart[i][0] + missRange + userRange) && time > (chart[i][0] - missRange + userRange))  // MISS
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
                else                                                                             // ��ǥ�� ������ �ð��� �ȸ¾� ����X�� ���
                {
                    break; 
                }
            }
        }
    }

    public void Stop()
    {
        if (!isGame) return;

        // ���� ����
        isGame = false;

        // ���� �ð��� ���� ��Ʈ�� �ð����� ����(�ٽ� ���� ����)
        gameTime = chart[noteIndex][0];

        // ���� ����
        BGM.Pause();
        
        // ���� �ð� ���� 
        if (chart[noteIndex][0] < 1) return;
        BGM.time = chart[noteIndex][0] - 1;
    }
    public void Continue()
    {
        // ���� ���
        // ���� �� ���� ���� ����
        if (isStart)
        {
            StartCoroutine(ContinueCo());
        }
    }

    IEnumerator ContinueCo()
    {
        // �÷��̾� ��Ʈ ������ ��ġ
        PlayerReposition();

        yield return StartCoroutine(ShowNextNoteCo());
        yield return StartCoroutine(TimeCountCo());

        // ���� ����(��Ʈ ����)
        isGame = true;

        // 1�� �Ŀ� ���� ����
        // ��Ʈ ���� �ð� ������
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
        // �÷��̾� ��Ʈ ���� ��ġ

        Vector2 firstNote = new Vector2(
                MainGame.instance.chart[MainGame.instance.noteIndex][1],
                MainGame.instance.chart[MainGame.instance.noteIndex][2]);

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
}
