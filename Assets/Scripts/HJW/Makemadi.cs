using System;
using System.Collections;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Makemadi : MonoBehaviour
{
    EditorData editordata => DataManager.Instance.editordata;

    public Makenote note;
    public Settings settings;
    public static Makemadi instance;
    public Audio audio_;
    public GameObject madi;
    public GameObject Slider;
    public GameObject getendtime;
    public GameObject getmiddletime;
    public GameObject getstarttime;
    [Space(20)]
    public GameObject editmodeui;
    public Image editmodeiconimg;
    public Sprite[] editmodeicon;
    [Space(20)]
    [Header("Settings")]
    public GameObject ui;
    public GameObject err;
    public GameObject Dismatch_ui;
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
    public float anchorpos = 4.014713f;
    public bool slidermoving;
    bool noslider;
    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        projectname = "New Project";
        madiset();
    }

    void madiset()
    {
        for (int i = 0; i < note.notedata.Count; i++)
        {
            Destroy(note.notedata[i].noteobj);
        }
        note.notedata.Clear();
        madi.GetComponent<RectTransform>().pivot = new Vector2(0, 0.5f);
        madi.GetComponent<RectTransform>().anchoredPosition = new Vector2(4.014713f, 0);
        madi.GetComponent<RectTransform>().sizeDelta = new Vector3(sec * madimultiplyer, 16.2721f);
        if(sec < 40)
        {
            Slider.SetActive(false);
            noslider = true;
        }
        else
        {
            Slider.SetActive(true);
            noslider = false;
            sliderefresh();
        }
    }

    void madirefresh()
    {
        madi.GetComponent<RectTransform>().sizeDelta = new Vector3(sec * madimultiplyer, 16.2721f);
        for(int i = 0; i < note.notedata.Count;i++)
        {
            note.notedata[i].noteobj.GetComponent<RectTransform>().anchoredPosition = new Vector2( (float)note.notedata[i].notedata * madimultiplyer, 0);
            if (note.notedata[i].noteduration > 0)
            {
                var mid = note.notedata[i].noteobj.transform.GetChild(1);
                var over = note.notedata[i].noteobj.transform.GetChild(2);
                var end = note.notedata[i].noteobj.transform.GetChild(3);
                mid.GetComponent<RectTransform>().sizeDelta = new Vector2(note.notedata[i].noteduration * madimultiplyer, 103.87f);
                note.notedata[i].noteobj.GetComponent<BoxCollider2D>().size = new Vector2(note.notedata[i].noteduration * madimultiplyer + 0.6f, 201.7123f);
                note.notedata[i].noteobj.GetComponent<BoxCollider2D>().offset = new Vector2((note.notedata[i].noteduration * madimultiplyer / 2) + 0.15f, 15.45131f);
                over.GetComponent<BoxCollider2D>().size = new Vector2(note.notedata[i].noteduration * madimultiplyer + 3, 201.7123f);
                over.GetComponent<BoxCollider2D>().offset = new Vector2(note.notedata[i].noteduration * madimultiplyer / 2, 15.45131f);
                end.GetComponent<RectTransform>().anchoredPosition = new Vector2(note.notedata[i].noteduration * madimultiplyer + 2, 0);
            }
        }
        if(madi.GetComponent<RectTransform>().sizeDelta.x < 78)
        {
            noslider = true;
            Slider.SetActive(false);
        }
        else
        {
            noslider=false;
            Slider.SetActive(true);
            sliderefresh();
        }
    }
    // Update is called once per frame
    void Update()
    {
        noteidx.text = Maketile.instance.makenote.notedata.Count.ToString();
        boxidx.text = Maketile.instance.boxdata.Length.ToString();
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
                if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
                {
                    if (madimultiplyer >= 5)
                    {
                        return;
                    }
                    madimultiplyer++;
                    var pivot = new Vector2(0, -madi.transform.InverseTransformPoint(getmiddletime.transform.position.x, getmiddletime.transform.position.y, 0).x);
                    var posy = madi.GetComponent<RectTransform>().anchoredPosition.y;
                    madi.GetComponent<RectTransform>().pivot = pivot;
                    anchorpos = (pivot.y - 6.1682188789645f) / -1.4120789173537f;
                    madi.GetComponent<RectTransform>().anchoredPosition = new Vector2(anchorpos, posy);
                    madirefresh();
                    if (0 > madi.transform.InverseTransformPoint(getstarttime.transform.position.x, getstarttime.transform.position.y, 0).x / madimultiplyer)
                    {
                        madi.GetComponent<RectTransform>().anchoredPosition = new Vector2(anchorpos, 0);
                    }
                    return;
                }
                if (madi.GetComponent<RectTransform>().sizeDelta.x < 78)
                {
                    return;
                }
                if (sec * madimultiplyer + (pos.y - madimultiplyer) - 78< madimultiplyer)
                {
                     madi.GetComponent<RectTransform>().anchoredPosition = new Vector2(anchorpos, - sec * madimultiplyer + 78);
                    sliderefresh();
                }
                else
                {
                    madi.GetComponent<RectTransform>().anchoredPosition = new Vector2(anchorpos, pos.y - madimultiplyer);
                    sliderefresh();
                }
            }
            if (Input.mouseScrollDelta.y > 0) //back +
            {
                if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
                {
                    if (madimultiplyer <= 1)
                    {
                        return;
                    }
                    madimultiplyer--;
                    var pivot = new Vector2(0, -madi.transform.InverseTransformPoint(getmiddletime.transform.position.x, getmiddletime.transform.position.y, 0).x);
                    var posy = madi.GetComponent<RectTransform>().anchoredPosition.y;
                    madi.GetComponent<RectTransform>().pivot = pivot;
                    anchorpos = (pivot.y - 6.1682188789645f) / -1.4120789173537f;
                    madi.GetComponent<RectTransform>().anchoredPosition = new Vector2(anchorpos, posy);
                    madirefresh();
                    if (sec < madi.transform.InverseTransformPoint(getendtime.transform.position.x, getendtime.transform.position.y, 0).x / madimultiplyer)
                    {
                        madi.GetComponent<RectTransform>().anchoredPosition = new Vector2(anchorpos, - sec * madimultiplyer + 78);
                    }
                    if (0 > madi.transform.InverseTransformPoint(getstarttime.transform.position.x, getstarttime.transform.position.y, 0).x / madimultiplyer)
                    {
                        madi.GetComponent<RectTransform>().anchoredPosition = new Vector2(anchorpos, 0);
                    }
                    return;
                }
                if(madi.GetComponent<RectTransform>().sizeDelta.x < 78)
                {
                    return;
                }
                if (- (pos.y + madimultiplyer) < 2)
                {
                    madi.GetComponent<RectTransform>().anchoredPosition = new Vector2(anchorpos, 0);
                    sliderefresh();
                }
                else
                {
                    madi.GetComponent<RectTransform>().anchoredPosition = new Vector2(anchorpos, pos.y + madimultiplyer);
                    sliderefresh();
                }
            }
        }
        var sliderpos = Slider.GetComponent<RectTransform>().anchoredPosition.y;
        var madipos = madi.GetComponent<RectTransform>().anchoredPosition;
        if (sliderpos < 0 && madi.GetComponent<RectTransform>().sizeDelta.x > 78)
        {
            Slider.GetComponent<RectTransform>().anchoredPosition = new Vector2(6.8f, 0);
            madi.GetComponent<RectTransform>().anchoredPosition = new Vector2(madipos.x, 0);
        }
        else if (sliderpos > 78 - (1 / (sec * madimultiplyer * 0.0005f)) && madi.GetComponent<RectTransform>().sizeDelta.x > 78)
        {
            Slider.GetComponent<RectTransform>().anchoredPosition = new Vector2(6.8f, 78 - (1 / (sec * madimultiplyer * 0.0005f)));
            madi.GetComponent<RectTransform>().anchoredPosition = new Vector2(madipos.x, -sec * madimultiplyer + 78);
        }
    }
    public void slidermovestart()
    {
        if(noslider || Audio.playing)
        {
            return;
        }
        slidermoving = true;
        StartCoroutine(slidermovestartcor());
    }
    IEnumerator slidermovestartcor()
    {
        var mospos = gameObject.transform.InverseTransformPoint(Maketile.instance.mospos.x, Maketile.instance.mospos.y, 0).y + 20;
        var madipos = madi.GetComponent<RectTransform>().anchoredPosition;
        var slidertrans = Slider.GetComponent<RectTransform>();
        while (slidermoving)
        {
            mospos = gameObject.transform.InverseTransformPoint(Maketile.instance.mospos.x, Maketile.instance.mospos.y, 0).y + 20;
            slidertrans.anchoredPosition = new Vector2(6.8f, mospos);
            madi.GetComponent<RectTransform>().anchoredPosition = new Vector2(madipos.x, slidertrans.anchoredPosition.y * ((-sec * madimultiplyer + 78) / (78 - (1 / (sec * madimultiplyer * 0.0005f)))));
            yield return null;
        }
    }
    public void slidermovend()
    {
        slidermoving = false;
        StopCoroutine(slidermovestartcor());
    }
    public void sliderefresh() 
    {
        var slidertrans =Slider.GetComponent<RectTransform>();
        var madipos = madi.GetComponent<RectTransform>().anchoredPosition.y;
        slidertrans.sizeDelta = new Vector2(1, 1 / (sec*madimultiplyer * 0.0005f));
        //0 ~ -sec * madimultiplyer + 78
        //0 ~ 78
        //madipos * (78-sliderlength) /end
        slidertrans.anchoredPosition = new Vector2(6.8f, madipos * ((78 - (1 / (sec * madimultiplyer * 0.0005f))) / (-sec * madimultiplyer + 78)));
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
        note.previewbox.GetComponent<SpriteRenderer>().enabled = false;
        madiset();
    }

    public void check()
    {
        int result;
        if((!int.TryParse(length_ui.text, out result) && !int.TryParse(bpm_ui.text, out result)) || Convert.ToInt64(length_ui.text) < 1)
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
        settings.Saveeditordatadele();
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
