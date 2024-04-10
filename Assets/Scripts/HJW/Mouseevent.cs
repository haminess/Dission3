using UnityEngine;
using UnityEngine.UI;

public class Mouseevent : MonoBehaviour
{
    public Makenote note;
    public bool simplepointer;
    public static bool nopointer;
    public bool hidingpointer;
    private void Start()
    {
        note = GameObject.Find("note").GetComponent<Makenote>();
    }
    private void Update()
    {
       var a = Physics2D.Raycast(Maketile.instance.mospos, Vector3.forward, 2, LayerMask.GetMask("UI"));
        if(a)
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
                    hidingpointer = true;
                    return;
                }
                if (Maketile.instance.curpointer.GetComponent<Image>())
                {
                    Maketile.instance.curpointer.GetComponent<Image>().enabled = false;
                    hidingpointer = true;
                }
                else if (Maketile.instance.curpointer.GetComponent<SpriteRenderer>())
                {
                    Maketile.instance.curpointer.GetComponent<SpriteRenderer>().enabled = false;
                    hidingpointer = true;
                }

            }
        }
        else
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
                        hidingpointer = true;
                    }
                    else if (Maketile.instance.curpointer.GetComponent<SpriteRenderer>())
                    {
                        Maketile.instance.curpointer.GetComponent<SpriteRenderer>().enabled = false;
                        hidingpointer = true;
                    }
                    return;
                }
                if (Maketile.instance.curpointer.GetComponent<Image>())
                {
                    Maketile.instance.curpointer.GetComponent<Image>().enabled = true;
                    hidingpointer = false;
                }
                else if (Maketile.instance.curpointer.GetComponent<SpriteRenderer>())
                {
                    Maketile.instance.curpointer.GetComponent<SpriteRenderer>().enabled = true;
                    hidingpointer = false;
                }
            }
        }
    }
}
