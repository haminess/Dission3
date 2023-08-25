using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StageManager : MonoBehaviour
{
    // 데이터 불러오기
    private MainGameData maingamedata => DataManager.Instance.maingamedata;

    public bool find = false;
    public DataManager data;

    public GameObject[] stage;
    public bool[] isUnlock;

    public int[] stageScore;


    // Start is called before the first frame update
    void Start()
    {
        if (GameObject.Find("Data"))
        {
            find = true;
            data = GameObject.Find("Data").GetComponent<DataManager>();
        }

    }
    void OnEnable()
    {
        // 씬 매니저의 sceneLoaded에 체인을 건다.
        SceneManager.sceneLoaded += OnStageLoaded;
    }

    // 체인을 걸어서 이 함수는 매 씬마다 호출된다.
    void OnStageLoaded(Scene scene, LoadSceneMode mode)
    {
        // 데이터 불러오기
        // 스테이지 기록 가져와서 화면 출력
        // 스테이지 기록에 따라 해금 기능 구현
        if (SceneManager.GetActiveScene().name == "StageScene")
        {
            isUnlock[0] = true;
            stage[0].GetComponent<Button>().enabled = true;

            for (int i = 1; i < stage.Length; i++)
            {
                if (stageScore[i] > 1000)
                {
                    isUnlock[i] = true;
                    stage[i].GetComponent<Button>().enabled = true;
                }
                else
                {
                    isUnlock[i] = false;
                    stage[i].GetComponent<Button>().enabled = false;
                }
            }
        }
    }


    // Update is called once per frame
    void Update()
    {

    }

    public void StageSelect(int _stage)
    {
        if (!find) return;
        data.stageNum = _stage;
    }

    public void ShowStage(int _stage)
    {

    }
}
