using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainGameData //: MonoBehaviour
{
    //public int STageNum;
    public int[] stageNum = new int[5];

    //public int score;
    public int[] score = new int[5];

    //public int combo;
    //public int curCombo;
    //public int perfect;
    //public int good;
    //public int bad;
    //public int miss;

    //public int collection;
    public int[] collection = new int[5];
    public bool[] happy = new bool[5];
    public bool[] sad = new bool[5];

    public float synk;
    public float judge;

    public int[] first_stage;
    public int[] second_stage;
    public int[] third_stage;
    public int[] fourth_stage;
    public int[] fifth_stage;

    public string[] first_stage_n;
    public string[] second_stage_n;
    public string[] third_stage_n;
    public string[] fourth_stage_n;
    public string[] fifth_stage_n;

    public MainGameData()
    {
        // �ʱ�ȭ
        for (int i = 0; i < 5; i++)
        {
            stageNum[i] = i + 1;
            score[i] = 0;
            collection[i] = 0;
            happy[i] = false;
            sad[i] = false;
        }


        synk = 0;
        judge = 0;
        //collection = 0;
    }

    //private void Awake()
    //{
    //    DontDestroyOnLoad(gameObject);
    //}

    //// Start is called before the first frame update
    //void Start()
    //{
        
    //}

    //// Update is called once per frame
    //void Update()
    //{
        
    //}

    //public void GoScoreScene()
    //{
    //    SceneManager.LoadScene("ScoreScene");
    //}

}
