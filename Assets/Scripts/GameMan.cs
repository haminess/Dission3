using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class GameMan : MonoBehaviour
{
    #region singleton
    private static GameMan inst = null;
    public static GameMan Instance
    {
        get
        {
            if (null == inst)
                return null;

            return inst;
        }
    }
    #endregion

    [Header("Manager")]

    [SerializeField] DataMan dataMan;
    [SerializeField] MainMan mainMan;

    [Header("Data")]
    public tJudgeData tJudgeRecord;

    private void Awake()
    {
        // 프로그램 시작 시 코드를 작성합니다.
        

        // 싱글톤 초기화
        if(null == inst)
        {
            inst = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
        mainMan = MainMan.instance;

        // 로컬 데이터 불러오기
    }

    private void OnApplicationQuit()
    {
        // 프로그램 종료 시 코드를 작성합니다.
        // 로컬 데이터 저장
    }

    // data manage

    // scene manage
    void LoadScene(SCENE _scene)
    {
        // enum과 scene의 순서 맞춰야 한다.
        SceneManager.LoadScene((int)_scene);
    }
}

