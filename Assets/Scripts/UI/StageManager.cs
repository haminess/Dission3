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

    public string[] location = { "����", "����", "�� ȭ��", "���" };
    public int[] difficulty = { 2, 3, 3, 2 };   // 1: ����, 2: ����, 3: �����

    // �������� ������Ʈ
    TextMeshProUGUI[] stageInfo;

    // Start is called before the first frame update
    void Start()
    {
        stageInfo = GameObject.Find("StagePanel").GetComponentsInChildren<TextMeshProUGUI>();

        // �ʱⰪ ����
        curStage = 2;

        // ������ �ҷ�����
        if (GameObject.Find("Data"))
        {
            find = true;
            data = GameObject.Find("Data").GetComponent<DataManager>();
        }

        if(GameObject.Find("SoundManager"))
        {
            GameObject total = GameObject.Find("SoundManager");
            soundMan = total.GetComponent<SoundManager>();
            this.bgm = soundMan.bgm;
            this.effect = soundMan.effect;
        }

        // ���� ������ �ҷ�����
        DataManager.Instance.LoadSoundData();
        bgm.volume = sounddata.bgm;

        DataManager.Instance.LoadMainGameData();
        for(int i = 0; i < maingamedata.score.Length; i++)
        {

            stageScore[i] = maingamedata.score[i];
        }

        isUnlock[0] = true;

        // �رݱ��
        SetStageLock();

        isUnlock[1] = false;
        isUnlock[2] = false;
        isUnlock[3] = false;
        ShowStage();

    }
    void Awake()
    {
        // �� �Ŵ����� sceneLoaded�� ü���� �Ǵ�.
        //SceneManager.sceneLoaded += OnStageLoaded;
    }

    void OnStageLoaded(Scene scene, LoadSceneMode mode)
    {
        // ü���� �ɾ �� �Լ��� �� ������ ȣ��ȴ�.
    }


    // Update is called once per frame
    void Update()
    {

    }

    public void SetStage(int _stage)
    {
        curStage = _stage;
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

        // ���� ���
        bgm.clip = soundMan.bgmClip[curStage - 1];
        bgm.time = soundMan.bgmHookTime[curStage - 1];
        bgm.Play();

        // �������� ���� ���
        // �������� ��ȣ
        stageInfo[0].text = "stage " + curStage.ToString();
        // ���
        stageInfo[1].text = location[curStage - 1].ToString();
        // �� �̸�
        stageInfo[2].text = soundMan.bgmClip[curStage - 1].name.ToString();
        // �� �ð�
        stageInfo[3].text = (Mathf.Floor(soundMan.bgmClip[curStage - 1].length / 60.0f)).ToString("00") + ":" + (soundMan.bgmClip[curStage - 1].length % 60).ToString("00");
        // ���̵�
        stageInfo[4].text = "Difficulty";

        switch (difficulty[curStage - 1])
        {
            case 1:
                stageInfo[5].text = "TAA";
                break;
            case 2:
                stageInfo[5].text = "TTA";
                break;
            case 3:
                stageInfo[5].text = "TTT";
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
        // �������� ��Ͽ� ���� �ر� ��� ����
        for (int i = 1; i < stageScore.Length; i++)
        {

            if (stageScore[i] > 1000)
            {
                isUnlock[i] = true;
            }
            else
            {
                isUnlock[i] = false;
            }
        }
    }

    public void LockPlayButton()
    {

        // �رݱ�� 
        GameObject playButton = GameObject.Find("PlayButton");
        if (isUnlock[curStage - 1])
        {
            stage[curStage - 1].GetComponent<Button>().enabled = true;
            playButton.GetComponent<Button>().enabled = true;
            playButton.GetComponent<Image>().color = Color.white;
            playButton.GetComponentInChildren<TextMeshProUGUI>().text = "Play";
            playButton.GetComponentInChildren<TextMeshProUGUI>().fontSize = 24;
        }
        else
        {
            stage[curStage - 1].GetComponent<Button>().enabled = false;
            playButton.GetComponent<Button>().enabled = false;
            playButton.GetComponent<Image>().color = Color.gray;
            playButton.GetComponentInChildren<TextMeshProUGUI>().text = "Coming Soon..";
            playButton.GetComponentInChildren<TextMeshProUGUI>().fontSize = 16;
        }
    }

}
