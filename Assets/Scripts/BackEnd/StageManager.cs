using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class StageManager : MonoBehaviour
{
    //클래스 불러오기
    private GameData gamedata => DataManager.Instance.gamedata;

    // Start is called before the first frame update
    void Start()
    {
        //// 스테이지 기본 설정
        ////Stage1 Unlocked
        //gamedata.Unlocked[0] = true;

        ////Stage2~5 Locked
        //for (int i = 1; i < 5; i++)
        //{
        //    gamedata.Unlocked[i] = false;
        //}
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //스테이지 1~5 Lock/Unlock 기능
    public void Stage1()
    {
        if (gamedata.Unlocked[0] == true)
        {
            gamedata.Unlocked[0] = false;
            Debug.Log("Stage1 Locked");
        }
        else
        {
            gamedata.Unlocked[0] = true;
            Debug.Log("Stage1 Unlocked");
        }
    }

    public void Stage2()
    {
        if (gamedata.Unlocked[1] == true)
        {
            gamedata.Unlocked[1] = false;
            Debug.Log("Stage2 Locked");
        }
        else
        {
            gamedata.Unlocked[1] = true;
            Debug.Log("Stage2 Unlocked");
        }
    }

    public void Stage3()
    {
        if (gamedata.Unlocked[2] == true)
        {
            gamedata.Unlocked[2] = false;
            Debug.Log("Stage3 Locked");
        }
        else
        {
            gamedata.Unlocked[2] = true;
            Debug.Log("Stage3 Unlocked");
        }
    }

    public void Stage4()
    {
        if (gamedata.Unlocked[3] == true)
        {
            gamedata.Unlocked[3] = false;
            Debug.Log("Stage4 Locked");
        }
        else
        {
            gamedata.Unlocked[3] = true;
            Debug.Log("Stage4 Unlocked");
        }
    }

    public void Stage5()
    {
        if (gamedata.Unlocked[4] == true)
        {
            gamedata.Unlocked[4] = false;
            Debug.Log("Stage4 Locked");
        }
        else
        {
            gamedata.Unlocked[4] = true;
            Debug.Log("Stage5 Unlocked");
        }
    }
}
