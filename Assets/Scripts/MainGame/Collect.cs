using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collect : MonoBehaviour
{
    public int CNum = 1;
    public string Cname;
    public string[] content;
    

    private void OnTriggerEnter2D(Collider2D collision)
    {
        MainMan.instance.collection += CNum;
        MainMan.instance.collections.Add(content);
        MainMan.instance.effect.clip = MainMan.instance.soundMan.effectClip[1];
        MainMan.instance.effect.Play();
        Destroy(gameObject);
    }
}
