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
    public Vector2[] notepos;
    public int[] notegroup;
    public int[] notetype;

    private double data;
    public double madi_sec;
    bool holding;
    public static bool hold;
    private RaycastHit2D b;
    private RaycastHit2D a;
    public bool test;
    private void Start()
    {
        chartmode = false;
        previewbox.GetComponent<SpriteRenderer>().enabled = false;
        for (int i = 0; i < 6; i++)
        {
            noteanim[i].SetBool("boxmode", false);
            noteanim[i].enabled = true;
        }
        makemoderepaintanim();
    }
    private void Update()
    {
        if (makemadi.is_smallmadi || Audio.playing || Settings.popup)
        {
            return;
        }
        var mospos = Camera.main.ScreenToWorldPoint(Input.mousePosition) + new Vector3(0, 0, 9);
        a = Physics2D.Raycast(mospos, Vector3.forward, 2, LayerMask.GetMask("Noteonchart"));
        b = Physics2D.Raycast(mospos, Vector3.forward, 2, LayerMask.GetMask("Charts"));
        if(chartmode && Maketile.instance.mode != 0) //overmouse
        {
            if ((a && test == true && b == false) || (a && b && test == true))
            {
                highlightnote();
                previewbox.GetComponent<SpriteRenderer>().enabled = true;
                if (Maketile.instance.curpointer.GetComponent<Image>())
                {
                    Maketile.instance.curpointer.GetComponent<Image>().enabled = false;
                }
                else if (Maketile.instance.curpointer.GetComponent<SpriteRenderer>())
                {
                    Maketile.instance.curpointer.GetComponent<SpriteRenderer>().enabled = false;
                }
                var c = Array.IndexOf(notesobj, a.collider.gameObject);
                if (Maketile.instance.boxpos.Length > 0)
                {
                    previewbox.GetComponent<SpriteRenderer>().enabled = true;
                }
                else if (Maketile.instance.boxpos.Length <= 0)
                {
                    previewbox.GetComponent<SpriteRenderer>().enabled = false;
                }
                if (Maketile.instance.boxpos.Length > c && !hold)
                {
                    previewbox.transform.position = new Vector2(Maketile.instance.boxpos[c].x - 0.496885f, Maketile.instance.boxpos[c].y + 0.48292f);
                }
                test = false;
            }
            else if ((b || makemadi.chart == false) && test == false && a == false)
            {
                init();
                previewbox.GetComponent<SpriteRenderer>().enabled = false;
                previewbox.transform.position = new Vector2(-11.09f, -6.46f);
                test = true;
            }
        }
        if (Maketile.instance.mode == 0)
        {
            if (Input.GetMouseButton(0) && makemadi.chart && chartmode && Maketile.instance.is_fucking) //make note
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

                Array.Resize(ref notepos, notepos.Length + 1);
                notepos[notepos.Length - 1] = n.transform.localPosition;

                Array.Resize(ref notegroup, notepos.Length + 1);
                notegroup[notegroup.Length - 1] = Convert.ToInt32( n.transform.parent.name);
                notegroup = Array.FindAll(notegroup, num => num != 0).ToArray();

                Array.Resize(ref notetype, notepos.Length + 1);
                notetype[notetype.Length - 1] = mode + 1;
                notetype = Array.FindAll(notetype, num => num != 0).ToArray();

                sort();
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
                    notepos[noteindex] = new Vector2(0,0);
                    notepos = Array.FindAll(notepos, num => num != notepos[noteindex]).ToArray();
                    notegroup[noteindex] = 0;
                    notegroup = Array.FindAll(notegroup, num => num != 0).ToArray();
                    notetype[noteindex] = 0;
                    notetype = Array.FindAll(notetype, num => num != 0).ToArray();
                    Destroy(a.collider.gameObject);

                    sort();
                }
            }
        }
        else if (Maketile.instance.mode == 2) //edit note
        {
            if (Input.GetMouseButtonDown(0) && makemadi.chart && chartmode)
            {
                if (a && !holding) //hold
                {
                    var noteindex = Array.IndexOf(notesobj, a.collider.gameObject);
                    Maketile.instance.curpointer = notesobj[noteindex];
                    notesobj = Array.FindAll(notesobj, num => num != notesobj[noteindex]).ToArray();
                    notedata = Array.FindAll(notedata, num => num != notedata[noteindex]).ToArray();
                    notepos[noteindex] = new Vector2(0, 0);
                    notepos = Array.FindAll(notepos, num => num != notepos[noteindex]).ToArray();
                    notegroup[noteindex] = 0;
                    notegroup = Array.FindAll(notegroup, num => num != 0).ToArray();
                    notetype[noteindex] = 0;
                    notetype = Array.FindAll(notetype, num => num != 0).ToArray();

                    Maketile.instance.curpointer.GetComponent<Image>().enabled = true;
                    switch (Maketile.instance.curpointer.name)
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
                else if (holding && Maketile.instance.is_fucking) //place
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

                    Array.Resize(ref notepos, notepos.Length + 1);
                    notepos[notepos.Length - 1] = n.transform.localPosition;

                    Array.Resize(ref notegroup, notepos.Length + 1);
                    notegroup[notegroup.Length - 1] = Convert.ToInt32(n.transform.parent.name);
                    notegroup = Array.FindAll(notegroup, num => num != 0).ToArray();

                    Array.Resize(ref notetype, notepos.Length + 1);
                    notetype[notetype.Length - 1] = 10;
                    notetype = Array.FindAll(notetype, num => num != 0).ToArray();
                    notetype[notetype.Length - 1] = mode + 1;

                    sort();

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
    public void dehold()
    {
        if (holding && Maketile.instance.is_fucking)
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

            Array.Resize(ref notepos, notepos.Length + 1);
            notepos[notepos.Length - 1] = n.transform.localPosition;

            Array.Resize(ref notegroup, notepos.Length + 1);
            notegroup[notegroup.Length - 1] = Convert.ToInt32(n.transform.parent.name);
            notegroup = Array.FindAll(notegroup, num => num != 0).ToArray();

            Array.Resize(ref notetype, notepos.Length + 1);
            notetype[notetype.Length - 1] = 10;
            notetype = Array.FindAll(notetype, num => num != 0).ToArray();
            notetype[notetype.Length - 1] = mode + 1;

            sort();

            Destroy(Maketile.instance.curpointer);
            Maketile.instance.curpointer = Maketile.instance.note;
            Maketile.instance.curpointer.GetComponent<SpriteRenderer>().enabled = false;
            Mouseevent.nopointer = true;
            hold = false;
            holding = false;
        }
    }
    public void noteload()
    {
        Invoke("noteloadd", 0.2f);
    }
    public void noteloadd()
    {
        for (int i = 0; i < notesobj.Length; i++)
        {
            Destroy(notesobj[i]);
        }
        Array.Resize(ref notesobj, notedata.Length);
        for (int i = 0; i < notedata.Length; i++)
        {
            var n = Instantiate(notes[notetype[i] - 1], notepos[i], Quaternion.identity, GameObject.Find(notegroup[i].ToString()).transform);
            n.transform.localPosition = notepos[i];
            notesobj[i] = n;
            notesobj[i].GetComponent<Noteindex>().index = i;
        }
        madi_sec = editordata.madi_sec;
    }

    public void Savenotepos()
    {
        Array.Resize(ref editordata.notedata, notedata.Length);
        for (int i = 0; i < notedata.Length; i++)
        {
            editordata.notedata[i] = notedata[i];
        }
        Array.Resize(ref editordata.notepos, notepos.Length);
        for (int i = 0; i < notepos.Length; i++)
        {
            editordata.notepos[i] = notepos[i];
        }
        Array.Resize(ref editordata.notegroup, notegroup.Length);
        for (int i = 0; i < notegroup.Length; i++)
        {
            editordata.notegroup[i] = notegroup[i];
        }
        Array.Resize(ref editordata.notetype, notetype.Length);
        for (int i = 0; i < notetype.Length; i++)
        {
            editordata.notetype[i] = notetype[i];
        }
        editordata.madi_sec = madi_sec;
    }

    void sort()
    {
        for (int i = 0; i < notedata.Length - 1; i++)   //i = 0 to N - 1
        {
            for (int j = i + 1; j < notedata.Length; j++)  //j = i + 1 to N
            {
                if (notedata[i] > notedata[j])       //부등호 방향: 오름차순(>), 내림차순(<)
                {
                    var temp1 = notegroup[i]; notegroup[i] = notegroup[j]; notegroup[j] = temp1; //SWAP
                    var temp2 = notesobj[i]; notesobj[i] = notesobj[j]; notesobj[j] = temp2; //SWAP
                    var temp3 = notepos[i]; notepos[i] = notepos[j]; notepos[j] = temp3; //SWAP
                    var temp4 = notetype[i]; notetype[i] = notetype[j]; notetype[j] = temp4; //SWAP
                    var temp5 = notedata[i]; notedata[i] = notedata[j]; notedata[j] = temp5; //SWAP
                }
            }
            for(int a = 0; a < notesobj.Length; a++)
            {
                notesobj[a].GetComponent<Noteindex>().index = a;
            }
        }
    }
    void highlightnote()
    {
        switch (a.collider.gameObject.name)
        {
            case "note1(Clone)":
                a.collider.gameObject.GetComponent<Image>().sprite = noteimg_high[0];
                break;
            case "note2(Clone)":
                a.collider.gameObject.GetComponent<Image>().sprite = noteimg_high[1];
                break;
            case "note3(Clone)":
                a.collider.gameObject.GetComponent<Image>().sprite = noteimg_high[2];
                break;
            case "note4(Clone)":
                a.collider.gameObject.GetComponent<Image>().sprite = noteimg_high[3];
                break;
            case "note5(Clone)":
                a.collider.gameObject.GetComponent<Image>().sprite = noteimg_high[4];
                break;
            case "note6(Clone)":
                a.collider.gameObject.GetComponent<Image>().sprite = noteimg_high[5];
                break;
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
    public void makemodeanimt() //activate all
    {
        for(int i = 0; i < noteanim.Length; i++)
        {
            noteanim[i].SetBool("make", true);
        }
    }

    public void makemodeanimf() //deactivate all
    {
        for (int i = 0; i < noteanim.Length; i++)
        {
            noteanim[i].SetBool("make", false);
            noteanim[i].SetBool("nohighlight", false);
            noteanim[i].Play("Normal");
        }
    }
    public void makemodeanim(int a) //select
    {
        for (int i = 0; i < noteanim.Length; i++)
        {
            if(i == a)
            {
                noteanim[i].SetBool("make", true);
            }
            else
            {
                noteanim[i].SetBool("make", false);

            }
        }
    }
    public void nohighlight(int i)
    {
        for(int a = 0; a < noteanim.Length; a++)
        {
            if(a == i)
            {
                noteanim[a].SetBool("nohighlight", true);
            }
            else
            {
                noteanim[a].SetBool("nohighlight", false);
            }
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
                nohighlight(2);
                makemodeanim(2);
                break;
            case 1:
                noteanim[1].Play("Normal");
                noteanim[0].Play("Normal");
                noteanim[2].Play("Normal");
                noteanim[3].Play("Selected");
                noteanim[4].Play("Normal");
                noteanim[5].Play("Normal");
                nohighlight(3);
                makemodeanim(3);
                break;
            case 2:
                noteanim[2].Play("Normal");
                noteanim[1].Play("Normal");
                noteanim[0].Play("Normal");
                noteanim[3].Play("Normal");
                noteanim[4].Play("Selected");
                noteanim[5].Play("Normal");
                nohighlight(4);
                makemodeanim(4);
                break;
            case 3:
                noteanim[3].Play("Normal");
                noteanim[1].Play("Normal");
                noteanim[2].Play("Normal");
                noteanim[0].Play("Normal");
                noteanim[4].Play("Normal");
                noteanim[5].Play("Selected");
                nohighlight(5);
                makemodeanim(5);
                break;
            case 4:
                noteanim[4].Play("Normal");
                noteanim[1].Play("Normal");
                noteanim[2].Play("Normal");
                noteanim[3].Play("Normal");
                noteanim[0].Play("Selected");
                noteanim[5].Play("Normal");
                nohighlight(0);
                makemodeanim(0);
                break;
            case 5:
                noteanim[5].Play("Normal");
                noteanim[1].Play("Selected");
                noteanim[2].Play("Normal");
                noteanim[3].Play("Normal");
                noteanim[4].Play("Normal");
                noteanim[0].Play("Normal");
                nohighlight(1);
                makemodeanim(1);
                break;
        }
    }
    void Datacal() //madi_sec = one madi length
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
        else if(Maketile.instance.zabun == 0)
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
        else if (Maketile.instance.palbun == 0)
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
        else if (Maketile.instance.sipukbun == 0)
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
        else if (Maketile.instance.samsipebun == 0)
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
        else if (Maketile.instance.unmun == 0)
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
        else if (Maketile.instance.ebun == 0)
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
        if(Maketile.instance.note == null)
        {
            var n =Instantiate(Maketile.instance.emergencynote);
            Maketile.instance.note = n;
            Maketile.instance.curpointer = Maketile.instance.note;
        }
        Maketile.instance.note.GetComponent<SpriteRenderer>().sprite = noteimg[mode];
        Maketile.instance.note.GetComponent<SpriteRenderer>().enabled = true;
        Maketile.instance.curpointer = Maketile.instance.note;
        Maketile.instance.tile.GetComponent<SpriteRenderer>().enabled = false;
        Maketile.instance.note.GetComponent<SpriteRenderer>().enabled = true;
        switch(Maketile.instance.mode)
        {
            case 0:
                Maketile.instance.make();
                makemoderepaintanim();
                break;
            case 1:
                Maketile.instance.erase();
                break;
            case 2:
                Maketile.instance.edit();
                break;
        }
        chartmode = true;
        for(int i = 0; i < 6; i++)
        {
            noteanim[i].SetBool("boxmode", false);
            noteanim[i].enabled = true;
        }
        Maketile.instance.rebutton();
    }
    #endregion
}
