using UnityEngine;
using UnityEngine.UI;

public class Mouseevent : MonoBehaviour
{
    public Makenote note;
    public bool simplepointer;
    public bool isnote;
    public Sprite high;
    public Sprite nor;
    public static bool nopointer;
    private void Start()
    {
        note = GameObject.Find("note").GetComponent<Makenote>();
    }

    private void OnMouseOver()
    {
        if (Settings.popup)
        {
            return;
        }
        if (isnote && Makemadi.instance.chart && Makenote.chartmode && Maketile.instance.mode != 0 && Makenote.hold == false)
        {
            gameObject.GetComponent<Image>().sprite = high;
        }
    }
    private void OnMouseEnter()
    {
        if (Settings.popup)
        {
            return;
        }
        if (simplepointer)
        {
            if (nopointer)
            {
                if (Maketile.instance.curpointer.GetComponent<Image>())
                {
                    Maketile.instance.curpointer.GetComponent<Image>().enabled = false;
                }
                else if (Maketile.instance.curpointer.GetComponent<SpriteRenderer>())
                {
                    Maketile.instance.curpointer.GetComponent<SpriteRenderer>().enabled = false;
                }
                return;
            }
            if (Makemadi.instance.chart && Makenote.chartmode) //editing madi
            {
                return;
            }
            if (Maketile.instance.curpointer.GetComponent<Image>())
            {
                Maketile.instance.curpointer.GetComponent<Image>().enabled = false;
            }
            else if (Maketile.instance.curpointer.GetComponent<SpriteRenderer>())
            {
                Maketile.instance.curpointer.GetComponent<SpriteRenderer>().enabled = false;
            }

        }
    }

    private void OnMouseExit()
    {
        if(Settings.popup)
        {
            return;
        }
        if (isnote && Makemadi.instance.chart && Makenote.chartmode && Maketile.instance.mode != 0 && Makenote.hold == false )
        {
            note.init();
            return;
        }
        if (simplepointer)
        {
            if(nopointer)
            {
                if(Maketile.instance.curpointer.GetComponent<Image>())
                {
                    Maketile.instance.curpointer.GetComponent<Image>().enabled = false;
                }
                else if(Maketile.instance.curpointer.GetComponent<SpriteRenderer>())
                {
                    Maketile.instance.curpointer.GetComponent<SpriteRenderer>().enabled = false;
                }
                return;
            }
            if (Makemadi.instance.chart && Makenote.chartmode) //editing madi
            {
                return;
            }
                if(Maketile.instance.curpointer.GetComponent<Image>())
                {
                    Maketile.instance.curpointer.GetComponent<Image>().enabled = true;
                }
                else if(Maketile.instance.curpointer.GetComponent<SpriteRenderer>())
                {
                    Maketile.instance.curpointer.GetComponent<SpriteRenderer>().enabled = true;
                }
        }
    }
}
