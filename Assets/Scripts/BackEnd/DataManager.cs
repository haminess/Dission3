using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.Playables;
using UnityEngine.UI;
using UnityEngine.SocialPlatforms.Impl;
using Unity.Burst.Intrinsics;

public class DataManager : MonoBehaviour
{
    static GameObject container;

    //�̱������� ����
    static DataManager instance;

    // ���� ������ dontdestroy ����
    public int characterNum;
    public int stageNum;
    public int difficulty;

    //����� Ŭ���� ����
    public MainGameData maingamedata = new MainGameData();
    public SoundData sounddata = new SoundData();

    public static DataManager Instance
    {
        get
        {
            if (!instance)
            {
                instance = FindObjectOfType<DataManager>();

                if (!instance)
                {
                    container = new GameObject();
                    container.name = "DataManager";
                    instance = container.AddComponent(typeof(DataManager)) as DataManager;
                    DontDestroyOnLoad(container);
                }
            }
            return instance;
        }
    }

    //���� �̸� ����
    string MainGameDataFileName = "MainGameData.json"; //������ �����ε� ä�� �ø��ų� �� �� ����ڰ� �Է��� �� �ְ�
    string SoundDataFileName = "SoundData.json";


    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
        maingamedata = new MainGameData();
        sounddata = new SoundData();

        print(maingamedata.score.Length);
        for (int i = 0; i < maingamedata.score.Length; i++)
        {
            print(maingamedata.score[i]);
        }
        print("Data Manager ������ ��ü ���� �� �ʱ�ȭ");
    }

    // Start is called before the first frame update
    void Start()
    {
        // ���� �ʱ�ȭ
        characterNum = 0;
        stageNum = 1;
        difficulty = 0;

        LoadMainGameData();
        LoadSoundData();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void LoadMainGameData()
    {
        //Load
        string filePath = Application.persistentDataPath + "/" + MainGameDataFileName;

        if(File.Exists(filePath))
        {
            //�ҷ�����
            string FromJsonData = File.ReadAllText(filePath);
            maingamedata = JsonUtility.FromJson<MainGameData>(FromJsonData);
            Debug.Log("LoadMainGameData() ����"); //(Ȯ�ο�)

            if(maingamedata.score.Length == 4 && maingamedata.collection.Length == 4 && maingamedata.stageNum.Length == 4)
            {
                // ������ �� �����̸� ����
                print("����� ���� ���� �ε�");
                print("���� ���� 1: " + maingamedata.score[0] + "2: " + maingamedata.score[1] + "3: " + maingamedata.score[2] + "4: " + maingamedata.score[3]);
                return;
            }

        }

        print("�ε� �ʱ�ȭ");
        //��� �ʱ�ȭ �ڵ�
        maingamedata = new MainGameData(); // �Ǵ� �ٸ� �ʱⰪ���� ������ �� ����

        //�ʱ�ȭ
        for (int i = 0; i < 4; i++)
        {
            maingamedata.stageNum[i] = i + 1;
        }

        for (int i = 0; i < 4; i++)
        {
            maingamedata.score[i] = 0;
        }

        for (int i = 0; i < 4; i++)
        {
            maingamedata.collection[i] = 0;
        }

        SaveMainGameData();

        Debug.Log("LoadMainGameData().else ����"); //(Ȯ�ο�)
    }

    public void LoadSoundData()
    {
        //Load
        string filePath = Application.persistentDataPath + "/" + SoundDataFileName;

        if (File.Exists(filePath))
        {
            //�ҷ�����
            string FromJsonData = File.ReadAllText(filePath);
            sounddata = JsonUtility.FromJson<SoundData>(FromJsonData);
            Debug.Log("LoadSoundData() ����"); //(Ȯ�ο�)
        }
        else
        {
            //�⺻�� �ʱ�ȭ �ڵ�
            sounddata = new SoundData();

            sounddata.bgm = 1;
            sounddata.effect = 1;

            SaveSoundData();       

            Debug.Log("LoadSoundData().else ����"); //(Ȯ�ο�)
        }

        //Debug.Log(sounddata.bgm); //(Ȯ�ο�)
        //Debug.Log(sounddata.effect); //(Ȯ�ο�)
        if (sounddata != null)
        {
            Debug.Log(sounddata.bgm); //(Ȯ�ο�)
            Debug.Log(sounddata.effect); //(Ȯ�ο�)
        }
    }

    public void SaveMainGameData()
    {
        //Ŭ����->Json ��ȯ
        string ToJsonData = JsonUtility.ToJson(maingamedata, true);
        string filePath = Application.persistentDataPath+ "/" + MainGameDataFileName;
        Debug.Log(Application.persistentDataPath); //���� ��ġ ��� (Ȯ�ο�)

        //Write
        File.WriteAllText(filePath, ToJsonData);

        Debug.Log("SaveMainGameData() ����"); //(Ȯ�ο�)

        Debug.Log(maingamedata.stageNum); //(Ȯ�ο�)

    }

    public void SaveSoundData()
    {
        //Ŭ����->Json ��ȯ
        string ToJsonData = JsonUtility.ToJson(sounddata, true);
        string filePath = Application.persistentDataPath + "/" + SoundDataFileName;
        Debug.Log(Application.persistentDataPath); //���� ��ġ ��� (Ȯ�ο�)

        //Write
        File.WriteAllText(filePath, ToJsonData);

        Debug.Log("SaveSoundData() ����"); //(Ȯ�ο�)

        if (sounddata != null)
        {
            Debug.Log(sounddata.bgm); //(Ȯ�ο�)
            Debug.Log(sounddata.effect); //(Ȯ�ο�)
        }
    }
}
