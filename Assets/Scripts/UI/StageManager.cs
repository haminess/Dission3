using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class StageManager : MonoBehaviour
{
    // ������ �ҷ�����
    private MainGameData maingamedata => DataManager.Instance.maingamedata;
    private SoundData sounddata => DataManager.Instance.sounddata;

    bool find = false;
    DataManager data;

    // ���� ���� ����
    public GameObject[] stage;
    public bool[] isUnlock;
    public int[] stageScore = new int[4];
    public int curStage = 2;
    float highlightTime = 0;

    // ����
    // �������� ���� ����
    public AudioSource bgm;
    public AudioSource effect;
    public SoundManager soundMan;

    public string[] location = { "����", "����", "���", "����" , "���"};
    public int[] difficulty = { 3, 1, 3, 2, 2 };   // 1: ����, 2: ����, 3: �����

    // �������� ������Ʈ
    TextMeshProUGUI[] stageInfo;

    // Start is called before the first frame update
    void Start()
    {
        stageInfo = GameObject.Find("StagePanel").GetComponentsInChildren<TextMeshProUGUI>();

        // �ʱⰪ ����
        //curStage = 2;


        isUnlock[0] = true;

        ConnectData();

        // �رݱ��
        SetStageLock();

        ShowStage();

        SetStage(2);
    }

    public void SetStage(int _stage)
    {
        curStage = _stage;
        if(data)
            data.stageNum = _stage;
    }
    public void SetHighLight(float _highlight)
    {
        highlightTime = _highlight;
    }

    public void StageSelect()
    {
        if (!find) return;
        data.stageNum = curStage;
    }

    public void ShowStage()
    {
        // ������ �ҷ�����
        DataManager.Instance.LoadMainGameData();

        // 배경음악을 설정하는 코드를 작성하세요.
        bgm.Play();

        stageInfo[0].text = "stage " + curStage.ToString();
        stageInfo[1].text = location[curStage - 1].ToString();

        // 스테이지에 해당하는 배경음악 정보를 출력하는 코드를 작성하세요.

        stageInfo[4].text = "Difficulty";

        switch (difficulty[curStage - 1])
        {
            case 1:
                stageInfo[5].text = "TAAAA";
                break;
            case 2:
                stageInfo[5].text = "TTAAA";
                break;
            case 3:
                stageInfo[5].text = "TTTAA";
                break;
            case 4:
                stageInfo[5].text = "TTTTA";
                break;
            case 5:
                stageInfo[5].text = "TTTTT";
                break;
        }

        // �ְ�����
        stageInfo[6].text = maingamedata.score[curStage - 1].ToString() + " Score";
        string rank = "";
        int rankscore = maingamedata.score[curStage - 1];
        if (rankscore > 10000)
        {
            rank = "SS";
        }
        else if (rankscore > 5000)
        {
            rank = "S";
        }
        else if (rankscore > 1000)
        {
            rank = "A";
        }
        else if (rankscore > 100)
        {
            rank = "B";
        }
        else if (rankscore == 0)
        {
            rank = "None";
        }
        else
        {
            rank = "F";
        }
        stageInfo[7].text = rank + " Rank";
        if(maingamedata.collection[curStage - 1] > 1)
        {
            stageInfo[8].text = "Happy Ending";
            stageInfo[9].text = maingamedata.collection[curStage - 1].ToString() + " Collection";
        }
        else
        {
            stageInfo[8].text = "Sad Ending";
            stageInfo[9].text = maingamedata.collection[curStage - 1].ToString() + " Collection";
        }

        // �ر� ���
        LockPlayButton();

    }
    
    public void SetStageLock()
    {
        isUnlock[0] = true;
        // �������� ��Ͽ� ���� �ر� ��� ����
        for (int i = 0; i < stageScore.Length; i++)
        {
            if (stageScore[i] > 10000)
            {
                isUnlock[i + 1] = true;
            }
        }

        // �ӽ÷� ���� �� ����
        isUnlock[1] = true;
        isUnlock[2] = true;
        isUnlock[3] = true;
        isUnlock[4] = true;
    }

    public void LockPlayButton()
    {

        // �رݱ�� 
        GameObject playButton = GameObject.Find("PlayButton");
        if (isUnlock[curStage - 1])
        {
            stage[curStage - 1].GetComponent<Button>().enabled = true;
            playButton.GetComponent<Button>().interactable = true;
            playButton.GetComponentInChildren<TextMeshProUGUI>().text = "Play";
        }
        else
        {
            stage[curStage - 1].GetComponent<Button>().enabled = false;
            playButton.GetComponent<Button>().interactable = false;
            playButton.GetComponentInChildren<TextMeshProUGUI>().text = "Locked";
        }
    }

    public void ConnectData()
    {
        // ������ �ҷ�����
        if (GameObject.Find("Data"))
        {
            find = true;
            data = GameObject.Find("Data").GetComponent<DataManager>();
            data.SaveMainGameData();
            data.LoadMainGameData();
            data.LoadSoundData();
        }

        // ���� ������ �ҷ�����
        DataManager.Instance.LoadSoundData();
        bgm.volume = sounddata.bgm;

        DataManager.Instance.LoadMainGameData();
        for (int i = 0; i < maingamedata.score.Length; i++)
        {
            stageScore[i] = maingamedata.score[i];
            print(maingamedata.score[i] + "����");
        }
    }

}
