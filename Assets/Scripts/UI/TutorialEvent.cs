using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialEvent : MonoBehaviour
{
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
        if (gameObject.name == "reposition")
        {
            collision.transform.position = Vector3.zero;
            collision.GetComponent<TutorialMove>().CurPos = Vector3.zero;
        }
    }
}
