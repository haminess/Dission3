using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.Playables;
using UnityEngine.UI;

public class DataManager : MonoBehaviour
{
    static GameObject container;

    //싱글톤으로 선언
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

    //파일 이름 설정
    string GameDataFileName = "GameData.json"; //지금은 지정인데 채보 올리거나 할 땐 사용자가 입력할 수 있게
    string IntDataFileName = "Int2DArray.json";

    //저장용 클래스 변수
    public GameData gamedata = new GameData(); //클래스가 여러 개 필요하면 이름 수정 or 추가
    public IntData intData = new IntData(); //추가!


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

        if(File.Exists(filePath)) //저장된 파일이 있으면 불러오기
        {
            //있는 거 불러오기
            string  FromJsonData = File.ReadAllText(filePath);
            gamedata = JsonUtility.FromJson<GameData>(FromJsonData);
            Debug.Log("LoadGameData() 실행");
        }

        else //저장된 파일이 없으면 생성
        {
                gamedata = new GameData();

                // 스테이지 기본 설정
                //Stage1 Unlocked
                gamedata.Unlocked[0] = true;

                //Stage2~5 Locked
                for (int i = 1; i < 5; i++)
                {
                    gamedata.Unlocked[i] = false;
                }

            SaveGameData();

            Debug.Log("LoadGameData() else 실행");
        }

        //진행상황 확인용
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
            Debug.Log("Load IntData() 실행");
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
            Debug.Log("Load IntData() else 실행");
        }

    }

    public void SaveGameData()
    {
        //클래스->Json 전환 (true: 가독성)
        string ToJsonData = JsonUtility.ToJson(gamedata, true);
        string filePath = Application.persistentDataPath + "/" + GameDataFileName;
        Debug.Log(Application.persistentDataPath);
        //string ToJsonData = JsonUtility.ToJson(gamedata, true);
        //string filePath = Application.persistentDataPath + GameDataFileName;

        //Write
        File.WriteAllText(filePath, ToJsonData);

        Debug.Log("SaveGameData() 실행");

        //진행상황 확인용
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

        Debug.Log("Save IntData() 실행");

        // Progress checking
        // ...
    }
}
