using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.Playables;

public class GameData //: MonoBehaviour
{
    public bool[] Unlocked = new bool[5];

    public GameData()
    {
        // 스테이지 기본 설정
        //Stage1 Unlocked
        Unlocked[0] = true;

        //Stage2~5 Locked
        for (int i = 1; i < 5; i++)
        {
            Unlocked[i] = false;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
