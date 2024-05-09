using System;
using System.Linq;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Makenote : MonoBehaviour
{
    EditorData editordata => DataManager.Instance.editordata;

    public Makemadi makemadi;
    public static bool chartmode; //현재 음표를 수정하고 있습니다.
    public GameObject previewbox;
    public Image transitionicon;
    public Sprite[] transicon;
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
    public RaycastHit2D a;

    Vector2 firstmospos;
    GameObject n;
    float dur;
    int curindex;
    [SerializeField]
    bool tooshort;
    private void Start()
    {
        chartmode = false;
        previewbox.GetComponent<SpriteRenderer>().enabled = false;
    }
    private void Update()
    {
        if (Audio.playing || Settings.popup)
        {
            return;
        }
        a = Physics2D.Raycast(Maketile.instance.mospos, Vector3.forward, 2, LayerMask.GetMask("note"));
        if (Maketile.instance.mode == 0)
        {
            if (Input.GetMouseButtonDown(0) && makemadi.chart && chartmode) //make note
            {
                tooshort = false;
                var pos = Maketile.instance.curpointer.GetComponent<RectTransform>().localPosition;
                data = pos.x / makemadi.madimultiplyer;
                if(Array.Exists(notedata, x => x == data))
                {
                    return;
                }
                n = Instantiate(note, makemadi.madi.transform);
                n.transform.localPosition = pos;
                firstmospos = pos;

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
            if(Input.GetMouseButton(0) && Input.GetAxis("Mouse X") != 0 && makemadi.chart && chartmode)
            {
                var pos = Maketile.instance.curpointer.GetComponent<RectTransform>().localPosition;
                dur = firstmospos.x - pos.x;
                var mid = n.transform.GetChild(1);
                mid.gameObject.SetActive(true);
                mid.GetComponent<RectTransform>().sizeDelta = new Vector2( -dur + 0.6f, 103.87f);
                if(MathF.Abs( dur) < 2 || dur > 0)
                {
                    mid.gameObject.SetActive(false);
                    tooshort = true;
                    n.GetComponent<BoxCollider2D>().size = new Vector2(0.650923f, 201.7123f);
                    n.GetComponent<BoxCollider2D>().offset = new Vector2(0.18f, 15.45131f);
                }
                else { tooshort = false; }
            }
            if (Input.GetMouseButtonUp(0) && makemadi.chart && chartmode && !tooshort)
            {
                n.GetComponent<BoxCollider2D>().size = new Vector2(MathF.Abs(dur) + 1.5f, 201.7123f);
                n.GetComponent<BoxCollider2D>().offset = new Vector2(MathF.Abs(dur) / 2 + 0.5f, 15.45131f);
                var end = n.transform.GetChild(2);
                end.SetParent(makemadi.madi.transform);
                end.gameObject.SetActive(true);
                end.gameObject.GetComponent<RectTransform>().localPosition = new Vector2(firstmospos.x - dur + 0.6f, 0);
                end.SetParent(n.transform);
                end.SetAsLastSibling();
                noteduration[curindex] = MathF.Abs(dur);
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
                if (!holding && a) //hold
                {
                    var noteindex = Array.IndexOf(notesobj, a.collider.gameObject);
                    notesobj = Array.FindAll(notesobj, num => num != notesobj[noteindex]).ToArray();
                    notedata = Array.FindAll(notedata, num => num != notedata[noteindex]).ToArray();
                    noteduration[noteindex] = -1;
                    noteduration = Array.FindAll(noteduration, num => num != noteduration[noteindex]).ToArray();
                    Destroy(a.collider.gameObject);
                    Maketile.instance.curpointer.GetComponent<Image>().enabled = true;
                    Mouseevent.nopointer = false;
                    hold = true;
                    holding = true;
                }
                else if (holding) //place
                {
                    var pos = Maketile.instance.curpointer.GetComponent<RectTransform>().localPosition;
                    data = pos.x / makemadi.madimultiplyer;
                    if (Array.Exists(notedata, x => x == data))
                    {
                        return;
                    }
                    var n = Instantiate(note, makemadi.madi.transform);
                    n.transform.localPosition = pos;
                    n.GetComponent<Button>().interactable = true;

                    Array.Resize(ref notesobj, notesobj.Length + 1);
                    notesobj[notesobj.Length - 1] = n;

                    Array.Resize(ref notedata, notedata.Length + 1);
                    notedata[notedata.Length - 1] = data;

                    Array.Resize(ref noteduration, noteduration.Length + 1);
                    noteduration[noteduration.Length - 1] = 0;

                    sort();

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
    public void buttoninteraction(bool interact)
    {
        for(int i = 0;i < notesobj.Length;i++) 
        {
            notesobj[i].GetComponent<Button>().interactable = interact;
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
                }
            }
        }
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
