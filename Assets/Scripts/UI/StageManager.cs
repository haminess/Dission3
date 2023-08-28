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
    public int curStage = 1;
    float highlightTime = 0;

    // ����
    // �������� ���� ����
    AudioSource audioManager;

    public string[] location = { "����", "����", "�� ȭ��", "���" };
    public string[] bgmName = { "�뷡1", "�뷡2", "�뷡3", "�뷡4" };
    public int[] difficulty = { 2, 3, 3, 2 };   // 1: ����, 2: ����, 3: �����
    public AudioClip[] bgmClip;

    // �������� ������Ʈ
    TextMeshProUGUI[] stageInfo;

    // Start is called before the first frame update
    void Start()
    {
        audioManager = GameObject.Find("BGM").GetComponent<AudioSource>();
        stageInfo = GameObject.Find("StagePanel").GetComponentsInChildren<TextMeshProUGUI>();
        if (GameObject.Find("Data"))
        {
            find = true;
            data = GameObject.Find("Data").GetComponent<DataManager>();
        }

        // ������ �ҷ�����
        DataManager.Instance.LoadSoundData();
        audioManager.volume = sounddata.bgm;

        DataManager.Instance.LoadMainGameData();
        for(int i = 0; i < maingamedata.score.Length; i++)
        {

            stageScore[i] = maingamedata.score[i];
        }

        isUnlock[0] = true;
        stage[0].GetComponent<Button>().enabled = true;

        // �رݱ��
        SetStageLock();

    }
    void Awake()
    {
        print("awake fuck");
        // �� �Ŵ����� sceneLoaded�� ü���� �Ǵ�.
        //SceneManager.sceneLoaded += OnStageLoaded;
    }

    // ü���� �ɾ �� �Լ��� �� ������ ȣ��ȴ�.
    void OnStageLoaded(Scene scene, LoadSceneMode mode)
    {
        print("fuck");
        // ������ �ҷ�����
        // �������� ��� �����ͼ� ȭ�� ���

        // �ر� ��� ȣ��
        if (SceneManager.GetActiveScene().name == "StageScene")
        {

        }
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
        audioManager.clip = bgmClip[curStage - 1];
        audioManager.time = highlightTime;
        audioManager.Play();

        // �������� ���� ���
        stageInfo[0].text = "stage " + curStage.ToString();
        stageInfo[1].text = location[curStage - 1].ToString();
        stageInfo[2].text = bgmClip[curStage - 1].name.ToString();
        stageInfo[3].text = (Mathf.Floor( bgmClip[curStage - 1].length / 60.0f)).ToString("00") + ":" + (bgmClip[curStage - 1].length % 60).ToString("00");
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

        // �رݱ�� 
        if (isUnlock[curStage - 1])
        {
            stage[curStage - 1].GetComponent<Button>().enabled = true;
        }
        else
        {
            stage[curStage - 1].GetComponent<Button>().enabled = false;
        }
    }
    

    public void SetStageLock()
    {
        print("setstagelock ����" + stageScore.Length);
        // �������� ��Ͽ� ���� �ر� ��� ����
        for (int i = 1; i < stageScore.Length; i++)
        {

            print("setstagelock for�� ����");
            if (stageScore[i] > 1000)
            {
                isUnlock[i] = true;
                print("setstagelock unlock" + i);
                //stage[i].GetComponent<Button>().enabled = true;
            }
            else
            {
                isUnlock[i] = false;
                print("setstagelock lock" + i);
                //stage[i].GetComponent<Button>().enabled = false;
            }
        }
    }

}
