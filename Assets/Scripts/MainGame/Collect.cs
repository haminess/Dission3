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
        MainGame.instance.collection += CNum;
        MainGame.instance.collections.Add(content);
        MainGame.instance.effect.clip = MainGame.instance.soundMan.effectClip[1];
        MainGame.instance.effect.Play();
        Destroy(gameObject);
    }
}
