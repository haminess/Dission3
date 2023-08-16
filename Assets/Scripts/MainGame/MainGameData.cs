using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainGameData : MonoBehaviour
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


    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GoScoreScene()
    {
        SceneManager.LoadScene("ScoreScene");
    }

}
