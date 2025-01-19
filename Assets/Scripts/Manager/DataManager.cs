using UnityEngine;
using System.IO;
using System.Linq;
using System;
using TMPro;
using UnityEditor.Experimental.GraphView;

public class DataManager : MonoBehaviour
{
    public enum Mode
    {
        Debug,
        Stage,
        Play
    }

    static GameObject container;


    public string chartNum;

    public MainGameData maingamedata = new MainGameData();
    public SoundData sounddata = new SoundData();
    public EditorData editordata = new EditorData();


    private static DataManager instance;
    public static DataManager Instance
    {
        get
        {
            if (instance == null)
            {
                // 1. 먼저 씬에서 찾아보기
                instance = FindObjectOfType<DataManager>();

                // 2. 씬에도 없다면 프리팹에서 생성
                if (instance == null)
                {
                    // Resources 폴더에서 프리팹 로드
                    GameObject prefab = Resources.Load<GameObject>("Prefabs/DataManager");
                    if (prefab == null)
                    {
                        Debug.LogError("DataManager 프리팹을 찾을 수 없습니다!");
                        return null;
                    }

                    GameObject container = Instantiate(prefab);
                    instance = container.GetComponent<DataManager>();
                    container.name = "DataManager";
                }

                DontDestroyOnLoad(instance.gameObject);
            }
            return instance;
        }
    }


    string MainGameDataFileName = "MainGameData.json"; 
    string SoundDataFileName = "SoundData.json";
    string EditorDataFileName = "EditorData.json";

    public string[] editorfilelist;


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
        LoadMainGameData();
        LoadSoundData();
    }


    public void LoadMainGameData()
    {
        //Load
        string filePath = Application.persistentDataPath + "/" + MainGameDataFileName;

        if(File.Exists(filePath))
        {
            string FromJsonData = File.ReadAllText(filePath);
            maingamedata = JsonUtility.FromJson<MainGameData>(FromJsonData);
            if (maingamedata.score.Length == 4 && maingamedata.collection.Length == 4 && maingamedata.stageNum.Length == 4)
            {
                return;
            }
        }
        else
        {
            maingamedata = new MainGameData();

            for (int i = 0; i < 4; i++)
            {
                maingamedata.stageNum[i] = i + 1;
                maingamedata.score[i] = 0;
                maingamedata.collection[i] = 0;
                maingamedata.happy[i] = false;
                maingamedata.sad[i] = false;
            }

            maingamedata.synk = 0;
            maingamedata.judge = 0;

            SaveMainGameData();
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

            Debug.Log("LoadSoundData().else �ʱ�ȭ ����"); //(Ȯ�ο�)
        }

        //Debug.Log(sounddata.bgm); //(Ȯ�ο�)
        //Debug.Log(sounddata.effect); //(Ȯ�ο�)
        if (sounddata != null)
        {
            //Debug.Log(sounddata.bgm); //(Ȯ�ο�)
            //Debug.Log(sounddata.effect); //(Ȯ�ο�)
        }
    }

    public void SaveMainGameData()
    {
        print("�����ϱ� " + maingamedata.synk);

        //Ŭ����->Json ��ȯ
        string ToJsonData = JsonUtility.ToJson(maingamedata, true);
        string filePath = Application.persistentDataPath+ "/" + MainGameDataFileName;
        Debug.Log(Application.persistentDataPath); //���� ��ġ ��� (Ȯ�ο�)

        //Write
        File.WriteAllText(filePath, ToJsonData);

        Debug.Log("SaveMainGameData() ����"); //(Ȯ�ο�)

        //Debug.Log(maingamedata.stageNum); //(Ȯ�ο�)

        print("�����ϱ� " + maingamedata.synk);
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
            //Debug.Log(sounddata.bgm); //(Ȯ�ο�)
            //Debug.Log(sounddata.effect); //(Ȯ�ο�)
        }
    }


    public void LoadEditorData(string i)
    {
        //Load
        string filePath = Application.persistentDataPath + "/" + editorfilelist[Convert.ToInt32(i)] + ".json";

        if (File.Exists(filePath))
        {
            Makemadi.instance.audio_.resetmusic();
            string FromJsonData = File.ReadAllText(filePath);
            editordata = JsonUtility.FromJson<EditorData>(FromJsonData);

            Array.Resize(ref Maketile.instance.boxdata, editordata.boxdata.Length);

            for (int a = 0; a < Makemadi.instance.note.notedata.Count; a++)
            {
                Destroy(Makemadi.instance.note.notedata[a].noteobj);
            }
            Makemadi.instance.note.notedata.Clear();
            for (int a = 0; a < editordata.notedata.Count; a++)
            {
                Notedata tempdata = new Notedata();
                Makemadi.instance.note.notedata.Add(tempdata);
                Makemadi.instance.note.notedata[a].notedata = editordata.notedata[a];
                Makemadi.instance.note.notedata[a].noteduration = editordata.noteduration[a];
            }
            for (int a = 0; a < editordata.boxdata.Length; a++)
            {
                Maketile.instance.boxdata[a] = editordata.boxdata[a];
            }

            Maketile.instance.boxposload();
            Makemadi.instance.Loadinfo();
        }
    }

    public void LoadEditorDataToMain(string i)
    {
        string filePath = Application.persistentDataPath + "/" + editorfilelist[Convert.ToInt32(i)] + ".json";

        if (File.Exists(filePath))
        {
            string FromJsonData = File.ReadAllText(filePath);
            editordata = JsonUtility.FromJson<EditorData>(FromJsonData);
        }
    }


    public void SaveEditorData()
    {
        //?    ->Json   ?
        string ToJsonData = JsonUtility.ToJson(editordata, true);
        EditorDataFileName = Makemadi.instance.projectname + ".json";
        string filePath = Application.persistentDataPath + "/" + EditorDataFileName;
        Debug.Log(Application.persistentDataPath); //       ?     (? �� )

        //Write
        File.WriteAllText(filePath, ToJsonData);

        //file list contrl
        bool list = true;
        for (int i = 0; i < editorfilelist.Length; i++)
        {
            if (editorfilelist[i] == Makemadi.instance.projectname)
            {
                list = false;
            }
        }
        if (list)
        {
            Array.Resize(ref editorfilelist, editorfilelist.Length + 1);
            editorfilelist[editorfilelist.Length - 1] = Makemadi.instance.projectname;
        }

        reloadeditorlist();
    }
    public void listload()
    {
        for (int i = 0; i < Makemadi.instance.filepar.childCount; i++)
        {
            Destroy(Makemadi.instance.filepar.GetChild(i).gameObject);
        }
        string filepath = Application.persistentDataPath + "/" + "Editorlist.json";
        if (File.Exists(filepath))
        {
            var read = File.ReadAllLines(filepath);
            Array.Resize(ref editorfilelist, read.Length);
            for (int i = 0; i < read.Length; i++)
            {
                editorfilelist[i] = read[i];
            }
        }
        for (int i = 0; i < editorfilelist.Length; i++)
        {
            var a = Instantiate(Makemadi.instance.fileprefeb, Makemadi.instance.filepar);
            a.name = i.ToString();
            a.GetComponentInChildren<TextMeshProUGUI>().text = editorfilelist[i];
        }
    }
    public void Playmodelistload()
    {
        for (int i = 0; i < PlayManager.instance.fileparent.childCount; i++)
        {
            Destroy(PlayManager.instance.fileparent.GetChild(i).gameObject);
        }
        string filepath = Application.persistentDataPath + "/" + "Editorlist.json";
        if (File.Exists(filepath))
        {
            var read = File.ReadAllLines(filepath);
            Array.Resize(ref editorfilelist, read.Length);
            for (int i = 0; i < read.Length; i++)
            {
                editorfilelist[i] = read[i];
            }
        }
        if(editorfilelist.Length == 0)
        {
            PlayManager.instance.nofile.SetActive(true);
            return;
        }
        for (int i = 0; i < editorfilelist.Length; i++)
        {
            var a = Instantiate(PlayManager.instance.fileprefeb, PlayManager.instance.fileparent);
            a.name = i.ToString();
            a.GetComponentsInChildren<TextMeshProUGUI>()[0].text = editorfilelist[i];

            string filePath = Application.persistentDataPath + "/" + editorfilelist[i] + ".json";
            string FromJsonData = File.ReadAllText(filePath);
            editordata = JsonUtility.FromJson<EditorData>(FromJsonData);

            a.GetComponentsInChildren<TextMeshProUGUI>()[1].text = editordata.musicname.ToString();
            a.GetComponentsInChildren<TextMeshProUGUI>()[2].text = editordata.creator;
        }
    }
    public void Loadplaymodedata(string i)
    {
        // Play Scene Init
        PlayManager.instance.infopannal.SetActive(true);
        PlayManager.instance.music.Stop();

        //Load
        string filePath = Application.persistentDataPath + "/" + editorfilelist[Convert.ToInt32(i)] + ".json";
        if (File.Exists(filePath))
        {
            string FromJsonData = File.ReadAllText(filePath);
            editordata = JsonUtility.FromJson<EditorData>(FromJsonData);

            PlayManager.instance.projectname.text = editordata.projectname;
            PlayManager.instance.bgmname.text = editordata.musicname;
            PlayManager.instance.notecount.text = editordata.boxdata.Length + " Notes".ToString();
            string min = (Mathf.Floor((float)editordata.sec / 60)).ToString();
            string sec = (editordata.sec % 60).ToString();
            if((editordata.sec / 60) < 10)
            {
                min = "0" + (Mathf.Floor((float)editordata.sec / 60)).ToString();
            }
            if((editordata.sec % 60)  < 10)
            {
                sec = "0" + (editordata.sec %60).ToString();
            }
            PlayManager.instance.sec.text = min + ":" + sec.ToString();
            PlayManager.instance.creator.text = editordata.creator;
            PlayManager.instance.music.clip = Resources.Load("Sound/" + editordata.musicname) as AudioClip;
            PlayManager.instance.music.Play();
        }
    }
    public void deleteeditordata(string i)
    {
        int index = Convert.ToInt32(i);
        string EditorDataFileName = editorfilelist[index] + ".json";
        string filePath = Application.persistentDataPath + "/" + EditorDataFileName;
        File.Delete(filePath);
        editorfilelist[index] = "";
        editorfilelist = Array.FindAll(editorfilelist, str => str != "").ToArray();
        reloadeditorlist();
        listload();
    }
    public void reloadeditorlist()
    {
        string listfilePath = Application.persistentDataPath + "/" + "Editorlist.json";
        string editorlistcon = "";
        for (int i = 0; i < editorfilelist.Length; i++)
        {
            editorlistcon = editorlistcon + editorfilelist[i] + "\n";
        }
        File.WriteAllText(listfilePath, editorlistcon);
    }



    private string SavePath => Application.persistentDataPath;

    // 제네릭을 사용한 저장 함수
    public void Save<T>(T data) where T : ISaveData
    {
        try
        {
            string json = JsonUtility.ToJson(data, true);
            string filePath = Path.Combine(SavePath, data.GetFileName());
            File.WriteAllText(filePath, json);
            Debug.Log($"Data saved successfully: {filePath}");
        }
        catch (Exception e)
        {
            Debug.LogError($"Failed to save data: {e.Message}");
        }
    }

    // 제네릭을 사용한 로드 함수
    public T Load<T>() where T : ISaveData, new()
    {
        T data = new T();
        string filePath = Path.Combine(SavePath, data.GetFileName());

        try
        {
            if (File.Exists(filePath))
            {
                string json = File.ReadAllText(filePath);
                data = JsonUtility.FromJson<T>(json);
                return data;
            }
            else
            {
                // 파일이 없다면 기본값을 반환
                Debug.LogWarning($"Save file not found: {filePath}");
                return data;
            }
        }
        catch (Exception e)
        {
            Debug.LogError($"Failed to load data: {e.Message}");
            return data;
        }
    }
    public T Load<T>(string _file_name) where T : ISaveData, new()
    {
        T data = new T();
        string filePath = Path.Combine(SavePath, _file_name);

        try
        {
            if (File.Exists(filePath))
            {
                string json = File.ReadAllText(filePath);
                data = JsonUtility.FromJson<T>(json);
                return data;
            }
            else
            {
                // 파일이 없다면 기본값을 반환
                Debug.LogWarning($"Save file not found: {filePath}");
                return data;
            }
        }
        catch (Exception e)
        {
            Debug.LogError($"Failed to load data: {e.Message}");
            return data;
        }
    }


    // 저장 로드 예시
    void SaveExample()
    {
        // 플레이어 데이터 저장
        PlayerData playerData = new PlayerData
        {
            playerName = "Player1",
            level = 10,
            health = 100f,
            position = new Vector3(1f, 2f, 3f)
        };
        DataManager.Instance.Save(playerData);

        // 게임 설정 저장
        GameSettingsData settingsData = new GameSettingsData
        {
            masterVolume = 0.8f,
            bgmVolume = 0.7f,
            sfxVolume = 0.9f,
            isFullScreen = true
        };
        DataManager.Instance.Save(settingsData);
    }

    void LoadExample()
    {
        // 플레이어 데이터 로드
        PlayerData playerData = DataManager.Instance.Load<PlayerData>();

        // 게임 설정 로드
        GameSettingsData settingsData = DataManager.Instance.Load<GameSettingsData>();
    }
}

