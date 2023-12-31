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
            chart[i] = new float[3] { (float)makenote.notedata[i], Maketile.instance.boxpos[i].x - 0.496885f, Maketile.instance.boxpos[i].y + 0.48292f };
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
        if (chart[noteIndex][0] - 1< 0)
        {
            noteIndex++;
        }
        else if (Makemadi.instance.audio_.audiosourse.time > chart[noteIndex][0] - 1 + offset)       // ���� �ð��� ���۽ð� ���ķ� ������ �ð��� ������ ����
        {
            MakeNote();
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
}
