
using System;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class Audio : MonoBehaviour
{
    public static bool playing;
    public Makenote note;
    public AudioSource mainmusic;
    public AudioSource hitsound;
    public Sprite play; //stopping
    public Sprite resume; //playing
    public float time;
    public AudioSource notesound;
    public int noteindx;
    [Space(20)]
    public AudioClip[] defaultmusic;
    private void Start()
    {
        DataManager.Instance.LoadSoundData();
        mainmusic.volume = DataManager.Instance.sounddata.bgm;
    }
    public void playmus()
    {
        if (Makenote.hold) { return; }
        if (playing == true) //play -> stop
        {
            gameObject.GetComponent<Image>().sprite = play;
            mainmusic.Pause();
            playing = false;
            note.merge();
        }
        else //stop -> play
        {
            mainmusic.time = Mathf.Abs( Makemadi.instance.madi.transform.InverseTransformPoint(Makemadi.instance.getstarttime.transform.position.x, Makemadi.instance.getstarttime.transform.position.y, 0).x / Makemadi.instance.madimultiplyer);
            gameObject.GetComponent<Image>().sprite = resume;
            mainmusic.Play();
            playing = true;
            for(int i = 0; i < note.notedata.Count; i++)
            {
                if(note.notedata[i].notedata > mainmusic.time)
                {
                    noteindx = i;
                    break;
                }
            }
        }
    }
    private void Update()
    {
        time = mainmusic.time;
        if (playing)
        {
            Makemadi.instance.sliderefresh();
            if (Makemadi.instance.madi.GetComponent<RectTransform>().anchoredPosition.y >= -(Makemadi.instance.sec * Makemadi.instance.madimultiplyer- 77.8f))
            {
                Makemadi.instance.madi.GetComponent<RectTransform>().anchoredPosition = new Vector2(Makemadi.instance.anchorpos, -Makemadi.instance.madimultiplyer * mainmusic.time);
            }
            if(Mathf.Abs( mainmusic.time - Makemadi.instance.sec )< 0.05f) //end
            {
                resetmusic();
            }
            if(note.notedata.Count > 0 && note.notedata[0].notedata - time < 0.05f && note.notedata[0].notedata - time > 0)
            {
                Destroy(note.notedata[0].noteobj);
                note.notedata.RemoveAt(0);
            }
        }
    }
    public void resetmusic()
    {
        mainmusic.Stop();
        gameObject.GetComponent<Image>().sprite = play;
        mainmusic.time = 0;
        playing = false;
        Makemadi.instance.madi.GetComponent<RectTransform>().anchoredPosition = new Vector2(Makemadi.instance.anchorpos, 0);
        noteindx = 0;
        Makemadi.instance.Slider.GetComponent<RectTransform>().anchoredPosition = new Vector2(7, 0);
    }
    int a = 0;
    public void musicchange()
    {
        a++;
        if(a > 4)
        {
            a = 0;
        }
        mainmusic.clip = defaultmusic[a];
        switch (a)
        {
            case 0:
                Makemadi.instance.musicnamee = "Dinosaurs Are Still Alive";
                Makemadi.instance.sec = 211;
                break;
            case 1:
                Makemadi.instance.musicnamee = "Back in my days";
                Makemadi.instance.sec = 135;
                break;
            case 2:
                Makemadi.instance.musicnamee = "Cat and Dog";
                Makemadi.instance.sec = 146;
                break;
            case 3:
                Makemadi.instance.musicnamee = "Little Slime";
                Makemadi.instance.sec = 119;
                break;
            case 4:
                Makemadi.instance.musicnamee = "insert-coint-k-sun";
                Makemadi.instance.sec = 86;
                break;
        }
        Makemadi.instance.musicname.text = mainmusic.clip.ToString();
        Makemadi.instance.sec = Mathf.Round(defaultmusic[a].length);
        Makemadi.instance.uiset();
        Makemadi.instance.check();
        resetmusic();
    }
}
