using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class Tutorial : MonoBehaviour
{
    // �̰� ��ųʸ��� �߾� Ʃ�丮���� ������ �� �ȵż�
    Dictionary<int, string[]> explain = new Dictionary<int, string[]>();
    public TextMeshProUGUI tJudge;
    public TextMeshProUGUI tExplain;

    public TutorialMove player;
    public AudioSource bgm;
    public Metronome metronome;
    public GameObject note;
    public GameObject preNote;

    float[][] chart = new float[100][];
    public float time;
    public int noteIndex;
    public int curNote;


    // ��������
    public float perfectRange = 0.05f;
    public float goodRange = 0.1f;
    public float badRange = 0.2f;
    public float missRange = 0.5f;
    public float userRange = 0f;


    // tutorial ���� �޼�
    public int isNext;

    public enum Step
    {
        Wait,
        Move,
        Note1,
        Note2,
        Note3,
        Note4
        // �ȳ�, �б��� �����ϱ� ���� ���۹��� ���� �Ұ��Ұ�!
        // Move
        // ���� Ű������ ȭ��ǥ Ű�� ������ ��, ��, ��, ��� �̵��� �� �־�.
        // �ѹ� �̵��غ���?
        // Note1
        // ���߾�! �������� '��Ʈ'�� �����ٰ�!
        // '��Ʈ'�� �츮�� ����� �� ���� ���ڿ� �ش��ϰ�, ���ڿ� ���� ��Ʈ�� �̵��ϸ� ������ �� �־�.
        // ���ִ� ���� ù ��Ʈ ��ġ�� ������ ����, 3�� ī��Ʈ �Ŀ� ������.
        // ������ ��Ʈ�� �� ä������ ��������� �ٲ�� Ÿ�ֿ̹� ��Ʈ�� �̵��ϸ� ��.
        // �ѹ� �����غ���?
        // Note2
        // �� ������ �̵� �� ����?
        // Note3
        // ���߾�! ���� ������ ������ ��Ʈ�� ���� �̵��غ���.
        // Note4
        // ���߾�! ���� �پ��� ���ڿ� ���� ��Ʈ�� ���� �̵��غ���.
    }

    public Step step = Step.Wait;

    // Start is called before the first frame update
    void Start()
    {
        ResetTotal();

        // Ʃ�丮�� ����
        StartCoroutine(StartTutorial());
    }

    // Update is called once per frame
    void Update()
    {
        switch (step)
        {
            case Step.Wait:
                break;

            case Step.Move:
                if (Input.GetKeyDown(KeyCode.UpArrow))
                {
                    isNext |= 0b1000;
                    tJudge.text += "��";
                }
                if (Input.GetKeyDown(KeyCode.DownArrow))
                {
                    isNext |= 0b0100;
                    tJudge.text += "��";
                }
                if (Input.GetKeyDown(KeyCode.LeftArrow))
                {
                    isNext |= 0b0010;
                    tJudge.text += "��";
                }
                if (Input.GetKeyDown(KeyCode.RightArrow))
                {
                    isNext |= 0b0001;
                    tJudge.text += "��";
                }

                // ���
                if (isNext== 0b1111)
                {
                    player.enabled = false;
                    step = Step.Wait;
                    isNext = 0;
                    StartCoroutine(StepNote1());
                }
                break;

            case Step.Note1:
                RunTime();
                ShowNote();
                if (Input.anyKeyDown)
                {
                    Judge(time, player.CurPos.x, player.CurPos.y);
                }

                // ���
                if (isNext > 5)
                {
                    player.enabled = false;
                    step = Step.Wait;
                    isNext = 0;
                    StartCoroutine(StepNote2());
                }
                break;

            case Step.Note2:
                RunTime();
                ShowNote();
                if (Input.anyKeyDown)
                {
                    Judge(time, player.CurPos.x, player.CurPos.y);
                }

                // ���
                if (isNext > 5)
                {
                    player.enabled = false;
                    step = Step.Wait;
                    isNext = 0;
                    StartCoroutine(StepNote3());
                }
                break;

            case Step.Note3:
                RunTime();
                ShowNote();
                if (Input.anyKeyDown)
                {
                    Judge(time, player.CurPos.x, player.CurPos.y);
                }

                // ���
                if (isNext > 5)
                {
                    player.enabled = false;
                    step = Step.Wait;
                    isNext = 0;
                    StartCoroutine(StepNote4());
                }
                break;

            case Step.Note4:
                RunTime();
                ShowNote();
                if (Input.anyKeyDown)
                {
                    Judge(time, player.CurPos.x, player.CurPos.y);
                }

                // ���
                if (isNext > 10)
                {
                    StartCoroutine(FinishTutorial());
                }

                break;
        }
    }

    void CreateExplain()
    {
        explain.Add(0, new string[] { "�ȳ�, �б��� �����ϱ� ���� ���۹��� ���� �Ұ��Ұ�!",
                                      "���� Ű������ ȭ��ǥ Ű�� ������ ��, ��, ��, ��� �̵��� �� �־�.",
                                      "�ѹ� �̵��غ���?"});
        explain.Add(1, new string[] { "���߾�! �������� '��Ʈ'�� �����ٰ�!",
                                      "'��Ʈ'�� �츮�� ����� �� ���� ���ڿ� �ش��ϰ�, ���ڿ� ���� ��Ʈ�� �̵��ϸ� ������ �� �־�.",
                                      "���ִ� ���� ù ��Ʈ ��ġ�� ������ ����, 3�� ī��Ʈ �Ŀ� ������.",
                                      "������ ��Ʈ�� �� ä������ ��������� �ٲ�� Ÿ�ֿ̹� ��Ʈ�� �̵��ϸ� ��.",
                                      "�ѹ� �����غ���?"});
        explain.Add(2, new string[] { "���߾�! �� ������ �̵� �� ����?" });
        explain.Add(3, new string[] { "���߾�! ���� ������ ������ ��Ʈ�� ���� �̵��غ���." });
        explain.Add(4, new string[] { "���߾�! ���� �پ��� ���ڿ� ���� ��Ʈ�� ���� �̵��غ���." });
        explain.Add(5, new string[] { "�Ǹ���! ���� ȥ�ڼ��� ����� ���ƴٴ� �� �ְڴ°�?" ,
                                      "���� ������ ���� ��Ʈ�� ��ġ�ų� Ʋ�� ������ ���� LIFE�� �ϳ��� �پ���.",
                                      "LIFE�� 0���� �پ��� ��� ���� �����Ǵ� ������!",
                                      "�׸��� �������� Perfect, Good, Bad, Miss�� �ִµ�,",
                                      "�󸶳� ��Ȯ�� ���ڸ� ����� ���� ���� ���� ������ ȹ���� �� �־�.",
                                      "�������� Combo ������ �ݿ��Ǵµ�, Bad�� Miss�� ������ 0���� �ʱ�ȭ�Ǵ� ������!",
                                      "������ �󸶳� ����Ŀ� ���� �������� ������ �޶����ž�. �׷��� ������ �ؾ߰���?",
                                      "��, ���� �б� �� �ϸ� ���ҳ�? ��ſ� �б� ��Ȱ�� ��ܺ���!"});
    }

    IEnumerator ShowExplain(int _id)
    {
        tExplain.text = "";
        tJudge.text = "";
        for (int i = 0; i < explain[_id].Length; i++)
        {
            tExplain.text = explain[_id][i];
            yield return new WaitForSeconds(4);
        }
        tExplain.text = "";
    }

    // Ʃ�丮�� �帧
    IEnumerator StartTutorial()
    {
        yield return StartCoroutine(ShowExplain(0));
        tExplain.text = "��, ��, ��, ��� �̵��غ���";
        step = Step.Move;
        player.enabled = true;
    }
    IEnumerator StepNote1()
    {
        yield return StartCoroutine(ShowExplain(1));
        tExplain.text = "���ڿ� ���� �̵��غ���";

        // �÷���
        // ä��1
        {
            // �ʱ�ȭ
            ResetChart();

            // ����
            chart[0] = new float[3] { 1.5f, 1, 0 };
        }

        bgm.Stop();
        player.enabled = true;
        PlayerReposition();
        yield return StartCoroutine(ShowNextNoteCo());
        yield return StartCoroutine(Count());

        // ����
        step = Step.Note1;
    }
    IEnumerator StepNote2()
    {
        yield return StartCoroutine(ShowExplain(2));
        tExplain.text = "���ڿ� ���� �̵��غ���";

        // �÷���
        // ä��2
        {
            // �ʱ�ȭ
            ResetChart();

            // ����
            chart[0] = new float[3] { 1, 1, 0 };
            chart[1] = new float[3] { 2, 2, 0 };
            chart[2] = new float[3] { 3, 3, 0 };
            chart[3] = new float[3] { 4, 4, 0 };
            chart[4] = new float[3] { 5, 5, 0 };
            chart[5] = new float[3] { 6, 6, 0 };
            chart[6] = new float[3] { 7, 7, 0 };
            chart[7] = new float[3] { 8, 8, 0 };
            chart[8] = new float[3] { 9, 9, 0 };
            chart[9] = new float[3] { 10, 10, 0 };
        }

        bgm.Stop();
        player.enabled = true;
        PlayerReposition();
        yield return StartCoroutine(ShowNextNoteCo());
        yield return StartCoroutine(Count());

        // ����
        step = Step.Note2;
    }
    IEnumerator StepNote3()
    {
        yield return StartCoroutine(ShowExplain(3));

        // �÷���
        // ä��3
        {
            // �ʱ�ȭ
            ResetChart();

            // ����
            chart[0] = new float[3] { 1.0f, 1, 0 };
            chart[1] = new float[3] { 1.5f, 2, 0 };
            chart[2] = new float[3] { 2.0f, 3, 0 };
            chart[3] = new float[3] { 2.5f, 4, 0 };
            chart[4] = new float[3] { 3.0f, 5, 0 };
            chart[5] = new float[3] { 3.5f, 6, 0 };
            chart[6] = new float[3] { 4.0f, 7, 0 };
            chart[7] = new float[3] { 4.5f, 8, 0 };
            chart[8] = new float[3] { 5.0f, 9, 0 };
            chart[9] = new float[3] { 5.5f, 10, 0 };
        }

        bgm.Stop();
        player.enabled = true;
        PlayerReposition();
        yield return StartCoroutine(ShowNextNoteCo());
        yield return StartCoroutine(Count());

        // ����
        step = Step.Note3;
    }
    IEnumerator StepNote4()
    {
        yield return StartCoroutine(ShowExplain(4));
        tExplain.text = "���ڿ� ���� �̵��غ���";

        // �÷���
        // ä��4
        {
            // �ʱ�ȭ
            ResetChart();

            // ����
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

        // ����
        step = Step.Note4;
    }
    IEnumerator FinishTutorial()
    {
        step = Step.Wait;
        yield return StartCoroutine(ShowExplain(5));
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

    public void ShowNote()
    {
        // Ʃ�丮�� ä���� 1�� �̻����θ� ������ ��.
        if (noteIndex < chart.Length - 1 && time > chart[noteIndex][0] - 1)
        {
            if (chart[noteIndex][0] == 0) return;
            if(step == Step.Note1) PlayerReposition();
            // ��Ʈ ����
            GameObject note1 = Instantiate(note);
            note1.transform.position = new Vector3(chart[noteIndex][1], chart[noteIndex][2], 0);
            noteIndex++;

            // ���� �Ҹ� ���
            Invoke("PlayBeat", 1f);
        }
    }
    public void RunTime()
    {
        // Ʃ�丮�� �ð�
        time += Time.deltaTime;


        // miss ó��
        if (chart[curNote][0] != 0 &&
            time > (chart[curNote][0] + badRange + userRange))
        {
            curNote++;

            // miss ó��
            tJudge.text = "MISS";
            isNext = 0;
        }
        if(chart[curNote][0] == 0)
        {
            noteIndex = 0;
            curNote = 0;
            time = 0;
            PlayerReposition();
        }
    }

    public void PlayBeat()
    {
        metronome.metAudio.Play();
    }

    public void Judge(float _time, float _x, float _y)
    {
        if (step == Step.Move || step == Step.Wait) return; // ���� Ȯ��
        if (chart[curNote][0] == 0) return; // ä�� ���̸� ����
        if (player.CurPos.x == chart[curNote][1] && player.CurPos.y == chart[curNote][2]) // ��ǥ ��ġ Ȯ��
        {

            // �����ð� ��ġ Ȯ��
            if (time < (chart[curNote][0] + perfectRange + userRange) && time > (chart[curNote][0] - perfectRange + userRange))  // PERFECT
            {
                curNote++;
                isNext++;

                // judge
                tJudge.text = "PERFECT!";
            }
            else if (time < (chart[curNote][0] + goodRange + userRange) && time > (chart[curNote][0] - goodRange + userRange))   // GOOD
            {
                curNote++;
                isNext++;

                // judge
                tJudge.text = "GOOD";
            }
            else if (time < (chart[curNote][0] + badRange + userRange) && time > (chart[curNote][0] - badRange + userRange))    // BAD
            {
                curNote++;

                // judge
                tJudge.text = "BAD";
            }
            else if (time < (chart[curNote][0] + missRange + userRange) && time > (chart[curNote][0] - missRange + userRange))  // MISS
            {
                curNote++;
                isNext = 0;

                // judge
                tJudge.text = "MISS";
            }
            else
            {
                // ��ǥ�� ��ġ�ϳ� �ð� ������ ���� ����
                print("��ǥ�� ��ġ");
            }
        }
        else
        {
            print("��ǥ ����ġ");
        }
    }

    public void PlayerReposition()
    {
        // ���� ��ġ ����

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
        step = Step.Wait;
        time = 0;
        noteIndex = 0;
        curNote = 0;
        isNext = 0;
        player.enabled = false;
        CreateExplain();
    }
}