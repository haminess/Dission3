using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestScript : MonoBehaviour
{
    public GameObject obj;
    // Start is called before the first frame update
    void Start()
    {
        Animation anim = obj.GetComponentInChildren<Animation>();
        anim.Play("FadeAnim");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
