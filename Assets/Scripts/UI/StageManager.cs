using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageManager : MonoBehaviour
{
    public bool find = false;
    public DataManager data;

    // Start is called before the first frame update
    void Start()
    {
        if (GameObject.Find("Data"))
        {
            find = true;
            data = GameObject.Find("Data").GetComponent<DataManager>();
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void StageSelect(int _stage)
    {
        if (!find) return;
        data.stageNum = _stage;
    }
}
