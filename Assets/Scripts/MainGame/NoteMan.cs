using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteMan : MonoBehaviour
{
    MainMan main;

    public float time;
    public int note_idx;            // ä�� ������(0 ~ ��Ʈ ����)
    float[][] chart;                // ä��, ��: ä�� ��Ʈ �ν��Ͻ�, ��: {time, x, y}
    Note[] note;                    // ä��, ��: ä�� ��Ʈ �ν��Ͻ�, ��: {time, x, y}
    public GameObject notePrefab;   // �ٴڿ� �Ѹ� ��Ʈ ������
    public GameObject routePrefab;
    public GameObject maskPrefab;

    // ���� ���
    Vector3 u = Vector3.up;
    Vector3 d = Vector3.down;
    Vector3 l = Vector3.left;
    Vector3 r = Vector3.right;
    Vector3 n = Vector3.zero;

    // [setting]
    // ��ġ ���� ��� -> �� ������ ����
    float dptime = 1;   // note display time

    // Start is called before the first frame update
    void Start()
    {
        // ä�� �ҷ�����
        //SetChart(MainManager.instance.note);
        main = GetComponent<MainMan>();
        time = 0;
        note_idx = 0;
    }

    // Update is called once per frame
    void Update()
    {
        // ���� �����ϸ�
        if (main.isGame)
        {
            // �ٴڿ� ��Ʈ ����
            ShowNote();
        }
        else if(main.isStart && !main.isGame)
        {
            // ���� �� �ߴ� �� �߻�
            // ���� ���� �� continue�� �� �뷡 5�� ������ ����
            // ��� ���¿��� ù ��Ʈ ǥ�����ֱ�
            OnSetting();
        }
        else if(!main.isStart && !main.isGame)
        {
            //Start();
        }
    }

    // ä�� ��Ʈ ����
    void ShowNote()
    {
        if (note_idx > note.Length - 1) return;

        // ó�� ��� 4ĭ ����
        if (!main.bgm.isPlaying)
        {
            if (GameObject.Find("route")) return;
            MakeRoute(ref note[0], 1).name = "route";
            MakeRoute(ref note[1], 1);
            MakeRoute(ref note[2], 1);
            MakeRoute(ref note[3], 1);
            MakeRoute(ref note[4], 1);
        }

        // ��Ʈ�� 1�� ���� ��츸 ���� ó��
        if (note[note_idx].time - 1 - main.notesynkRange < 0)
        {
            if(main.gameTime > note[note_idx].time)
            {
                MakeNote(ref note[note_idx]);
                MakeRoute(ref note[note_idx + 4]).name = "route" + (note_idx + 4);
            }
            return;
        }
        else if (main.bgm.time > note[note_idx].time - 1 - main.notesynkRange)       // ���� �ð��� ���۽ð� ���ķ� ������ �ð��� ������ ����
        {
            // 4ĭ �� ��� ����
            if (note_idx < note.Length - 4)
            {
                MakeRoute(ref note[note_idx + 4]).name = "route" + (note_idx + 4);
            }

            // ���� ��Ʈ �Ѹ���
            MakeNote(ref note[note_idx]);
        }
    }

    void OnSetting()
    {
        note_idx = main.noteIndex;
    }

    void MakeNote()
    {
        var note1 = Instantiate(notePrefab);
        note1.name = "note" + note_idx.ToString();
        note1.transform.position = new Vector3(chart[note_idx][1], chart[note_idx][2], 0);
        note_idx++;
    }

    void MakeNote(ref Note _note)
    {
        var newnote = Instantiate(notePrefab);

        newnote.name = "note" + note_idx.ToString();
        newnote.transform.position = _note.pos;

        LNote longnote = newnote.GetComponent<LNote>();
        longnote.LongNote(_note);

        _note.note = newnote;

        note_idx++;
    }

    GameObject MakeRoute(int _index)
    {
        // route ����
        GameObject route = Instantiate(routePrefab);

        // route ��ġ ����
        route.transform.position = new Vector2(chart[_index][1], chart[_index][2]);

        // ������ �ð� = �����ð� - ����ð� (�����ɶ� �����)
        Destroy(route, chart[_index][0] - main.bgm.time);

        return route;
    }


    GameObject MakeRoute(ref Note _note, float _offset = 0)
    {
        GameObject route = null;

        if (_note == null) return route;

        // �ճ�Ʈ
        if (_note.ltype)
        {
            route = MakeLongRoute(_note);

            // ������ �ð� = �����ð� - ����ð� (�����ɶ� �����)
            Destroy(route, _note.time - main.bgm.time + _note.duration + 0.5f + _offset);
        }
        // ����Ʈ
        else
        {
            // route ����
            route = Instantiate(routePrefab);

            // route ��ġ ����
            route.transform.position = _note.pos + (0.5f * Vector3.down);

            // ������ �ð� = �����ð� - ����ð� (�����ɶ� �����)
            Destroy(route, _note.time - main.bgm.time + 0.5f);
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
                route = Instantiate(routePrefab);
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

        return longroute;
    }

    public void SetChart(float[][] _chart)
    {
        chart = _chart;
    }
    public void SetChart(ref Note[] _note)
    {
        note = _note;
    }

    public float GetNoteDisplayTime()
    {
        return dptime;
    }
    public void SetNoteDisplayTime(float _time)
    {
        dptime = _time;
    }
}
