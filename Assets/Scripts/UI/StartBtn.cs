using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartBtn : MonoBehaviour
{
    //���丮 ��ư -> ĳ���� ����â
    public void OnClickPlay()
    {
        SceneManager.LoadScene("SelectScene");
    }
}
