using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Note : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {
        // 생성 후 1.5초 뒤 삭제
        Destroy(gameObject, 1.5f);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
