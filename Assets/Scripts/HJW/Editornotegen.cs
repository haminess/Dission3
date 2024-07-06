using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Editornotegen : MonoBehaviour
{
    public Note[] note;
    public int route_idx;   // show chart idx
    public AudioSource bgm;
    public GameObject notePrefab;
    public GameObject preNote;
    public GameObject routeNote;
    public GameObject lRouteNote;
    public GameObject maskPrefab;

    Vector3 u = Vector3.up;
    Vector3 d = Vector3.down;
    Vector3 l = Vector3.left;
    Vector3 r = Vector3.right;
    Vector3 n = Vector3.zero;
    // Start is called before the first frame update
    private void Update()
    {
        if(Audio.playing&&Filedataconvey.playmode)
        {
            ShowNewNote();
        }
    }
    public void dataconvey()
    {
        int length;
        int boxlength = Maketile.instance.boxdata.Length;
        int notelength = Maketile.instance.makenote.notedata.Count;
        if(boxlength > notelength) { length = notelength; }
        else { length = boxlength; }
         Array.Resize(ref note, length);
        for (int i = 0; i < length; i++)
        {
            if (Maketile.instance.boxdata[i].boxroute_.Count > 0)
            {
                note[i] = new Note(Maketile.instance.makenote.notedata[i].notedata, Maketile.instance.gameObject.transform.GetChild(i + 1).position, Maketile.instance.boxdata[i].boxroute_.ToArray(), Maketile.instance.makenote.notedata[i].noteduration);

            }
            else
            {

                note[i] = new Note(Maketile.instance.makenote.notedata[i].notedata, Maketile.instance.gameObject.transform.GetChild(i + 1).position);
            }
        }
    }

    void ShowNewNote()
    {
        if(route_idx >= note.Length) return;

        // ��Ʈ�� 1�� ���� ��츸 ���� ó��
        if (note[route_idx].time - 1< 0)
        {
            if (bgm.time > note[route_idx].time - 1)
            {
                MakeNote(ref note[route_idx]);
            }
            return;
        }

        // ���� �ð��� ���۽ð� ���ķ� ������ �ð��� ������ ����
        else if (bgm.time > note[route_idx].time - 1)
        {
            GameObject rt = MakeRoute(note[route_idx]);
            if (rt) rt.name = "route" + (route_idx);

            // ���� ��Ʈ �Ѹ���
            MakeNote(ref note[route_idx]);
        }

        
    }
    void MakeNote(ref Note _note)
    {
        var newnote = Instantiate(notePrefab);

        newnote.name = "note" + route_idx.ToString();
        newnote.transform.position = _note.pos;

        LNote longnote = newnote.GetComponent<LNote>();
        longnote.LongNote(_note);

        _note.note = newnote;

        route_idx++;
    }

    GameObject MakeRoute(Note _note)
    {
        GameObject route = null;

        if (_note == null) return route;

        // �ճ�Ʈ
        if (_note.ltype)
        {
            route = MakeLongRoute(_note);

            // ������ �ð� = �����ð� - ����ð� (�����ɶ� �����)
            Destroy(route, note[route_idx].duration + 1.5f);
        }
        // ����Ʈ
        else
        {
            // route ����
            route = Instantiate(routeNote);

            // route ��ġ ����
            route.transform.position = _note.pos;

            // ������ �ð� = �����ð� - ����ð� (�����ɶ� �����)
            Destroy(route, note[route_idx].duration + 1.5f);
        }

        return route;

    }
    GameObject MakeLongRoute(Note _note)
    {
        // 1. �θ� ������Ʈ ����
        GameObject longroute = new GameObject("longroute");

        Vector3 curPos = _note.pos;
        for (int i = 0; i < _note.route.Length - 1; ++i)
        {
            GameObject route = null;
            GameObject mask = null;

            // ���� �ٲ�� �շ�Ʈ ����
            if (i == 0 || _note.route[i] != _note.route[i - 1])
            {
                route = Instantiate(lRouteNote);
                route.name = i.ToString();

                // ���� ����
                int size = 1;
                int di = i;
                while (_note.route[di] == _note.route[i])
                {
                    size++;
                    ++di;
                }
                route.GetComponent<SpriteRenderer>().size = new Vector2(1, size);

                // ���� ����
                Vector3 rot = Vector3.forward * 90;

                // ��ġ ����
                Vector2 pos = curPos;
                if (_note.route[i] == u)
                {
                    rot *= 0;
                    pos += Vector2.down * 0.5f;
                }
                else if (_note.route[i] == l)
                {
                    rot *= 1;
                    pos += Vector2.right * 0.5f;
                }
                else if (_note.route[i] == d)
                {
                    rot *= 2;
                    pos += Vector2.up * 0.5f;
                }
                else if (_note.route[i] == r)
                {
                    rot *= 3;
                    pos += Vector2.left * 0.5f;
                }
                else if (_note.route[i] == n)
                {
                    rot *= 0;
                    pos += Vector2.down * 0.5f;
                }
                route.transform.Rotate(rot);
                route.transform.position = pos;

                // �θ� ������ �ֱ�
                route.transform.SetParent(longroute.transform);
            }

            // ����ũ ����
            mask = Instantiate(maskPrefab);
            mask.name = "mask" + i.ToString();

            // ���� ����ũ
            if (_note.route[i] == u || _note.route[i] == d)
            {
                mask.transform.localScale = new Vector3(0.935f, 0.2f);
            }
            // ���� ����ũ
            else if (_note.route[i] == l || _note.route[i] == r)
            {
                mask.transform.localScale = new Vector3(0.2f, 0.935f);
            }

            mask.transform.position = curPos + _note.route[i] * 0.5f;

            mask.transform.SetParent(longroute.transform);

            // ���� ��ġ ������Ʈ
            curPos += _note.route[i];
        }

        // ����
        {
            // 2. ���̴� ��ŭ �շ�Ʈ ����
            // - �շ�Ʈ ��ġ ���� 1
            // - �շ�Ʈ ���� ����
            // - �շ�Ʈ ���� ����
            // - �շ�Ʈ ��ġ ���� 2

            // 3. ��Ʈ ������ŭ ����ũ ����
            // - ����ũ ���� ���� ������ ����
            // ���� : 0.935, 0.2
            // ���� : 0.2, 0.935
            // - ����ũ ��ġ ����

            // ó���� ��, n�� ��� ���
        }

        return longroute;
    }
}
