using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteMan : MonoBehaviour
{
    MainMan main;

    public float time;
    public int note_idx;            // 채보 포인터(0 ~ 노트 개수)
    float[][] chart;                // 채보, 행: 채보 노트 인스턴스, 열: {time, x, y}
    Note[] note;                    // 채보, 행: 채보 노트 인스턴스, 열: {time, x, y}
    public GameObject notePrefab;   // 바닥에 뿌릴 노트 프리팹
    public GameObject routePrefab;
    public GameObject maskPrefab;

    // 설정 상수
    Vector3 u = Vector3.up;
    Vector3 d = Vector3.down;
    Vector3 l = Vector3.left;
    Vector3 r = Vector3.right;
    Vector3 n = Vector3.zero;

    // [setting]
    // 위치 수정 요망 -> 한 곳에서 관리
    float dptime = 1;   // note display time

    // Start is called before the first frame update
    void Start()
    {
        // 채보 불러오기
        //SetChart(MainManager.instance.note);
        main = GetComponent<MainMan>();
        time = 0;
        note_idx = 0;
    }

    // Update is called once per frame
    void Update()
    {
        // 게임 시작하면
        if (main.isGame)
        {
            // 바닥에 노트 생성
            ShowNote();
        }
        else if(main.isStart && !main.isGame)
        {
            // 설정 및 중단 시 발생
            // 설정 해제 후 continue할 때 노래 5초 전으로 당기기
            // 대기 상태에서 첫 노트 표시해주기
            OnSetting();
        }
        else if(!main.isStart && !main.isGame)
        {
            //Start();
        }
    }

    // 채보 노트 생성
    void ShowNote()
    {
        if (note_idx > note.Length - 1) return;

        // 처음 경로 4칸 띄우기
        if (!main.bgm.isPlaying)
        {
            if (GameObject.Find("route")) return;
            MakeRoute(ref note[0], 1).name = "route";
            MakeRoute(ref note[1], 1);
            MakeRoute(ref note[2], 1);
            MakeRoute(ref note[3], 1);
            MakeRoute(ref note[4], 1);
        }

        // 노트가 1초 내인 경우만 따로 처리
        if (note[note_idx].time - 1 - main.notesynkRange < 0)
        {
            if(main.gameTime > note[note_idx].time)
            {
                MakeNote(ref note[note_idx]);
                MakeRoute(ref note[note_idx + 4]).name = "route" + (note_idx + 4);
            }
            return;
        }
        else if (main.bgm.time > note[note_idx].time - 1 - main.notesynkRange)       // 현재 시간이 시작시간 이후로 데이터 시간이 지나면 생성
        {
            // 4칸 앞 경로 띄우기
            if (note_idx < note.Length - 4)
            {
                MakeRoute(ref note[note_idx + 4]).name = "route" + (note_idx + 4);
            }

            // 실제 노트 뿌리기
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
        // route 생성
        GameObject route = Instantiate(routePrefab);

        // route 위치 지정
        route.transform.position = new Vector2(chart[_index][1], chart[_index][2]);

        // 삭제될 시간 = 판정시간 - 현재시간 (판정될때 사라짐)
        Destroy(route, chart[_index][0] - main.bgm.time);

        return route;
    }


    GameObject MakeRoute(ref Note _note, float _offset = 0)
    {
        GameObject route = null;

        if (_note == null) return route;

        // 롱노트
        if (_note.ltype)
        {
            route = MakeLongRoute(_note);

            // 삭제될 시간 = 판정시간 - 현재시간 (판정될때 사라짐)
            Destroy(route, _note.time - main.bgm.time + _note.duration + 0.5f + _offset);
        }
        // 숏노트
        else
        {
            // route 생성
            route = Instantiate(routePrefab);

            // route 위치 지정
            route.transform.position = _note.pos + (0.5f * Vector3.down);

            // 삭제될 시간 = 판정시간 - 현재시간 (판정될때 사라짐)
            Destroy(route, _note.time - main.bgm.time + 0.5f);
        }

        return route;

    }
    GameObject MakeLongRoute(Note _note)
    {
        // 1. 부모 오브젝트 생성
        GameObject longroute = new GameObject("longroute");

        Vector3 curPos = _note.pos;
        for (int i = 0; i < _note.route.Length - 1; ++i)
        {
            GameObject route = null;
            GameObject mask = null;

            // 방향 바뀌면 롱루트 생성
            if (i == 0 || _note.route[i] != _note.route[i - 1])
            {
                route = Instantiate(routePrefab);
                route.name = i.ToString();

                // 길이 조정
                int size = 1;
                int di = i;
                while (_note.route[di] == _note.route[i])
                {
                    size++;
                    ++di;
                }
                route.GetComponent<SpriteRenderer>().size = new Vector2(1, size);

                // 각도 조정
                Vector3 rot = Vector3.forward * 90;

                // 위치 조정
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

                // 부모 밑으로 넣기
                route.transform.SetParent(longroute.transform);
            }

            // 마스크 생성
            mask = Instantiate(maskPrefab);
            mask.name = "mask" + i.ToString();

            // 가로 마스크
            if (_note.route[i] == u || _note.route[i] == d)
            {
                mask.transform.localScale = new Vector3(0.935f, 0.2f);
            }
            // 세로 마스크
            else if (_note.route[i] == l || _note.route[i] == r)
            {
                mask.transform.localScale = new Vector3(0.2f, 0.935f);
            }

            mask.transform.position = curPos + _note.route[i] * 0.5f;

            mask.transform.SetParent(longroute.transform);

            // 다음 위치 업데이트
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
