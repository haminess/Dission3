using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class ResultManager : MonoBehaviour
{
    // ����� ������ �ҷ�����
    private MainGameData maingamedata => DataManager.Instance.maingamedata;

    public SoundManager soundmanager;

    public int StageNum;

    //public int score;
    public int score;
    public int combo;
    public int curCombo;
    public int perfect;
    public int good;
    public int bad;
    public int miss;
    //public int collection;
    public int collection;

    private void Awake()
    {
        var objs = FindObjectsOfType<ResultManager>();
        if (objs.Length > 1)
        {
            Destroy(gameObject);
        }
        
        DontDestroyOnLoad(gameObject);
    }

    void OnEnable()
    {
        // �� �Ŵ����� sceneLoaded�� ü���� �Ǵ�.
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    // ü���� �ɾ �� �Լ��� �� ������ ȣ��ȴ�.
    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (SceneManager.GetActiveScene().name == "ScoreScene")
        {

            if (GameObject.Find("Data"))
            {
                GameObject.Find("Data").GetComponent<DataManager>().LoadMainGameData();
            }

            soundmanager = GameObject.Find("resultBGM").GetComponent<SoundManager>();

            if (GameObject.Find("SoundManager"))
            {
                soundmanager = GameObject.Find("SoundManager").GetComponent<SoundManager>();
            }



            // ��� ȭ�� ���(����)
            ShowResult();

            // (������)
            if (GameObject.Find("rightpanel"))
            {
                Animator right = GameObject.Find("rightpanel").GetComponent<Animator>();

                string animName = StageNum.ToString() + "_";

                if (collection > 2)
                {
                    soundmanager.SetEffect(3);
                    soundmanager.PlayEffect();
                    animName += "happy";
                }
                else
                {
                    soundmanager.SetEffect(4);
                    soundmanager.PlayEffect();
                    animName += "sad";
                }
                right.Play(animName);
            }

            // ���� ������ ����
            SaveResult();
        }
    }

    public void SendResult()
    {
        // ���ΰ��� ����� ������� ����
        StageNum = MainGame.instance.stageNum;
        score = MainGame.instance.score;
        combo = MainGame.instance.combo;
        curCombo = MainGame.instance.curCombo;
        perfect = MainGame.instance.perfect;
        good = MainGame.instance.good;
        bad = MainGame.instance.bad;
        miss = MainGame.instance.miss;
        collection = MainGame.instance.collection;
    }

    public void SaveResult()
    {
        if (StageNum < 1 || StageNum > 4)
        {
            print("�������� ��ȣ ���������� ���� ����");
            return;
        }
        if (score > maingamedata.score[StageNum - 1])
        {
            maingamedata.score[StageNum - 1] = score;
            maingamedata.collection[StageNum - 1] = collection;
            print(score + "�ű��" + maingamedata.score);
            print("���ΰ��� ����");
        }
        else
        {
            print(MainGame.instance.score + "�ű�� ����" + maingamedata.score);
            print("���ΰ��� ����ȵ�");
        }

        if(collection > maingamedata.collection[StageNum - 1])
        {
            print("����ǰ �ű�� ����");
            maingamedata.collection[StageNum - 1] = collection;
        }

        if (GameObject.Find("Data"))
        {
            GameObject.Find("Data").GetComponent<DataManager>().SaveMainGameData();
        }
    }

    public void ShowResult()
    {
        GameObject canvas = GameObject.Find("Canvas");
        GameObject scorePanel = canvas.transform.GetChild(1).gameObject;
        GameObject collectPanel = canvas.transform.GetChild(2).gameObject;

        // score �г�
        TextMeshProUGUI[] contents = GameObject.Find("Content").GetComponentsInChildren<TextMeshProUGUI>();

        string rank = "";
        if (score > 10000)
        {
            rank = "SS";
        }
        else if (score > 5000)
        {
            rank = "S";
        }
        else if (score > 1000)
        {
            rank = "A";
        }
        else if (score > 100)
        {
            rank = "B";
        }
        else
        {
            rank = "F";
        }

        contents[0].text = rank;
        contents[1].text = score.ToString();
        contents[2].text = combo.ToString();
        contents[3].text = perfect.ToString();
        contents[4].text = good.ToString();
        contents[5].text = bad.ToString();
        contents[6].text = miss.ToString();

        // collect �г�
        collectPanel.SetActive(true);
        Button[] collects = collectPanel.GetComponentsInChildren<Button>();

        if(collection > 2)
        {
            collects[0].gameObject.SetActive(true);
            collects[1].gameObject.SetActive(false);
        }
        else
        {
            collects[0].gameObject.SetActive(false);
            collects[1].gameObject.SetActive(true);
        }

        for (int i = 0; i < collection; i++)
        {
            GameObject c = Instantiate(collects[2].gameObject, collectPanel.transform);
            c.transform.localPosition += Vector3.right * 110 * i;
        }

        Destroy(contents[2].gameObject);
        collectPanel.SetActive(false);
    }
}
