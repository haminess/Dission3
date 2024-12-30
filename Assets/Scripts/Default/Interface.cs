using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ISwitch
{
    int number { get; set; }
    void Switch(int _num);
}

public interface ISaveData
{
    string GetFileName();  // 저장될 파일 이름을 반환
}