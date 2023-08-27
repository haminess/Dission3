using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ReBtn : MonoBehaviour
{
    public void OnClickRe()
    {
        SceneManager.LoadScene("MainGame");
    }
}
