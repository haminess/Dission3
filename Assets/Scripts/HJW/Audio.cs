
using UnityEngine;
using UnityEngine.UI;

public class Audio : MonoBehaviour
{
    public static bool playing;
    public NoteGeneratorforeditor notegen;
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
        if (playing == true) //play -> stop
        {
            gameObject.GetComponent<Image>().sprite = play;
            mainmusic.Pause();
            playing = false;
            Maketile.instance.showtile();
        }
        else //stop -> play
        {
            mainmusic.time = (Makemadi.instance.madi.GetComponent<RectTransform>().anchoredPosition.y + 39) / -Makemadi.instance.madimultiplyer;
            gameObject.GetComponent<Image>().sprite = resume;
            mainmusic.Play();
            playing = true;
            Maketile.instance.hidetile();
            for(int i = 0; i < note.notedata.Length; i++)
            {
                if(note.notedata[i] > mainmusic.time)
                {
                    noteindx = i;
                    break;
                }
            }
            notegen.refresh();
        }
    }
    private void Update()
    {
        time = mainmusic.time;
        if (playing)
        {
            if (Makemadi.instance.madi.GetComponent<RectTransform>().anchoredPosition.y >= -((Makemadi.instance.sec * Makemadi.instance.madimultiplyer) + 39 - 77.8f))
            {
                Makemadi.instance.madi.GetComponent<RectTransform>().anchoredPosition = new Vector2(3.5f, (-Makemadi.instance.madimultiplyer * mainmusic.time) - 39);
            }
            if(Mathf.Abs( mainmusic.time - Makemadi.instance.sec )< 0.05f)
            {
                resetmusic();
            }
            if(note.notedata.Length > 0 && note.notedata[noteindx] - time < 0.05f && note.notedata[noteindx] - time > 0)
            {
                notesound.Play();
                if(note.notedata.Length - 1 == noteindx)
                {
                    noteindx = 0;
                }
                noteindx++;
            }
        }
    }
    public void resetmusic()
    {
        mainmusic.Stop();
        gameObject.GetComponent<Image>().sprite = play;
        mainmusic.time = 0;
        playing = false;
        Makemadi.instance.madi.GetComponent<RectTransform>().anchoredPosition = new Vector2(3.5f, -39f);
        noteindx = 0;
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

    public void notegenrefresh()
    {
        notegen.refresh();
    }
}
