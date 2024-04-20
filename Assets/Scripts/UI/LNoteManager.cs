using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class LNoteManager : MonoBehaviour
{
    // 싱글톤
    public static LNoteManager l_instance;

    Dictionary<int, string[]> explain = new Dictionary<int, string[]>();
    public TextMeshProUGUI tJudge;
    public TextMeshProUGUI tJudgeValue;
    public TextMeshProUGUI tExplain;
    public TextMeshProUGUI tScore;

    public GameObject guidePrefab;
    public GameObject canvas;

    public LMove player;
    public AudioSource bgm;
    public Metronome metronome;
    public GameObject notePrefab;
    public GameObject preNote;
    public GameObject routeNote;
    public GameObject lRouteNote;
    public GameObject maskPrefab;

    // 상태 변수
    float[][] chart = new float[100][];
    Note[] note = new Note[300];
    public float time;
    public int route_idx;   // show chart idx
    public int judge_idx;  // judge chart idx

    // 판정범위
    public float perfect_range = 0.05f;
    public float good_range = 0.1f;
    public float bad_range = 0.2f;
    public float miss_range = 0.5f;
    public float synk_range = 0f;
    public float judge_range = 0f;

    // 설정 상수
    const int ctime = 1;
    const int cx = 1;
    const int cy = 1;
    const int cllen = 3;
    const int cldir = 4;
    Vector3 u = Vector3.up;
    Vector3 d = Vector3.down;
    Vector3 l = Vector3.left;
    Vector3 r = Vector3.right;
    Vector3 n = Vector3.zero;

    // tutorial 조건 달성
    public int isNext;
    public int score;

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

        {

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

        // right, left, up, down

        // 채보 1
        {

            //note[0] = new Note(1, new Vector3(1, 0), new Vector3[] { r, d, r, u, u, n }, 1); // long
            //note[1] = new Note(7, new Vector3(7, 0), new Vector3[] { l, u, r, d, n }, 1);
            //note[2] = new Note(12, new Vector3(12, 0), new Vector3[] { r, r, r, n }, 1);
            //note[3] = new Note(16, new Vector3(16, 0), new Vector3[] { r, r, n }, 1);
            //note[4] = new Note(19, new Vector3(19, 0), new Vector3[] { r, n }, 1);
            //note[5] = new Note(21, new Vector3(21, 0));    // short
            //note[6] = new Note(22, new Vector3(22, 0), new Vector3[] { r, n }, 1);
            //note[7] = new Note(24, new Vector3(24, 0), new Vector3[] { r, r, n }, 1);
            //note[8] = new Note(27, new Vector3(27, 0), new Vector3[] { r, r, r, n }, 1);
            //note[9] = new Note(31, new Vector3(31, 0), new Vector3[] { r, r, r, r, n }, 1);
            //note[10] = new Note(36, new Vector3(36, 0), new Vector3[] { r, r, r, r, r, n }, 1);

        }


        // 채보 2
        {

            note[0] = new Note(1.000f, new Vector3(8, -28));
            note[1] = new Note(1.770f, new Vector3(9, -28));
            note[2] = new Note(2.474f, new Vector3(9, -27), new Vector3[] { u, r, n }, 0.5f);
            note[3] = new Note(3.029f, new Vector3(10, -27));
            note[4] = new Note(3.690f, new Vector3(11, -27), new Vector3[] { d, d, d, r, u, u, n }, 1.5f);
            note[5] = new Note(5.696f, new Vector3(12, -27));
            note[6] = new Note(6.464f, new Vector3(13, -27));
            note[7] = new Note(6.997f, new Vector3(13, -28), new Vector3[] { d, r, n }, 0.5f);
            note[8] = new Note(7.744f, new Vector3(14, -28));
            note[9] = new Note(8.085f, new Vector3(14, -27));
            note[10] = new Note(8.237f, new Vector3(15, -27));
            note[11] = new Note(8.533f, new Vector3(16, -27));
            note[12] = new Note(8.704f, new Vector3(17, -27));
            note[13] = new Note(9.066f, new Vector3(17, -28));
            note[14] = new Note(9.237f, new Vector3(18, -28));
            note[15] = new Note(9.685f, new Vector3(19, -28));
            note[16] = new Note(10.474f, new Vector3(19, -27), new Vector3[] { u, r, n }, 0.2f);
            note[17] = new Note(11.008f, new Vector3(20, -27));
            note[18] = new Note(11.690f, new Vector3(21, -27), new Vector3[] { d, r, n }, 0.2f);
            note[19] = new Note(12.053f, new Vector3(22, -27));
            note[20] = new Note(12.202f, new Vector3(23, -27));
            note[21] = new Note(12.586f, new Vector3(24, -27));
            note[22] = new Note(12.736f, new Vector3(25, -27));
            note[23] = new Note(13.034f, new Vector3(26, -27));
            note[24] = new Note(13.226f, new Vector3(27, -27));
            note[25] = new Note(13.653f, new Vector3(27, -28));
            note[26] = new Note(14.506f, new Vector3(28, -28));
            note[27] = new Note(15.040f, new Vector3(29, -28));
            note[28] = new Note(15.701f, new Vector3(30, -28));
            note[29] = new Note(16.064f, new Vector3(31, -28));
            note[30] = new Note(16.149f, new Vector3(31, -27));
            note[31] = new Note(16.512f, new Vector3(32, -27));
            note[32] = new Note(16.704f, new Vector3(33, -27));
            note[33] = new Note(17.066f, new Vector3(34, -27));
            note[34] = new Note(17.237f, new Vector3(35, -27));
            note[35] = new Note(17.688f, new Vector3(36, -27));
            note[36] = new Note(18.474f, new Vector3(37, -27));
            note[37] = new Note(19.008f, new Vector3(38, -27));
            note[38] = new Note(19.712f, new Vector3(39, -27));
            note[39] = new Note(20.202f, new Vector3(40, -27));
            note[40] = new Note(20.672f, new Vector3(41, -27));
            note[41] = new Note(21.184f, new Vector3(42, -27));
            note[42] = new Note(21.717f, new Vector3(42, -26));
            note[43] = new Note(22.528f, new Vector3(43, -26));
            note[44] = new Note(23.040f, new Vector3(44, -26));
            note[45] = new Note(23.680f, new Vector3(45, -26));
            note[46] = new Note(24.192f, new Vector3(45, -27));
            note[47] = new Note(24.405f, new Vector3(46, -27));
            note[48] = new Note(24.704f, new Vector3(47, -27));
            note[49] = new Note(24.874f, new Vector3(48, -27));
            note[50] = new Note(25.173f, new Vector3(49, -27));
            note[51] = new Note(25.344f, new Vector3(49, -28));
            note[52] = new Note(25.834f, new Vector3(50, -28));
            note[53] = new Note(26.517f, new Vector3(51, -28));
            note[54] = new Note(27.050f, new Vector3(51, -27));
            note[55] = new Note(27.712f, new Vector3(52, -27));
            note[56] = new Note(28.074f, new Vector3(53, -27));
            note[57] = new Note(28.245f, new Vector3(54, -27));
            note[58] = new Note(28.586f, new Vector3(55, -27));
            note[59] = new Note(28.778f, new Vector3(56, -27));
            note[60] = new Note(29.098f, new Vector3(57, -27));
            note[61] = new Note(29.269f, new Vector3(58, -27));
            note[62] = new Note(29.738f, new Vector3(58, -28));
            note[63] = new Note(30.464f, new Vector3(59, -28));
            note[64] = new Note(30.976f, new Vector3(60, -28));
            note[65] = new Note(31.637f, new Vector3(61, -28));
            note[66] = new Note(32.064f, new Vector3(62, -28));
            note[67] = new Note(32.213f, new Vector3(63, -28));
            note[68] = new Note(32.576f, new Vector3(63, -27));
            note[69] = new Note(32.746f, new Vector3(63, -26));
            note[70] = new Note(33.088f, new Vector3(63, -25));
            note[71] = new Note(33.258f, new Vector3(63, -24));
            note[72] = new Note(33.685f, new Vector3(63, -23));
            note[73] = new Note(34.474f, new Vector3(63, -22));
            note[74] = new Note(34.965f, new Vector3(63, -21));
            note[75] = new Note(35.712f, new Vector3(63, -20));
            note[76] = new Note(36.160f, new Vector3(62, -20));
            note[77] = new Note(36.693f, new Vector3(61, -20));
            note[78] = new Note(37.205f, new Vector3(60, -20));
            note[79] = new Note(37.696f, new Vector3(59, -20));
            note[80] = new Note(38.528f, new Vector3(58, -20));
            note[81] = new Note(38.997f, new Vector3(57, -20));
            note[82] = new Note(39.680f, new Vector3(56, -20));
            note[83] = new Note(40.874f, new Vector3(55, -20));
            note[84] = new Note(41.109f, new Vector3(55, -21));
            note[85] = new Note(41.280f, new Vector3(54, -21));
            note[86] = new Note(41.813f, new Vector3(53, -21));
            note[87] = new Note(42.304f, new Vector3(52, -21));
            note[88] = new Note(42.752f, new Vector3(51, -21));
            note[89] = new Note(43.242f, new Vector3(50, -21));
            note[90] = new Note(44.800f, new Vector3(49, -21));
            note[91] = new Note(45.120f, new Vector3(48, -21));
            note[92] = new Note(45.312f, new Vector3(47, -21));
            note[93] = new Note(45.760f, new Vector3(47, -20));
            note[94] = new Note(46.272f, new Vector3(46, -20));
            note[95] = new Note(46.720f, new Vector3(46, -19));
            note[96] = new Note(47.232f, new Vector3(45, -19));
            note[97] = new Note(48.298f, new Vector3(44, -19));
            note[98] = new Note(49.344f, new Vector3(43, -19));
            note[99] = new Note(49.493f, new Vector3(42, -19));
            note[100] = new Note(49.664f, new Vector3(41, -19));
            note[101] = new Note(49.792f, new Vector3(40, -19));
            note[102] = new Note(50.090f, new Vector3(39, -19));
            note[103] = new Note(50.560f, new Vector3(38, -19));
            note[104] = new Note(50.709f, new Vector3(37, -19));
            note[105] = new Note(51.264f, new Vector3(36, -19));
            note[106] = new Note(51.434f, new Vector3(35, -19));
            note[107] = new Note(51.584f, new Vector3(34, -19));
            note[108] = new Note(51.733f, new Vector3(34, -20));
            note[109] = new Note(52.117f, new Vector3(33, -20));
            note[110] = new Note(52.565f, new Vector3(32, -20));
            note[111] = new Note(52.714f, new Vector3(31, -20));
            note[112] = new Note(53.077f, new Vector3(30, -20));
            note[113] = new Note(53.226f, new Vector3(29, -20));
            note[114] = new Note(54.677f, new Vector3(28, -20));
            note[115] = new Note(54.784f, new Vector3(27, -20));
            note[116] = new Note(55.104f, new Vector3(26, -20));
            note[117] = new Note(55.488f, new Vector3(25, -20));
            note[118] = new Note(56.000f, new Vector3(24, -20));
            note[119] = new Note(56.746f, new Vector3(23, -20));
            note[120] = new Note(57.066f, new Vector3(22, -20));
            note[121] = new Note(57.237f, new Vector3(21, -20));
            note[122] = new Note(57.685f, new Vector3(21, -21));
            note[123] = new Note(58.218f, new Vector3(20, -21));
            note[124] = new Note(58.666f, new Vector3(19, -21));
            note[125] = new Note(59.221f, new Vector3(18, -21));
            note[126] = new Note(60.757f, new Vector3(17, -21));
            note[127] = new Note(61.098f, new Vector3(16, -21));
            note[128] = new Note(61.248f, new Vector3(16, -20));
            note[129] = new Note(61.717f, new Vector3(15, -20));
            note[130] = new Note(62.186f, new Vector3(14, -20));
            note[131] = new Note(62.677f, new Vector3(13, -20));
            note[132] = new Note(63.210f, new Vector3(12, -20));
            note[133] = new Note(64.277f, new Vector3(11, -20));
            note[134] = new Note(65.322f, new Vector3(10, -20));
            note[135] = new Note(65.472f, new Vector3(9, -20));
            note[136] = new Note(65.621f, new Vector3(8, -20));
            note[137] = new Note(65.770f, new Vector3(7, -20));
            note[138] = new Note(66.112f, new Vector3(7, -19));
            note[139] = new Note(66.517f, new Vector3(6, -19));
            note[140] = new Note(66.688f, new Vector3(5, -19));
            note[141] = new Note(67.264f, new Vector3(5, -20));
            note[142] = new Note(67.413f, new Vector3(4, -20));
            note[143] = new Note(67.584f, new Vector3(3, -20));
            note[144] = new Note(67.754f, new Vector3(2, -20));
            note[145] = new Note(68.096f, new Vector3(1, -20));
            note[146] = new Note(68.565f, new Vector3(1, -19));
            note[147] = new Note(68.736f, new Vector3(1, -18));
            note[148] = new Note(69.077f, new Vector3(2, -18));
            note[149] = new Note(69.248f, new Vector3(3, -18));
            note[150] = new Note(70.720f, new Vector3(4, -18));
            note[151] = new Note(70.805f, new Vector3(4, -19));
            note[152] = new Note(71.125f, new Vector3(4, -20));
            note[153] = new Note(71.594f, new Vector3(5, -20));
            note[154] = new Note(72.064f, new Vector3(5, -19));
            note[155] = new Note(72.448f, new Vector3(6, -19));
            note[156] = new Note(72.874f, new Vector3(7, -19));
            note[157] = new Note(73.258f, new Vector3(8, -19));
            note[158] = new Note(73.429f, new Vector3(9, -19));
            note[159] = new Note(73.578f, new Vector3(9, -18));
            note[160] = new Note(73.749f, new Vector3(9, -17));
            note[161] = new Note(74.005f, new Vector3(9, -16));
            note[162] = new Note(74.133f, new Vector3(9, -15));
            note[163] = new Note(74.304f, new Vector3(9, -14));
            note[164] = new Note(74.624f, new Vector3(9, -13));
            note[165] = new Note(74.773f, new Vector3(9, -12));
            note[166] = new Note(75.093f, new Vector3(8, -12));
            note[167] = new Note(75.264f, new Vector3(8, -11));
            note[168] = new Note(75.733f, new Vector3(8, -10));
            note[169] = new Note(76.544f, new Vector3(8, -9));
            note[170] = new Note(77.013f, new Vector3(7, -9));
            note[171] = new Note(77.760f, new Vector3(7, -8));
            note[172] = new Note(78.058f, new Vector3(6, -8));
            note[173] = new Note(78.250f, new Vector3(5, -8));
            note[174] = new Note(78.570f, new Vector3(4, -8));
            note[175] = new Note(78.762f, new Vector3(3, -8));
            note[176] = new Note(79.104f, new Vector3(2, -8));
            note[177] = new Note(79.253f, new Vector3(1, -8));
            note[178] = new Note(79.744f, new Vector3(1, -7));
            note[179] = new Note(80.576f, new Vector3(1, -6));
            note[180] = new Note(81.024f, new Vector3(1, -5));
            note[181] = new Note(81.706f, new Vector3(1, -4));
            note[182] = new Note(82.069f, new Vector3(1, -3));
            note[183] = new Note(82.240f, new Vector3(1, -2));
            note[184] = new Note(82.624f, new Vector3(1, -1));
            note[185] = new Note(82.794f, new Vector3(1, 0));
            note[186] = new Note(83.114f, new Vector3(1, 1));
            note[187] = new Note(83.306f, new Vector3(2, 1));
            note[188] = new Note(83.733f, new Vector3(2, 0));
            note[189] = new Note(84.522f, new Vector3(3, 0));
            note[190] = new Note(85.013f, new Vector3(4, 0));
            note[191] = new Note(85.717f, new Vector3(5, 0));
            note[192] = new Note(86.186f, new Vector3(6, 0));
            note[193] = new Note(86.698f, new Vector3(7, 0));
            note[194] = new Note(87.210f, new Vector3(7, 1));
            note[195] = new Note(87.701f, new Vector3(8, 1));
            note[196] = new Note(88.512f, new Vector3(9, 1));
            note[197] = new Note(88.981f, new Vector3(9, 2));
            note[198] = new Note(89.706f, new Vector3(9, 3));
            note[199] = new Note(90.048f, new Vector3(10, 3));
            note[200] = new Note(90.240f, new Vector3(10, 4));
            note[201] = new Note(90.581f, new Vector3(10, 5));
            note[202] = new Note(90.773f, new Vector3(10, 6));
            note[203] = new Note(91.136f, new Vector3(10, 7));
            note[204] = new Note(91.306f, new Vector3(10, 8));
            note[205] = new Note(91.733f, new Vector3(9, 8));
            note[206] = new Note(92.522f, new Vector3(9, 9));
            note[207] = new Note(92.970f, new Vector3(10, 9));
            note[208] = new Note(93.696f, new Vector3(10, 10));
            note[209] = new Note(94.037f, new Vector3(10, 11));
            note[210] = new Note(94.208f, new Vector3(9, 11));
            note[211] = new Note(94.570f, new Vector3(9, 12));
            note[212] = new Note(94.720f, new Vector3(9, 13));
            note[213] = new Note(95.061f, new Vector3(8, 13));
            note[214] = new Note(95.253f, new Vector3(8, 14));
            note[215] = new Note(95.701f, new Vector3(8, 15));
            note[216] = new Note(96.512f, new Vector3(7, 15));
            note[217] = new Note(97.002f, new Vector3(6, 15));
            note[218] = new Note(97.664f, new Vector3(5, 15));
            note[219] = new Note(98.048f, new Vector3(4, 15));
            note[220] = new Note(98.218f, new Vector3(3, 15));
            note[221] = new Note(98.581f, new Vector3(2, 15));
            note[222] = new Note(98.794f, new Vector3(2, 14));
            note[223] = new Note(99.093f, new Vector3(2, 13));
            note[224] = new Note(99.264f, new Vector3(1, 13));
            note[225] = new Note(99.690f, new Vector3(0, 13));
            note[226] = new Note(100.480f, new Vector3(-1, 13));
            note[227] = new Note(100.992f, new Vector3(-2, 13));
            note[228] = new Note(101.696f, new Vector3(-3, 13));
            note[229] = new Note(102.186f, new Vector3(-3, 14));
            note[230] = new Note(102.677f, new Vector3(-3, 15));
            note[231] = new Note(103.189f, new Vector3(-3, 16));
            note[232] = new Note(103.701f, new Vector3(-3, 17));
            note[233] = new Note(104.512f, new Vector3(-3, 18));
            note[234] = new Note(104.960f, new Vector3(-3, 19));
            note[235] = new Note(105.642f, new Vector3(-3, 20));
        }

        StartCoroutine(StepNote4());
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
                ShowNewNote();
                //RunTime();
                NewRunTime();

                break;

            case Step.Synk:
                RunTime();
                PlayerReposition();
                ShowNote(1 - synk_range, transform);
        
                if (Input.anyKeyDown)
                {
                    Judge(time + judge_range);
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
        bgm.Stop();
        bgm.Play();
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
            //// 초기화
            //ResetChart();

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
        }

        bgm.Stop();
        player.enabled = true;
        PlayerReposition();
        yield return StartCoroutine(ShowNextNoteCo());
        yield return StartCoroutine(Count());

        // 시작
        bgm.Play();
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
        if (route_idx < chart.Length - 1 && time > chart[route_idx][ctime] - 1)
        {
            if (chart[route_idx][ctime] == 0) return;
            if (step == Step.Note1) PlayerReposition();
            // 노트 생성
            GameObject note1 = Instantiate(notePrefab, _parent);
            note1.transform.localPosition = Vector3.zero;
            route_idx++;

            // 박자 소리 출력
            Invoke("PlayBeat", _time);
        }
    }
    public void ShowNote(float _time, Vector3 _pos)
    {
        // 튜토리얼 채보는 1초 이상으로만 구성할 것.
        if (route_idx < chart.Length - 1 && time > chart[route_idx][0] - 1)
        {
            if (chart[route_idx][0] == 0) return;
            if (step == Step.Note1) PlayerReposition();
            // 노트 생성
            GameObject note1 = Instantiate(notePrefab);
            note1.transform.position = _pos;
            route_idx++;

            // 박자 소리 출력
            Invoke("PlayBeat", _time);  
        }
    }
    public void ShowNote(float _time)
    {
        ShowNote(_time, new Vector3(chart[route_idx][1], chart[route_idx][2], 0));
    }
    // 채보 노트 생성
    void ShowNote()
    {
        // 노트 끝나면 리턴
        //if (noteIndex > chart.Length - 1) return;
        if (route_idx > 0 && chart[route_idx][0] == 0) return;

 
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
        if (chart[route_idx][0] - 1 - synk_range < 0)
        {
            if (time > chart[route_idx][0] - 1)
            {
                MakeNote();
            }
            return;
        }

        // 현재 시간이 시작시간 이후로 데이터 시간이 지나면 생성
        else if (time > chart[route_idx][0] - 1 - synk_range)
        {
            // 4칸 앞 경로 띄우기
            if (route_idx < chart.Length - 4)
            {
                GameObject rt = MakeRoute(route_idx + 4);
                if (rt) rt.name = "route" + (route_idx + 4);
            }

            // 실제 노트 뿌리기
            MakeNote();
        }
    }
    void ShowNewNote()
    {
        // 노트 끝나면 리턴
        if (note[route_idx] == null) return;
        if (route_idx > 0 && note[route_idx].time == 0) return;

        // 처음 경로 4칸 띄우기
        if (route_idx == 0 && !GameObject.Find("route"))
        {
            GameObject rt = MakeRoute(note[0]);
            if (rt) rt.name = "route";
            MakeRoute(note[1]).name = "route" + 1;
            MakeRoute(note[2]).name = "route" + 2;
            MakeRoute(note[3]).name = "route" + 3;
        }

        // 노트가 1초 내인 경우만 따로 처리
        if (note[route_idx].time - 1 - synk_range < 0)
        {
            if (bgm.time > note[route_idx].time - 1)
            {
                MakeNote(ref note[route_idx]);
            }
            return;
        }

        // 현재 시간이 시작시간 이후로 데이터 시간이 지나면 생성
        else if (bgm.time > note[route_idx].time - 1 - synk_range)
        {
            GameObject rt = MakeRoute(note[route_idx + 4]);
            if (rt) rt.name = "route" + (route_idx + 4);

            // 실제 노트 뿌리기
            MakeNote(ref note[route_idx]);
        }
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
        var newnote = Instantiate(notePrefab);
        newnote.name = "note" + route_idx.ToString();
        newnote.transform.position = new Vector3(chart[route_idx][1], chart[route_idx][2], 0);

        // long note
        if (chart[route_idx][3] > 1)
        {
            newnote.transform.Rotate(Vector3.forward * 90 * chart[route_idx][4]);

            LNote longnote = newnote.GetComponent<LNote>();

            longnote.ltype = true;
            longnote.length = (int)chart[route_idx][3];
            longnote.s_time = chart[route_idx][0] - time;
            longnote.e_time = chart[route_idx + 1][0] - 1 - time;
            Vector3 head = Vector3.up;
            switch (chart[route_idx][4])
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

        route_idx++;
    }
    void MakeNote(ref Note _note)
    {
        var newnote = Instantiate(notePrefab);

        newnote.name = "note" + route_idx.ToString();
        newnote.transform.position = _note.pos;

        LNote longnote = newnote.GetComponent<LNote>();
        longnote.LongNote(_note);

        _note.note = newnote;

        route_idx++;
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
    GameObject MakeRoute(Note _note)
    {
        GameObject route = null;

        if (_note == null) return route;

        // 롱노트
        if (_note.ltype)
        {
            route = MakeLongRoute(_note);

            // 삭제될 시간 = 판정시간 - 현재시간 (판정될때 사라짐)
            Destroy(route, _note.time - time + _note.duration);
        }
        // 숏노트
        else
        {
            // route 생성
            route = Instantiate(routeNote);

            // route 위치 지정
            route.transform.position = _note.pos;

            // 삭제될 시간 = 판정시간 - 현재시간 (판정될때 사라짐)
            Destroy(route, _note.time - time + 0.5f);
        }

        return route;

    }
    GameObject MakeLongRoute(Note _note)
    {
        // 1. 부모 오브젝트 생성
        GameObject longroute = new GameObject("longroute");

        Vector3 curPos = _note.pos;
        for(int i = 0; i < _note.route.Length - 1; ++i)
        {
            GameObject route = null;
            GameObject mask = null;

            // 방향 바뀌면 롱루트 생성
            if (i == 0 || _note.route[i] != _note.route[i - 1])
            {
                route = Instantiate(lRouteNote);
                route.name = i.ToString();

                // 길이 조정
                int size = 1;
                int di = i;
                while (_note.route[di] == _note.route[i])
                {
                    size++;
                    ++di;
                }
                route.GetComponent<SpriteRenderer>().size = new Vector2(1, size);

                // 각도 조정
                Vector3 rot = Vector3.forward * 90;

                // 위치 조정
                Vector2 pos = curPos;
                if (_note.route[i] == u)
                {
                    rot *= 0;
                    pos += Vector2.down * 0.5f;
                }
                else if (_note.route[i] == l)
                {
                    rot *= 1;
                    pos += Vector2.right * 0.5f;
                }
                else if (_note.route[i] == d)
                {
                    rot *= 2;
                    pos += Vector2.up * 0.5f;
                }
                else if (_note.route[i] == r)
                {
                    rot *= 3;
                    pos += Vector2.left * 0.5f;
                }
                else if (_note.route[i] == n)
                {
                    rot *= 0;
                    pos += Vector2.down * 0.5f;
                }
                route.transform.Rotate(rot);
                route.transform.position = pos;

                // 부모 밑으로 넣기
                route.transform.SetParent(longroute.transform);
            }

            // 마스크 생성
            mask = Instantiate(maskPrefab);
            mask.name = "mask" + i.ToString();

            // 가로 마스크
            if(_note.route[i] == u || _note.route[i] == d)
            {
                mask.transform.localScale = new Vector3(0.935f, 0.2f);
            }
            // 세로 마스크
            else if(_note.route[i] == l || _note.route[i] == r)
            {
                mask.transform.localScale = new Vector3(0.2f, 0.935f);
            }

            mask.transform.position = curPos + _note.route[i] * 0.5f;

            mask.transform.SetParent(longroute.transform);

            // 다음 위치 업데이트
            curPos += _note.route[i];
        }

        // 설계
        {
            // 2. 꺾이는 만큼 롱루트 생성
            // - 롱루트 위치 조정 1
            // - 롱루트 길이 조정
            // - 롱루트 각도 조정
            // - 롱루트 위치 조정 2

            // 3. 루트 개수만큼 마스크 생성
            // - 마스크 가로 세로 스케일 조정
            // 가로 : 0.935, 0.2
            // 세로 : 0.2, 0.935
            // - 마스크 위치 조정

            // 처음과 끝, n일 경우 고려
        }

        return longroute;
    }

    public void RunTime()
    {
        // 튜토리얼 시간
        time += Time.deltaTime;

        //// miss 처리
        //if (chart[judge_idx][0] != 0 &&
        //    time > (chart[judge_idx][0] + bad_range + judge_range))
        if (chart[judge_idx][0] != 0 &&
            time > (chart[judge_idx][0] + bad_range + judge_range))
        {
            judge_idx++;

            // miss 처리
            tJudge.text = "MISS";
            isNext = 0;
        }
    }
    public void NewRunTime()
    {
        // 튜토리얼 시간
        time += Time.deltaTime;

        // miss
        if (note[judge_idx].time != 0 &&
            time > (note[judge_idx].time + bad_range + judge_range))
        {
            judge_idx++;

            // miss 처리
            tJudge.text = "MISS";
            isNext = 0;
        }
    }
    public void PlayBeat()
    {
        metronome.metAudio.Play();
    }

    public void NewJudge(float _time, float _x, float _y)
    {
        if (step == Step.Move || step == Step.Wait) return; // 상태 확인
        if (note[judge_idx].time == 0) return; // 채보 끝이면 리턴

        if (player.CurPos == note[judge_idx].pos) // 좌표 일치 확인
        {
            print("좌표 일치 " + judge_idx);
            NewJudge(_time);
        }
        else
        {
            print("좌표 불일치");
        }
    }
    public void NewJudge(float _time)
    {
        if (tJudgeValue) tJudgeValue.text = ((_time - (note[judge_idx].time + judge_range)) * 1000).ToString("0") + "ms";
        int judge_score = 0;

        // 판정시간 일치 확인
        if (_time < (note[judge_idx].time + perfect_range + judge_range) &&
            _time > (note[judge_idx].time - perfect_range + judge_range))  // PERFECT
        {
            isNext++;
            judge_score = 500;

            // judge
            tJudge.text = "PERFECT!";
        }
        else if (_time < (note[judge_idx].time + good_range + judge_range) &&
                _time > (note[judge_idx].time - good_range + judge_range))   // GOOD
        {
            isNext++;
            judge_score = 100;

            // judge
            tJudge.text = "GOOD";
        }
        else if (_time < (note[judge_idx].time + bad_range + judge_range) &&
                _time > (note[judge_idx].time - bad_range + judge_range))    // BAD
        {
            judge_score = 30;

            // judge
            tJudge.text = "BAD";
        }
        else if (_time < (note[judge_idx].time + miss_range + judge_range) &&
                _time > (note[judge_idx].time - miss_range + judge_range))  // MISS
        {
            judge_idx++;
            isNext = 0;

            // judge
            tJudge.text = "MISS";
            return;
        }
        else
        {
            // 좌표는 일치하나 시간 범위에 맞지 않음
            return;
        }

        // 롱노트 판정
        if (note[judge_idx].ltype)
        {
            StartCoroutine(LongJudge(judge_idx));
        }
        tJudge.text = ((_time - (note[judge_idx].time + judge_range)) * 1000).ToString("0");

        score += judge_score;
        tScore.text = score.ToString();
        judge_idx++;
    }

    IEnumerator LongJudge(int _idx)
    {
        LNote lnote = note[_idx].note.GetComponent<LNote>();
        Vector3 endPos = lnote.route[lnote.route.Length - 1];

        player.ChangeMode(MoveMode.Slide);
        while (Input.anyKey && note[_idx].note)
        {
            player.transform.position = note[_idx].note.transform.position;
            yield return null;
        }
        player.ChangeMode(MoveMode.Default);

        if (player.CurPos == endPos)
        {
            score += 100;
            tScore.text = score.ToString();
        }
    }
    public void Judge(float _time, float _x, float _y)
    {
        if (step == Step.Move || step == Step.Wait) return; // 상태 확인
        if (chart[judge_idx][0] == 0) return; // 채보 끝이면 리턴

        if( _time < 0)
        {
            // 롱노트
            judge_idx++;
            return;
        }
        if (player.CurPos.x == chart[judge_idx][1] && player.CurPos.y == chart[judge_idx][2]) // 좌표 일치 확인
        {
            Judge(_time);
        }
        else
        {
            //print("좌표 불일치");
        }
    }
    public void Judge(float _time)
    {
        if (tJudgeValue) tJudgeValue.text = ((time - (chart[judge_idx][0] + judge_range)) * 1000).ToString("0") + "ms";
        // 판정시간 일치 확인
        if (time < (chart[judge_idx][0] + perfect_range + judge_range) && time > (chart[judge_idx][0] - perfect_range + judge_range))  // PERFECT
        {
            judge_idx++;
            isNext++;

            // judge
            tJudge.text = "PERFECT!";
        }
        else if (time < (chart[judge_idx][0] + good_range + judge_range) && time > (chart[judge_idx][0] - good_range + judge_range))   // GOOD
        {
            judge_idx++;
            isNext++;

            // judge
            tJudge.text = "GOOD";
        }
        else if (time < (chart[judge_idx][0] + bad_range + judge_range) && time > (chart[judge_idx][0] - bad_range + judge_range))    // BAD
        {
            judge_idx++;

            // judge
            tJudge.text = "BAD";
        }
        else if (time < (chart[judge_idx][0] + miss_range + judge_range) && time > (chart[judge_idx][0] - miss_range + judge_range))  // MISS
        {
            judge_idx++;
            isNext = 0;

            // judge
            tJudge.text = "MISS";
        }
        else
        {
            // 좌표는 일치하나 시간 범위에 맞지 않음
        }
    }

    public void PlayerReposition()
    {
        // 시작 위치 조정

        // 채보 중간이면 탈출
        if (judge_idx == 0 && player.CurPos != note[0].pos)
        {

        }
        else if (note[judge_idx].time != 0 && judge_idx != 0)
        {
            return;
        }

        // 채보 끝났으면 초기화
        else if (judge_idx > 1 && bgm.time > note[judge_idx - 1].time + note[judge_idx - 1].duration + 0.5f)
        {
            route_idx = 0;
            judge_idx = 0;
            time = 0;
        }
        else
        {
            return;
        }


        // 맨 처음이면 시작위치 이동
        Vector3 firstNote = note[0].pos;

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
        for (int i = 0; i < chart.Length; i++)
        {
            note[i] = new Note(0, Vector3.zero);
        }
        route_idx = 0;
        judge_idx = 0;
        time = 0;
    }
    public void ResetTotal()
    {
        time = 0;
        route_idx = 0;
        judge_idx = 0;
        isNext = 0;
        //if(player != null) player.enabled = false;
        //CreateExplain();
    }
}
