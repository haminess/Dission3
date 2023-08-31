using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Unity.VisualScripting;
using UnityEngine.Rendering.Universal;

public class MainGame : MonoBehaviour
{
    // ���?��ũ��Ʈ���� ���� �����ϰ� �ϱ�
    public static MainGame instance;

    // ������Ʈ ����
    public AudioSource bgm;
    public AudioSource effect;
    public SoundManager soundMan;
    StoryManager storyManager;
    ChangeScene sceneManager;

    // ������Ʈ ����
    public Player player;
    public GameObject note;
    public GameObject judgeEffect;
    public Animation comboeff;
    public Animation judgeeff;

    // ������ �ҷ�����
    DataManager DataObject;

    // ä�� ���� ������
    public float[][] chart;              // ä��, ��: ä�� ��Ʈ �ν��Ͻ�, ��: {time, x, y}
    public int noteIndex;                // ä�� ������

    // �ð� ����
    public bool startButton = false;      // true�� ���ӽ���
    public int stageNum = 1;
    public bool stageMode = false;      // true�� ���ӽ���
    public bool isStart = false;         // true�� �������� ����
    public bool isGame = false;          // true�� ������?����
    public bool isEnd = false;           // true�� ��������
    public float gameTime;               // ������?
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
    public Color[] color;

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
    public int missScore = 0;
    public int comboScore = 10;


    // ���� UI
    public int uiHideTime = 5;
    public TextMeshProUGUI judgeUI;
    public TextMeshProUGUI comboUI;
    public TextMeshProUGUI combotext;

    // ���� UI
    public GameObject gameCanvas;
    public TextMeshProUGUI scoreUI;
    public TextMeshProUGUI countUI;
    public Slider progressUI;



    // Start is called before the first frame update
    public void Start()
    {

        // �����?�� ����Ʈ
        // ȿ������ �־���
        // ����Ʈ�� �־���
        // ���?���?
        // ������ ���?
        // ��ũ �ý���

        // �� �ʱ�ȭ
        MainGame.instance = this;

        // ������Ʈ �ҷ�����
        player = GameObject.Find("Player").GetComponent<Player>();
        sceneManager = GameObject.Find("SceneManager").GetComponent<ChangeScene>();
        storyManager = GetComponent<StoryManager>();

        // ���� ������ �ʱ�ȭ
        ResetMain();

        // ������ �ҷ�����
        GetMainData();



        // ä�� ������ �ҷ�����(chart ä�� �������迭 ��, ��Ʈ ����)
        {
            chart = new float[236][];
            chart[0] = new float[3] { 1, 7, -27 };
            chart[1] = new float[3] { 1.770f, 8, -27 };
            chart[2] = new float[3] { 2.474f, 9, -27 };
            chart[3] = new float[3] { 3.029f, 10, -27 };
            chart[4] = new float[3] { 3.690f, 11, -27 };
            chart[5] = new float[3] { 5.696f, 12, -27 };
            chart[6] = new float[3] { 6.464f, 12, -28 };
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

        // �������� ����̸�?�ٷ� ����
        if(stageMode)
        {
            StageStart();
        }

        noteIndex = 0;
        isStart = false;
        isGame = false;
        isEnd = false;
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


        if (!isGame)
            return;

        // ���� ���̸� ������?����
        if (isGame)
        {
            gameTime += Time.deltaTime;
            musicTime = bgm.time;
        }

        // miss ó��
        // **��������
        if (isGame && noteIndex < chart.Length - 1 &&                   // ���������鼭 ������ ��Ʈ�� �����ְ�
            bgm.time > (chart[noteIndex][0] + badRange + userRange))    // ���� �ð��� �����ð��� �������� (�����ð� + �������� + �����ð� 1��)
        {
            noteIndex++;
            miss++;
            combo = curCombo;
            curCombo = 0;
            score += missScore;
            judgeeff.Play();
            judgeUI.color = color[3];
            judgeUI.text = "MISS";
            comboUI.text = "";
            combotext.text = "";
            scoreUI.text = "SCORE\n" + score.ToString();
        }

        // ���� ����
        // ���?��Ʈ �����ϸ�
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
        ResetMain();
        gameCanvas.SetActive(true);
        StartCoroutine(GameStartCo());
    }

    IEnumerator GameStartCo()
    {
        print("���� ����");
        scoreUI.text = "";
        judgeUI.text = "";
        comboUI.text = "";
        combotext.text = "";

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
        bgm.Stop();                  
        bgm.time = 0;                

        // 1�� �� ���� Ʋ��
        yield return new WaitForSeconds(1);
        bgm.Play();
        yield return new WaitForSeconds(3);
        Settable(true);        // ����â ��밡��?
    }


    // �������� ����
    public void StageStart()
    {
        print("�������� ����");
        ResetMain();
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
        if(bgm.time > 0)
            progressUI.value = bgm.time / chart[chart.Length - 1][0];
    }

    // ���� ����
    public void GameEnd()
    {
        StartCoroutine(GameEndCo());


        // ���丮 ����

        // ���?ȭ�� ����

        // ���?ȭ�� ���?
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

        // 5�� �� ���� ���?
        yield return new WaitForSeconds(5);

        // ui �ʱ�ȭ
        judgeUI.text = "";
        comboUI.text = "";
        combotext.text = "";
        judgeUI.color = Color.white;

        // ���� �ʱ�ȭ
        bgm.Stop();
    }

    // ���� ������ ���� ���?
    // ���� ����
    public void GameOver()
    {
        StartCoroutine(GameEndCo());

    }
    IEnumerator GameOverCo()
    {
        // ���� ����
        yield return StartCoroutine(GameEndCo());

        // ���?ȭ�� ��ȯ
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

        // ���� ���丮 ���?
        yield return new WaitForSeconds(1);
        storyManager.storyID = stageNum - 1;
        if(collection > 3)
        {
            storyManager.storyID += 1;
        }
        else
        {
            storyManager.storyID += 2;
        }
        yield return StartCoroutine(storyManager.ShowStoryCo());

        // ���?ȭ�� ��ȯ
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
                    comboeff.Play();
                    score = score + perfectScore + comboScore * curCombo;
                    noteIndex++;
                    judgeUI.text = "PERFECT!";
                    judgeUI.color = color[0];
                    judgeeff.Play();
                    comboUI.text = curCombo.ToString();
                    combotext.text = "Combo";
                    scoreUI.text = "SCORE\n" + score.ToString();

                    GameObject effect = Instantiate(judgeEffect);
                    effect.transform.localPosition = new Vector3(chart[i][1], chart[i][2]);
                    Destroy(effect, 0.5f);
                    break;
                }
                else if (time < (chart[i][0] + goodRange + userRange) && time > (chart[i][0] - goodRange + userRange))   // GOOD
                {
                    good++;
                    curCombo++;
                    comboeff.Play();
                    score = score + goodScore + comboScore * curCombo;
                    noteIndex++;
                    judgeUI.text = "GOOD";
                    judgeUI.color = color[1];
                    judgeeff.Play();
                    comboUI.text = curCombo.ToString();
                    combotext.text = "Combo";
                    scoreUI.text = "SCORE\n" + score.ToString();

                    GameObject effect = Instantiate(judgeEffect);
                    effect.transform.localPosition = new Vector3(chart[i][1], chart[i][2]);
                    Destroy(effect, 0.5f);
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
                    judgeUI.color = color[2];
                    judgeeff.Play();
                    comboUI.text = "";
                    combotext.text = "";
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
                    judgeUI.color = color[3];
                    judgeeff.Play();
                    comboUI.text = "";
                    combotext.text = "";
                    scoreUI.text = "SCORE\n" + score.ToString();
                    break;
                }
                else                                                                             // ��ǥ�� ������ �ð��� �ȸ¾� ����X�� ���?
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
        bgm.Pause();
        
        // ���� �ð� ���� 
        if (chart[noteIndex][0] < 1) return;
        bgm.time = chart[noteIndex][0] - 1;
    }
    public void Continue()
    {
        // ���� ���?
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
        bgm.Play();
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


    public void ResetMain()
    {
        // ������ �ʱ�ȭ�Լ�
        // //���� �ʱ�ȭ
        noteIndex = 0;
        isStart = false;
        isGame = false;
        isEnd = false;
        bgm.Stop();
        Settable(false);     // ����â ���?

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
        if (stageNum >= 1 && stageNum <= 4)
        {
            score = 0;
        }
        combo = 0;
        curCombo = 0;
        collection = 0;
        if (stageNum >= 1 && stageNum <= 4)
        {
            collection = 0;
        }

        // UI ����
        gameCanvas.SetActive(false);
    }

    public void GetMainData()
    {
        // soundmanager �ҷ�����
        if (GameObject.Find("mainBGM"))
        {
            GameObject total = GameObject.Find("mainBGM");
            soundMan = total.GetComponent<SoundManager>();
            bgm = soundMan.bgm;
            effect = soundMan.effect;
        }

        if (GameObject.Find("SoundManager"))
        {
            GameObject total = GameObject.Find("SoundManager");
            soundMan = total.GetComponent<SoundManager>();
            bgm = soundMan.bgm;
            effect = soundMan.effect;
        }

        // ������ �ҷ�����
        var data = GameObject.Find("Data");
        if (data)
        {
            DataObject = GameObject.Find("Data").GetComponent<DataManager>();
            stageNum = DataObject.stageNum;
        }

        // ���� ������ �ҷ�����
        Connector connector = GetComponent<Connector>();
        connector.UpdateData();
        userRange = connector.maingamedata.judge;
    }
}
