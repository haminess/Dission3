using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainGameData //: MonoBehaviour
{
    public int STageNum;

    public int score;
    public int combo;
    public int curCombo;
    public int perfect;
    public int good;
    public int bad;
    public int miss;
    public int collection;

    public MainGameData()
    {
        //√ ±‚»≠
        STageNum = 1;
        score = 0;
        combo = 0;
        curCombo = 0;
        perfect = 0;
        good = 0;
        bad = 0;
        miss = 0;
        collection = 0;
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
