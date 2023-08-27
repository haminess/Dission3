using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class Audio : MonoBehaviour
{
    public static bool playing;
    Vector2 pos;
    public AudioSource audiosourse;
    public Sprite play; //stopping
    public Sprite resume; //playing
    public float time;
    public float time_length;
    public float length;
    float stoppos;
    int index;
    public int[] intlist;
    private void Start()
    {
        pos = Makemadi.instance.charts.GetComponent<RectTransform>().anchoredPosition;
        length = (float)Makemadi.instance.madi * 21f;
        index = 3;
    }
    public void playmus()
    {
        if (playing == true)
        {
            gameObject.GetComponent<Image>().sprite = play;
            stoppos = Makemadi.instance.charts.GetComponent<RectTransform>().anchoredPosition.y;
            audiosourse.Pause();
            playing = false;
        }
        else
        {
            if (audiosourse.time == 0)
            {
                gameObject.GetComponent<Image>().sprite = resume;
                Makemadi.instance.page = 0;
                audiosourse.Play();
            }
            else
            {
                gameObject.GetComponent<Image>().sprite = resume;
                time_length = time_length + (stoppos - Makemadi.instance.charts.GetComponent<RectTransform>().anchoredPosition.y);
                audiosourse.time = Mathf.Abs(time_length / (length / (float)Makemadi.instance.sec));
                repaint();
                audiosourse.UnPause();
            }
            playing = true;
        }
    }
    private void Update()
    {
        if (playing)
        {
            time = audiosourse.time;
            time_length = (length / (float)Makemadi.instance.sec) * audiosourse.time;
            Makemadi.instance.charts.GetComponent<RectTransform>().anchoredPosition = new Vector2(pos.x, pos.y - time_length);
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
            return;
        }
        else if(mostnear == intlist.Length - 1)
        {
            index = (mostnear) * 3;
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
        Array.Resize(ref intlist, 0);
    }
}
