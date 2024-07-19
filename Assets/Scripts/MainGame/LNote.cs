using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LNote : MonoBehaviour
{
    public Note note;

    // Long Note Data
    public Vector3[] route;

    // Setting Value
    Animator anim;

    Vector3 u = Vector3.up;
    Vector3 d = Vector3.down;
    Vector3 l = Vector3.left;
    Vector3 r = Vector3.right;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();

        // Init
        if (null == note)
        {
            InitNote();
        }

        // �����ð� 1�� (���� 1�� �� ����)
        Invoke(nameof(OnDestroyAnim), note.duration + 1);

        // ���� �� 1.5�� �� ����
        // ������� �ð� 0.5�� (���� 0.5�� �� ����)*
        Destroy(gameObject, note.duration + 1 + 0.5f);

        // Long Note Progress
        if (note.ltype)
        {
            DirectionToPos();
            StartCoroutine(Slide());
        }

    }


    void DirectionToPos()
    {
        route = new Vector3[note.route.Length];

        Vector3 inroute = note.pos;
        route[0] = inroute;
        for (int i = 1; i < note.route.Length; i++)
        {
            inroute += note.route[i - 1];
            route[i] = inroute;
        }
    }

    // �յ�Ʈ �̵�
    IEnumerator Slide()
    {
        yield return new WaitForSeconds(1f);

        for (int i = 0; i < note.route.Length - 1; i++)
        {
            yield return StartCoroutine(Run(route[i], route[i + 1], note.duration / (note.route.Length - 1)));
        }

    }

    // �̵�
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

    // ��Ʈ �ʱ�ȭ �Լ�
    public void LongNote(Note _note)
    {
        note = _note;
    }

    void OnDestroyAnim()
    {
        anim.SetTrigger("Destroy");
    }

    void InitNote()
    {
        note = new Note(0, new Vector3(0, 0));
    }

}


// Check Debug List
// 1. ä�� �� 0, 0�� �ߴ� ��Ʈ �ִ� ��
