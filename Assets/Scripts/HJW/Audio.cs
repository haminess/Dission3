using System;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Audio : MonoBehaviour
{
    public static bool playing;
    public Makenote note;
    Vector2 pos;
    public AudioSource audiosourse;
    public Sprite play; //stopping
    public Sprite resume; //playing
    public float time;
    public float time_length;
    public float length;
    float stoppos;
    public int index;
    public int[] intlist;
    private void Start()
    {
        pos = Makemadi.instance.charts.GetComponent<RectTransform>().anchoredPosition;
        length = (float)Makemadi.instance.madi * 21f;
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
        }
        else
        {
            if (audiosourse.time == 0) //first play
            {
                Maketile.instance.curpointer.GetComponent<SpriteRenderer>().enabled = false;
                gameObject.GetComponent<Image>().sprite = resume;
                Makemadi.instance.page = 0;
                repaint();
                audiosourse.Play();
                playing = true;
            }
            else //resume
            {
                Maketile.instance.curpointer.GetComponent<SpriteRenderer>().enabled = false;
                gameObject.GetComponent<Image>().sprite = resume;
                time_length = time_length + (stoppos - Makemadi.instance.charts.GetComponent<RectTransform>().anchoredPosition.y);
                audiosourse.time = Mathf.Abs(time_length / (length / (float)Makemadi.instance.sec));
                audiosourse.UnPause();
                playing = true;
                repaint();
            }
        }
    }
    private void Update()
    {
        if(Endstamp.isend)
        {
            audiosourse.Stop();
            gameObject.GetComponent<Image>().sprite = play;
            Makemadi.instance.page = 0;
            audiosourse.time = 0;
            time_length = 0;
            index = 0;
            playing = false;
            Makemadi.instance.charts.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, pos.y - time_length);
        }
        if (playing)
        {
            time = audiosourse.time;
            time_length = (length / (float)Makemadi.instance.sec) * audiosourse.time;
            Makemadi.instance.charts.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, pos.y - time_length);
            if((int)time_length == index)
            {
                Makemadi.instance.page++;
                index += 3;
            }
        }
    }
    void repaint()
    {
        for (int i = 0; i < length; i +=3)
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
        if(index > length)
        {
            repaint();
        }
    }
}
