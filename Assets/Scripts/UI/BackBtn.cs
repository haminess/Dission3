using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class BackBtn : MonoBehaviour
{
    public TMP_InputField rankname;
    public void OnClickBack()
    {
        SceneManager.LoadScene("StageScene");
    }

    public void findresultman()
    {
        GameObject.Find("ResultData").GetComponent<ResultManager>().Rank();
    }
}
