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
            if(!instance)
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
    string GameDataFileName = "GameData.json"; //������ �����ε� ä�� �ø��ų� �� �� ����ڰ� �Է��� �� �ְ�
    string IntDataFileName = "Int2DArray.json";

    //����� Ŭ���� ����
    public GameData gamedata = new GameData(); //Ŭ������ ���� �� �ʿ��ϸ� �̸� ���� or �߰�
    public IntData intData = new IntData(); //�߰�!


    // Start is called before the first frame update
    void Start()
    {
        LoadGameData();

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void LoadGameData()
    {
        //Load GameData
        string filePath = Application.persistentDataPath + "/" + GameDataFileName;
        //string filePath = Application.persistentDataPath + GameDataFileName;

        if(File.Exists(filePath)) //����� ������ ������ �ҷ�����
        {
            //�ִ� �� �ҷ�����
            string  FromJsonData = File.ReadAllText(filePath);
            gamedata = JsonUtility.FromJson<GameData>(FromJsonData);
            Debug.Log("LoadGameData() ����");
        }

        else //����� ������ ������ ����
        {
                gamedata = new GameData();

                // �������� �⺻ ����
                //Stage1 Unlocked
                gamedata.Unlocked[0] = true;

                //Stage2~5 Locked
                for (int i = 1; i < 5; i++)
                {
                    gamedata.Unlocked[i] = false;
                }

            SaveGameData();

            Debug.Log("LoadGameData() else ����");
        }

        //�����Ȳ Ȯ�ο�
        for (int i = 0; i < gamedata.Unlocked.Length; i++)
        {
            Debug.Log($"Stage{i + 1} Unlocked : " + gamedata.Unlocked[i]);
        }


        // Load IntData
        string intDataFilePath = Application.persistentDataPath + "/" + IntDataFileName;
        if (File.Exists(intDataFilePath))
        {
            string intDataJson = File.ReadAllText(intDataFilePath);
            intData = JsonUtility.FromJson<IntData>(intDataJson);
            Debug.Log("Load IntData() ����");
        }
        else
        {
            intData = new IntData();
            // Set default IntData (2D array)
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 5; j++)
                {
                    intData.Int2DArray[i, j] = 0; // Set default value (you can change it as needed)
                }
            }

            SaveIntData(); // Save new IntData
            Debug.Log("Load IntData() else ����");
        }

    }

    public void SaveGameData()
    {
        //Ŭ����->Json ��ȯ (true: ������)
        string ToJsonData = JsonUtility.ToJson(gamedata, true);
        string filePath = Application.persistentDataPath + "/" + GameDataFileName;
        Debug.Log(Application.persistentDataPath);
        //string ToJsonData = JsonUtility.ToJson(gamedata, true);
        //string filePath = Application.persistentDataPath + GameDataFileName;

        //Write
        File.WriteAllText(filePath, ToJsonData);

        Debug.Log("SaveGameData() ����");

        //�����Ȳ Ȯ�ο�
        for (int i = 0; i < gamedata.Unlocked.Length; i++)
        {
            Debug.Log($"Stage{i + 1} Unlocked : " + gamedata.Unlocked[i]);
        }
    }

    public void SaveIntData()
    {
        // Convert IntData to JSON
        string intDataJson = JsonUtility.ToJson(intData, true);
        string intDataFilePath = Application.persistentDataPath + "/" + IntDataFileName;
        // Write JSON data to the file
        File.WriteAllText(intDataFilePath, intDataJson);

        Debug.Log("Save IntData() ����");

        // Progress checking
        // ...
    }
}
