using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class StoryManager : MonoBehaviour
{
    public int storyID = 0;

    public bool showButton;

    // �� ������Ʈ
    public GameObject player;
    public GameObject playerCam;
    public GameObject storyCamera;
    public GameObject gameCanvas;
    public GameObject playerPrefab;
    public GameObject npcPrefab;
    public GameObject[] stageObject;     // �������� ���� ������Ʈ

    // ĳ���� �̹��� ��������Ʈ
    public Sprite baby;
    public Sprite student;
    public Sprite friend1;
    public GameObject black;

    public GameObject ChatPrefab;

    // ��ũ��Ʈ ����
    public float chatSpeed = 2;
    Dictionary<int, string[]> scripts = new Dictionary<int, string[]>();
    GameObject[] storyNpc;
    int[][] npcNum;

    // ��ũ��Ʈ ����

    // Start is called before the first frame update
    void Start()
    {
        playerCam.SetActive(true);
        storyCamera.SetActive(false);
        gameCanvas.SetActive(false);
        CreateScripts();

        // �������� ��
        // �� ������Ʈ Ȱ��ȭ
    }

    // Update is called once per frame
    void Update()
    {
        if (showButton)
        {
            showButton = false;
            ShowStory(storyID);
        }
    }

    void CreateScripts()
    {
        // 1��������
        scripts.Add(0, new string[] { "�ٵ� ������ħ~",                      // ������
                                      "������ �츮 �ݿ� ���л��� �Ծ��~",
                                      "���л��� �ڱ�Ұ� �غ���?",
                                      "�ȳ� ���� �����̶�� ��!",             // ���ΰ�
                                      "������ �� ��Ź��, ģ�ϰ� ������!",
                                      // �ڸ��� �ɴ´�.
                                      "�ȳ� ������, ������ �ݰ���!!",          // ģ��1
                                      "���� �б� �Ұ������״ϱ� ���� ����?",
                                      "����!"});                             // ���ΰ�
        scripts.Add(1, new string[] { "�̰ɷ� �б� �Ұ��� ���̾�!",            // ģ��1
                                      "������ ����ְ� �б���Ȱ �غ���",
                                      "�� �´�! �� ������ ������ ���� ��!!",
                                      "�ͽ��� �鷶�ٴ� �ҹ��� �����ϱ�~ ����",
                                      "(��.. ���� �����ϰ� �����..)"});       // ���ΰ�
        scripts.Add(2, new string[] { "�б� ������ �����ؼ� ���� �Ҿ���Ⱦ�..",
                                      "������ ��𿴴���?"});
        npcNum = new int[16][];
        npcNum[0] = new int[] { 1, 1, 1, 0, 0, 2, 2, 0 };
        npcNum[1] = new int[] { 2, 2, 2, 2, 0 };
        npcNum[2] = new int[] { 0, 0 };

        // 2��������
        scripts.Add(3, new string[] { "�� �� �ҹ� �����?",                    // ģ��1
                                      "�츮 �б� �޽Ľǿ��� ���Ḷ��..",
                                      "�̻��� �Ҹ��� ����..!!",
                                      "���� �����ȵ�! �����̰���~",            // ģ��2
                                      "��¥��ϱ�!!",                         // ģ��1
                                      "..�׷� ���� �ѹ� Ȯ���غ���?",          // ģ��2
                                      "�׷���? ���� ���..",                 // ģ��1
                                      "�츮 �����̼� ����׽�Ʈ �غ���?!?!",    // ģ��1
                                      "����! �׷� ���ῡ �޽Ľǿ��� ������!!",  // ģ��2
                                      "(��.. ������ �� ������;;)"});           // ���ΰ�
        scripts.Add(4, new string[] { "��� ���� ���� �޽Ľǿ��� ����̸� Ű��� �־���..", // ģ��3
                                      "�� ����� ��� ���� Ÿ��",
                                      "���� ��������� ������ �� ����..",
                                      "�̷��� ���� Ŀ�� �� �����µ�.. �̾�!",
                                      "�׷� �� ������.. �׷� �ͽ��� ���°ǰ�?", // ģ��1
                                      "��? ���������� ���µ�?",
                                      // ����̰� ������ ����� ���ΰ����� �ǳ��ش�.
                                      "(���? �� ���� �ƴ� �����ε�..)",         // ���ΰ�
                                      "(��� �ô���..)"});
        scripts.Add(5, new string[] { "���� �ʹ� ������..",
                                      "�׳� �����ϰ� �����߰ھ�.."});

        npcNum[3] = new int[] { 1, 1, 1, 2, 1, 2, 1, 1, 2, 0 };
        npcNum[4] = new int[] { 3, 3, 3, 3, 1, 1, 0, 0 };
        npcNum[5] = new int[] { 0, 0 };

        // 3��������
        scripts.Add(6, new string[] { "������ ���� ��ٸ��� ��ٸ���..",        // ģ��1
                                      "�ٷ� ü�� ��ȸ��!!",
                                      "�ٵ� �ּ��� ���ؼ� ����ְ� �غ��ڤ���",
                                      "������ ���� �Ǳ� ���� ����! ��ġ�� ����!!", // ģ��2
                                      "�� ����!!"});                           // ���ΰ�
        scripts.Add(7, new string[] { // ��� ���� ��Ȳ�� �����Ű�� ����� ª�� ��������.
                                      // (������ ���ϴ� ���� ���� ������ ����..)
                                      // ���ǽǿ��� ���ΰ��� �����.
                                      "������??", // ģ��2
                                      "�� �Ǳ� ���� �ϴٰ� ���ڱ� ��������!!",
                                      "��ü �� �׷��ž�?",
                                      "���� �� �𸣰ھ�..",                       // ���ΰ�
                                      "���ڱ� �Ӹ��� ���ļ� ������ ���� �� ����..",
                                      "(���� �������� �ߴ� �� ������..)"});
        scripts.Add(8, new string[] { // ���ǽǿ��� ���ΰ��� �����.
                                      "�� �Ǳ� ���� �ϴٰ� ���ڱ� ��������!!",      // ģ��2
                                      "��ü �� �׷��ž�?",
                                      "���ڱ� �Ӹ��� ���ļ� ������ ���� �� ����.."}); // ���ΰ�

        npcNum[6] = new int[] { 1, 1, 1, 2, 0 };
        npcNum[7] = new int[] { 2, 2, 2, 0, 0, 0 };
        npcNum[8] = new int[] { 2, 2, 0 };

        // 4��������
        scripts.Add(9, new string[] { "���..? �� ���� �� ����ִ°���?",           // ���ΰ�
                                      "�ȿ��� ���� �Ҹ��� �鸮�� �� ���⵵ �ϰ�..",
                                      "������ ������..",
                                      "�б� �ȿ� �� ���� ������ ���� ������?",
                                      "�ѹ� ã�ƺ���!"});
        scripts.Add(10, new string[] { // ���� ���� ������ �ſ�(��������)���� �Ҹ��� �����ִ�.
                                      "���� �� �̻� ���� �� �� �ִ°� ������.",
                                      "���� ȯ�ں��� �����Ա⿡ �޷Ƚ��ϴ�..",
                                      "������.. ������ �ϰ� �־�..",
                                      "�ٸ� ������� �ƹ��� �����϶�� �ص�",
                                      "������ ���� ������ ���� ���ϰ� ��� ��ٸ�����..",
                                      "������ �����..",
                                      "������",
                                      "�¾�.. ����� �� ����� ����� �� �� �� ���迴��..",
                                      "������ �ʹ� ���ο��� ���� ��� �ʹ� ���� �ٰ� �־�����",
                                      "�� �̰��� �ͼ� ������ �ູ�ѵ�.. ��� �ؾ� �ұ�"});
        scripts.Add(11, new string[] { "�� ���� ������ ��ã�ڳ�..",
                                      "�׳� ����� â����� ��!"});
        npcNum[9] = new int[] { 0, 0, 0, 0, 0 };
        npcNum[10] = new int[] { 1, 1, 2, 2, 2, 2, 0, 0, 0, 0 };
        npcNum[11] = new int[] { 0, 0 };

        // 5��������
        scripts.Add(12, new string[] { // ģ���� ���ΰ��� å�� ��ó���� ����ִ�.
                                      "������! ���� ������ � ���ٱ�??",
                                      "������ �?",
                                      "��! �׳� å�� �켭 ���� �������� ��� �˾ƺ��°ž�!!",
                                      "���� �������� ���..",
                                      "�������� ���ϴ� ������ ���� ��µ�?",
                                      "(�� ������ ���ϴ� ��..?)"});
        scripts.Add(13, new string[] { "ȯ�ں� ������ �弼��!?",                 
                                      "������!! ���� �������..!",
                                      "������ ����.. �ʹ� ����...",
                                      "(���Ⱑ �����̱���.. ���� ���ƿԾ�..)",
                                      "(���..? ���� �� ���� ��� �� ������.. ��������?)"});
        scripts.Add(14, new string[] { "������! ���� �޽� ���Ƕ��̽� ���´�!!",    // ���ΰ�
                                      "�ʰ� ���� �����ϴ� ���ݾ� ����",
                                      "�� ���� �� ġ�ڸ��� �پ��!!!",
                                      "�� ����!!",
                                      "(���� �̰��� ������ �� �ູ�� �� ����..)",
                                      "(���� ���Ⱑ �� �����̾�!! �ٸ��� �ƹ��͵� �ʿ����..)"});
        scripts.Add(15, new string[] { "���� ���� �ٸ� ���� �Ϸ翴��!", // ���ΰ�
                                      "��� �ǹ̰� ���� �𸣰ڳ�..",
                                      "���� �ϰų����ų��ΰ�!",
                                      "���� ���� �� ������.. ���ſ�̰���?"});
        npcNum[12] = new int[] { 1, 0, 1, 1, 2, 0 };
        npcNum[13] = new int[] { 3, 4, 4, 0, 0 };
        npcNum[14] = new int[] { 2, 2, 0, 2, 0, 0 };
        npcNum[15] = new int[] { 0, 0, 0, 0 };
    }

    public void ShowStory(int stroyID)
    {
        this.storyID = stroyID;
        StartCoroutine(ShowStoryCo());
    }

    public IEnumerator ShowStoryCo()
    {
        // �÷��̾� ������ Off
        player.GetComponent<Player>().enabled = false;
        player.GetComponentInChildren<SpriteRenderer>().enabled = false;

        // ���� UI Off
        gameCanvas.SetActive(false);

        // ���丮 ����
        switch (this.storyID)
        {
            case 0000:
                yield return StartCoroutine(Story1());
                break;
            case 0001:
                yield return StartCoroutine(Story1Happy());
                break;
            case 0002:
                yield return StartCoroutine(Story1Sad());
                break;
            case 0003:
                yield return StartCoroutine(Story2());
                break;
            case 0004:
                yield return StartCoroutine(Story2_1());
                break;
            case 0005:
                yield return StartCoroutine(Story2_2());
                break;
            case 0006:
                yield return StartCoroutine(Story3());
                break;
            case 0007:
                yield return StartCoroutine(Story3_1());
                break;
            case 0008:
                yield return StartCoroutine(Story3_2());
                break;
            case 9999:
                yield return StartCoroutine(Tutorial());
                break;
        }



        // �÷��̾� ������ On
        player.GetComponent<Player>().enabled = true;
        player.GetComponentInChildren<SpriteRenderer>().enabled = true;

        // ���� UI On
        gameCanvas.SetActive(true);
    }

    // ���丮 ��ŵ
    public void Skip()
    {
        StopAllCoroutines();
        StartCoroutine(SkipCo());
    }

    public IEnumerator SkipCo()
    {
        yield return StartCoroutine(Fade(black));
        storyCamera.SetActive(false);
        playerCam.SetActive(true);
        player.SetActive(true);
        player.GetComponent<Player>().enabled = true;
        player.GetComponentInChildren<SpriteRenderer>().enabled = true;
        yield return StartCoroutine(Fade(black, false));


        if (!MainGame.instance.isEnd)
        {
            MainGame.instance.GameStart();
        }
        else
        {
            MainGame.instance.GameEnd();
        }
    }


    IEnumerator Story1()
    {
        // ȭ�� ��ȯ
        yield return StartCoroutine(Fade(black));

        // ī�޶� ����
        playerCam.SetActive(false);
        storyCamera.SetActive(true);
        storyCamera.transform.position = new Vector3(0, 0, -10);

        yield return StartCoroutine(Fade(black, false));

        yield return new WaitForSeconds(1);

        // # 1
        int id = 0;

        GameObject teacher = NPC(baby, 6, 0);
        yield return StartCoroutine(Fade(teacher));

        yield return new WaitForSeconds(1);

        yield return StartCoroutine(Move(teacher, Vector3.left, 6));

        yield return new WaitForSeconds(1);
        yield return StartCoroutine(Typing(teacher, scripts[id][0]));
        yield return StartCoroutine(Typing(teacher, scripts[id][1]));
        yield return StartCoroutine(Typing(teacher, scripts[id][2]));

        GameObject players = NPC(student, 6, 0);
        yield return StartCoroutine(Fade(players));

        yield return new WaitForSeconds(1);

        yield return StartCoroutine(Move(players, Vector3.left, 5));

        yield return new WaitForSeconds(1);
        yield return StartCoroutine(Typing(players, scripts[id][3]));
        yield return StartCoroutine(Typing(players, scripts[id][4]));

        yield return StartCoroutine(Move(players, Vector3.down, 8));
        yield return StartCoroutine(Move(players, Vector3.left));

        // ȭ�� ��ȯ
        yield return StartCoroutine(Fade(black));
        Destroy(teacher);
        Destroy(players);

        // # 2
        storyCamera.transform.position = new Vector3(0, -7, -10);
        GameObject minji = NPC(friend1, 0, 0);
        players = NPC(student, -1, -8);
        yield return StartCoroutine(Fade(black, false));

        yield return new WaitForSeconds(1);

        yield return StartCoroutine(Move(minji, Vector3.down, 8));

        yield return new WaitForSeconds(1);
        yield return StartCoroutine(Typing(minji, scripts[id][5]));
        players.GetComponentInChildren<SpriteRenderer>().flipX = true;
        yield return StartCoroutine(Typing(minji, scripts[id][6]));
        yield return StartCoroutine(Typing(players, scripts[id][7]));

        // Story Off
        yield return StartCoroutine(Fade(black));
        Destroy(players);
        Destroy(minji);

        // ī�޶� �ǵ�����
        storyCamera.SetActive(false);
        playerCam.SetActive(true);

        yield return StartCoroutine(Fade(black, false));
    }

    IEnumerator Story1Happy()
    {
        // ȭ�� ��ȯ
        Fade(black);

        // ī�޶� ����
        playerCam.SetActive(false);
        storyCamera.SetActive(true);
        storyCamera.transform.position = new Vector3(2, 18, -10);

        // ȭ�� ����
        GameObject players = NPC(student, 0, 17);
        GameObject minji = NPC(friend1, 4, 17);
        players.GetComponentInChildren<SpriteRenderer>().flipX = true;

        Fade(black, false);

        yield return new WaitForSeconds(1);

        Talk(minji, "��ȭ�ϴ� ���� �츮 �ϳ� �������� ���� �� ����!");
        yield return new WaitForSeconds(chatSpeed);
        Talk(minji, "�����ε� ģ�ϰ� ������~");
        yield return new WaitForSeconds(chatSpeed);
        Talk(players, "��! �б��� �Ұ����� �༭ ����~");
        yield return new WaitForSeconds(chatSpeed);
        Talk(players, "���� �� ��Ź��!");
        yield return new WaitForSeconds(chatSpeed);

        yield return new WaitForSeconds(1);

        // ȭ�� ��ȯ
        Fade(black);

        Destroy(minji);
        Destroy(players);
        storyCamera.SetActive(false);
        playerCam.SetActive(true);

        Fade(black, false);
    }

    IEnumerator Story1Sad()
    {
        // ȭ�� ��ȯ
        Fade(black);

        // ī�޶� ����
        playerCam.SetActive(false);
        storyCamera.SetActive(true);
        storyCamera.transform.position = new Vector3(2, 18, -10);

        // ȭ�� ����
        GameObject minji = NPC(friend1, 4, 17);

        Fade(black, false);

        yield return new WaitForSeconds(1);

        Talk(minji, "��? 00�̰� ���� �� ����?");
        yield return new WaitForSeconds(chatSpeed);


        // ȭ�� ��ȯ
        Fade(black);
        Destroy(minji);
        storyCamera.transform.position = new Vector3(0, 0, -10);
        GameObject players = NPC(student, 0, 0);
        Fade(black, false);

        Talk(players, "���Ⱑ �����?");
        yield return new WaitForSeconds(chatSpeed);
        Talk(players, "��� �Ѵ� �Ĵ� ���̿� ������ �������..");
        yield return new WaitForSeconds(chatSpeed);

        yield return new WaitForSeconds(1);

        // ȭ�� ��ȯ
        Fade(black);

        Destroy(minji);
        Destroy(players);
        storyCamera.SetActive(false);
        playerCam.SetActive(true);

        Fade(black, false);
    }

    IEnumerator Story2()
    {
        // ȭ�� ��ȯ
        Fade(black);

        // ī�޶� ����
        playerCam.SetActive(false);
        storyCamera.SetActive(true);
        storyCamera.transform.position = new Vector3(11, -68, -10);

        // ���丮 ����
        GameObject hanni = NPC(friend1, 5, -70);
        hanni.GetComponentInChildren<SpriteRenderer>().flipX = true;
        Fade(black, false);

        yield return new WaitForSeconds(1);

        GameObject players = NPC(student, 7, -72);
        Fade(players);
        yield return StartCoroutine(Move(players, Vector2.up, 2));

        Talk(hanni, "�� �̷��� �ʰ� �Ծ�!!");
        yield return new WaitForSeconds(chatSpeed);
        Talk(hanni, "���� �����ΰ� ����?!");
        yield return new WaitForSeconds(chatSpeed);
        Talk(players, "�� �ʾ �̾�, ���� ����!!");
        yield return new WaitForSeconds(chatSpeed);
        Talk(players, "���� �ʹ� ��հڴ� ����");
        yield return new WaitForSeconds(chatSpeed);

        yield return new WaitForSeconds(1);

        // ȭ�� ��ȯ
        Fade(black);

        Destroy(hanni);
        Destroy(players);
        storyCamera.SetActive(false);
        playerCam.SetActive(true);

        Fade(black, false);
    }
    IEnumerator Story2_1()
    {
        // ȭ�� ��ȯ
        Fade(black);

        // ī�޶� ����
        playerCam.SetActive(false);
        storyCamera.SetActive(true);
        storyCamera.transform.position = new Vector3(40, -53, -10);

        // ���丮 ����
        GameObject hanni = NPC(friend1, 39, -55);
        hanni.GetComponentInChildren<SpriteRenderer>().flipX = true;
        GameObject players = NPC(student, 41, -55);
        Fade(black, false);

        yield return new WaitForSeconds(1);

        Talk(hanni, "�츮 ���� ���� ������!!");
        yield return new WaitForSeconds(chatSpeed);
        Talk(hanni, "�ٵ� �����߾�~");
        yield return new WaitForSeconds(chatSpeed);
        Talk(players, "�ٵ� ���� ���Ҿ�! ���Ϻ���!!");
        yield return new WaitForSeconds(chatSpeed);

        yield return new WaitForSeconds(1);

        // ȭ�� ��ȯ
        Fade(black);

        Destroy(hanni);
        Destroy(players);
        storyCamera.SetActive(false);
        playerCam.SetActive(true);

        Fade(black, false);
    }
    IEnumerator Story2_2()
    {
        // ȭ�� ��ȯ
        Fade(black);

        // ī�޶� ����
        playerCam.SetActive(false);
        storyCamera.SetActive(true);
        storyCamera.transform.position = new Vector3(40, -53, -10);

        // ���丮 ����
        GameObject hanni = NPC(friend1, 39, -55);
        hanni.GetComponentInChildren<SpriteRenderer>().flipX = true;
        GameObject players = NPC(student, 41, -55);
        Fade(black, false);

        yield return new WaitForSeconds(1);

        Talk(hanni, "���� �ǻ��� �� ã�Ƽ� �ᱹ ���뿡 �� �ö󰡴ٴ�..");
        yield return new WaitForSeconds(chatSpeed);
        Talk(players, "�׷���.. �ʹ� �ƽ���...");
        yield return new WaitForSeconds(chatSpeed);

        yield return new WaitForSeconds(1);

        // ȭ�� ��ȯ
        Fade(black);

        Destroy(hanni);
        Destroy(players);
        storyCamera.SetActive(false);
        playerCam.SetActive(true);

        Fade(black, false);
    }

    IEnumerator Story3()
    {
        // ȭ�� ��ȯ
        Fade(black);

        // ī�޶� ����
        playerCam.SetActive(false);
        storyCamera.SetActive(true);
        storyCamera.transform.position = new Vector3(43, 0, -10);
        GameObject players = NPC(student, 46, -2);

        Fade(black, false);

        yield return new WaitForSeconds(1);

        GameObject diniel = NPC(friend1, 34, -2);
        diniel.GetComponentInChildren<SpriteRenderer>().flipX = true;
        Fade(diniel);
        yield return StartCoroutine(Move(diniel, Vector3.right, 9));

        Talk(diniel, "ū�ϳ���..!!");
        yield return new WaitForSeconds(chatSpeed);
        Talk(diniel, "�츮 ��Ƽ ����� ��Ҵ�..");
        yield return new WaitForSeconds(chatSpeed);
        Talk(diniel, "�б޺� �������..!!");
        yield return new WaitForSeconds(chatSpeed);
        Talk(diniel, "�̵����� �ϸ鼭 ��� ����߸� �� ����..");
        yield return new WaitForSeconds(chatSpeed);
        Talk(diniel, "Ȥ�� �������� ���� ���� ã���ٷ�..?");
        yield return new WaitForSeconds(chatSpeed);

        yield return new WaitForSeconds(1);

        // ȭ�� ��ȯ
        Fade(black);

        Destroy(diniel);
        Destroy(players);
        storyCamera.SetActive(false);
        playerCam.SetActive(true);

        Fade(black, false);
    }

    IEnumerator Story3_1()
    {
        // ȭ�� ��ȯ
        Fade(black);

        // ī�޶� ����
        playerCam.SetActive(false);
        storyCamera.SetActive(true);
        storyCamera.transform.position = new Vector3(7, -18, -10);
        GameObject diniel = NPC(friend1, 8, -20);
        diniel.GetComponentInChildren<SpriteRenderer>().flipX = true;
        GameObject players = NPC(student, 10, -20);

        Fade(black, false);

        yield return new WaitForSeconds(1);

        Talk(diniel, "�� �ƴϿ����� ���� ū�ϳ� ���߾�..");
        yield return new WaitForSeconds(chatSpeed);
        Talk(diniel, "�� ���� �ְ��� ģ����..");
        yield return new WaitForSeconds(chatSpeed);
        Talk(diniel, "���� ����!!");
        yield return new WaitForSeconds(chatSpeed);
        Talk(players, "ģ����� �翬�� ���;���~");

        yield return new WaitForSeconds(1);

        // ȭ�� ��ȯ
        Fade(black);

        Destroy(diniel);
        Destroy(players);
        storyCamera.SetActive(false);
        playerCam.SetActive(true);

        Fade(black, false);
    }
    IEnumerator Story3_2()
    {
        // ȭ�� ��ȯ
        Fade(black);

        // ī�޶� ����
        playerCam.SetActive(false);
        storyCamera.SetActive(true);
        storyCamera.transform.position = new Vector3(43, 0, -10);
        GameObject diniel = NPC(friend1, 42, -2);
        diniel.GetComponentInChildren<SpriteRenderer>().flipX = true;
        GameObject players = NPC(student, 44, -2);

        Fade(black, false);

        yield return new WaitForSeconds(1);


        Talk(players, "�����.. �ƹ��� ã�Ƶ� �Ⱥ���");
        yield return new WaitForSeconds(chatSpeed);
        Talk(diniel, "�׷���.. ��¿ �� ����..");
        yield return new WaitForSeconds(chatSpeed);
        Talk(diniel, "�� ���� ä���� �� �� ����..");
        yield return new WaitForSeconds(chatSpeed);
        Talk(diniel, "�� ���� ���� ��� ������..?");
        yield return new WaitForSeconds(chatSpeed);
        Talk(diniel, "��_��");
        yield return new WaitForSeconds(chatSpeed);

        yield return new WaitForSeconds(1);

        // ȭ�� ��ȯ
        Fade(black);

        Destroy(diniel);
        Destroy(players);
        storyCamera.SetActive(false);
        playerCam.SetActive(true);

        yield return StartCoroutine(Fade(black, false));
    }
    IEnumerator Story4()
    {
        // ȭ�� ��ȯ
        yield return StartCoroutine(Fade(black));

        // ī�޶� ����
        playerCam.SetActive(false);
        storyCamera.SetActive(true);
        storyCamera.transform.position = new Vector3(40, -53, -10);
        GameObject players = NPC(student, 38, -55);

        yield return StartCoroutine(Fade(black, false));

        yield return new WaitForSeconds(1);

        Talk(players, "");

        yield return new WaitForSeconds(1);

        // ȭ�� ��ȯ
        Fade(black);

        storyCamera.SetActive(false);
        playerCam.SetActive(true);

        yield return StartCoroutine(Fade(black, false));
    }
    IEnumerator Story()
    {

        // ȭ�� ��ȯ
        yield return StartCoroutine(Fade(black));

        // ī�޶� ����
        playerCam.SetActive(false);
        storyCamera.SetActive(true);
        storyCamera.transform.position = new Vector3(0, 0, -10);
        yield return StartCoroutine(Fade(black, false));

        yield return new WaitForSeconds(1);


        yield return new WaitForSeconds(1);

        // ȭ�� ��ȯ
        Fade(black);

        storyCamera.SetActive(false);
        playerCam.SetActive(true);

        yield return StartCoroutine(Fade(black, false)); 
    }

    // ������ ���¿��� ��Ÿ���� �ϴ� ȿ�� �޼���

    IEnumerator Fade(GameObject obj, bool IsShowing = true)
    {
        {
            //// time�� ���������� �ö󰡴� ��
            //float time = 0f;

            //// �̹��� �޾ƿ���
            //Image sprite = obj.GetComponentInChildren<Image>();

            //// �÷� �޾ƿ���
            //Color color = sprite.color;

            //if (IsShowing)
            //{
            //    color.a = 0f;
            //    sprite.color = color;

            //    // 1�� ������ ��µǴ� ���� ����
            //    while (color.a < 1f)
            //    {
            //        time += 0.02f;
            //        color.a = Mathf.Lerp(0, 1, time);
            //        sprite.color = color;
            //        yield return null;
            //    }
            //}
            //else if (!IsShowing)
            //{
            //    // �������� ������ ���ҵǴ� ���� �� ����
            //    while (color.a > 0f)
            //    {
            //        time += Time.deltaTime;
            //        color.a = Mathf.Lerp(1, 0, time);
            //        sprite.color = color;
            //        yield return null;
            //    }
            //}
        }

        Animation anim = obj.GetComponentInChildren<Animation>();

        anim.enabled = false;
        anim.enabled = true;

        if (IsShowing)
        {
            anim.Play("FadeAnim");
            yield return new WaitForSeconds(anim.GetClip("FadeAnim").length);
        }
        else
        {
            anim.Play("FadeOutAnim");
            yield return new WaitForSeconds(anim.GetClip("FadeOutAnim").length);
        }

    }

    public void Talk(GameObject talker, string chat, float tDestroy = 2)
    {
        GameObject chatBox = Instantiate(ChatPrefab, talker.transform);
        chatBox.transform.SetParent(talker.transform);
        TextMeshProUGUI chatUI = chatBox.gameObject.GetComponentInChildren<TextMeshProUGUI>();
        chatUI.text = chat;
        Destroy(chatBox, tDestroy);
    }
    IEnumerator Typing(GameObject talker, string chat, float tDestroy = 1)
    {
        GameObject chatBox = Instantiate(ChatPrefab, talker.transform);
        chatBox.transform.SetParent(talker.transform);
        TextMeshProUGUI chatUI = chatBox.gameObject.GetComponentInChildren<TextMeshProUGUI>();

        for(int i = 0; i < chat.Length; i++)
        {
            chatUI.text += chat[i];
            yield return new WaitForSeconds(0.05f);
        }

        yield return new WaitForSeconds(tDestroy);
        Destroy(chatBox);
        yield return null;
    }

    IEnumerator Move(GameObject npc, Vector3 head, int length = 1, float speed = 0.05f)
    {
        for (int i = 0; i < length; i++)
        {
            Vector3 curPos = npc.transform.localPosition;
            Vector3 headPos = curPos + head;
            // ĳ���� ��ǥ �̵�
            while (true)
            {
                npc.transform.Translate(head * speed);
                if (Mathf.Abs(npc.transform.localPosition.x - headPos.x) <= 0.1f &&
                    Mathf.Abs(npc.transform.localPosition.y - headPos.y) <= 0.1f)
                {
                    npc.transform.localPosition = headPos;
                    break;
                }
                yield return null;
            }
        }
    }



    // npc ĳ���� ����
    public GameObject NPC(Sprite _sprite, float _x, float _y)
    {
        GameObject npc = Instantiate(npcPrefab);
        npc.GetComponentInChildren<SpriteRenderer>().sprite = _sprite;
        npc.transform.position = new Vector2(_x, _y);
        npc.transform.SetParent(storyCamera.transform);

        return npc;
    }

    IEnumerator Tutorial()
    {
        // ī�޶� ����
        playerCam.SetActive(false);
        storyCamera.SetActive(true);
        storyCamera.transform.position = new Vector3(0, 0, -10);

        yield return new WaitForSeconds(1);


        yield return new WaitForSeconds(1);

        storyCamera.SetActive(false);
        playerCam.SetActive(true);
    }

}
