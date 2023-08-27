using System;
using System.Linq;
using UnityEngine;

public class Makenote : MonoBehaviour
{
    public Makemadi makemadi;
    public static int mode;
    public static bool chartmode;
    public Sprite[] noteimg;
    public GameObject[] notes;
    [Space(20)]
    public double[] notedata;
    private double data;

    private void Start()
    {
    }
    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && makemadi.chart && chartmode)
        {
            double madi_sec = Makemadi.instance.sec / Makemadi.instance.madi; //how long is one madi
            int result;
            int curmadi = 0;
            if(int.TryParse(Makemadi.instance.curmadi, out result))
            {
                curmadi = result;
            }
            if (mode == 0) //4
            {
                data = (curmadi * madi_sec) + ((madi_sec / (Maketile.instance.divide * Maketile.instance.zabun * 2)) * Maketile.instance.curpos);
            }
            else if (mode == 1) //8
            {
                data = (curmadi * madi_sec) + ((madi_sec / (Maketile.instance.divide * Maketile.instance.palbun * 2)) * Maketile.instance.curpos);
            }
            else if (mode == 2) //16
            {
                data = (curmadi * madi_sec) + ((madi_sec / (Maketile.instance.divide * Maketile.instance.sipukbun * 2)) * Maketile.instance.curpos);
            }
            else if (mode == 3) //32
            {
                data = (curmadi * madi_sec) + ((madi_sec / (Maketile.instance.divide * Maketile.instance.samsipebun * 2)) * Maketile.instance.curpos);
            }
            else if (mode == 4) //1
            {
                data = (curmadi * madi_sec) + ((madi_sec / (Maketile.instance.divide * Maketile.instance.unmun * 2)) * Maketile.instance.curpos);
            }
            else if (mode == 5) //2
            {
                data = (curmadi * madi_sec) + ((madi_sec / (Maketile.instance.divide * Maketile.instance.ebun * 2)) * Maketile.instance.curpos);
            }
            if (Array.Exists(notedata, x => x == data))
            {
                var mospos = Camera.main.ScreenToWorldPoint(Input.mousePosition) + new Vector3(0, 0, 9);
                var a = Physics2D.Raycast(mospos, Vector3.forward, 2, LayerMask.GetMask("Noteonchart"));
                if(a)
                {
                    Destroy(a.collider.gameObject);
                }
                notedata = Array.FindAll(notedata, num => num != data).ToArray();
                return;
            }
            var n = Instantiate(notes[mode], Maketile.instance.curmadiobj.transform);
            n.GetComponent<RectTransform>().localPosition = new Vector2(Maketile.instance.curpointer.transform.localPosition.x, Maketile.instance.curpointer.transform.localPosition.y);

            Array.Resize(ref notedata, notedata.Length + 1);
            notedata[notedata.Length - 1] = data;
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
        tochart();
        mode = 0;
    }
    public void mode1()
    {
        var data = Maketile.instance.divide * Maketile.instance.palbun;
        if(Maketile.instance.fuckkkk == true && Maketile.instance.totalbak < 0.5f)
        {
            return;
        }
        else if ((MathF.Floor(data) != data || data == 0 ) && Maketile.instance.fuckkkk == false)
        {
            return;
        }
        Maketile.instance.note.GetComponent<SpriteRenderer>().sprite = noteimg[1];
        tochart();
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
        tochart();
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
        tochart();
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
        tochart();
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
        tochart();
        mode = 5;
    }

    void tochart()
    {
        Maketile.instance.curpointer = Maketile.instance.note;
        Maketile.instance.tile.GetComponent<SpriteRenderer>().enabled = false;
        Maketile.instance.note.GetComponent<SpriteRenderer>().enabled = true;
        chartmode = true;
    }
    #endregion
}
