using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.Playables;
using UnityEngine.UI;

public class DataManager : MonoBehaviour
{
    static GameObject container;

    //�̱������� ����
    static DataManager instance;
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

    //����� Ŭ���� ����
    public MainGameData maingamedata = new MainGameData();
    public SoundManager sounddata = new SoundManager();


    // Start is called before the first frame update
    void Start()
    {
        
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
        }
        else
        {
            //��� �ʱ�ȭ �ڵ�
            sounddata = new SoundManager(); // �Ǵ� �ٸ� �ʱⰪ���� ������ �� ����

            SaveMainGameData();

            Debug.Log("LoadMainGameData().else ����"); //(Ȯ�ο�)
        }
    }

    public void LoadSoundData()
    {
        //Load
        string filePath = Application.persistentDataPath + "/" + SoundDataFileName;

        if (File.Exists(filePath))
        {
            //�ҷ�����
            string FromJsonData = File.ReadAllText(filePath);
            sounddata = JsonUtility.FromJson<SoundManager>(FromJsonData);
            Debug.Log("LoadSoundData() ����"); //(Ȯ�ο�)
        }
        else
        {
            //��� �ʱ�ȭ �ڵ�

            SaveMainGameData();

            Debug.Log("LoadSoundData().else ����"); //(Ȯ�ο�)
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
    }
}