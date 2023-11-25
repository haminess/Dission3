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
        if (collision.gameObject.layer == 14 && Audio.playing)
        {
            audio_.Play();
        }
    }
}
