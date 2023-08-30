using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainGameData //: MonoBehaviour
{
    //public int STageNum;
    public int[] stageNum = new int[4];

    //public int score;
    public int[] score = new int[4];

    //public int combo;
    //public int curCombo;
    //public int perfect;
    //public int good;
    //public int bad;
    //public int miss;

    //public int collection;
    public int[] collection = new int[4];

    public float synk;
    public float judge;

    public MainGameData()
    {
        // √ ±‚»≠
        for (int i = 0; i < 4; i++)
        {
            stageNum[i] = i + 1;
        }

        for (int i = 0; i < 4; i++)
        {
            score[i] = 0;
        }

        for (int i = 0;i<4;i++)
        {
            collection[i] = 0;
        }

        synk = 1;
        judge = 1;
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
