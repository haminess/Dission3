using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteGeneratorforeditor : MonoBehaviour
{

    public Makenote makenote;
    public int noteIndex;                   // ä�� ������(0 ~ ��Ʈ ����)
    public float[][] chart;                  // ä��, ��: ä�� ��Ʈ �ν��Ͻ�, ��: {time, x, y}
    public GameObject note;           // �ٴڿ� �Ѹ� ��Ʈ ������
    public GameObject routeNote;
    public bool refreshok;

    public float offset;

    // Start is called before the first frame update
    void Start()
    {

    }
    public void refresh()
    {
        if (makenote.notedata.Length != Maketile.instance.boxpos.Length)
        {
            return;
        }

        chart = new float[makenote.notedata.Length][];
        //time, x, y
        for (int i = 0; i < makenote.notedata.Length; i++)
        {
            chart[i] = new float[3] { (float)makenote.notedata[i], Maketile.instance.boxpos[i].x, Maketile.instance.boxpos[i].y };
        }
        if(Makemadi.instance.page == 0)
        {
            noteIndex = 0;
        }
        else
        {
            for(int i = 0; i < makenote.notedata.Length; i++)
            {
                if(chart[i][0] - Makemadi.instance.audio_.audiosourse.time > 1)
                {
                    noteIndex = i;
                    break;
                }
            }
        }
    }
    // Update is called once per frame
    void Update()
    {
        // ���� �����ϸ�
        if (Audio.playing && makenote.notedata.Length == Maketile.instance.boxpos.Length)
        {
            // �ٴڿ� ��Ʈ ����
            ShowNote();
        }
    }

    // ä�� ��Ʈ ����
    void ShowNote()
    {
        if (noteIndex > chart.Length - 1) return;
        if (chart[noteIndex][0] - 1 + offset < 0)       // ���� �ð��� ���۽ð� ���ķ� ������ �ð��� ������ ����
        {
            // 4ĭ �� ��� ����
            if (noteIndex < chart.Length - 4)
            {
                MakeRoute(noteIndex + 4).name = "route";
            }
            // ���� ��Ʈ �Ѹ���
            MakeNote();
        }
        else if (Makemadi.instance.audio_.audiosourse.time > chart[noteIndex][0] - 1 + offset)       // ���� �ð��� ���۽ð� ���ķ� ������ �ð��� ������ ����
        {
            // 4ĭ �� ��� ����
            if (noteIndex < chart.Length - 4)
            {
                MakeRoute(noteIndex + 4).name = "route";
            }
                // ���� ��Ʈ �Ѹ���
            MakeNote();
        }

        // ó�� ��� 4ĭ ����
        if (!Audio.playing)
        {
            if (GameObject.Find("route")) return;
            MakeRoute(noteIndex + 0);
            MakeRoute(noteIndex + 1);
            MakeRoute(noteIndex + 2);
            MakeRoute(noteIndex + 3).name = "route";
        }
    }
    void MakeNote()
    {
        var note1 = Instantiate(note);
        note1.name = "note" + noteIndex.ToString();
        note1.transform.position = new Vector3(chart[noteIndex][1], chart[noteIndex][2], 0);
        noteIndex++;
        Destroy(note1, 3);
    }

    GameObject MakeRoute(int _index)
    {
        // route ����
        GameObject route = Instantiate(routeNote);

        // route ��ġ ����
        route.transform.position = new Vector2(chart[_index][1], chart[_index][2]);

        // ������ �ð� = �����ð� - ����ð� (�����ɶ� �����)
        Destroy(route, chart[_index][0] - Makemadi.instance.audio_.audiosourse.time);

        return route;
    }
}
