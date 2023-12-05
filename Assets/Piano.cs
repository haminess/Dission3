using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Piano : MonoBehaviour
{
    public AudioClip[] pianoClip;
    AudioSource ads;

    // Start is called before the first frame update
    void Start()
    {
        ads = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player" && pianoClip.Length > 0)
        {
            ads.clip = pianoClip[Random.Range(0, pianoClip.Length)];
            ads.Play();
        }
    }
}
