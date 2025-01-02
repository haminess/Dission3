using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class ChangeScene : MonoBehaviour
{
    public void ToTitleScene()
    {
        SceneManager.LoadScene("TitleScene");
    }

    public void ToSelectScene()
    {
        SceneManager.LoadScene("SelectScene");
    }

    public void ToStageScene()
    {
        SceneManager.LoadScene("StageScene");
    }

    public void ToMainGameScene()
    {
        SceneManager.LoadScene("MainGame");
    }

    public void ToScoreScene()
    {
        SceneManager.LoadScene("ScoreScene");
    }

    public void ToEditor_HJW()
    {
        SceneManager.LoadScene("Editor_HJW");
    }
    public void ToTutorialScene()
    {
        SceneManager.LoadScene("TutorialScene");
    }

    public void ToPlayScene()
    {
        SceneManager.LoadScene("PlayScene");
    }

}
