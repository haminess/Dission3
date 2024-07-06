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

        // 노트가 1초 내인 경우만 따로 처리
        if (note[route_idx].time - 1< 0)
        {
            if (bgm.time > note[route_idx].time - 1)
            {
                MakeNote(ref note[route_idx]);
            }
            return;
        }

        // 현재 시간이 시작시간 이후로 데이터 시간이 지나면 생성
        else if (bgm.time > note[route_idx].time - 1)
        {
            GameObject rt = MakeRoute(note[route_idx]);
            if (rt) rt.name = "route" + (route_idx);

            // 실제 노트 뿌리기
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

        // 롱노트
        if (_note.ltype)
        {
            route = MakeLongRoute(_note);

            // 삭제될 시간 = 판정시간 - 현재시간 (판정될때 사라짐)
            Destroy(route, note[route_idx].duration + 1.5f);
        }
        // 숏노트
        else
        {
            // route 생성
            route = Instantiate(routeNote);

            // route 위치 지정
            route.transform.position = _note.pos;

            // 삭제될 시간 = 판정시간 - 현재시간 (판정될때 사라짐)
            Destroy(route, note[route_idx].duration + 1.5f);
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
                route = Instantiate(lRouteNote);
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

        // 설계
        {
            // 2. 꺾이는 만큼 롱루트 생성
            // - 롱루트 위치 조정 1
            // - 롱루트 길이 조정
            // - 롱루트 각도 조정
            // - 롱루트 위치 조정 2

            // 3. 루트 개수만큼 마스크 생성
            // - 마스크 가로 세로 스케일 조정
            // 가로 : 0.935, 0.2
            // 세로 : 0.2, 0.935
            // - 마스크 위치 조정

            // 처음과 끝, n일 경우 고려
        }

        return longroute;
    }
}
