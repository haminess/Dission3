using UnityEngine;
using System.IO;
using System.Linq;
using System;
using TMPro;

public class DataManager : MonoBehaviour
{
    static GameObject container;

    //싱글톤으로 선언
    static DataManager instance;

    // 게임 진행중 dontdestroy 정보
    public int characterNum;
    public int stageNum;
    public int difficulty;
    public string guideKey = "GuideLooked";
    public int GuideLooked;

    //저장용 클래스 변수
    public MainGameData maingamedata = new MainGameData();
    public SoundData sounddata = new SoundData();
    public EditorData editordata = new EditorData();

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

        GuideLooked = PlayerPrefs.GetInt(guideKey, 0);
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
        // 기몬값 초기화 코드
        maingamedata = new MainGameData(); // 또는 다른 초기값으로 설정할 수 있음

        // 초기화
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

    public int GetGuide()
    {
        GuideLooked = PlayerPrefs.GetInt(guideKey, 0);
        return GuideLooked;
    }

    public void SetGuide(int _state)
    {
        GuideLooked |= _state;
        PlayerPrefs.SetInt(guideKey, GuideLooked);
    }

    public bool isLookGuide(int _state)
    {
        if((GetGuide() & _state) > 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public void LoadEditorData(string i)
    {
        //Load
        string filePath = Application.persistentDataPath + "/" + editorfilelist[Convert.ToInt32(i)] + ".json";

        if (File.Exists(filePath))
        {
            Makemadi.instance.audio_.resetmusic();
            // ?     
            string FromJsonData = File.ReadAllText(filePath);
            editordata = JsonUtility.FromJson<EditorData>(FromJsonData);

            Array.Resize(ref Makemadi.instance.note.notedata, editordata.notedata.Length);
            Array.Resize(ref Makemadi.instance.note.noteorder, editordata.noteorder.Length);
            Array.Resize(ref Maketile.instance.boxpos, editordata.boxpos.Length);
            Array.Resize(ref Makemadi.instance.note.notepos, editordata.notepos.Length);
            Array.Resize(ref Makemadi.instance.note.notegroup, editordata.notegroup.Length);
            Array.Resize(ref Makemadi.instance.note.notetype, editordata.notetype.Length);

            for (int a = 0; a < editordata.notedata.Length; a++)
            {
                Makemadi.instance.note.notedata[a] = editordata.notedata[a];
            }
            for (int a = 0; a < editordata.noteorder.Length; a++)
            {
                Makemadi.instance.note.noteorder[a] = editordata.noteorder[a];
            }
            for (int a = 0; a < editordata.boxpos.Length; a++)
            {
                Maketile.instance.boxpos[a] = editordata.boxpos[a];
            }
            for (int a = 0; a < editordata.notepos.Length; a++)
            {
                Makemadi.instance.note.notepos[a] = editordata.notepos[a];
            }
            for (int a = 0; a < editordata.notegroup.Length; a++)
            {
                Makemadi.instance.note.notegroup[a] = editordata.notegroup[a];
            }
            for (int a = 0; a < editordata.notetype.Length; a++)
            {
                Makemadi.instance.note.notetype[a] = editordata.notetype[a];
            }


            Maketile.instance.boxposload();
            Makemadi.instance.Loadinfo();
        }
    }


    public void SaveEditorData()
    {
        //?    ->Json   ?
        string ToJsonData = JsonUtility.ToJson(editordata, true);
        EditorDataFileName = Makemadi.instance.projectname + ".json";
        string filePath = Application.persistentDataPath + "/" + EditorDataFileName;
        Debug.Log(Application.persistentDataPath); //       ?     (? ο )

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

    public void opensettingfolder()
    {
        System.Diagnostics.Process.Start(Application.persistentDataPath);
    }
}

