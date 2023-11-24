using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class StageManager : MonoBehaviour
{
    // 데이터 불러오기
    private MainGameData maingamedata => DataManager.Instance.maingamedata;
    private SoundData sounddata => DataManager.Instance.sounddata;

    bool find = false;
    DataManager data;

    // 유저 정보 관리
    public GameObject[] stage;
    public bool[] isUnlock;
    public int[] stageScore = new int[4];
    public int curStage = 2;
    float highlightTime = 0;

    // 음악
    // 스테이지 정보 관리
    public AudioSource bgm;
    public AudioSource effect;
    public SoundManager soundMan;

    public string[] location = { "교실", "교문", "뒷 화단", "운동장" };
    public int[] difficulty = { 2, 3, 3, 2 };   // 1: 쉬움, 2: 보통, 3: 어려움

    // 스테이지 오브젝트
    TextMeshProUGUI[] stageInfo;

    // Start is called before the first frame update
    void Start()
    {
        stageInfo = GameObject.Find("StagePanel").GetComponentsInChildren<TextMeshProUGUI>();

        // 초기값 세팅
        //curStage = 2;


        isUnlock[0] = true;

        ConnectData();

        // 해금기능
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
        // 데이터 불러오기
        DataManager.Instance.LoadMainGameData();

        // 음악 출력
        bgm.clip = soundMan.bgmClip[curStage - 1];
        bgm.time = soundMan.bgmHookTime[curStage - 1];
        bgm.Play();

        // 스테이지 정보 출력
        // 스테이지 번호
        stageInfo[0].text = "stage " + curStage.ToString();
        // 장소
        stageInfo[1].text = location[curStage - 1].ToString();
        // 곡 이름
        stageInfo[2].text = soundMan.bgmClip[curStage - 1].name.ToString();
        // 곡 시간
        stageInfo[3].text = (Mathf.Floor(soundMan.bgmClip[curStage - 1].length / 60.0f)).ToString("00") + ":" + (soundMan.bgmClip[curStage - 1].length % 60).ToString("00");
        // 난이도
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

        // 최고점수
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

        // 해금 기능
        LockPlayButton();

    }
    
    public void SetStageLock()
    {
        isUnlock[0] = true;
        // 스테이지 기록에 따라 해금 기능 구현
        for (int i = 0; i < stageScore.Length; i++)
        {
            if (stageScore[i] > 10000)
            {
                isUnlock[i + 1] = true;
            }
        }

        // 임시로 해제 해 놓기
        isUnlock[1] = true;
        isUnlock[2] = true;
        isUnlock[3] = true;
        isUnlock[4] = true;
    }

    public void LockPlayButton()
    {

        // 해금기능 
        GameObject playButton = GameObject.Find("PlayButton");
        if (isUnlock[curStage - 1])
        {
            stage[curStage - 1].GetComponent<Button>().enabled = true;
            playButton.GetComponent<Button>().interactable = true;
            //playButton.GetComponent<Image>().color = Color.white;
            playButton.GetComponentInChildren<TextMeshProUGUI>().text = "Play";
        }
        else
        {
            stage[curStage - 1].GetComponent<Button>().enabled = false;
            playButton.GetComponent<Button>().interactable = false;
            //playButton.GetComponent<Image>().color = Color.gray;
            playButton.GetComponentInChildren<TextMeshProUGUI>().text = "Locked";
        }
    }

    public void ConnectData()
    {
        // 데이터 불러오기
        if (GameObject.Find("Data"))
        {
            find = true;
            data = GameObject.Find("Data").GetComponent<DataManager>();
        }

        // 로컬 데이터 불러오기
        DataManager.Instance.LoadSoundData();
        bgm.volume = sounddata.bgm;

        DataManager.Instance.LoadMainGameData();
        for (int i = 0; i < maingamedata.score.Length; i++)
        {
            stageScore[i] = maingamedata.score[i];
            print(maingamedata.score[i] + "점수");
        }
    }

}
