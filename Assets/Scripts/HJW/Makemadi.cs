using System;
using TMPro;
using UnityEngine;

public class Makemadi : MonoBehaviour
{
    EditorData editordata => DataManager.Instance.editordata;

    public Makenote note;
    public Settings settings;
    public static Makemadi instance;
    public Audio audio_;
    public GameObject madi;
    public GameObject getendtime;
    [Space(20)]
    [Header("Settings")]
    public GameObject ui;
    public GameObject err;
    public GameObject delete_ui;
    public GameObject delete_obj;
    public TMP_InputField name_ui;
    public TMP_InputField length_ui;
    public TMP_InputField creator_ui;
    public TMP_InputField bpm_ui;
    public TextMeshProUGUI musicname;
    public TextMeshProUGUI noteidx;
    public TextMeshProUGUI boxidx;
    public GameObject fileprefeb;
    public Transform filepar;
    [Header("Info")]
    public string projectname;
    public float sec; //total sec
    public string musicnamee;
    public int bpm;
    public float madimultiplyer;
    [Space(20)]
    public bool chart; //마디 범위 내에 들어와 있습니다.
    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        madiset();
        projectname = "New Project";
    }

    void madiset()
    {
        for (int i = 0; i < note.notesobj.Length; i++)
        {
            Destroy(note.notesobj[i]);
        }
        Array.Resize(ref note.notedata, 0);
        Array.Resize(ref note.notesobj, 0);
        Array.Resize(ref note.noteduration, 0);
        Maketile.instance.initile();
        madi.GetComponent<RectTransform>().sizeDelta = new Vector3(sec * madimultiplyer, 16.2721f);
    }

    void madirefresh()
    {
        madi.GetComponent<RectTransform>().sizeDelta = new Vector3(sec * madimultiplyer, 16.2721f);
        for(int i = 0; i < note.notesobj.Length;i++)
        {
            note.notesobj[i].transform.localPosition = new Vector2( (float)note.notedata[i] * madimultiplyer, 0); 
        }
    }
    // Update is called once per frame
    void Update()
    {
        noteidx.text = Maketile.instance.makenote.notedata.Length.ToString();
        boxidx.text = Maketile.instance.boxpos.Length.ToString();
        var mospos = Camera.main.ScreenToWorldPoint(Input.mousePosition) + new Vector3(0, 0, 9);
        var a = Physics2D.Raycast(mospos, Vector3.forward, 2, LayerMask.GetMask("Chart"));
        if (a)
        {
            chart = true;
        }
        else
        {
            chart = false;
        }
        if (chart && !Audio.playing)
        {
            var pos = madi.GetComponent<RectTransform>().anchoredPosition;

            if(Input.mouseScrollDelta.y < 0) //for - **
            {
                if(Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
                {
                    if(madimultiplyer <= 1)
                    {
                        return;
                    }
                    madimultiplyer--;
                    madirefresh();
                    if ((sec / 2) < madi.transform.InverseTransformPoint(getendtime.transform.position.x, getendtime.transform.position.y, 0).x / madimultiplyer)
                    {
                        madi.GetComponent<RectTransform>().anchoredPosition = new Vector2(pos.x, 38.8f - sec * madimultiplyer);
                    }
                    return;
                }
                if(- (38.8f - sec * madimultiplyer) + (pos.y - madimultiplyer) < madimultiplyer)
                {
                     madi.GetComponent<RectTransform>().anchoredPosition = new Vector2(pos.x, 38.8f - sec * madimultiplyer);
                }
                else
                {
                    madi.GetComponent<RectTransform>().anchoredPosition = new Vector2(pos.x, pos.y - madimultiplyer);
                }
            }
            if (Input.mouseScrollDelta.y > 0) //back +
            {
                if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift) )
                {
                    if(madimultiplyer >= 5)
                    {
                        return;
                    }
                    madimultiplyer++;
                    madirefresh();
                    return;
                }
                if (-39 - (pos.y + madimultiplyer) < 2)
                {
                    madi.GetComponent<RectTransform>().anchoredPosition = new Vector2(pos.x, -39);
                }
                else
                {
                    madi.GetComponent<RectTransform>().anchoredPosition = new Vector2(pos.x, pos.y + madimultiplyer);
                }
            }
        }
    }
    public void uiset()
    {
        musicname.text = musicnamee.ToString();
        length_ui.text = sec.ToString();
        name_ui.text = projectname.ToString();
        creator_ui.text = editordata.creator;
        bpm_ui.text = bpm.ToString();
    }

    public void uitodata()
    {        
        sec = Convert.ToInt64(length_ui.text);
        projectname = name_ui.text;
        bpm = Convert.ToInt32(bpm_ui.text);
        madiset();
    }

    public void check()
    {
        int result;
        if(!int.TryParse(length_ui.text, out result) && !int.TryParse(bpm_ui.text, out result))
        {
            err.SetActive(true);
            return;
        }
        uitodata();
    }

    public void Saveinfo()
    {
        editordata.projectname = projectname;
        editordata.sec = sec;
        editordata.music = audio_.mainmusic.clip;
        editordata.musicname = musicnamee;
        editordata.creator = creator_ui.text;
        editordata.bpm = Convert.ToInt32(bpm_ui.text);
    }
    public void Loadinfo()
    {
        projectname = editordata.projectname;   
        sec = editordata.sec;
        musicnamee = editordata.musicname;
        audio_.mainmusic.clip = Resources.Load(musicnamee) as AudioClip;

        uiset();
        madisetforload();
    }

    void madisetforload()
    {
        madi.GetComponent<RectTransform>().sizeDelta = new Vector3(sec * madimultiplyer, 16.2721f);
        note.noteload();
    }
    public void deletee()
    {
        DataManager.Instance.deleteeditordata(delete_obj.name);
    }
}
