using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    void Update()
    {
        if(Input.GetKey(KeyCode.LeftArrow))
        {
            this.transform.Translate(-0.5f, 0f, 0f);
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            this.transform.Translate(0.5f, 0f, 0f);
        }
        if (Input.GetKey(KeyCode.UpArrow))
        {
            this.transform.Translate(0f, 0f, -0.5f);
        }
        if (Input.GetKey(KeyCode.DownArrow))
        {
            this.transform.Translate(0f, 0f, 0.5f);
        }
    }
}
