using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float x;
    public float y;
    public Vector3 CurPos;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // 이동
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            CurPos += Vector3.left;
        }
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            CurPos += Vector3.right;
        }
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            CurPos += Vector3.up;
        }
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            CurPos += Vector3.down;
        }

        transform.localPosition = Vector3.Lerp(transform.localPosition, CurPos, 0.05f);
        if (transform.localPosition == CurPos)
        {
            transform.localPosition = CurPos;
        }


    }
    private void OnTriggerEnter2D(Collider2D collision)
    {

        Debug.Log("충돌");
    }
}
