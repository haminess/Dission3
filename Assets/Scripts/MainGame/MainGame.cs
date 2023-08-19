using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class MainGame : MonoBehaviour
{
    // ��� ��ũ��Ʈ���� ���� �����ϰ� �ϱ�
    public static MainGame instance;

    // ������Ʈ ����
    public AudioSource BGM;
    public StoryManager storyManager;

    // ������Ʈ ����
    public Player player;

    public MainGameData DataObject;


    // ä�� ���� ������
    public float[][] chart;              // ä��, ��: ä�� ��Ʈ �ν��Ͻ�, ��: {time, x, y}
    public int noteIndex;                // ä�� ������

    // �ð� ����
    public bool startButton = false;      // true�� ���ӽ���
    public int stageNum = 1;
    public bool stageButton = false;      // true�� ���ӽ���
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
    public GameObject resultUI;
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
        storyManager = GetComponent<StoryManager>();
        if (DataObject)
        {
            DataObject = GameObject.Find("Data").GetComponent<MainGameData>();
            stageNum = DataObject.STageNum;
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

        // ���� �ʱ�ȭ
        isStart = false;
        isGame = false;
        isEnd = false;
        BGM.Stop();
        
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
        resultUI.SetActive(false);
        gameCanvas.SetActive(false);
    }

    

    // Update is called once per frame
    void Update()
    {
        // startButton == true �Ǹ� ���ӽ���
        if (startButton)
        {
            GameStart();
            startButton = false;
        }

        // startButton == true �Ǹ� �������� ����
        if (stageButton)
        {
            StageStart(stageNum);
            stageButton = false;
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
        if (noteIndex == chart.Length && !isEnd)
        {
            // ���� ����
            isEnd = true;
            GameEnd();
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
        isStart = true;
        isGame = true;
        startTime = Time.time;
        BGM.Stop();
        BGM.time = 0;

        // 1�� �� ���� Ʋ��
        yield return new WaitForSeconds(1);
        BGM.Play();
    }


    // �������� ����
    public void StageStart(int _stageNum)
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
        progressUI.value = BGM.time / BGM.clip.length;
    }

    // ���� ����
    public void GameEnd()
    {
        // ���� ����
        isStart = false;
        isGame = false;
        judgeUI.text = "Game Clear!";
        judgeUI.color = Color.yellow;
        StartCoroutine(GameEndCo());

        // ������ ����

        // ���丮 ����

        // ��� ȭ�� ����

        // ��� ȭ�� ����
    }

    IEnumerator GameEndCo()
    {
        // 5�� �� ���� ����
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

        // ���â ����
        resultUI.SetActive(true);
        TextMeshProUGUI[] results = resultUI.transform.GetChild(1).gameObject.GetComponentsInChildren<TextMeshProUGUI>();
        results[0].text = rank;
        results[1].text = score.ToString();
        results[2].text = combo.ToString();
        results[3].text = perfect.ToString();
        results[4].text = good.ToString();
        results[5].text = bad.ToString();
        results[6].text = miss.ToString();
    }

    // ���� ������ ���� ���
    // ���� ����
    public void GameOver()
    {
        // ���� ����
        isStart = false;
        isGame = false;
        StartCoroutine(GameEndCo());

        // ��� ȭ�� ����
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
}
