using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class ResultManager : MonoBehaviour
{
    // 저장된 데이터 불러오기
    private MainGameData maingamedata => DataManager.Instance.maingamedata;

    public int StageNum;

    //public int score;
    public int[] score = new int[4];
    public int combo;
    public int curCombo;
    public int perfect;
    public int good;
    public int bad;
    public int miss;
    //public int collection;
    public int[] collection = new int[4];

    private void Awake()
    {
        var objs = FindObjectsOfType<ResultManager>();
        if (objs.Length > 1)
        {
            Destroy(gameObject);
        }
        
        DontDestroyOnLoad(gameObject);
    }

    void OnEnable()
    {
        // 씬 매니저의 sceneLoaded에 체인을 건다.
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    // 체인을 걸어서 이 함수는 매 씬마다 호출된다.
    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (SceneManager.GetActiveScene().name == "ScoreScene")
        {

            if (GameObject.Find("Data"))
            {
                GameObject.Find("Data").GetComponent<DataManager>().LoadMainGameData();
            }
            TextMeshProUGUI[] contents = GameObject.Find("Content").GetComponentsInChildren<TextMeshProUGUI>();


            string rank = "";
            if (StageNum >= 1 && StageNum <= 4)
            {
                if (score[StageNum - 1] > 10000)
                {
                    rank = "SS";
                }
                else if (score[StageNum - 1] > 5000)
                {
                    rank = "S";
                }
                else if (score[StageNum - 1] > 1000)
                {
                    rank = "A";
                }
                else if (score[StageNum - 1] > 100)
                {
                    rank = "B";
                }
                else
                {
                    rank = "F";
                }

                contents[0].text = rank;
                contents[1].text = score.ToString();
                contents[2].text = combo.ToString();
                contents[3].text = perfect.ToString();
                contents[4].text = good.ToString();
                contents[5].text = bad.ToString();
                contents[6].text = miss.ToString();

                SaveResult();
            }
        }
    }

    public void SendResult()
    {
        StageNum = MainGame.instance.stageNum;
        //score = MainGame.instance.score;
        //score[StageNum - 1] = maingamedata.score[StageNum - 1];
        if (StageNum >= 1 && StageNum <= 4)
        {
            score[StageNum - 1] = maingamedata.score[StageNum - 1];
        }
        combo = MainGame.instance.combo;
        curCombo = MainGame.instance.curCombo;
        perfect = MainGame.instance.perfect;
        good = MainGame.instance.good;
        bad = MainGame.instance.bad;
        miss = MainGame.instance.miss;
        //collection = MainGame.instance.collection;
        //collection[StageNum - 1] = maingamedata.collection[StageNum-1];
        if (StageNum >= 1 && StageNum <= 4)
        {
            collection[StageNum - 1] = maingamedata.collection[StageNum - 1];

        }
    }

    public void SaveResult()
    {
        if (score[StageNum - 1] > maingamedata.score[StageNum - 1])
        {
            maingamedata.score[StageNum-1] = score[StageNum - 1];
            maingamedata.collection = collection;
            if (GameObject.Find("Data"))
            {
                GameObject.Find("Data").GetComponent<DataManager>().SaveMainGameData();
            }
            print(score[StageNum-1] + "신기록" + maingamedata.score);
            print("메인게임 저장");
        }
        else
        {
            print(MainGame.instance.score + "신기록 실패" + maingamedata.score);
            print("메인게임 저장안됨");
        }
    }
}
