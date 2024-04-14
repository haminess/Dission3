using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LNote : MonoBehaviour
{
    public Note note;
    public Vector3 start;
    public Vector3 end;
    public float time; 
    public bool ltype;   // 0: short, 1: long

    // Long Note Data
    public Vector3 head;
    Vector3 u = Vector3.up;
    Vector3 d = Vector3.down;
    Vector3 l = Vector3.left;
    Vector3 r = Vector3.right;
    public Vector3[] route;
    public int length;

    public float s_time = 1;        // destroy time
    public float e_time = 1;     // destroy time
    Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();

        time = 0;

        // 생성 후 1.5초 뒤 삭제

        // 생성시간 1초 (생성 1초 후 판정)
        // 사라지는 시간 0.5초 (판정 0.5초 후 삭제)*

        if(ltype)
        {
            DirectionToPos();
            StartCoroutine(Slide());
        }

        Invoke(nameof(OnDestroyAnim), e_time);
        Destroy(gameObject, e_time + 0.5f);
    }


    void DirectionToPos()
    {
        Vector3 inroute = note.pos;
        route[0] = inroute;
        for (int i = 1; i < note.route.Length; i++)
        {
            inroute += note.route[i - 1];
            route[i] = inroute;
        }
    }

    // 롱도트 이동
    IEnumerator Slide()
    {
        yield return new WaitForSeconds(1f);

        for (int i = 0; i < note.route.Length - 1; i++)
        {
            yield return StartCoroutine(Run(route[i], route[i + 1], note.duration / (note.route.Length - 1)));
        }

    }

    // 이동
    IEnumerator Run(Vector3 _start, Vector3 _end, float _duration)
    {
        float runtime = 0f;
        while(true)
        {
            if (transform.position == _end) break;

            runtime += Time.deltaTime;
            transform.position = Vector3.Lerp(_start, _end, runtime / _duration);
            yield return null;
        }
    }

    // 노트 초기화 함수
    public void LongNote(Note _note)
    {
        note = _note;

        // state
        ltype = _note.ltype;

        // long note
        if (!ltype) return;

        route = new Vector3[_note.route.Length];

        // position
        start = _note.pos;
        end = start;
        for (int i = 0; i < route.Length; i++)
        {
            route[i] = _note.route[i];
            end += route[i];
        }

        // time
        s_time = 1;
        e_time = note.duration + 1;
    }

    void OnDestroyAnim()
    {
        anim.SetTrigger("Destroy");
    }

}
