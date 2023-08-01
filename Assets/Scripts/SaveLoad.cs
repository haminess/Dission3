using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class SaveLoad : MonoBehaviour
{
    GameData gamedata;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnApplicationQuit() //게임 종료 시 자동 저장
    {
        DataManager.Instance.SaveGameData();
    }

    //private void StageUnlocked(int chapnum) //스테이지 오픈 시 자동 저장
    //{
    //    DataManager.Instance.gamedata.isClear[chapnum] = true;
    //    DataManager.Instance.SaveGameData;
    //}
}
