using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collect : MonoBehaviour
{
    public int CNum = 1;
    public string Cname;
    public string[] content;

    //클래스 불러오기

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        MainGame.instance.collection += CNum;
        MainGame.instance.collections.Add(content);
        Destroy(gameObject);
    }
}
