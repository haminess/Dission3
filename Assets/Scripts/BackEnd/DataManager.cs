using UnityEngine;
using System.IO;
using System.Linq;
using System;
using TMPro;

public class DataManager : MonoBehaviour
{
    static GameObject container;

    //�̱������� ����
    static DataManager instance;

    // ���� ������ dontdestroy ����
    public int characterNum;
    public int stageNum;
    public int difficulty;
    public string guideKey = "GuideLooked";
    public int GuideLooked;

    //����� Ŭ���� ����
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

    //���� �̸� ����
    string MainGameDataFileName = "MainGameData.json"; //������ �����ε� ä�� �ø��ų� �� �� ����ڰ� �Է��� �� �ְ�
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
            print("���� ��ũ " + maingamedata.synk + "����" + maingamedata.judge);

            if (maingamedata.score.Length == 4 && maingamedata.collection.Length == 4 && maingamedata.stageNum.Length == 4)
            {
                // ������ �� �����̸� ����
                print("����� ���� ���� �ε�");
                print("���� ���� 1: " + maingamedata.score[0] + "2: " + maingamedata.score[1] + "3: " + maingamedata.score[2] + "4: " + maingamedata.score[3]);

                print("���� ��ũ " + maingamedata.synk + "����" + maingamedata.judge);
                return;
            }

        }

        print("�ε� �ʱ�ȭ");
        // ��� �ʱ�ȭ �ڵ�
        maingamedata = new MainGameData(); // �Ǵ� �ٸ� �ʱⰪ���� ������ �� ����

        // �ʱ�ȭ
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

        Debug.Log("LoadMainGameData().else �ʱ�ȭ ����"); //(Ȯ�ο�)
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
            Array.Resize(ref Maketile.instance.boxpos, editordata.boxpos.Length);
            Array.Resize(ref Makemadi.instance.note.notepos, editordata.notepos.Length);
            Array.Resize(ref Makemadi.instance.note.notegroup, editordata.notegroup.Length);
            Array.Resize(ref Makemadi.instance.note.notetype, editordata.notetype.Length);

            for (int a = 0; a < editordata.notedata.Length; a++)
            {
                Makemadi.instance.note.notedata[a] = editordata.notedata[a];
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
        for (int i = 0; i < editorfilelist.Length; i++)
        {
            var a = Instantiate(PlayManager.instance.fileprefeb, PlayManager.instance.fileparent);
            a.name = i.ToString();
            a.GetComponentsInChildren<TextMeshProUGUI>()[0].text = editorfilelist[i];

            string filePath = Application.persistentDataPath + "/" + editorfilelist[i] + ".json";
            string FromJsonData = File.ReadAllText(filePath);
            editordata = JsonUtility.FromJson<EditorData>(FromJsonData);

            char[] musicname = editordata.music.ToString().ToCharArray(); //24
            for (int j = musicname.Length - 1; j > musicname.Length - 24; j--)
            {
                musicname[j] = ' ';
            }
            a.GetComponentsInChildren<TextMeshProUGUI>()[1].text = String.Join("", musicname);
            a.GetComponentsInChildren<TextMeshProUGUI>()[2].text = editordata.creator;
        }
    }
    public void Loadplaymodedata(string i)
    {
        PlayManager.instance.infopannal.SetActive(true);
        //Load
        string filePath = Application.persistentDataPath + "/" + editorfilelist[Convert.ToInt32(i)] + ".json";

        if (File.Exists(filePath))
        {
            string FromJsonData = File.ReadAllText(filePath);
            editordata = JsonUtility.FromJson<EditorData>(FromJsonData);

            PlayManager.instance.projectname.text = editordata.projectname;
            char[] musicname = editordata.music.ToString().ToCharArray(); //24
            for (int j = musicname.Length - 1; j > musicname.Length - 24; j--)
            {
                musicname[j] = ' ';
            }
            PlayManager.instance.bgmname.text = String.Join("", musicname);
            PlayManager.instance.bpm.text = editordata.bpm + " Bpm".ToString();
            PlayManager.instance.notecount.text = editordata.boxpos.Length + " Notes".ToString();
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
}

