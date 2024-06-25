using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteGeneratorforeditor : MonoBehaviour
{

    public Makenote makenote;
    public int noteIndex;                   // ä�� ������(0 ~ ��Ʈ ����)
    public Vector2[] boxpos;
    public GameObject note;           // �ٴڿ� �Ѹ� ��Ʈ ������
    public float offset;
    int length;
    int altlength;
    // Start is called before the first frame update
    void Start()
    {

    }
    public void refresh()
    {
        if( makenote.notedata.Count > Maketile.instance.boxpos.Length)
        {
            length = makenote.notedata.Count;
            altlength = Maketile.instance.boxpos.Length;
        }
        else
        {
            length = Maketile.instance.boxpos.Length;
            altlength = makenote.notedata.Count;
        }
        boxpos = new Vector2[length];
        //time, x, y
        for (int i = 0; i < Maketile.instance.boxpos.Length; i++)
        {
            boxpos[i] = new Vector2(Maketile.instance.boxpos[i].x - 0.496885f, Maketile.instance.boxpos[i].y + 0.48292f);
        }
        for(int i = 0; i < makenote.notedata.Count  ; i++)
        {
            if (makenote.notedata[i].notedata - Makemadi.instance.audio_.mainmusic.time > 1)
            {
                noteIndex = i;
                break;
            }
        }
    }
    // Update is called once per frame
    void Update()
    {
        // ���� �����ϸ�
        if (Audio.playing)
        {
            // �ٴڿ� ��Ʈ ����
            ShowNote();
        }
    }

    // ä�� ��Ʈ ����
    void ShowNote()
    {
        if (noteIndex > altlength - 1) return;
        if (makenote.notedata[noteIndex].notedata - 1< 0)
        {
            MakeNote();
        }
        if (Makemadi.instance.audio_.mainmusic.time > makenote.notedata[noteIndex].notedata - 1 + offset)       // ���� �ð��� ���۽ð� ���ķ� ������ �ð��� ������ ����
        {
            MakeNote();
        }
    }
    void MakeNote()
    {
        var note1 = Instantiate(note);
        note1.name = "note" + noteIndex.ToString(); 
        note1.transform.position = new Vector3(boxpos[noteIndex].x, boxpos[noteIndex].y, 0);
        noteIndex++;
        Destroy(note1, 3);
    }
}
