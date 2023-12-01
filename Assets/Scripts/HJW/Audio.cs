using System;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Audio : MonoBehaviour
{
    public static bool playing;
    public NoteGeneratorforeditor notegen;
    public Makenote note;
    Vector2 pos;
    Vector2 testpos;
    public AudioSource audiosourse;
    public Sprite play; //stopping
    public Sprite resume; //playing
    public float time;
    public float time_length;
    public float offset;
    float stoppos;
    public int index;
    public int[] intlist;
    [Space(20)]
    public GameObject testmadi;
    public static bool testing;
    public GameObject testingpopup;
    public AudioClip[] defaultmusic;
    private void Start()
    {
        pos = Makemadi.instance.charts.GetComponent<RectTransform>().anchoredPosition;
        testpos = testmadi.GetComponent<RectTransform>().anchoredPosition;
        index = 3;
    }
    public void playmus()
    {
        if (playing == true) //stop
        {
            Maketile.instance.curpointer.GetComponent<SpriteRenderer>().enabled = true;
            gameObject.GetComponent<Image>().sprite = play;
            stoppos = Makemadi.instance.charts.GetComponent<RectTransform>().anchoredPosition.y;
            audiosourse.Pause();
            repaint();
            playing = false;
            Maketile.instance.showtile();
            var n = GameObject.FindGameObjectsWithTag("previewnote");
            for(int i = 0; i < n.Length; i++)
            {
                Destroy(n[i]);
            }

        }
        else
        {
            Maketile.instance.curpointer.GetComponent<SpriteRenderer>().enabled = false;
            gameObject.GetComponent<Image>().sprite = resume;
            time_length = time_length + (stoppos - Makemadi.instance.charts.GetComponent<RectTransform>().anchoredPosition.y);
            if(index == 3)
            {
                audiosourse.time = 0;
            }
            else
            {
                audiosourse.time = (float)((time_length / Makemadi.instance.madilength) * Makemadi.instance.sec);
            }
            audiosourse.Play();
            playing = true;
            repaint();
            Maketile.instance.hidetile();
            notegen.refresh();
        }
    }
    private void Update()
    {
        if(testing)
        {
            if(testmadi.GetComponent<RectTransform>().anchoredPosition.y < -50.1f)
            {
                Maketile.instance.makenote.madi_sec = audiosourse.time;
                audiosourse.Stop();
                audiosourse.volume = 100;
                time = 0;
                time_length = 0;
                testmadi.GetComponent<RectTransform>().anchoredPosition = testpos;
                testmadi.SetActive(false);
                testing = false;
                testingpopup.SetActive(false);
            }

            time = audiosourse.time;
            time_length = (time / (float)Makemadi.instance.sec) * Makemadi.instance.madilength;
            testmadi.GetComponent<RectTransform>().anchoredPosition = new Vector2(time_length * 0.0013f, testpos.y - time_length * offset);

        }
        if(Endstamp.isend && playing || Settings.popup && playing || time >= Makemadi.instance.sec)
        {
            print("reset");
            resetmusic();
        }
        if (playing)
        {
            time = audiosourse.time;
            time_length = (time / (float)Makemadi.instance.sec) * Makemadi.instance.madilength; //madilength = total madilength(X)
            Makemadi.instance.charts.GetComponent<RectTransform>().anchoredPosition = new Vector2(time_length * 0.0013f, pos.y - time_length * offset);
            if((int)time_length * offset == index)
            {
                Makemadi.instance.page++;
                index += 3;
            }
        }
    }

    public void maditest()
    {
        resetmusic();
        audiosourse.volume = 0;
        audiosourse.Play();
        testmadi.SetActive(true);
        testingpopup.SetActive(true);
        testing = true;
    }

    public void resetmusic()
    {
        audiosourse.Stop();
        gameObject.GetComponent<Image>().sprite = play;
        Makemadi.instance.page = 0;
        audiosourse.time = 0;
        time_length = 0;
        index = 3;
        playing = false;
        Makemadi.instance.charts.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, -16.157f);
    }
    void repaint()
    {
        for (int i = 0; i < Makemadi.instance.madilength; i +=3)
        {
            var cha = (int)time_length - i;
            Array.Resize(ref intlist, intlist.Length + 1);
            intlist[i / 3] = Mathf.Abs(cha);
        }
        int minValue = intlist.Min();
        var mostnear = Array.IndexOf(intlist, minValue);
        if(mostnear == 0)
        {
            index = 3;
            Makemadi.instance.page = 0;
            return;
        }
        else if(mostnear == intlist.Length - 1)
        {
            index = (intlist.Length - 1) * 3;
            Makemadi.instance.page = intlist.Length - 1;
            return;
        }
        var next = MathF.Abs( (int)time_length - (3 * (mostnear + 1)));
        var back = MathF.Abs( (int)time_length - (3 * (mostnear - 1)));
        if(next < back)
        {
           index = (mostnear + 1) * 3;
        }
        else if(back < next)
        {
            index = (mostnear) * 3;
        }
        Makemadi.instance.page = index / 3;
        Array.Resize(ref intlist, 0);
        if(index > Makemadi.instance.madilength)
        {
            repaint();
        }
    }
    int a = 0;
    public void musicchange()
    {
        if(a > 4)
        {
            a = 0;
        }
        audiosourse.clip = defaultmusic[a];
        Makemadi.instance.musicname.text = audiosourse.clip.ToString();
        Makemadi.instance.sec = Mathf.Round(defaultmusic[a].length);
        switch (a)
        {
            case 0:
                Makemadi.instance.bpm = 120;
                break;
            case 1:
                Makemadi.instance.bpm = 118;
                Makemadi.instance.starttime = 0;
                break;
            case 2:
                Makemadi.instance.bpm = 128;
                Makemadi.instance.starttime = 0;
                break;
            case 3:
                Makemadi.instance.bpm = 150;
                Makemadi.instance.starttime = 0;
                break;
            case 4:
                Makemadi.instance.bpm = 78;
                Makemadi.instance.starttime = 0;
                break;
        }
        Makemadi.instance.uiset();
        Makemadi.instance.check();
        resetmusic();
        a++;
    }
}
