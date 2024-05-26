using System;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using static UnityEditor.PlayerSettings;

public class Makenote : MonoBehaviour
{
    EditorData editordata => DataManager.Instance.editordata;

    public Makemadi makemadi;
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
    public double[] notedata;
    public GameObject[] notesobj;
    public float[] noteduration;

    bool holding;
    double data;
    public static bool hold;
    RaycastHit2D a;
    RaycastHit2D b;

    Vector2 firstmospos;
    GameObject n;
    public float dur;
    int curindex;
    bool tooshort;
    bool toofast;
    bool overwritten;
    [SerializeField]
    bool edit_overwritten;
    public bool movinglongnote;
    Transform mid;
    Transform end;
    Transform over;
    float temp = 0;
    float timetemp = 0;
    float audiodur;
    private void Start()
    {
        chartmode = false;
        previewbox.GetComponent<SpriteRenderer>().enabled = false;
    }
    private void Update()
    {
        if (Audio.playing && Input.GetKeyDown(KeyCode.Space))
        {
            tooshort = false;
            timetemp = Maketile.instance.audio_.time;
            n = Instantiate(note, makemadi.madi.transform);
            n.transform.localPosition = makemadi.madi.transform.InverseTransformPoint(new Vector2(-8.494f, -4.683f));
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
        }
        if (Audio.playing || Settings.popup)
        {
            return;
        }
        a = Physics2D.Raycast(Maketile.instance.mospos, Vector3.forward, 2, LayerMask.GetMask("note"));
        if (Maketile.instance.mode == 0)
        {
            if (Input.GetMouseButtonDown(0) && makemadi.chart && chartmode && !a) //make note
            {
                tooshort = false;
                overwritten = false;
                var pos = Maketile.instance.curpointer.GetComponent<RectTransform>().localPosition;
                data = pos.x / makemadi.madimultiplyer;
                n = Instantiate(note, makemadi.madi.transform);
                n.transform.localPosition = pos;
                firstmospos = pos;
                mid = n.transform.GetChild(1);
                end = n.transform.GetChild(2);
                over = n.transform.GetChild(3);

                Array.Resize(ref notesobj, notesobj.Length + 1);
                notesobj[notesobj.Length - 1] = n;

                Array.Resize(ref notedata, notedata.Length + 1);
                notedata[notedata.Length - 1] = data;

                Array.Resize(ref noteduration, noteduration.Length + 1);
                noteduration[noteduration.Length - 1] = 0;

                curindex = noteduration.Length - 1;
                sort();
                tooshort = true;

            }
            if (Input.GetMouseButton(0)&& makemadi.chart && chartmode && !tooshort && !overwritten) //overwrite and overscreen //not moving
            {
                b = Physics2D.Raycast(end.position, Vector3.forward, 2, LayerMask.GetMask("overwrite"));
                Debug.DrawRay(end.position, Vector3.forward * 2, Color.red);
                var campos = Camera.main.WorldToViewportPoint(end.transform.position);
                var pos = makemadi.madi.GetComponent<RectTransform>().anchoredPosition;
                if (campos.x > 0.9f) //overmadi goooo
                {
                    if (-(38.8f - makemadi.sec * makemadi.madimultiplyer) + (pos.y - makemadi.madimultiplyer) < makemadi.madimultiplyer)
                    {
                        makemadi.madi.GetComponent<RectTransform>().anchoredPosition = new Vector2(pos.x, 38.8f - makemadi.sec * makemadi.madimultiplyer);
                    }
                    else
                    {
                        makemadi.madi.GetComponent<RectTransform>().anchoredPosition = new Vector2(pos.x, pos.y - makemadi.madimultiplyer);
                    }
                }
                if (b || campos.x > 1f) //too overmadi destroy
                {
                    overwritten = true;
                    Destroy(notesobj[curindex]);
                    notesobj = Array.FindAll(notesobj, num => num != notesobj[curindex]).ToArray();
                    notedata = Array.FindAll(notedata, num => num != notedata[curindex]).ToArray();
                    noteduration[curindex] = -1;
                    noteduration = Array.FindAll(noteduration, num => num != noteduration[curindex]).ToArray();
                }

            }
            if (Input.GetMouseButton(0) && Input.GetAxis("Mouse X") != 0 && makemadi.chart && chartmode&& !overwritten) //hold //moving
            {
                if(Input.GetAxis("Mouse X") > 0.2f)
                {
                    toofast = true;
                    return;
                }
                toofast = false;
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
            if (Input.GetMouseButtonUp(0) && makemadi.chart && chartmode && !overwritten) //end
            {
                over.GetComponent<BoxCollider2D>().enabled = true;
                if(!tooshort)
                {
                    if (toofast)
                    {
                        return;
                    }
                    over.GetComponent<BoxCollider2D>().enabled = true;
                    n.GetComponent<BoxCollider2D>().size = new Vector2(MathF.Abs(dur) + 1.5f, 201.7123f);
                    n.GetComponent<BoxCollider2D>().offset = new Vector2(MathF.Abs(dur) / 2 + 0.5f, 15.45131f);
                    over.GetComponent<BoxCollider2D>().size = new Vector2(MathF.Abs(dur) + 3, 201.7123f);
                    over.GetComponent<BoxCollider2D>().offset = new Vector2(MathF.Abs(dur) / 2 + 0.5f, 15.45131f);
                    end.SetParent(makemadi.madi.transform);
                    end.gameObject.GetComponent<Image>().enabled = true;
                    end.SetParent(n.transform);
                    end.SetAsLastSibling();
                    noteduration[curindex] = MathF.Abs(dur) / makemadi.madimultiplyer;
                }
            }
        }
        else if (Maketile.instance.mode == 1)
        {
            if (Input.GetMouseButton(0) && makemadi.chart && chartmode) //erase note
            {
                if (a)
                {
                    var noteindex = Array.IndexOf(notesobj, a.collider.gameObject);
                    notesobj = Array.FindAll(notesobj, num => num != notesobj[noteindex]).ToArray();
                    notedata = Array.FindAll(notedata, num => num != notedata[noteindex]).ToArray();
                    noteduration[noteindex] = -1;
                    noteduration = Array.FindAll(noteduration, num => num != noteduration[noteindex]).ToArray();
                    Destroy(a.collider.gameObject);

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
                    var noteindex = Array.IndexOf(notesobj, a.collider.gameObject);
                    notesobj = Array.FindAll(notesobj, num => num != notesobj[noteindex]).ToArray();
                    notedata = Array.FindAll(notedata, num => num != notedata[noteindex]).ToArray();
                    if (noteduration[noteindex] > 0) { movinglongnote = true;  temp = noteduration[noteindex]; dur = temp; }
                    noteduration[noteindex] = -1;
                    noteduration = Array.FindAll(noteduration, num => num != noteduration[noteindex]).ToArray();
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
                        Vector2 first = measure[0].transform.position;
                        Vector2 end = measure[1].transform.position;
                        var length = new Vector2(Mathf.Abs(end.x - first.x) + 0.1f, 1.5f);
                        if (Physics2D.BoxCast(new Vector2(first.x + length.x / 2, first.y), length, 0, Vector2.down, 0, LayerMask.GetMask("note")) || localfirst.x < 0 || localend.x > 0) //edit too over
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
                    GameObject n = null;
                    if(!movinglongnote)
                    {
                        n = Instantiate(note, makemadi.madi.transform);
                        n.transform.localPosition = pos;
                        n.transform.GetChild(3).GetComponent<BoxCollider2D>().enabled = true;
                    }
                    Array.Resize(ref notesobj, notesobj.Length + 1);
                    Array.Resize(ref noteduration, noteduration.Length + 1);
                    if (movinglongnote) 
                    { 
                        notesobj[notesobj.Length - 1] = Maketile.instance.curpointer; 
                        noteduration[noteduration.Length - 1] = temp; 
                        Maketile.instance.curpointer.GetComponent<BoxCollider2D>().enabled = true; 
                    }
                    else 
                    { 
                        notesobj[notesobj.Length - 1] = n; 
                        noteduration[noteduration.Length - 1] = 0; 
                    }

                    Array.Resize(ref notedata, notedata.Length + 1);
                    notedata[notedata.Length - 1] = data;

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
        Array.Resize(ref notesobj, notedata.Length);
        for (int i = 0; i < notedata.Length; i++)
        {
            var newpos = new Vector2((float)notedata[i] * makemadi.madimultiplyer, 0);
            var n = Instantiate(note, newpos, Quaternion.identity, makemadi.madi.transform);
            if (noteduration[i] > 0)
            {
                var mid = n.transform.GetChild(1);
                var end = n.transform.GetChild(2);
                var over = n.transform.GetChild(3);
                mid.gameObject.SetActive(true);
                over.GetComponent<BoxCollider2D>().enabled = true;
                end.gameObject.GetComponent<Image>().enabled = true;
                mid.GetComponent<RectTransform>().sizeDelta = new Vector2(noteduration[i] * makemadi.madimultiplyer, 103.87f);
                n.GetComponent<BoxCollider2D>().size = new Vector2(noteduration[i] * makemadi.madimultiplyer + 0.6f, 201.7123f);
                n.GetComponent<BoxCollider2D>().offset = new Vector2((noteduration[i] * makemadi.madimultiplyer / 2) + 0.15f, 15.45131f);
                over.GetComponent<BoxCollider2D>().size = new Vector2(noteduration[i] * makemadi.madimultiplyer + 3, 201.7123f);
                over.GetComponent<BoxCollider2D>().offset = new Vector2(noteduration[i] * makemadi.madimultiplyer / 2, 15.45131f);
                end.GetComponent<RectTransform>().localPosition = new Vector2(noteduration[i] * makemadi.madimultiplyer, 0);
                end.SetAsLastSibling();
            }
            n.transform.localPosition = newpos;
            notesobj[i] = n;
        }
    }

    public void Savenotepos()
    {
        Array.Resize(ref editordata.notedata, notedata.Length);
        Array.Resize(ref editordata.noteduration, noteduration.Length);
        for (int i = 0; i < notedata.Length; i++)
        {
            editordata.notedata[i] = notedata[i];
        }
        for (int i = 0; i < noteduration.Length; i++)
        {
            editordata.noteduration[i] = noteduration[i];
        }
    }
    void sort()
    {
        for (int i = 0; i < notedata.Length - 1; i++)   //i = 0 to N - 1
        {
            for (int j = i + 1; j < notedata.Length; j++)  //j = i + 1 to N
            {
                if (notedata[i] > notedata[j])       //부등호 방향: 오름차순(>), 내림차순(<)
                {
                    var temp2 = notesobj[i]; notesobj[i] = notesobj[j]; notesobj[j] = temp2; //SWAP
                    var temp5 = notedata[i]; notedata[i] = notedata[j]; notedata[j] = temp5; //SWAP
                    var temp3 = noteduration[i]; noteduration[i] = noteduration[j]; noteduration[j] = temp3; //SWAP
                    if(j == curindex)
                    {
                        curindex = i;
                    }
                }
            }
        }
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
