using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckScript : MonoBehaviour
{

    //Ŭ���� �ҷ�����
    private MainGameData maingamedata => DataManager.Instance.maingamedata;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void STageNumCheck()
    {
        maingamedata.STageNum++;
        Debug.Log("STageNumCheck() ����");
        Debug.Log(maingamedata.STageNum);

        DataManager.Instance.SaveMainGameData();
    }

    //private void OnApplicationQuit() //���� ���� �� �ڵ� ����
    //{
    //    DataManager.Instance.SaveMainGameData();
    //    DataManager.Instance.SaveSoundData();
    //}
}
