using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Startstamp : MonoBehaviour
{
    public AudioSource audio_;
    public NoteGeneratorforeditor notegen;
    public float offset;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.layer == 14 && Audio.playing)
        {
            StartCoroutine(delay(collision));
        }
    }
    IEnumerator delay(Collision2D collision)
    {
        if (collision.gameObject.GetComponent<Noteindex>().verifyed == false)
        {
            Maketile.instance.makenote.notedata[collision.gameObject.GetComponent<Noteindex>().index] = Maketile.instance.audio_.time;
            collision.gameObject.GetComponent<Noteindex>().verifyed = true;
            notegen.refresh();
        }
        yield return new WaitForSeconds(offset);
        audio_.Play();
    }
}
