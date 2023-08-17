using UnityEngine;

public class Mouseevent : MonoBehaviour
{
    public bool simplepointer;
    public bool anim;
    public bool clickanim;
    public Animator target;
    public static bool nopointer;
    private void OnMouseEnter()
    {
        if (simplepointer)
        {
            if (nopointer)
            {
                Maketile.instance.curpointer.GetComponent<SpriteRenderer>().enabled = false;
                return;
            }
            if (Makemadi.instance.chart && Makenote.chartmode) //editing madi
            {
                return;
            }
            Maketile.instance.curpointer.GetComponent<SpriteRenderer>().enabled = false;

        }
        if (anim)
        {
            target.SetInteger("a", 1);
        }
    }

    private void OnMouseExit()
    {
        if (simplepointer)
        {
            if(nopointer)
            {
                Maketile.instance.curpointer.GetComponent<SpriteRenderer>().enabled = false;
                return;
            }
            if (Makemadi.instance.chart && Makenote.chartmode) //editing madi
            {
                return;
            }
            Maketile.instance.curpointer.GetComponent<SpriteRenderer>().enabled = true;

        }
        if (anim)
        {
            target.SetInteger("a", 0);
        }
    }

    private void OnMouseDown()
    {
        if (clickanim)
        {
            target.SetInteger("c", 1);
        }
    }

    private void OnMouseUp()
    {
        if (clickanim)
        {
            target.SetInteger("c", 0);
        }
    }
}
