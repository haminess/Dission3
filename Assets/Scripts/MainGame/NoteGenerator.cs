using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteGenerator : MonoBehaviour
{
    
    public int noteIndex;                   // 채보 포인터(0 ~ 노트 개수)
    float[][] chart;                  // 채보, 행: 채보 노트 인스턴스, 열: {time, x, y}
    public GameObject note;           // 바닥에 뿌릴 노트 프리팹
    public GameObject routeNote;

    // Start is called before the first frame update
    void Start()
    {
        // 도움말
        // 연습 기회


        // 시작했을 때 캐릭터, 스테이지 인덱스 출력

        // 채보 데이터 불러오기

        // 더미데이터(추후 삭제)
        noteIndex = 0;
        chart = new float[71][];

        // 1초 내의 노트들을 출력하기 위한 방책
        // 1초 내의 노트들은 시작하자마자 뿌려지는 문제..
        chart[0] = new float[3] { 0.639f, -6, 0 };
        chart[1] = new float[3] { 1.052f, -6, -1 };
        chart[2] = new float[3] { 1.258f, -5, -1 };
        chart[3] = new float[3] { 1.450f, -4, -1 };
        chart[4] = new float[3] { 1.642f, -3, -1 };
        chart[5] = new float[3] { 1.834f, -3, 0 };
        chart[6] = new float[3] { 2.303f, -3, 1 };
        chart[7] = new float[3] { 2.474f, -2, 1 };
        chart[8] = new float[3] { 2.666f, -1, 1 };
        chart[9] = new float[3] { 2.794f, 0, 1 };
        chart[10] = new float[3] { 3.029f, 0, 0 };
        chart[11] = new float[3] { 3.477f, 0, -1 };
        chart[12] = new float[3] { 3.669f, 1, -1 };
        chart[13] = new float[3] { 3.839f, 2, -1 };
        chart[14] = new float[3] { 4.031f, 3, -1 };
        chart[15] = new float[3] { 4.245f, 3, 0 };
        chart[16] = new float[3] { 4.671f, 2, 0 };
        chart[17] = new float[3] { 4.863f, 1, 0 };
        chart[18] = new float[3] { 5.034f, 0, 0 };
        chart[19] = new float[3] { 5.247f, -1, 0 };
        chart[20] = new float[3] { 5.461f, -2, 0 };

        for (int i = 0; i < 50; i++)
        {
            chart[21 + i] = new float[3] { 5.461f + (0.6f * i), 9, 0 - i };
        }

        //chart[0] = new float[3] { 0.584f, -6, 0 };
        //chart[1] = new float[3] { 1.052f, -6, -1 };
        //chart[2] = new float[3] { 1.253f, -5, -1 };
        //chart[3] = new float[3] { 1.458f, -4, -1 };
        //chart[4] = new float[3] { 1.635f, -3, -1 };
        //chart[5] = new float[3] { 1.819f, -3, 0 };
        //chart[6] = new float[3] { 2.269f, -3, 1 };
        //chart[7] = new float[3] { 2.470f, -2, 1 };
        //chart[8] = new float[3] { 2.652f, -1, 1 };
        //chart[9] = new float[3] { 2.835f, 0, 1 };
        //chart[10] = new float[3] { 3.00f, 0, 0 };
        //chart[11] = new float[3] { 3.453f, 0, -1 };
        //chart[12] = new float[3] { 3.671f, 1, -1 };
        //chart[13] = new float[3] { 3.837f, 2, -1 };
        //chart[14] = new float[3] { 4.020f, 3, -1 };
        //chart[15] = new float[3] { 4.203f, 3, 0 };
        //chart[16] = new float[3] { 4.686f, 2, 0 };
        //chart[17] = new float[3] { 4.871f, 1, 0 };
        //chart[18] = new float[3] { 5.053f, 0, 0 };
        //chart[19] = new float[3] { 5.222f, -1, 0 };
        //chart[20] = new float[3] { 5.420f, -2, 0 };

        noteIndex = -1;

    }

    // Update is called once per frame
    void Update()
    {
        // 게임 시작하면
        if (MainGame.instance.isGame)
        {
            // 바닥에 노트 생성
            ShowNote();
        }
        else if(MainGame.instance.isStart && !MainGame.instance.isGame)
        {
            // 설정 및 중단 시 발생
            // 설정 해제 후 continue할 때 노래 5초 전으로 당기기
            // 대기 상태에서 첫 노트 표시해주기
            OnSetting();
        }
        else if(!MainGame.instance.isStart && !MainGame.instance.isGame)
        {
            Start();
        }
    }

    // 채보 노트 생성
    void ShowNote()
    {
        if (noteIndex > chart.Length - 1) return;


        // 처음 경로 4칸 띄우기
        if (noteIndex == -1)
        {
            GameObject route = Instantiate(routeNote);
            route.transform.position = new Vector2(chart[0][1], chart[0][2]);
            Destroy(route, chart[0][0] + 1);
            route = Instantiate(routeNote);
            route.transform.position = new Vector2(chart[1][1], chart[1][2]);
            Destroy(route, chart[1][0] + 1);
            route = Instantiate(routeNote);
            route.transform.position = new Vector2(chart[2][1], chart[2][2]);
            Destroy(route, chart[2][0] + 1);
            route = Instantiate(routeNote);
            route.transform.position = new Vector2(chart[3][1], chart[3][2]);
            Destroy(route, chart[3][0] + 1);

            noteIndex++;
        }

        // 노트가 1초 내인 경우만 따로 처리
        if (chart[noteIndex][0] < 1f)
        {
            if(MainGame.instance.gameTime > chart[noteIndex][0])
            {
                MakeNote();
            }
            return;
        }
        else if (MainGame.instance.BGM.time + 1 > chart[noteIndex][0])       // 현재 시간이 시작시간 이후로 데이터 시간이 지나면 생성
        {
            // 4칸 앞 경로 띄우기
            if (noteIndex < chart.Length - 4)
            {
                GameObject route = Instantiate(routeNote);

                print(chart.Length);
                print(noteIndex);
                print(chart[noteIndex + 4][1]);
                route.transform.position = new Vector2(chart[noteIndex + 4][1], chart[noteIndex + 4][2]);
                Destroy(route, chart[noteIndex + 4][0] - MainGame.instance.BGM.time);
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

    void MakeRoute(int _index)
    {
        GameObject route = Instantiate(routeNote);
        route.transform.position = new Vector2(chart[_index][1], chart[_index][2]);
        Destroy(route, chart[_index][0] + 1);
    }
}
