using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartBtn : MonoBehaviour
{
    //스토리 버튼 -> 캐릭터 선택창
    public void OnClickPlay()
    {
        SceneManager.LoadScene("SelectScene");
    }
}
