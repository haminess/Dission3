using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class Collect : MonoBehaviour
{
    public int CNum = 1;
    public string Cname;

    //클래스 불러오기
    private MainGameData maingamedata => DataManager.Instance.maingamedata;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        MainGame.instance.collection[maingamedata.STageNum[MainGame.instance.stageNum-1]] += CNum;
        Destroy(gameObject);
    }
}
