using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EditorData //: MonoBehaviour
{
    public Vector2[] boxpos;
    public double[] notedata;
    public Vector2[] notepos;
    public int[] notegroup;
    public int[] notetype;

    public float starttime;
    public float length;
    public string projectname;
    public double bpm;
    public double sec; //total sec
    public int up;
    public int down;

    public string creator;
    public AudioClip music;
    public string musicname;
    public double madi_sec;
    public double madi_bar_depth;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
