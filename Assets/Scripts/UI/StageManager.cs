using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageManager : MonoBehaviour
{
    int stageNum;
    string[] stageInfo;
    bool isUnlock;

    DataManager data;

    // Start is called before the first frame update
    void Start()
    {
        data = new DataManager();
        // 데이터 불러오기

        // 잠금 기능 표시하기
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
