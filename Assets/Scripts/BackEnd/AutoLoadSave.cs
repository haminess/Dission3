using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoLoadSave : MonoBehaviour
{
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
        DataManager.Instance.SaveMainGameData();
        //등등 저장할것들 모아다가
    }
}
