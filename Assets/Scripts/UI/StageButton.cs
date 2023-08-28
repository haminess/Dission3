using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StageButton : MonoBehaviour
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
        StageManager sm = GameObject.Find("StageManager").GetComponent<StageManager>();
        print(gameObject.name + "스테이지 충돌");
        GetComponent<Button>().onClick.Invoke();
    }
}
