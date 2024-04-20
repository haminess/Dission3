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
        MainManager.instance.collection += CNum;
        MainManager.instance.collections.Add(content);
        MainManager.instance.effect.clip = MainManager.instance.soundMan.effectClip[1];
        MainManager.instance.effect.Play();
        Destroy(gameObject);
    }
}
