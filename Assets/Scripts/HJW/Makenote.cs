using System;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Makenote : MonoBehaviour
{
    public Makemadi makemadi;
    public static int mode;
    public static bool chartmode; //현재 음표를 수정하고 있습니다.
    public GameObject previewbox;
    public SpriteRenderer transitionicon;
    public Sprite[] transicon;
    public Sprite[] noteimg;
    public Sprite[] noteimg_high;
    public GameObject[] notes;
    public Animator[] noteanim;
    [Space(20)]
    public double[] notedata;
    public GameObject[] notesobj;

    private double data;
    double madi_sec;
    bool holding;
    public static bool hold;
    private void Start()
    {
        madi_sec = makemadi.sec / makemadi.madi; //how long is one madi
    }
    private void Update()
    {
        if(makemadi.is_smallmadi || Audio.playing || Settings.popup)
        {
            return;
        }
        var mospos = Camera.main.ScreenToWorldPoint(Input.mousePosition) + new Vector3(0, 0, 9);
        var a = Physics2D.Raycast(mospos, Vector3.forward, 2, LayerMask.GetMask("Noteonchart"));
        if(a)
        {
            previewbox.GetComponent<SpriteRenderer>().enabled = true;
            if (Maketile.instance.curpointer.GetComponent<Image>())
            {
                Maketile.instance.curpointer.GetComponent<Image>().enabled = false;
            }
            else if (Maketile.instance.curpointer.GetComponent<SpriteRenderer>())
            {
                Maketile.instance.curpointer.GetComponent<SpriteRenderer>().enabled = false;
            }
            var b = Array.IndexOf(notesobj, a.collider.gameObject);
            if(Maketile.instance.boxpos.Length > b && !hold)
            {
                previewbox.transform.position = Maketile.instance.boxpos[b];
            }
;        }
        else
        {
            previewbox.GetComponent<SpriteRenderer>().enabled = false;
            previewbox.transform.position = new Vector2(-11.09f, -6.46f);
        }
        if (Maketile.instance.mode == 0)
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

                Array.Sort(notedata, notesobj);
            }
        }
        else if(Maketile.instance.mode == 1)
        {
            if (Input.GetMouseButtonDown(0) && makemadi.chart && chartmode) //erase note
            {
                if (a)
                {
                    var noteindex = Array.IndexOf(notesobj, a.collider.gameObject);
                    notesobj = Array.FindAll(notesobj, num => num != notesobj[noteindex]).ToArray();
                    notedata = Array.FindAll(notedata, num => num != notedata[noteindex]).ToArray();
                    Destroy(a.collider.gameObject);

                    Array.Sort(notedata, notesobj);
                }
            }
        }
        else if(Maketile.instance.mode == 2) //edit note
        {
            if (Input.GetMouseButtonDown(0) && makemadi.chart && chartmode)
            {
                if(a && !holding) //hold
                {
                    var noteindex = Array.IndexOf(notesobj, a.collider.gameObject);
                    Maketile.instance.curpointer = notesobj[noteindex];
                    notesobj = Array.FindAll(notesobj, num => num != notesobj[noteindex]).ToArray();
                    notedata = Array.FindAll(notedata, num => num != notedata[noteindex]).ToArray();
                    Maketile.instance.curpointer.GetComponent<Image>().enabled = true;
                    switch(Maketile.instance.curpointer.name)
                    {
                        case "note1(Clone)":
                            mode = 0;
                            break;
                        case "note2(Clone)":
                            mode = 1;
                            break;
                        case "note3(Clone)":
                            mode = 2;
                            break;
                        case "note4(Clone)":
                            mode = 3;
                            break;
                        case "note5(Clone)":
                            mode = 4;
                            break;
                        case "note6(Clone)":
                            mode = 6;
                            break;
                    }
                    Mouseevent.nopointer = false;
                    hold = true;
                    holding = true;
                }
                else if(holding && Maketile.instance.is_fucking) //place
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

                    Array.Sort(notedata, notesobj);

                    Destroy(Maketile.instance.curpointer);
                    Maketile.instance.curpointer = Maketile.instance.note;
                    Maketile.instance.curpointer.GetComponent<SpriteRenderer>().enabled = false;
                    Mouseevent.nopointer = true;
                    hold = false;
                    holding = false;
                }
            }
        }
    }

    public void init()
    {
        for (int i = 0; i < notesobj.Length; i++)
        {
            switch (notesobj[i].name)
            {
                case "note1(Clone)":
                    notesobj[i].GetComponent<Image>().sprite = noteimg[0];
                    break;
                case "note2(Clone)":
                    notesobj[i].GetComponent<Image>().sprite = noteimg[1];
                    break;
                case "note3(Clone)":
                    notesobj[i].GetComponent<Image>().sprite = noteimg[2];
                    break;
                case "note4(Clone)":
                    notesobj[i].GetComponent<Image>().sprite = noteimg[3];
                    break;
                case "note5(Clone)":
                    notesobj[i].GetComponent<Image>().sprite = noteimg[4];
                    break;
                case "note6(Clone)":
                    notesobj[i].GetComponent<Image>().sprite = noteimg[5];
                    break;
            }
        }
    }
    public void makemodeanimt() //deactivate all
    {
        for(int i = 0; i < noteanim.Length; i++)
        {
            noteanim[i].SetBool("make", true);
        }
    }

    public void makemodeanimf() //activate all
    {
        for (int i = 0; i < noteanim.Length; i++)
        {
            noteanim[i].SetBool("make", false);
        }
    }
    public void makemoderepaintanim()
    {
        switch(mode)
        {
            case 0:
                noteanim[0].Play("Normal");
                noteanim[1].Play("Normal");
                noteanim[2].Play("Selected");
                noteanim[3].Play("Normal");
                noteanim[4].Play("Normal");
                noteanim[5].Play("Normal");
                break;
            case 1:
                noteanim[1].Play("Normal");
                noteanim[0].Play("Normal");
                noteanim[2].Play("Normal");
                noteanim[3].Play("Selected");
                noteanim[4].Play("Normal");
                noteanim[5].Play("Normal");
                break;
            case 2:
                noteanim[2].Play("Normal");
                noteanim[1].Play("Normal");
                noteanim[0].Play("Normal");
                noteanim[3].Play("Normal");
                noteanim[4].Play("Selected");
                noteanim[5].Play("Normal");
                break;
            case 3:
                noteanim[3].Play("Normal");
                noteanim[1].Play("Normal");
                noteanim[2].Play("Normal");
                noteanim[0].Play("Normal");
                noteanim[4].Play("Normal");
                noteanim[5].Play("Selected");
                break;
            case 4:
                noteanim[4].Play("Normal");
                noteanim[1].Play("Normal");
                noteanim[2].Play("Normal");
                noteanim[3].Play("Normal");
                noteanim[0].Play("Selected");
                noteanim[5].Play("Normal");
                break;
            case 5:
                noteanim[5].Play("Normal");
                noteanim[1].Play("Selected");
                noteanim[2].Play("Normal");
                noteanim[3].Play("Normal");
                noteanim[4].Play("Normal");
                noteanim[0].Play("Normal");
                break;
        }
    }
    void Datacal()
    {
        if (mode == 0) //4
        {
            data = makemadi.starttime + ((Maketile.instance.curmadi - 1) * madi_sec) + ((madi_sec / (Maketile.instance.divide * Maketile.instance.zabun * 2)) * Maketile.instance.curpos);
        }
        else if (mode == 1) //8
        {
            data = makemadi.starttime + ((Maketile.instance.curmadi - 1) * madi_sec) + ((madi_sec / (Maketile.instance.divide * Maketile.instance.palbun * 2)) * Maketile.instance.curpos);
        }
        else if (mode == 2) //16
        {
            data = makemadi.starttime + ((Maketile.instance.curmadi - 1) * madi_sec) + ((madi_sec / (Maketile.instance.divide * Maketile.instance.sipukbun * 2)) * Maketile.instance.curpos);
        }
        else if (mode == 3) //32
        {
            data = makemadi.starttime + ((Maketile.instance.curmadi - 1) * madi_sec) + ((madi_sec / (Maketile.instance.divide * Maketile.instance.samsipebun * 2)) * Maketile.instance.curpos);
        }
        else if (mode == 4) //1
        {
            data = makemadi.starttime + ((Maketile.instance.curmadi - 1) * madi_sec) + ((madi_sec / (Maketile.instance.divide * Maketile.instance.unmun * 2)) * Maketile.instance.curpos);
        }
        else if (mode == 5) //2
        {
            data = makemadi.starttime + ((Maketile.instance.curmadi - 1) * madi_sec) + ((madi_sec / (Maketile.instance.divide * Maketile.instance.ebun * 2)) * Maketile.instance.curpos);
        }
    }
    #region switch

    public void mode0()
    {
        Maketile.instance.make();
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
        makemoderepaintanim();
    }
    public void mode1()
    {
        Maketile.instance.make();
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
        makemoderepaintanim();
    }
    public void mode2()
    {
        Maketile.instance.make();
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
        makemoderepaintanim();

    }
    public void mode3()
    {
        Maketile.instance.make();
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
        makemoderepaintanim();
    }
    public void mode4()
    {
        Maketile.instance.make();
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
        makemoderepaintanim();
    }
    public void mode5()
    {
        Maketile.instance.make();
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
        makemoderepaintanim();
    }
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
        Maketile.instance.note.GetComponent<SpriteRenderer>().enabled = true;
        Maketile.instance.curpointer = Maketile.instance.note;
        Maketile.instance.tile.GetComponent<SpriteRenderer>().enabled = false;
        Maketile.instance.note.GetComponent<SpriteRenderer>().enabled = true;
        Maketile.instance.rebutton();
        Maketile.instance.note.GetComponent<SpriteRenderer>().sprite = noteimg[0];
        mode = 0;
        chartmode = true;
        for(int i = 0; i < 6; i++)
        {
            noteanim[i].SetBool("boxmode", false);
            noteanim[i].enabled = true;
        }
        mode0();
        Maketile.instance.rebutton();
    }
    #endregion
}
