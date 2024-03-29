using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LNoteGenerator : MonoBehaviour
{
    public int noteIndex;             // ä�� ������(0 ~ ��Ʈ ����)
    float[][] chart;                  // ä��, ��: ä�� ��Ʈ �ν��Ͻ�, ��: {time, x, y}
    public GameObject note;           // �ٴڿ� �Ѹ� ��Ʈ ������
    public GameObject routeNote;
    public GameObject lRouteNote;

    // Start is called before the first frame update
    void Start()
    {
        // ����
        // ���� ��ȸ


        // �������� �� ĳ����, �������� �ε��� ���

        // ä�� ������ �ҷ�����
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
        // ���� �����ϸ�
        if(MainGame.instance)
        {
            if (MainGame.instance.isGame)
            {
                // �ٴڿ� ��Ʈ ����
                ShowNote();
            }
            else if (MainGame.instance.isStart && !MainGame.instance.isGame)
            {
                // ���� �� �ߴ� �� �߻�
                // ���� ���� �� continue�� �� �뷡 5�� ������ ����
                // ��� ���¿��� ù ��Ʈ ǥ�����ֱ�
                OnSetting();
            }
            else if (!MainGame.instance.isStart && !MainGame.instance.isGame)
            {
                //Start();
            }
        }
    }

    // ä�� ��Ʈ ����
    void ShowNote()
    {
        if (noteIndex > chart.Length - 1) return;

        // ó�� ��� 4ĭ ����
        if (!MainGame.instance.bgm.isPlaying)
        {
            if (GameObject.Find("route")) return;
            MakeRoute(noteIndex + 0);
            MakeRoute(noteIndex + 1);
            MakeRoute(noteIndex + 2);
            MakeRoute(noteIndex + 3).name = "route";
        }

        // ��Ʈ�� 1�� ���� ��츸 ���� ó��
        if (chart[noteIndex][0] - 1 - MainGame.instance.notesynkRange < 0)
        {
            if(MainGame.instance.gameTime > chart[noteIndex][0])
            {
                MakeNote();
            }
            return;
        }
        else if (MainGame.instance.bgm.time > chart[noteIndex][0] - 1 - MainGame.instance.notesynkRange)       // ���� �ð��� ���۽ð� ���ķ� ������ �ð��� ������ ����
        {
            // 4ĭ �� ��� ����
            if (noteIndex < chart.Length - 4)
            {
                MakeRoute(noteIndex + 4).name = "route";
            }

            // ���� ��Ʈ �Ѹ���
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

            // ���� ����
            route.GetComponent<SpriteRenderer>().size = Vector2.up * chart[_index][3];

            // ���� ����
            Vector3 rot = Vector3.forward * 90 * chart[_index][4];
            route.transform.rotation = new Quaternion(rot.x, rot.y, rot.z, route.transform.rotation.w);

            // ��ġ ����
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

            // �ճ�Ʈ
            return route;
        }
        else
        {
            // route ����
            GameObject route = Instantiate(routeNote);

            // route ��ġ ����
            route.transform.position = new Vector2(chart[_index][1], chart[_index][2]);

            // ������ �ð� = �����ð� - ����ð� (�����ɶ� �����)
            Destroy(route, chart[_index][0] - MainGame.instance.bgm.time);

            return route;
        }
    }

    public void SetChart(float[][] _chart)
    {
        chart = _chart;
    }
}
