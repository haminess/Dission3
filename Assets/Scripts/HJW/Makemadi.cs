using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Makemadi : MonoBehaviour
{
    EditorData editordata => DataManager.Instance.editordata;

    public Makenote note;
    public Settings settings;
    public static Makemadi instance;
    public Audio audio_;
    public Transform canvas;
    public Transform charts;
    public GameObject prefab;
    [Space(20)]
    [Header("Settings")]
    public GameObject ui;
    public GameObject err;
    public GameObject delete_ui;
    public GameObject delete_obj;
    public TMP_InputField name_ui;
    public TMP_InputField bpm_ui;
    public TMP_InputField length_ui;
    public TMP_InputField starttime_ui;
    public TMP_InputField bakjapyoup_ui;
    public TMP_InputField creator_ui;
    public TextMeshProUGUI musicname;
    public TextMeshProUGUI noteidx;
    public TextMeshProUGUI boxidx;
    public GameObject fileprefeb;
    public Transform filepar;
    [Space(20)]
    [Header("For fucking small madi")]
    public GameObject total;
    public GameObject start;
    public GameObject Middle;
    public GameObject End;
    public float starttime;
    public float length;
    [Space(20)]
    [Header("Info")]
    public string projectname;
    public double bpm;
    public double sec; //total sec
    public double madi;
    public float madilength;
    [Header("Backjapyo")]
    public int up;
    public int down;
    [Space(20)]
    public GameObject endmadi;
    public int page;
    public string curmadi = "0";
    bool chart_;
    public bool chart; //마디 범위 내에 들어와 있습니다.
    public bool is_smallmadi;
    [Space(20)]
    public TextMeshProUGUI scrollpowert;
    public int scrollpower;
    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        madiset();
        projectname = "New Project";
        scrollpower = 1;
        scrollpowert.text = scrollpower.ToString();
    }

    void madiset()
    {
        for(int i = 0; i < charts.childCount; i++)
        {
            if(charts.GetChild(i).name != "0" && charts.GetChild(i).name != "maditest")
            {
                Destroy(charts.GetChild(i).gameObject);
            }
        }
        //init
        madilength = 0;
        total.transform.SetParent(canvas.transform);
        total.GetComponent<RectTransform>().anchoredPosition = new Vector2(-708.0013f, 645.9995f);
        start.GetComponent<RectTransform>().anchoredPosition = new Vector2(-251, 0);
        End.GetComponent<RectTransform>().anchoredPosition = new Vector2(253.3f, 0);
        Middle.GetComponent<RectTransform>().anchoredPosition = new Vector2(1.100006f, 0.5999985f);
        //init
        madi = bpm / up * (sec / 60);
        if (starttime != 0)
        {
            
            length = (float)(starttime / Maketile.instance.makenote.madi_sec) * 505; //total length of startmadi  *startime : sec = length : madilength*
            Middle.GetComponent<RectTransform>().sizeDelta = new Vector2(length, Middle.GetComponent<RectTransform>().sizeDelta.y); //midddle madi
            total.GetComponent<BoxCollider2D>().size = new Vector2(length, total.GetComponent<RectTransform>().sizeDelta.y); //collider

            var startpos = start.GetComponent<RectTransform>().anchoredPosition;
            var endpos = End.GetComponent<RectTransform>().anchoredPosition;
            if (starttime < Maketile.instance.makenote.madi_sec)
            {
                start.GetComponent<RectTransform>().anchoredPosition = new Vector2(startpos.x + ((515 - length) / 2), startpos.y);
                End.GetComponent<RectTransform>().anchoredPosition = new Vector2(endpos.x - ((519 - length) / 2), endpos.y);
            }
            else
            {
                start.GetComponent<RectTransform>().anchoredPosition = new Vector2(startpos.x - ((length - 515) / 2), startpos.y);
                End.GetComponent<RectTransform>().anchoredPosition = new Vector2(endpos.x + ((length - 519) / 2), endpos.y);
            }

            total.GetComponent<RectTransform>().anchoredPosition = new Vector2(-575 + ((length - 519) / 2), -450);
            total.transform.SetParent(charts.transform);
        }
        else if(starttime == 0)
        {
            length = 0;
            total.transform.SetParent(canvas.transform);
            total.GetComponent<RectTransform>().anchoredPosition = new Vector2(-708.0013f, 645.9995f);
        }
        for (int i = 0; i <= Mathf.Ceil((float)madi); i++) //make madi
        {
            if (i == Mathf.Ceil((float)madi)) //endmadi
            {
                var b = Instantiate(endmadi, canvas);
                if (starttime == 0)
                {
                    b.GetComponent<RectTransform>().anchoredPosition = new Vector2((504 * i) - 575, -450);
                }
                else
                {
                    b.GetComponent<RectTransform>().anchoredPosition = new Vector2((504 * i) - 575 + (length - 16), -450);
                }
                b.transform.SetParent(charts);
                b.name = "End";
                madilength += 22.4f;
            }
            else
            {
                var a = Instantiate(prefab, canvas);
                if (starttime == 0)
                {
                    a.GetComponent<RectTransform>().anchoredPosition = new Vector2((504 * i) - 575, -450);
                }
                else
                {
                    a.GetComponent<RectTransform>().anchoredPosition = new Vector2((504 * i) - 575 + (length - 16), -450);
                }
                a.transform.SetParent(charts);
                a.name = (i + 1).ToString();
                madilength += 22;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        noteidx.text = Maketile.instance.makenote.notedata.Length.ToString();
        boxidx.text = Maketile.instance.boxpos.Length.ToString();
        var mospos = Camera.main.ScreenToWorldPoint(Input.mousePosition) + new Vector3(0, 0, 9);
        var a = Physics2D.Raycast(mospos, Vector3.forward, 2, LayerMask.GetMask("Chart"));
        var b = Physics2D.Raycast(mospos, Vector3.forward, 2, LayerMask.GetMask("Charts"));
        var c = Physics2D.Raycast(mospos, Vector3.forward, 2, LayerMask.GetMask("Smallmadi"));
        if (b)
        {
            curmadi = b.collider.name;
        }
        if (a)
        {
            chart = true;
            if(c)
            {
                if (Maketile.instance.curpointer.GetComponent<Image>())
                {
                    Maketile.instance.curpointer.GetComponent<Image>().enabled = false;
                }
                else if (Maketile.instance.curpointer.GetComponent<SpriteRenderer>())
                {
                    Maketile.instance.curpointer.GetComponent<SpriteRenderer>().enabled = false;
                }
                is_smallmadi = true;
            }
            else if (Makenote.chartmode && Mouseevent.nopointer == false && !Audio.playing)
            {
                if (Maketile.instance.curpointer.GetComponent<Image>())
                {
                    Maketile.instance.curpointer.GetComponent<Image>().enabled = true;
                }
                else if (Maketile.instance.curpointer.GetComponent<SpriteRenderer>())
                {
                    Maketile.instance.curpointer.GetComponent<SpriteRenderer>().enabled = true;
                }
                is_smallmadi = false;

            }
        }
        else
        {
            chart = false;
        }
        if (chart && !Audio.playing)
        {
            if (charts.GetComponent<RectTransform>().anchoredPosition.y > -19)
            {
                charts.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, -16);
                audio_.index = 3;
                page = 0;
            }
            if (Input.mouseScrollDelta.y > 0 && charts.GetComponent<RectTransform>().anchoredPosition.y < -16) //back
            {
                var pos = charts.GetComponent<RectTransform>().anchoredPosition;
                if(charts.GetComponent<RectTransform>().anchoredPosition.y > -19)
                {
                    charts.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, -16);
                    page = 0;
                }
                else
                {
                    charts.GetComponent<RectTransform>().anchoredPosition = new Vector2(pos.x - 0.004f * scrollpower, pos.y + 3 * scrollpower);
                    audio_.index -= 3 * scrollpower;
                    page--;
                }
            }
            if (Input.mouseScrollDelta.y < 0) //for
            {
                if(Endstamp.isend)
                {
                    return;
                }
                var pos = charts.GetComponent<RectTransform>().anchoredPosition;
                charts.GetComponent<RectTransform>().anchoredPosition = new Vector2(pos.x + 0.004f * scrollpower, pos.y - 3 * scrollpower);
                audio_.index += 3 * scrollpower;
                page++;
            }
        }
    }
    public void powerup()
    {
        if (scrollpower > 8)
        {
            return;
        }
        scrollpower++;
        scrollpowert.text = scrollpower.ToString();
    }
    public void powerdown()
    {
        if (scrollpower <= 1)
        {
            return;
        }
        scrollpower--;
        scrollpowert.text = scrollpower.ToString();
    }
    double temp_b;
    float temp_starttime;
    double temp_sec;
    int temp_up;
    int temp_down;
    public void uiset()
    {
        musicname.text = audio_.audiosourse.clip.ToString();
        bpm_ui.text = bpm.ToString();
        starttime_ui.text = starttime.ToString();
        length_ui.text = sec.ToString();
        bakjapyoup_ui.text = up.ToString();
        name_ui.text = projectname.ToString();
        temp_b = bpm;
        temp_starttime = starttime;
        temp_sec = sec;
        temp_up = up;
        temp_down = down;
        creator_ui.text = editordata.creator;
    }

    public void uitodata()
    {        
        bpm = Convert.ToDouble(bpm_ui.text);
        starttime = (float)Convert.ToDouble(starttime_ui.text);
        sec = Convert.ToInt64(length_ui.text);
        up = Convert.ToInt32(bakjapyoup_ui.text);
        projectname = name_ui.text;
        Maketile.instance.note.transform.SetParent(canvas);
        Maketile.instance.curpointer.transform.SetParent(canvas);
        Maketile.instance.fakepointer.transform.SetParent(canvas);
        Maketile.instance.bakjapyoset();
        Array.Resize(ref note.notedata, 0);
        Array.Resize(ref note.notesobj, 0);
        Array.Resize(ref note.notepos, 0);
        Array.Resize(ref note.notegroup, 0);
        Array.Resize(ref note.notetype, 0);
        madiset();
    }

    public void check()
    {
        int result;
        if(!int.TryParse(length_ui.text, out result) || !int.TryParse(bakjapyoup_ui.text, out result))
        {
            err.SetActive(true);
            return;
        }
        uitodata();
    }

    public void Saveinfo()
    {
        editordata.starttime = starttime;
        editordata.length = length;
        editordata.projectname = projectname;
        editordata.bpm = bpm;
        editordata.sec = sec;
        editordata.up = up;
        editordata.down = down;
        editordata.music = audio_.audiosourse.clip;
        editordata.creator = creator_ui.text; 
    }
    public void Loadinfo()
    {
        Maketile.instance.makenote.madi_sec = sec / madi;
        starttime = editordata.starttime;
        length = editordata.length;
        projectname = editordata.projectname;   
        bpm = editordata.bpm;
        sec = editordata.sec;
        up = editordata.up;
        down = editordata.down;
        settings.downsel(down);
        audio_.audiosourse.clip = editordata.music;
        musicname.text = editordata.music.name;
        creator_ui.text = editordata.creator;

        uiset();
        madisetforload();
    }

    void madisetforload()
    {
        madilength = 0;
        Maketile.instance.curpointer.transform.SetParent(null);
        Maketile.instance.fakepointer.transform.SetParent(null);
        Maketile.instance.tile.transform.SetParent(null);
        Maketile.instance.note.transform.SetParent(null);
        for (int i = 0; i < charts.childCount; i++)
        {
            if (charts.GetChild(i).name != "0" && charts.GetChild(i).name != "maditest")
            {
                Destroy(charts.GetChild(i).gameObject);
            }
        }
        //init
        total.transform.SetParent(canvas.transform);
        total.GetComponent<RectTransform>().anchoredPosition = new Vector2(-708.0013f, 645.9995f);
        start.GetComponent<RectTransform>().anchoredPosition = new Vector2(-251, 0);
        End.GetComponent<RectTransform>().anchoredPosition = new Vector2(253.3f, 0);
        Middle.GetComponent<RectTransform>().anchoredPosition = new Vector2(1.100006f, 0.5999985f);
        //init
        madi = bpm / up * (sec / 60);
        if (starttime != 0)
        {
            length = (float)(starttime / Maketile.instance.makenote.madi_sec) * 505; //total length of madi  *startime : sec = length : madilength*
            Middle.GetComponent<RectTransform>().sizeDelta = new Vector2(length, Middle.GetComponent<RectTransform>().sizeDelta.y); //midddle madi
            total.GetComponent<BoxCollider2D>().size = new Vector2(length, total.GetComponent<RectTransform>().sizeDelta.y); //collider

            var startpos = start.GetComponent<RectTransform>().anchoredPosition;
            var endpos = End.GetComponent<RectTransform>().anchoredPosition;
            if (starttime < Maketile.instance.makenote.madi_sec)
            {
                start.GetComponent<RectTransform>().anchoredPosition = new Vector2(startpos.x + ((515 - length) / 2), startpos.y);
                End.GetComponent<RectTransform>().anchoredPosition = new Vector2(endpos.x - ((519 - length) / 2), endpos.y);
            }
            else
            {
                start.GetComponent<RectTransform>().anchoredPosition = new Vector2(startpos.x - ((length - 515) / 2), startpos.y);
                End.GetComponent<RectTransform>().anchoredPosition = new Vector2(endpos.x + ((length - 519) / 2), endpos.y);
            }

            total.GetComponent<RectTransform>().anchoredPosition = new Vector2(-575 + ((length - 519) / 2), -450);
            total.transform.SetParent(charts.transform);
        }
        else if (starttime == 0)
        {
            length = 0;
            total.transform.SetParent(canvas.transform);
            total.GetComponent<RectTransform>().anchoredPosition = new Vector2(-708.0013f, 645.9995f);
        }
        for (int i = 0; i <= Mathf.Ceil((float)madi); i++) //make madi
        {
            if (i == Mathf.Ceil((float)madi)) //endmadi
            {
                var b = Instantiate(endmadi, canvas);
                if (starttime == 0)
                {
                    b.GetComponent<RectTransform>().anchoredPosition = new Vector2((504 * i) - 575, -450);
                }
                else
                {
                    b.GetComponent<RectTransform>().anchoredPosition = new Vector2((504 * i) - 575 + (length - 16), -450);
                }
                b.transform.SetParent(charts);
                b.name = "End";
                madilength += 22.4f;
            }
            else
            {
                var a = Instantiate(prefab, canvas);
                if (starttime == 0)
                {
                    a.GetComponent<RectTransform>().anchoredPosition = new Vector2((504 * i) - 575, -450);
                }
                else
                {
                    a.GetComponent<RectTransform>().anchoredPosition = new Vector2((504 * i) - 575 + (length - 16), -450);
                }
                a.transform.SetParent(charts);
                a.name = (i + 1).ToString();
                madilength += 22;
            }
        }
        note.noteload();
    }
    public void deletee()
    {
        DataManager.Instance.deleteeditordata(delete_obj.name);
    }
}
