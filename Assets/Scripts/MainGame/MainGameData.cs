using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainGameData //: MonoBehaviour
{
    //public int STageNum;
    public int[] STageNum = new int[4];

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

    public MainGameData()
    {
        //�ʱ�ȭ
        for (int i = 0; i < 4; i++)
        {
            STageNum[i] = i + 1;
        }

        for (int i = 0; i < 4; i++)
        {
            score[i] = 0;
        }

        for (int i = 0;i<4;i++)
        {
            collection[i] = 0;
        }
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
