using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Note
{
    // [default]
    public float time;         // judge time
    public Vector3 pos;

    // [long]
    public bool ltype;         // note type: short(false), long(true)
    public Vector3[] route;
    public float duration;
    public GameObject note;


    public Note()
    {
        time = 0;
        pos = Vector3.zero;

        ltype = false;
        route = null;
        duration = 0;
    }

    public Note(float _time, Vector3 _pos)
    {
        time = _time;
        pos = _pos;

        ltype = false;
        route = null;
        duration = 0;
    }

    public Note(float _time, Vector3 _pos, Vector3[] _route, float _duration)
    {
        time = _time;
        pos = _pos;

        ltype = true;
        route = new Vector3[_route.Length];
        for(int i = 0; i < _route.Length; i++)
        {
            route[i] = _route[i];
        }
        duration = _duration;
    }
    
}

