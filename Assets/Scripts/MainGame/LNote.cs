using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LNote : MonoBehaviour
{
    public Vector2 pos;
    public float time;
    public bool type;   // 0: short, 1: long

    // Long Note Data
    public Vector2 head;
    public int length;


    // Start is called before the first frame update
    void Start()
    {
        // ���� �� 1.5�� �� ����
        Destroy(gameObject, 1.5f);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
