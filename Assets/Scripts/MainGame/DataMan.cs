using System;
using UnityEngine;
using System.IO;
using System.Linq;
using TMPro;


[Serializable]
public struct tStageData
{
    tPlayData playData;
    int stageNum;
    int ending;
    int collection;
}


[Serializable]
public struct tMusicData
{
    int musicNum;
}

[Serializable]
public struct tPlayData
{
    tJudgeData judgeData;
    float score;
    int combo;
    int life;
}

[Serializable]
public struct tJudgeData
{
    public int combo;
    public int miss;
    public int bad;
    public int good;
    public int perfect;
}


public class DataMan : MonoBehaviour
{
    public tJudgeData judgeData;
    void GetData(ref DataType _data)
    {
        _data.GetData();
    }
    static GameObject container;

    static DataManager instance;

    // dondestroy
    public int characterNum;
    public int stageNum;
    public int difficulty;
    public PLAY_MODE mode = PLAY_MODE.DEBUG;
    public string chartNum;

    public MainGameData maingamedata = new MainGameData();
    public SoundData sounddata = new SoundData();
    public EditorData editordata = new EditorData();


    // 데이터 저장 공간
    string MainGameDataFileName = "MainGameData.json"; 
    string SoundDataFileName = "SoundData.json";
    string EditorDataFileName = "EditorData.json";

    public string[] editorfilelist;


    private void Awake()
    {
        if (GameObject.FindObjectsOfType<DataManager>().Length == 1)
        {
            DontDestroyOnLoad(gameObject);
        }
        else if (GameObject.FindObjectsOfType<DataManager>().Length > 1)
        {
            Destroy(gameObject);
        }
        maingamedata = new MainGameData();
        sounddata = new SoundData();

    }

    public void LoadData(ref MainGameData _data)
    {
        string filePath = Application.persistentDataPath + "/" + MainGameDataFileName;
        _data = Load<MainGameData>(filePath);
    }

    private T Load<T>(string _path) where T : new()
    {
        T data;

        // Load
        if (File.Exists(_path))
        {
            string FromJsonData = File.ReadAllText(_path);
            data = JsonUtility.FromJson<T>(FromJsonData);
        }

        // Init
        else
        {
            data = new T();
        }

        return data;
    }
    public void SaveData(ref MainGameData _data)
    {
        string filePath = Application.persistentDataPath + "/" + MainGameDataFileName;
        Save<MainGameData>(filePath, ref _data);
    }
    private void Save<T>(string _path, ref T _data)
    {
        string ToJsonData = JsonUtility.ToJson(_data, true);
        File.WriteAllText(_path, ToJsonData);
    }

}


