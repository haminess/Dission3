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

    //싱글톤으로 선언
    static DataManager instance;

    // 게임 진행중 dontdestroy 정보
    public int characterNum;
    public int stageNum;
    public int difficulty;

    //저장용 클래스 변수
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

    //파일 이름 설정
    string MainGameDataFileName = "MainGameData.json"; //지금은 지정인데 채보 올리거나 할 땐 사용자가 입력할 수 있게
    string SoundDataFileName = "SoundData.json";


    private void Awake()
    {
        if(GameObject.FindObjectsOfType<DataManager>().Length == 1)
        {
            DontDestroyOnLoad(gameObject);
        }
        else if(GameObject.FindObjectsOfType<DataManager>().Length > 1)
        {
            Destroy(gameObject);
        }
        maingamedata = new MainGameData();
        sounddata = new SoundData();
    }

    // Start is called before the first frame update
    void Start()
    {
        // 정보 초기화
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
            //불러오기
            string FromJsonData = File.ReadAllText(filePath);
            maingamedata = JsonUtility.FromJson<MainGameData>(FromJsonData);
            Debug.Log("LoadMainGameData() 실행"); //(확인용)
            print("로컬 싱크 " + maingamedata.synk + "판정" + maingamedata.judge);

            if (maingamedata.score.Length == 4 && maingamedata.collection.Length == 4 && maingamedata.stageNum.Length == 4)
            {
                // 데이터 값 정상이면 리턴
                print("저장된 파일 정상 로드");
                print("파일 점수 1: " + maingamedata.score[0] + "2: " + maingamedata.score[1] + "3: " + maingamedata.score[2] + "4: " + maingamedata.score[3]);

                print("로컬 싱크 " + maingamedata.synk + "판정" + maingamedata.judge);
                return;
            }

        }

        print("로드 초기화");
        //기몬값 초기화 코드
        maingamedata = new MainGameData(); // 또는 다른 초기값으로 설정할 수 있음

        //초기화
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

        maingamedata.synk = 0;
        maingamedata.judge = 0;

        SaveMainGameData();

        Debug.Log("LoadMainGameData().else 초기화 실행"); //(확인용)
    }

    public void LoadSoundData()
    {
        //Load
        string filePath = Application.persistentDataPath + "/" + SoundDataFileName;

        if (File.Exists(filePath))
        {
            //불러오기
            string FromJsonData = File.ReadAllText(filePath);
            sounddata = JsonUtility.FromJson<SoundData>(FromJsonData);
            Debug.Log("LoadSoundData() 실행"); //(확인용)
        }
        else
        {
            //기본값 초기화 코드
            sounddata = new SoundData();

            sounddata.bgm = 1;
            sounddata.effect = 1;

            SaveSoundData();       

            Debug.Log("LoadSoundData().else 초기화 실행"); //(확인용)
        }

        //Debug.Log(sounddata.bgm); //(확인용)
        //Debug.Log(sounddata.effect); //(확인용)
        if (sounddata != null)
        {
            //Debug.Log(sounddata.bgm); //(확인용)
            //Debug.Log(sounddata.effect); //(확인용)
        }
    }

    public void SaveMainGameData()
    {
        print("저장하기 " + maingamedata.synk);

        //클래스->Json 전환
        string ToJsonData = JsonUtility.ToJson(maingamedata, true);
        string filePath = Application.persistentDataPath+ "/" + MainGameDataFileName;
        Debug.Log(Application.persistentDataPath); //저장 위치 출력 (확인용)

        //Write
        File.WriteAllText(filePath, ToJsonData);

        Debug.Log("SaveMainGameData() 실행"); //(확인용)

        //Debug.Log(maingamedata.stageNum); //(확인용)

        print("저장하기 " + maingamedata.synk);
    }

    public void SaveSoundData()
    {
        //클래스->Json 전환
        string ToJsonData = JsonUtility.ToJson(sounddata, true);
        string filePath = Application.persistentDataPath + "/" + SoundDataFileName;
        Debug.Log(Application.persistentDataPath); //저장 위치 출력 (확인용)

        //Write
        File.WriteAllText(filePath, ToJsonData);

        Debug.Log("SaveSoundData() 실행"); //(확인용)

        if (sounddata != null)
        {
            //Debug.Log(sounddata.bgm); //(확인용)
            //Debug.Log(sounddata.effect); //(확인용)
        }
    }
}
