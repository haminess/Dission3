using System.Collections;
using System.Collections.Generic;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

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
