using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Endstamp : MonoBehaviour
{
    public static bool isend;
    public string a;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        var b = Physics2D.Raycast(gameObject.transform.position, Vector3.forward, 2, LayerMask.GetMask("Charts"));
        if(b)
        {
            a = b.collider.name;
        }
        if(b && b.collider.name == "End")
        {
            isend = true;
        }
        else
        {
            isend = false;
        }
    }
}
