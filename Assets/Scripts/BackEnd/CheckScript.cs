using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckScript : MonoBehaviour
{

    //클래스 불러오기
    private MainGameData maingamedata => DataManager.Instance.maingamedata;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void STageNumCheck()
    {
        maingamedata.STageNum++;
        Debug.Log("STageNumCheck() 실행");
        Debug.Log(maingamedata.STageNum);

        DataManager.Instance.SaveMainGameData();
    }

    //private void OnApplicationQuit() //게임 종료 시 자동 저장
    //{
    //    DataManager.Instance.SaveMainGameData();
    //    DataManager.Instance.SaveSoundData();
    //}
}
