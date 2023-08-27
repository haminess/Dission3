using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StageManager : MonoBehaviour
{
    // ������ �ҷ�����
    private MainGameData maingamedata => DataManager.Instance.maingamedata;

    public bool find = false;
    public DataManager data;

    public GameObject[] stage;
    public bool[] isUnlock;

    public int[] stageScore;


    // Start is called before the first frame update
    void Start()
    {
        if (GameObject.Find("Data"))
        {
            find = true;
            data = GameObject.Find("Data").GetComponent<DataManager>();
        }

    }
    void OnEnable()
    {
        // �� �Ŵ����� sceneLoaded�� ü���� �Ǵ�.
        SceneManager.sceneLoaded += OnStageLoaded;
    }

    // ü���� �ɾ �� �Լ��� �� ������ ȣ��ȴ�.
    void OnStageLoaded(Scene scene, LoadSceneMode mode)
    {
        // ������ �ҷ�����
        // �������� ��� �����ͼ� ȭ�� ���
        // �������� ��Ͽ� ���� �ر� ��� ����
        if (SceneManager.GetActiveScene().name == "StageScene")
        {
            isUnlock[0] = true;
            stage[0].GetComponent<Button>().enabled = true;

            for (int i = 1; i < stage.Length; i++)
            {
                if (stageScore[i] > 1000)
                {
                    isUnlock[i] = true;
                    stage[i].GetComponent<Button>().enabled = true;
                }
                else
                {
                    isUnlock[i] = false;
                    stage[i].GetComponent<Button>().enabled = false;
                }
            }
        }
    }


    // Update is called once per frame
    void Update()
    {

    }

    public void StageSelect(int _stage)
    {
        if (!find) return;
        data.stageNum = _stage;
    }

    public void ShowStage(int _stage)
    {

    }
}
