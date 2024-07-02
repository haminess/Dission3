using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EditorData //: MonoBehaviour
{
    public boxroute[] boxdata;
    public List<double> notedata = new List<double>();
    public List<float> noteduration = new List<float>();

    public float length;
    public string projectname;
    public float sec; //total sec

    public string creator;
    public AudioClip music;
    public string musicname;
    public int bpm;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
