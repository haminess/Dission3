using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum JUDGE_TYPE
{
    MISS,
    BAD,
    GOOD,
    PERFECT,
    SIZE
}

public struct tStageInfo
{
    tPlayInfo play;
    int collection;
}

public struct tPlayInfo
{
    tJudgeInfo judge;
    int score;
    int combo;
    int life;
}
public struct tJudgeInfo
{
    int combo;
    int miss;
    int bad;
    int good;
    int perfect;
}


public class DataMan : MonoBehaviour
{
    
}
