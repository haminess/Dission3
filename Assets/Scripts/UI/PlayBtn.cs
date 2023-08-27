using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayBtn : MonoBehaviour
{
    public void OnClickPlay()
    {
        SceneManager.LoadScene("MainGame");
    }
}
