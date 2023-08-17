using System;
using System.Linq;
using TMPro;
using UnityEngine;

public class Makenote : MonoBehaviour
{
    public Makemadi makemadi;
    public static int mode;
    public static bool chartmode; //현재 음표를 수정하고 있습니다.
    public TextMeshProUGUI transitionbuttontext;
    public Sprite[] noteimg;
    public GameObject[] notes;
    [Space(20)]
    public double[] notedata;
    public GameObject[] notesobj;

    private double data;
    double madi_sec;
    bool holding;
    private void Start()
    {
        madi_sec = makemadi.sec / makemadi.madi; //how long is one madi
    }
    private void Update()
    {
        if(Maketile.instance.mode == 0)
        {
            if (Input.GetMouseButtonDown(0) && makemadi.chart && chartmode && Maketile.instance.is_fucking) //make note
            {
                Datacal();
                if (Array.Exists(notedata, x => x == data))
                {
                    return;
                }
                var n = Instantiate(notes[mode], Maketile.instance.curmadiobj.transform);
                n.GetComponent<RectTransform>().localPosition = new Vector2(Maketile.instance.curpointer.transform.localPosition.x, Maketile.instance.curpointer.transform.localPosition.y);

                Array.Resize(ref notesobj, notesobj.Length + 1);
                notesobj[notesobj.Length - 1] = n;

                Array.Resize(ref notedata, notedata.Length + 1);
                notedata[notedata.Length - 1] = data;
            }
        }
        else if(Maketile.instance.mode == 1)
        {
            if (Input.GetMouseButtonDown(0) && makemadi.chart && chartmode) //erase note
            {
                Datacal();
                if (Array.Exists(notedata, x => x == data) == false)
                {
                    return;
                }
                var noteindex = Array.IndexOf(notedata, data);
                Destroy(notesobj[noteindex]);
                //var mospos = Camera.main.ScreenToWorldPoint(Input.mousePosition) + new Vector3(0, 0, 9);
                //var a = Physics2D.Raycast(mospos, Vector3.forward, 2, LayerMask.GetMask("Noteonchart"));
                //if (a)
                //{
                //Destroy(a.collider.gameObject);
                //}
                notesobj = Array.FindAll(notesobj, num => num != notesobj[noteindex]).ToArray();
                notedata = Array.FindAll(notedata, num => num != data).ToArray();
            }
        }
        else if(Maketile.instance.mode == 2) //edit note
        {
            if (Input.GetMouseButtonDown(0) && makemadi.chart && chartmode)
            {
                Datacal();
                if (Array.Exists(notedata, x => x == data) == false && holding == false)
                {
                    return;
                }
                var noteindex = Array.IndexOf(notedata, data);
                if(holding && Maketile.instance.is_fucking)
                {
                    var n = Instantiate(notes[mode], Maketile.instance.curmadiobj.transform);
                    n.GetComponent<RectTransform>().localPosition = new Vector2(Maketile.instance.curpointer.transform.localPosition.x, Maketile.instance.curpointer.transform.localPosition.y);

                    Array.Resize(ref notesobj, notesobj.Length + 1);
                    notesobj[notesobj.Length - 1] = n;

                    Array.Resize(ref notedata, notedata.Length + 1);
                    notedata[notedata.Length - 1] = data;
                    Destroy(Maketile.instance.curpointer);
                    Maketile.instance.curpointer = Maketile.instance.note;
                    Maketile.instance.curpointer.GetComponent<SpriteRenderer>().enabled = false;
                    Mouseevent.nopointer = true;
                    holding = false;
                }
                else
                {
                    Maketile.instance.curpointer = notesobj[noteindex];
                    notesobj = Array.FindAll(notesobj, num => num != notesobj[noteindex]).ToArray();
                    notedata = Array.FindAll(notedata, num => num != data).ToArray();
                    Maketile.instance.curpointer.GetComponent<SpriteRenderer>().enabled = true;
                    switch(Maketile.instance.curpointer.GetComponent<SpriteRenderer>().sprite.ToString())
                    {
                        case "음표_0":
                            mode = 0;
                            break;
                        case "음표_1":
                            mode = 1;
                            break;
                        case "음표_2":
                            mode = 2;
                            break;
                        case "음표_3":
                            mode = 3;
                            break;
                        case "음표_5":
                            mode = 4;
                            break;
                        case "음표_6":
                            mode = 6;
                            break;
                    }
                    Mouseevent.nopointer = false;
                    holding = true;
                }
            }
        }
    }

    void Datacal()
    {
        if (mode == 0) //4
        {
            data = (Maketile.instance.curmadi * madi_sec) + ((madi_sec / (Maketile.instance.divide * Maketile.instance.zabun * 2)) * Maketile.instance.curpos);
        }
        else if (mode == 1) //8
        {
            data = (Maketile.instance.curmadi * madi_sec) + ((madi_sec / (Maketile.instance.divide * Maketile.instance.palbun * 2)) * Maketile.instance.curpos);
        }
        else if (mode == 2) //16
        {
            data = (Maketile.instance.curmadi * madi_sec) + ((madi_sec / (Maketile.instance.divide * Maketile.instance.sipukbun * 2)) * Maketile.instance.curpos);
        }
        else if (mode == 3) //32
        {
            data = (Maketile.instance.curmadi * madi_sec) + ((madi_sec / (Maketile.instance.divide * Maketile.instance.samsipebun * 2)) * Maketile.instance.curpos);
        }
        else if (mode == 4) //1
        {
            data = (Maketile.instance.curmadi * madi_sec) + ((madi_sec / (Maketile.instance.divide * Maketile.instance.unmun * 2)) * Maketile.instance.curpos);
        }
        else if (mode == 5) //2
        {
            data = (Maketile.instance.curmadi * madi_sec) + ((madi_sec / (Maketile.instance.divide * Maketile.instance.ebun * 2)) * Maketile.instance.curpos);
        }
    }
    #region switch

    public void mode0()
    {
        var data = Maketile.instance.divide * Maketile.instance.zabun;
        if (Maketile.instance.fuckkkk == true && Maketile.instance.totalbak < 1)
        {
            return;
        }
        else if ((MathF.Floor(data) != data || data == 0) && Maketile.instance.fuckkkk == false)
        {
            return;
        }
        Maketile.instance.note.GetComponent<SpriteRenderer>().sprite = noteimg[0];
        mode = 0;
    }
    public void mode1()
    {
        var data = Maketile.instance.divide * Maketile.instance.palbun;
        if (Maketile.instance.fuckkkk == true && Maketile.instance.totalbak < 0.5f)
        {
            return;
        }
        else if ((MathF.Floor(data) != data || data == 0) && Maketile.instance.fuckkkk == false)
        {
            return;
        }
        Maketile.instance.note.GetComponent<SpriteRenderer>().sprite = noteimg[1];
        mode = 1;
    }
    public void mode2()
    {
        var data = Maketile.instance.divide * Maketile.instance.sipukbun;
        if (Maketile.instance.fuckkkk == true && Maketile.instance.totalbak < 0.25f)
        {
            return;
        }
        else if ((MathF.Floor(data) != data || data == 0) && Maketile.instance.fuckkkk == false)
        {
            return;
        }
        Maketile.instance.note.GetComponent<SpriteRenderer>().sprite = noteimg[2];
        mode = 2;

    }
    public void mode3()
    {
        var data = Maketile.instance.divide * Maketile.instance.samsipebun;
        if (Maketile.instance.fuckkkk == true && Maketile.instance.totalbak < 0.125f)
        {
            return;
        }
        else if ((MathF.Floor(data) != data || data == 0) && Maketile.instance.fuckkkk == false)
        {
            return;
        }
        Maketile.instance.note.GetComponent<SpriteRenderer>().sprite = noteimg[3];
        mode = 3;
    }
    public void mode4()
    {
        var data = Maketile.instance.divide * Maketile.instance.unmun;
        if (Maketile.instance.fuckkkk == true && Maketile.instance.totalbak < 4)
        {
            return;
        }
        else if ((MathF.Floor(data) != data || data == 0) && Maketile.instance.fuckkkk == false)
        {
            return;
        }
        Maketile.instance.note.GetComponent<SpriteRenderer>().sprite = noteimg[4];
        mode = 4;
    }
    public void mode5()
    {
        var data = Maketile.instance.divide * Maketile.instance.ebun;
        if (Maketile.instance.fuckkkk == true && Maketile.instance.totalbak < 2)
        {
            return;
        }
        else if ((MathF.Floor(data) != data || data == 0) && Maketile.instance.fuckkkk == false)
        {
            return;
        }
        Maketile.instance.note.GetComponent<SpriteRenderer>().sprite = noteimg[5];
        mode = 5;
    }
    public void transition()
    {
        Mouseevent.nopointer = false;
        if(chartmode)
        {
            transitionbuttontext.text = "Chart";
            Maketile.instance.exitchart();
        }
        else if(chartmode == false)
        {
            transitionbuttontext.text = "Box";
            tochart();
        }
    }
    public void tochart()
    {
        Maketile.instance.note.GetComponent<SpriteRenderer>().enabled = true;
        Maketile.instance.curpointer = Maketile.instance.note;
        Maketile.instance.tile.GetComponent<SpriteRenderer>().enabled = false;
        Maketile.instance.note.GetComponent<SpriteRenderer>().enabled = true;
        Maketile.instance.mode = 0;
        chartmode = true;
    }
    #endregion
}
