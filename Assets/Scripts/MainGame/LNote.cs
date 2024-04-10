using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LNote : MonoBehaviour
{
    public Vector2 start;
    public Vector2 end;
    public float time; // start time
    public float etime; // end time
    public bool ltype;   // 0: short, 1: long

    // Long Note Data
    public Vector3 head;
    public int length;

    public float s_time = 1;    // destroy time
    public float e_time = 1.5f;    // destroy time
    Animator anim;


    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();

        time = 0;
        start = transform.position;
        end = transform.position + head * (length - 1);

        // 생성 후 1.5초 뒤 삭제

        // 생성시간 1초 (생성 1초 후 판정)
        // 사라지는 시간 0.5초 (판정 0.5초 후 삭제)*
        Invoke(nameof(OnDestroyAnim), e_time);
        Destroy(gameObject, e_time + 0.5f);
    }

    void Update()
    {
        time += Time.deltaTime;
        if(ltype && time > s_time && time < e_time)
        {
            transform.position = Vector3.Lerp(start, end, (time - s_time) / (e_time - s_time));
        }
    }

    void OnDestroyAnim()
    {
        anim.SetTrigger("Destroy");
    }
}
