using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LNoteGenerator : MonoBehaviour
{
    public int noteIndex;             // 채보 포인터(0 ~ 노트 개수)
    float[][] chart;                  // 채보, 행: 채보 노트 인스턴스, 열: {time, x, y}
    public GameObject note;           // 바닥에 뿌릴 노트 프리팹
    public GameObject routeNote;
    public GameObject lRouteNote;

    // Start is called before the first frame update
    void Start()
    {
        // 도움말
        // 연습 기회


        // 시작했을 때 캐릭터, 스테이지 인덱스 출력

        // 채보 데이터 불러오기
        {
            // time, x, y, length, direct
            // direct 0: up, 1: left, 2: down, 3: right
            chart = new float[10][];
            chart[0] = new float[5] { 1, 1, 0, 0, 0};
            chart[1] = new float[5] { 2, 2, 0, 0, 0};
            chart[2] = new float[5] { 3, 3, 0, 0, 0};
            chart[3] = new float[5] { 4, 4, 0, 0, 0};
            chart[4] = new float[5] { 5, 5, 0, 3, 4};
            chart[5] = new float[5] { 9, 9, 0, 0, 0};
            chart[6] = new float[5] { 10, 10, 0, 0, 0};
            chart[7] = new float[5] { 11, 11, 0, 0, 0};
            chart[8] = new float[5] { 12, 12, 0, 0, 0};
            chart[9] = new float[5] { 13, 13, 0, 5, 3};
        }
    }

    // Update is called once per frame
    void Update()
    {
        // 게임 시작하면
        if(MainGame.instance)
        {
            if (MainGame.instance.isGame)
            {
                // 바닥에 노트 생성
                ShowNote();
            }
            else if (MainGame.instance.isStart && !MainGame.instance.isGame)
            {
                // 설정 및 중단 시 발생
                // 설정 해제 후 continue할 때 노래 5초 전으로 당기기
                // 대기 상태에서 첫 노트 표시해주기
                OnSetting();
            }
            else if (!MainGame.instance.isStart && !MainGame.instance.isGame)
            {
                //Start();
            }
        }
    }

    // 채보 노트 생성
    void ShowNote()
    {
        if (noteIndex > chart.Length - 1) return;

        // 처음 경로 4칸 띄우기
        if (!MainGame.instance.bgm.isPlaying)
        {
            if (GameObject.Find("route")) return;
            MakeRoute(noteIndex + 0);
            MakeRoute(noteIndex + 1);
            MakeRoute(noteIndex + 2);
            MakeRoute(noteIndex + 3).name = "route";
        }

        // 노트가 1초 내인 경우만 따로 처리
        if (chart[noteIndex][0] - 1 - MainGame.instance.notesynkRange < 0)
        {
            if(MainGame.instance.gameTime > chart[noteIndex][0])
            {
                MakeNote();
            }
            return;
        }
        else if (MainGame.instance.bgm.time > chart[noteIndex][0] - 1 - MainGame.instance.notesynkRange)       // 현재 시간이 시작시간 이후로 데이터 시간이 지나면 생성
        {
            // 4칸 앞 경로 띄우기
            if (noteIndex < chart.Length - 4)
            {
                MakeRoute(noteIndex + 4).name = "route";
            }

            // 실제 노트 뿌리기
            MakeNote();
        }
    }

    void OnSetting()
    {
        noteIndex = MainGame.instance.noteIndex;
    }

    void MakeNote()
    {
        var note1 = Instantiate(note);
        note1.name = "note" + noteIndex.ToString();
        note1.transform.position = new Vector3(chart[noteIndex][1], chart[noteIndex][2], 0);
        noteIndex++;
    }

    GameObject MakeRoute(int _index)
    {
        if (chart[_index][3] > 0)
        {
            GameObject route = Instantiate(lRouteNote);

            // 길이 조정
            route.GetComponent<SpriteRenderer>().size = Vector2.up * chart[_index][3];

            // 각도 조정
            Vector3 rot = Vector3.forward * 90 * chart[_index][4];
            route.transform.rotation = new Quaternion(rot.x, rot.y, rot.z, route.transform.rotation.w);

            // 위치 조정
            Vector2 pos = new Vector2(chart[noteIndex][1], chart[noteIndex][2]);
            switch(chart[_index][4])
            {
                case 0:
                    pos += Vector2.down * 0.5f;
                    break;
                case 1:
                    pos += Vector2.right * 0.5f;
                    break;
                case 2:
                    pos += Vector2.up * 0.5f;
                    break;
                case 3:
                    pos += Vector2.left * 0.5f;
                    break;
            }
            route.transform.position = pos;

            // 롱노트
            return route;
        }
        else
        {
            // route 생성
            GameObject route = Instantiate(routeNote);

            // route 위치 지정
            route.transform.position = new Vector2(chart[_index][1], chart[_index][2]);

            // 삭제될 시간 = 판정시간 - 현재시간 (판정될때 사라짐)
            Destroy(route, chart[_index][0] - MainGame.instance.bgm.time);

            return route;
        }
    }

    public void SetChart(float[][] _chart)
    {
        chart = _chart;
    }
}
