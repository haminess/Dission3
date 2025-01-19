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
        // ���α׷� ���� �� �ڵ带 �ۼ��մϴ�.
        

        // �̱��� �ʱ�ȭ
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

        // ���� ������ �ҷ�����
    }

    private void OnApplicationQuit()
    {
        // ���α׷� ���� �� �ڵ带 �ۼ��մϴ�.
        // ���� ������ ����
    }

    // data manage

    // scene manage
    void LoadScene(SCENE _scene)
    {
        // enum�� scene�� ���� ����� �Ѵ�.
        SceneManager.LoadScene((int)_scene);
    }
}

