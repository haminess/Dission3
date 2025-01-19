using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ISwitch
{
    int number { get; set; }
    void Switch(int _num);
}
