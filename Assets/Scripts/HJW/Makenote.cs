using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
[System.Serializable]
public class Notedata
{
    public float notedata;
    public GameObject noteobj;
    public float noteduration;
}
public class Makenote : MonoBehaviour
{
    EditorData editordata => DataManager.Instance.editordata;
    //assigns
    public Makemadi makemadi;
    public Editornotegen notegen;
    public static bool chartmode; //현재 음표를 수정하고 있습니다.
    public GameObject previewbox;
    public Image transitionicon;
    public Sprite[] transicon;
    public GameObject[] measure;
    public Transform canvas;
    public Sprite noteimg;
    public Sprite noteimg_high;
    public GameObject note;
    [Space(20)]
    public List<Notedata> notedata = new List<Notedata>();
    public List<Notedata> notedata_space = new List<Notedata>();
    //datas
    public static bool hold;
    public bool movinglongnote;
    public float dur;
    GameObject n;
    Transform mid;
    Transform end;
    Transform over;
    bool holding;
    int curindexx;
    float temp;
    RaycastHit2D a;
    RaycastHit2D b;
    float data;
    Vector2 firstmospos;
    //fucking exceptions
    bool edit_overwritten;
    bool overwritten;
    public bool tooshort;
    bool toofast;
    bool overlock;
    //audios
    float timetemp;
    float audiodur;

    private void Start()
    {
        chartmode = false;
    }
    private void Update()
    {
        if(Filedataconvey.playmode) { return; }
        if (Audio.playing && Input.GetKeyDown(KeyCode.Space))
        {
            tooshort = false;
            timetemp = Maketile.instance.audio_.time;
            n = Instantiate(note, makemadi.madi.transform);
            n.transform.SetParent(canvas);
            n.transform.localPosition = new Vector2(-837.1682f, -455.0459f);
            n.transform.SetParent(makemadi.madi.transform);
            Notedata tempdata = new Notedata();
            notedata_space.Add(tempdata);
            notedata_space[notedata_space.Count - 1].noteobj = n;
            notedata_space[notedata_space.Count - 1].notedata = timetemp;
            notedata_space[notedata_space.Count - 1].noteduration = 0;

            curindexx = notedata_space.Count - 1;

            mid = n.transform.GetChild(1);
            end = n.transform.GetChild(2);
            over = n.transform.GetChild(3);
        }
        if (Audio.playing && Input.GetKey(KeyCode.Space))
        {
            audiodur = Maketile.instance.audio_.time - timetemp;
            if(audiodur < 2 / makemadi.madimultiplyer) //short note
            {
                tooshort = true;
                return;
            }
            else //longnote
            {
                tooshort=false;
                mid.gameObject.SetActive(true);
                mid.GetComponent<RectTransform>().sizeDelta = new Vector2(audiodur*makemadi.madimultiplyer + 0.6f, 103.87f);
            }
        }
        if (Audio.playing && Input.GetKeyUp(KeyCode.Space)&& !tooshort)
        {
            end.gameObject.GetComponent<RectTransform>().localPosition = new Vector2(audiodur * makemadi.madimultiplyer + 0.6f, 0);
            end.gameObject.GetComponent<Image>().enabled = true;
            end.SetAsLastSibling();
            over.GetComponent<BoxCollider2D>().enabled = true;
            n.GetComponent<BoxCollider2D>().size = new Vector2(audiodur * makemadi.madimultiplyer + 1.5f, 201.7123f);
            n.GetComponent<BoxCollider2D>().offset = new Vector2(audiodur * makemadi.madimultiplyer / 2 + 0.5f, 15.45131f);
            over.GetComponent<BoxCollider2D>().size = new Vector2(audiodur * makemadi.madimultiplyer + 3, 201.7123f);
            over.GetComponent<BoxCollider2D>().offset = new Vector2(audiodur * makemadi.madimultiplyer / 2 + 0.5f, 15.45131f);
            notedata_space[curindexx]. noteduration = MathF.Abs(audiodur);
        }
        if (Audio.playing || Settings.popup)
        {
            return;
        }
        a = Physics2D.Raycast(Maketile.instance.mospos, Vector3.forward, 2, LayerMask.GetMask("note"));
        if (Maketile.instance.mode == 0)
        {
            if(Input.GetMouseButtonDown(0) && makemadi.chart && chartmode && a)
            {
                overlock = true;
            }
            if (Input.GetMouseButtonDown(0) && makemadi.chart && chartmode && !a) //make note
            {
                overlock = false;
                tooshort = true;
                overwritten = false;
                toofast = false;
                var pos = Maketile.instance.curpointer.GetComponent<RectTransform>().localPosition;
                data = pos.x / makemadi.madimultiplyer;
                n = Instantiate(note, makemadi.madi.transform);
                n.transform.localPosition = pos;
                firstmospos = pos;
                mid = n.transform.GetChild(1);
                end = n.transform.GetChild(2);
                over = n.transform.GetChild(3);
                Notedata tempdata = new Notedata();
                notedata.Add(tempdata);
                notedata[notedata.Count - 1].noteobj = n;
                notedata[notedata.Count - 1].notedata = data;
                notedata[notedata.Count - 1].noteduration = 0;
                sort();
                curindexx = notedata.FindIndex(x => x.notedata == data);

            }
            if (Input.GetMouseButton(0)&& makemadi.chart && chartmode && !tooshort && !overwritten && !a && !overlock) //overwrite and overscreen //not moving
            {
                if(end == null) { return; }
                b = Physics2D.Raycast(end.position, Vector3.forward, 2, LayerMask.GetMask("overwrite"));
                Debug.DrawRay(end.position, Vector3.forward * 2, Color.red);
                var campos = Camera.main.WorldToViewportPoint(end.transform.position);
                var pos = makemadi.madi.GetComponent<RectTransform>().anchoredPosition;
                if (campos.x > 0.9f) //overmadi goooo
                {
                    if (makemadi.sec * makemadi.madimultiplyer + (pos.y - makemadi.madimultiplyer) - 78 < makemadi.madimultiplyer)
                    {
                        makemadi.madi.GetComponent<RectTransform>().anchoredPosition = new Vector2(makemadi.anchorpos, -makemadi.sec * makemadi.madimultiplyer + 78);
                    }
                    else
                    {
                        makemadi.madi.GetComponent<RectTransform>().anchoredPosition = new Vector2(makemadi.anchorpos, pos.y - makemadi.madimultiplyer);
                    }
                }
                if (!overlock && b || campos.x > 1f) //too overmadi destroy
                {
                    overwritten = true;
                    Destroy(notedata[curindexx].noteobj);
                    notedata.RemoveAt(curindexx);
                }

            }
            if (Input.GetMouseButton(0) && Input.GetAxis("Mouse X") != 0 && makemadi.chart && chartmode&& !overwritten && !a && !toofast && !overlock) //hold //moving
            {
                if(Input.GetAxis("Mouse X") > 1.5f)
                {
                    toofast = true;
                    Destroy(notedata[curindexx].noteobj);
                    notedata.RemoveAt(curindexx);
                    return;
                }
                var pos = Maketile.instance.curpointer.GetComponent<RectTransform>().localPosition;
                dur = firstmospos.x - pos.x;
                mid.gameObject.SetActive(true);
                mid.GetComponent<RectTransform>().sizeDelta = new Vector2( -dur + 0.6f, 103.87f);
                end.gameObject.GetComponent<RectTransform>().localPosition = new Vector2(-dur + 0.6f, 0);

                if (MathF.Abs(dur) / makemadi.madimultiplyer < 2 || dur > 0) //short note
                {
                    mid.gameObject.SetActive(false);
                    tooshort = true;
                }
                else { tooshort = false; }
            }
            if (Input.GetMouseButtonUp(0) && chartmode && !overwritten) //end
            {
                if (over != null) { over.GetComponent<BoxCollider2D>().enabled = true; }
                if (!tooshort) //longnote, give collider
                {
                    if (toofast)
                    {
                        return;
                    }
                    if (over != null)
                    {
                        over.GetComponent<BoxCollider2D>().enabled = true;
                        n.GetComponent<BoxCollider2D>().size = new Vector2(MathF.Abs(dur) + 1.5f, 201.7123f);
                        n.GetComponent<BoxCollider2D>().offset = new Vector2(MathF.Abs(dur) / 2 + 0.5f, 15.45131f);
                        over.GetComponent<BoxCollider2D>().size = new Vector2(MathF.Abs(dur) + 3, 201.7123f);
                        over.GetComponent<BoxCollider2D>().offset = new Vector2(MathF.Abs(dur) / 2 + 0.5f, 15.45131f);
                        end.SetParent(makemadi.madi.transform);
                        end.gameObject.GetComponent<Image>().enabled = true;
                        end.SetParent(n.transform);
                        end.SetAsLastSibling();
                        if(curindexx < notedata.Count ) { notedata[curindexx].noteduration = MathF.Abs(dur) / makemadi.madimultiplyer; }

                    }
                }
                //here comes a after overlap part (fuck)
                for(int i = 0; i < notedata.Count; i++) //delete weird ones
                {
                    if (notedata[i].noteobj.transform.GetChild(1).gameObject.activeInHierarchy && notedata[i].noteobj.transform.GetChild(2).GetComponent<Image>() && notedata[i].noteobj.transform.GetChild(2).GetComponent<Image>().enabled ==false )
                    {
                        overwritten = true;
                        Destroy(notedata[i].noteobj);
                        notedata.RemoveAt(i);
                    }
                    if (notedata[curindexx].notedata < notedata[i].notedata && notedata[i].notedata < notedata[curindexx].notedata + notedata[curindexx].noteduration)
                    {
                        overwritten = true;
                        Destroy(notedata[curindexx].noteobj);
                        notedata.RemoveAt(curindexx);
                    }
                }

            }
            
        }
        else if (Maketile.instance.mode == 1)
        {
            if (Input.GetMouseButton(0) && makemadi.chart && chartmode) //erase note
            {
                if (a)
                {
                    var noteindex = notedata.FindIndex(x => x.noteobj == a.collider.gameObject);
                    Destroy(a.collider.gameObject);
                    if(noteindex < notedata.Count) { notedata.RemoveAt(noteindex); }

                    sort();
                }
            }
        }
        else if (Maketile.instance.mode == 2) //edit note
        {

            if (Input.GetMouseButtonDown(0) && makemadi.chart && chartmode)
            {
                if (!holding && a && !edit_overwritten) //hold
                {
                    temp = 0;
                    measure[0].transform.SetParent(makemadi.madi.transform);
                    measure[1].transform.SetParent(makemadi.madi.transform);
                    movinglongnote = false;
                    var noteindex = notedata.FindIndex(x => x.noteobj == a.collider.gameObject);
                    if (notedata[noteindex].noteduration > 0) { movinglongnote = true; dur = -notedata[noteindex].noteduration; temp = notedata[noteindex].noteduration;}
                    notedata.RemoveAt(noteindex);
                    Maketile.instance.curpointer.GetComponent<Image>().enabled = true;
                    if(movinglongnote)
                    {
                        Maketile.instance.note.GetComponent<Image>().enabled = false;
                        a.collider.gameObject.GetComponent<BoxCollider2D>().enabled = false;
                        Maketile.instance.curpointer = a.collider.gameObject;
                    }
                    else
                    {
                        Destroy(a.collider.gameObject);
                    }
                    Mouseevent.nopointer = false;
                    hold = true;
                    holding = true;
                }
                else if (holding && !a) //place
                {
                    if(movinglongnote) //overlap_edit
                    {
                        measure[0].transform.SetParent(makemadi.madi.transform);
                        measure[1].transform.SetParent(makemadi.madi.transform);
                        var ln = Maketile.instance.curpointer;
                        measure[0].transform.localPosition = ln.transform.localPosition + ln.transform.GetChild(0).localPosition;
                        measure[1].transform.localPosition = ln.transform.localPosition + ln.transform.GetChild(3).localPosition;
                        var localfirst = measure[0].transform.localPosition;
                        var localend = measure[1].GetComponent<RectTransform>().anchoredPosition;
                        measure[0].transform.SetParent(canvas);
                        measure[1].transform.SetParent(canvas);
                        Vector2 m_first = measure[0].transform.position;
                        Vector2 m_end = measure[1].transform.position;
                        var length = new Vector2(Mathf.Abs(m_end.x - m_first.x) + 0.1f, 1.5f);
                        if (Physics2D.BoxCast(new Vector2(m_first.x + length.x / 2, m_first.y), length, 0, Vector2.down, 0, LayerMask.GetMask("note")) || localfirst.x < 0 || localend.x > 0) //edit too over
                        {
                            edit_overwritten = true;
                            return;
                        }
                        else
                        {
                            edit_overwritten = false;
                        }
                    }
                    var pos = Maketile.instance.curpointer.GetComponent<RectTransform>().localPosition;
                    data = pos.x / makemadi.madimultiplyer;
                    GameObject n2 = null;
                    Notedata tempdata = new Notedata();
                    notedata.Add(tempdata);
                    if(!movinglongnote) //shortnote
                    {
                        n2 = Instantiate(note, makemadi.madi.transform);
                        n2.layer = 7;
                        n2.transform.localPosition = pos;
                        n2.transform.GetChild(3).GetComponent<BoxCollider2D>().enabled = true;
                        notedata[notedata.Count - 1].noteobj = n2;
                        notedata[notedata.Count - 1].noteduration = 0;
                    }
                    if (movinglongnote) 
                    {
                        notedata[notedata.Count - 1].noteobj = Maketile.instance.curpointer;
                        notedata[notedata.Count - 1].noteduration = temp;
                        Maketile.instance.curpointer.GetComponent<BoxCollider2D>().enabled = true; 
                    }

                    notedata[notedata.Count - 1].notedata = data;

                    sort();

                    Maketile.instance.curpointer = Maketile.instance.note;
                    Maketile.instance.curpointer.GetComponent<Image>().enabled = false;
                    Mouseevent.nopointer = true;
                    hold = false;
                    holding = false;
                }
            }
        }
    }
    public void noteload()
    {
        for (int i = 0; i < notedata.Count; i++)
        {
            var newpos = new Vector2((float)notedata[i].notedata * makemadi.madimultiplyer, 0);
            var n = Instantiate(note, newpos, Quaternion.identity, makemadi.madi.transform);
            if (notedata[i].noteduration > 0)
            {
                var mid = n.transform.GetChild(1);
                var end = n.transform.GetChild(2);
                var over = n.transform.GetChild(3);
                mid.gameObject.SetActive(true);
                over.GetComponent<BoxCollider2D>().enabled = true;
                end.gameObject.GetComponent<Image>().enabled = true;
                mid.GetComponent<RectTransform>().sizeDelta = new Vector2(notedata[i].noteduration * makemadi.madimultiplyer, 103.87f);
                n.GetComponent<BoxCollider2D>().size = new Vector2(notedata[i].noteduration * makemadi.madimultiplyer + 0.6f, 201.7123f);
                n.GetComponent<BoxCollider2D>().offset = new Vector2((notedata[i].noteduration * makemadi.madimultiplyer / 2) + 0.15f, 15.45131f);
                over.GetComponent<BoxCollider2D>().size = new Vector2(notedata[i].noteduration * makemadi.madimultiplyer + 3, 201.7123f);
                over.GetComponent<BoxCollider2D>().offset = new Vector2(notedata[i].noteduration * makemadi.madimultiplyer / 2, 15.45131f);
                end.GetComponent<RectTransform>().localPosition = new Vector2(notedata[i].noteduration * makemadi.madimultiplyer, 0);
                end.SetAsLastSibling();
            }
            n.transform.localPosition = newpos;
            notedata[i].noteobj = n;
            n.name = i.ToString();
        }
    }

    public void Savenotepos()
    {
        editordata.notedata.Clear();
        editordata.noteduration.Clear();
        for (int i = 0; i < notedata.Count; i++)
        {
            editordata.notedata.Add(notedata[i].notedata);
            editordata.noteduration.Add(notedata[i].noteduration);
        }
    }
    void sort()
    {
        for (int i = 0; i < notedata.Count - 1; i++)   //i = 0 to N - 1    i > j
        {
            for (int j = i + 1; j < notedata.Count; j++)  //j = i + 1 to N
            {
                if (notedata[i].notedata > notedata[j].notedata)       //부등호 방향: 오름차순(>), 내림차순(<)
                {
                    var temp2 = notedata[i]; notedata[i] = notedata[j]; notedata[j] = temp2; //SWAP
                }
            }
        }
        for(int i = 0;i < notedata.Count;i++)
        {
            notedata[i].noteobj.name = i.ToString();
        }
    }
    public void merge()
    {
        over = null;
        notedata.AddRange(notedata_space);
        notedata_space.Clear();
        sort();
    }

    public void showpreviewbox(int i)
    {
        if (Filedataconvey.playmode) { return; }
        if(i < Maketile.instance.boxdata.Length)
        {
            previewbox.GetComponent<SpriteRenderer>().enabled = true;
            previewbox.transform.localPosition = Maketile.instance.gameObject.transform.GetChild(i+1).localPosition;

        }
        else { previewbox.GetComponent<SpriteRenderer>().enabled = false; }
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        var length = new Vector2(Mathf.Abs(measure[1].transform.position.x - measure[0].transform.position.x) + 0.1f, 1.5f);
        Gizmos.DrawWireCube(new Vector2( measure[0].transform.position.x + length.x / 2, measure[0].transform.position.y), length);
    }
    #region switch
    public void transition()
    {
        Mouseevent.nopointer = false;
        if(chartmode)
        {
            transitionicon.sprite = transicon[0];
            Maketile.instance.exitchart();
        }
        else if(chartmode == false)
        {
            transitionicon.sprite = transicon[1];
            tochart();
        }
    }
    public void tochart()
    {
        Maketile.instance.curpointer.transform.position = new Vector3 (-11.11f, -5.3f);
        Maketile.instance.curpointer = Maketile.instance.note;
        switch(Maketile.instance.mode)
        {
            case 0:
                Maketile.instance.make();
                break;
            case 1:
                Maketile.instance.erase();
                break;
            case 2:
                Maketile.instance.edit();
                break;
        }
        chartmode = true;
        Maketile.instance.rebutton();
    }
    #endregion
}
